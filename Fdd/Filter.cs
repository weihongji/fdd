using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdd
{
	public class Filter
	{
		private string text = "";
		private string name = "";
		private string location = "";

		public string Text {
			get { return text; }
		}

		public Filter(string text) {
			this.text = text == null ? "" : text.Trim();
			int index = this.text.IndexOf(':');
			if (index > 0) {
				this.location = this.text.Substring(0, index).Trim();
				int start = index + 1;
				this.name = start < this.text.Length ? this.text.Substring(start) : "";
			}
			else {
				this.name = this.text;
			}
		}

		public bool match(Backup db) {
			if (db == null) {
				return false;
			}
			if (this.location.Length > 0) {
				if (!db.Location.Equals(this.location, StringComparison.OrdinalIgnoreCase)) {
					return false;
				};
			}
			if (this.name.Length > 0 && db.FullName.IndexOf(this.name, StringComparison.OrdinalIgnoreCase) < 0) {
				return false;
			}
			return true;
		}

		public bool isSubsetOf(Filter f) {
			if (f == null) {
				return false;
			}
			if (f.location.Length > 0) {
				if (!this.location.Equals(f.location)) {
					return false;
				}
			}
			if (f.name.Length > 0) {
				if (this.name.IndexOf(f.name) < 0) {
					return false;
				}
			}
			return true;
		}

		public override bool Equals(object o) {
			if (o == null) {
				return false;
			}
			if (o is Filter) {
				var f = (Filter)o;
				return this.location.Equals(f.location) && this.name.Equals(f.name);
			}
			return false;
		}

		public bool Equals(string text) {
			text = text == null ? "" : text.Trim();
			return this.text.Equals(text);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}
	}
}
