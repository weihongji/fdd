using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdd
{
	public class Filter
	{
		private string name;
		private string[] nameArray;

		public string Name {
			get { return name; }
			set {
				name = value == null ? "" : value.Trim();
				nameArray = name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			}
		}

		public Filter(string name) {
			this.Name = name;
		}

		public bool matchName(string text) {
			foreach (var s in nameArray) {
				if (text.IndexOf(s, StringComparison.OrdinalIgnoreCase) >= 0) {
					return true;
				}
			}
			return false;
		}
	}
}
