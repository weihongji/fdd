using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdd
{
	public class Backup : IComparable<Backup>
	{
		private string fullName;
		private string db;
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

		public Backup(string fullName)
			: this(fullName, "") {
		}

		public Backup(string name, string location) {
			this.fullName = name == null ? "" : name.Trim(); // Servlet.ajax_db_20190313015526.BAK
			this.date = DateTime.MinValue;
			this.location = location == null ? "" : location;

			if (String.IsNullOrEmpty(this.fullName)) {
				return;
			}

			name = name.Trim().ToLower();

			string s = "servlet.";
			if (name.StartsWith(s) && !name.Equals(s)) {
				name = name.Substring(s.Length);
			}

			s = ".bak";
			if (name.EndsWith(s) && !name.Equals(s)) {
				name = name.Substring(0, name.Length - s.Length);
			}

			s = "_db_";
			int index = name.IndexOf(s);
			if (index > 0) {
				string dt = name.Substring(index + s.Length);
				if (dt.Length >= 8 && System.Text.RegularExpressions.Regex.IsMatch(dt, @"^20\d{6}")) {
					dt = dt.Substring(0, 4) + "-" + dt.Substring(4, 2) + "-" + dt.Substring(6, 2);
					if (DateTime.TryParse(dt, out this.date)) {
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

		public int CompareTo(Backup o) {
			return this.SortBy.CompareTo(o.SortBy);
		}
	}
}
