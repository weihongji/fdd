using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Fdd
{
	public class Backup : IComparable<Backup>
	{
		private string fullName;
		private string db;
		private long size; // in byte. -1 = N/A
		private DateTime date;
		private string location;

		public string FullName {
			get { return fullName; }
			set { fullName = value; }
		}

		public string DB {
			get { return db; }
			set { db = value; }
		}

		public long Size {
			get { return size; }
			set { size = value; }
		}

		public DateTime Date {
			get { return date; }
			set { date = value; }
		}

		public string Location {
			get { return location; }
			set { location = value; }
		}

		public string SortBy {
			get {
				var s = fullName == null ? "" : fullName.Trim().ToLower();
				if (s.StartsWith("servlet.")) {
					return s.Substring(8);
				}
				else {
					return s;
				}
			}
		}

		public Backup(string entry)
			: this(entry, "") {
		}

		public Backup(string entry, string location) {
			entry = entry == null ? "" : entry.Trim(); // E:\Backup\Servlet.ajax_db_20190313015526.BAK    2192498688

			// Remove the directory path.
			int index = entry.LastIndexOf('\\');
			if (index > 0) {
				entry = entry.Substring(index + 1).Trim();
			}

			// File name & size
			string delimiter = "    "; // Delimiter between file name and size. It's a string of 4 spaces.
			index = entry.IndexOf(delimiter);
			if (index > 0) {
				fullName = entry.Substring(0, index);
				entry = entry.Substring(index + 4).Trim(); // the size part
				if (!long.TryParse(entry, out size)) {
					size = -1;
				}
			}
			else {
				fullName = entry;
				size = -1;
			}

			date = DateTime.MinValue;
			this.location = location == null ? "" : location;

			if (String.IsNullOrEmpty(fullName)) {
				return;
			}

			// db & date
			string name = fullName.ToLower();
			string s = "servlet.";
			if (name.StartsWith(s) && !name.Equals(s)) {
				name = name.Substring(s.Length);
			}

			s = ".bak";
			if (name.EndsWith(s) && !name.Equals(s)) {
				name = name.Substring(0, name.Length - s.Length);
			}

			s = "_db_";
			index = name.IndexOf(s);
			if (index > 0) {
				string dt = name.Substring(index + s.Length);
				if (dt.Length >= 8 && Regex.IsMatch(dt, @"^20\d{6}")) {
					dt = dt.Substring(0, 4) + "-" + dt.Substring(4, 2) + "-" + dt.Substring(6, 2);
					if (DateTime.TryParse(dt, out date)) {
						db = name.Substring(0, index);
					}
				}
			}
			if (String.IsNullOrEmpty(db)) {
				db = name;
			}

		}

		public override string ToString() {
			return String.Format("{0} of {1} ({2})", db, date > DateTime.MinValue ? date.ToString("yyyy-MM-dd") : "N/A", location.Length > 0 ? location : "N/A");
		}

		public string ToString(string format) {
			if (String.IsNullOrWhiteSpace(format)) {
				return ToString();
			}

			string s = format.Replace("(db)", db)
				.Replace("(date)", date > DateTime.MinValue ? date.ToString("yyyy-MM-dd") : "N/A")
				.Replace("(location)", location.Length > 0 ? location : "N/A")
				.Replace("(size)", size >= 0 ? String.Format("{0:#,0} KB", size / 1024) : "size:N/A");
			return s;
		}

		public int CompareTo(Backup o) {
			return this.SortBy.CompareTo(o.SortBy);
		}
	}
}
