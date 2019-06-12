using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fdd
{
	public partial class FinderForm : Form
	{
		private string lastFilter = "";
		private string lastFailedFilter = "";
		private List<Backup> backups;
		private List<Backup> lastMatches;
		private bool raw_format = false;
		private bool show_size = true;
		private bool show_last_search_time = false;
		private bool show_search_detail = false;

		// Hard to understand
		private bool processing_command = false;

		public FinderForm() {
			InitializeComponent();
		}

		private void FinderForm_Load(object sender, EventArgs e) {
			// Load configuration
			this.raw_format = Util.GetConfigString("item_format").Equals("raw");
			this.show_size = Util.GetConfigBool("show_size", true);
			this.show_last_search_time = Util.GetConfigBool("show_last_search_time", false);
			this.show_search_detail = Util.GetConfigBool("show_search_detail", false);

			// Load backup records
			loadBackup();

			// Init controls
			this.txtFilter.Text = "";
			this.txtResult.Text = "";
			this.statusBarLabel1.Text = "Ready";
			this.statusBarLabel2.Text = "N/A";
			showTimestamp();
		}

		private void txtFilter_KeyDown(object sender, KeyEventArgs e) {
			this.processing_command = false;

			if (e.KeyCode == Keys.Escape) {
				this.txtFilter.Text = "";
				e.Handled = e.SuppressKeyPress = true; // Disable the beep when ESC key is pressed within the combox
			}
			else if (e.KeyCode == Keys.Enter) {
				e.Handled = e.SuppressKeyPress = true;
				string s = this.txtFilter.Text;
				// A command is ordered. Process it.
				if (isCommand(s)) {
					Cmd cmd = Command.getCommand(s);
					switch (cmd) {
						case Cmd.Help:
							this.txtResult.Text = Command.getHelp();
							this.statusBarLabel1.Text = "Help doc";
							break;
						case Cmd.Exit:
							Application.Exit();
							break;
						case Cmd.FormatRaw:
							this.raw_format = true;
							redoSearch();
							break;
						case Cmd.FormatParsed:
							this.raw_format = false;
							redoSearch();
							break;
						case Cmd.FileSize:
							this.show_size = !this.show_size;
							redoSearch();
							break;
						case Cmd.SearchTime:
							this.show_last_search_time = !this.show_last_search_time;
							showTimestamp();
							resetFilter();
							break;
						default:
							break;
					}
					this.processing_command = true;
				}
			}
		}

		private bool resetFilter() {
			if (!this.lastFilter.Equals(this.lastFailedFilter)) {
				this.txtFilter.Text = this.lastFilter;
				this.txtFilter.SelectAll();
				return true;
			}
			else {
				this.txtFilter.Text = "";
				return false;
			}
		}

		private void showTimestamp() {
			this.statusBarLabel2.Visible = this.show_last_search_time;
		}

		private void txtFilter_KeyUp(object sender, KeyEventArgs e) {
			string filter = this.txtFilter.Text.Trim();

			if (isCommand(filter) || this.processing_command) { // Command is entered and has been handled in KeyDown event.
				this.processing_command = false;
				return;
			}

			if (e != null && e.KeyCode == Keys.Enter) { // Forced search will be done if Enter key pressed.
				searchOnUI();
			}
			else {
				if (String.IsNullOrEmpty(filter)) {
					this.statusBarLabel1.Text = "Ready";
					return;
				}
				else if (filter.Equals(this.lastFilter)) {
					return; // Don't launch a search if the filter is same as that in last search. User just typing control keys like Home/End/Arrow.
				}
				else if (isFailedFilter(filter)) {
					if (filter.Length - this.lastFailedFilter.Length > 10) {
						this.txtResult.Text = "Press '?' and Enter key to show help information.";
					}
					return;
				}
				else {
					searchOnUI();
				}
			}
		}

		private void btnRefresh_Click(object sender, EventArgs e) {
			loadBackup();
			redoSearch();

			// Information on status bar
			string status = this.statusBarLabel1.Text;
			string refreshed = "Refreshed.";
			if (!status.StartsWith(refreshed)) {
				if (status.Length > 0) {
					refreshed += " ";
				}
				this.statusBarLabel1.Text = refreshed + status;
			}
		}

		private bool isFailedFilter(string filter) {
			if (this.lastFailedFilter.Length > 0) {
				return filter.IndexOf(this.lastFailedFilter) >= 0;
			}
			else {
				return false;
			}

		}

		private bool isSubsetFilter(Filter filter) {
			if (this.lastFilter.Length > 0) {
				return filter.Name.IndexOf(this.lastFilter) >= 0;
			}
			else {
				return false;
			}

		}

		private bool isCommand(string text) {
			return text.StartsWith(Command.cmd_prefix) || text.Equals("?") || text.Equals("help");
		}

		private void redoSearch() {
			if (resetFilter()) {
				searchOnUI();
			}
		}

		private int searchOnUI() {
			string filter = this.txtFilter.Text.Trim();
			int candidateCount;
			List<Backup> found = searchBackup(new Filter(filter), out candidateCount);
			List<string> items = new List<string>(found.Count);
			string s = "";
			foreach (var item in found) {
				s = this.raw_format ? item.FullName : item.ToString();
				if (this.show_size) {
					if (item.Size >= 0) {
						s += String.Format(" - {0:#,0} KB", item.Size / 1024);
					}
					else {
						s += " - size:N/A";
					}
				}
				items.Add(s);
			}

			this.lastFilter = filter;
			if (items.Count == 0) {
				if (filter.Length > 0) {
					this.lastFailedFilter = filter;
				}
			}
			else {
				this.lastFailedFilter = "";
			}
			this.txtResult.Text = String.Join("\r\n", items.ToArray());
			string status = String.Format("{0} {1} found.", items.Count, items.Count < 2 ? "record" : "records");
			if (this.show_search_detail) {
				status = String.Format("{0} {1} found in {2}.", items.Count, items.Count < 2 ? "record" : "records", candidateCount);
			}
			this.statusBarLabel1.Text = status;
			this.statusBarLabel2.Text = "Searched at: " + DateTime.Now.ToString("HH:mm:ss");

			return items.Count;
		}

		private List<Backup> searchBackup(Filter filter) {
			int candidateCount;
			return searchBackup(filter, out candidateCount);
		}

		private List<Backup> searchBackup(Filter filter, out int candidateCount) {
			if (this.backups == null) {
				loadBackup();
			}
			candidateCount = this.backups.Count;

			if (filter.Name.Length == 0 || filter.Name.Equals("*")) {
				return this.backups;
			}

			List<Backup> searchFrom;
			if (isSubsetFilter(filter) && this.lastMatches != null) {
				searchFrom = this.lastMatches;
			}
			else {
				searchFrom = this.backups;
				this.lastMatches = null;
			}
			candidateCount = searchFrom.Count;

			List<Backup> found = new List<Backup>();
			foreach (var db in searchFrom) {
				if (filter.matchName(db.FullName)) {
					found.Add(db);
				}
			}

			this.lastMatches = found;
			return found;
		}

		private void loadBackup() {
			this.backups = new List<Backup>();

			string[] files = getFileNames();
			foreach (var f in files) {
				string location = getLocation(f);
				List<string> items = readFile(f);
				foreach (var item in items) {
					this.backups.Add(new Backup(item, location));
				}
			}
			this.backups.Sort();
			this.lastFailedFilter = ""; // Clear the failed flag every time after backup is loaded.
		}

		private string getLocation(string filePath) {
			filePath = filePath == null ? "" : filePath.Trim();

			// Remove the suffix
			int index = filePath.LastIndexOf('.');
			if (index > 0) {
				filePath = filePath.Substring(0, index);
			}

			string defaultPattern = "*";
			string setting = Util.GetConfigString("location_pattern", defaultPattern);
			if (setting.Equals(defaultPattern)) {
				return filePath;
			}
			else {
				string loc = "";
				Match match = Regex.Match(filePath, setting);
				if (match != null) {
					GroupCollection groups = match.Groups;
					if (groups.Count == 1) {
						loc = groups[0].Value;
					}
					else if (groups.Count > 1) {
						for (int i = 1; i < groups.Count; i++) {
							loc += groups[i].Value;
						}
					}
				}
				return loc.Length > 0 ? loc : filePath;
			}
		}

		private string[] getFileNames() {
			string setting = Util.GetConfigString("fdd_files", "fdd.txt");
			string[] files = setting.Split(',');
			for (int i = 0; i < files.Length; i++) {
				files[i] = files[i].Trim();
			}
			return files;
		}

		private List<string> readFile(string filePath) {
			List<string> items = new List<string>();
			/*
			items.Add("Servlet.ajax_db_20190313015526.BAK");
			items.Add("Servlet.denver_db_20190313200753.BAK");
			items.Add("Servlet.ebparks_db_20190221182257.BAK");
			items.Add("Servlet.kansascityymca_db_20190304173132.BAK");
			items.Add("Servlet.santamonicarecreation_db_20190221222757.BAK");
			items.Add("Servlet.wsd3cc_db_20190307181238.BAK");
			items.Add("Servlet.ymcaofthesuncoast_db_20190307235934.BAK");
			*/

			if (File.Exists(filePath)) {
				StreamReader reader = null;
				string line = null;
				try {
					reader = new StreamReader(filePath);
					while ((line = reader.ReadLine()) != null) {
						line = line.Trim();
						if (line.Length > 0) {
							items.Add(line);
						}
					}
				}
				finally {
					if (reader != null) {
						reader.Close();
					}
				}
			}

			return items;
		}
	}
}
