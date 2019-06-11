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
		private List<Backup> backups;
		private bool show_reloaded = false;
		private bool raw_format = false;
		private bool show_size = true;

		public FinderForm() {
			InitializeComponent();
		}

		private void FinderForm_Load(object sender, EventArgs e) {
			// Init controls
			this.txtFilter.Text = "";
			this.txtResult.Text = "";

			// Load configuration
			this.raw_format = Util.GetConfigString("item_format").Equals("raw");
			this.show_size = Util.GetConfigBool("show_size", true);

			// Load backup records
			searchOnUI();
		}

		private void txtFilter_KeyDown(object sender, KeyEventArgs e) {
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
						case Cmd.ShowSize:
							this.show_size = true;
							redoSearch();
							break;
						case Cmd.HideSize:
							this.show_size = false;
							redoSearch();
							break;
						default:
							break;
					}
				}
			}
		}

		private void txtFilter_KeyUp(object sender, KeyEventArgs e) {
			string filter = this.txtFilter.Text;
			if (String.IsNullOrWhiteSpace(filter)) {
				return;
			}

			// Command is entered. Don't do search.
			if (isCommand(filter)) {
				if (filter.Equals("!")) {
					this.statusBarLabel1.Text = "Entering a command...";
				}
				return;
			}

			if (this.lastFilter.Equals(filter)) {
				return; // Don't launch a search if the filter is same as that in last search.
			}
			this.lastFilter = filter;
			searchOnUI();
		}

		private void btnRefresh_Click(object sender, EventArgs e) {
			this.backups = null;
			show_reloaded = true;
			searchOnUI();
		}

		private bool isCommand(string text) {
			return text.StartsWith("!") || text.Equals("?") || text.Equals("help");
		}

		private void redoSearch() {
			this.txtFilter.Text = this.lastFilter;
			searchOnUI();
		}

		private void searchOnUI() {
			if (String.IsNullOrWhiteSpace(this.txtFilter.Text)) {
				return;
			}

			List<Backup> found = searchBackup(new Filter(this.txtFilter.Text));
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
			this.txtResult.Text = String.Join("\r\n", items.ToArray());
			this.statusBarLabel1.Text = String.Format("{0} {1} found.", items.Count, items.Count < 2 ? "record" : "records");
			if (show_reloaded) {
				this.statusBarLabel1.Text = "Refreshed. " + this.statusBarLabel1.Text;
				show_reloaded = false;
			}
		}

		private List<Backup> searchBackup(Filter filter) {
			if (this.backups == null) {
				loadBackup();
			}

			if (filter.Name.Equals("*")) {
				return this.backups;
			}

			List<Backup> found = new List<Backup>();
			foreach (var db in this.backups) {
				if (filter.matchName(db.FullName)) {
					found.Add(db);
				}
			}
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
