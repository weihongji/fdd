using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdd
{
	public class Util
	{
		public static string GetConfigString(string key, string defaultValue = "") {
			var config = System.Configuration.ConfigurationManager.AppSettings[key];

			if (!string.IsNullOrEmpty(config)) {
				return config;
			}
			return defaultValue;
		}

		public static int GetConfigInt(string key, int defaultValue = 0) {
			var config = System.Configuration.ConfigurationManager.AppSettings[key];

			int i;
			if (!string.IsNullOrEmpty(config) && int.TryParse(config, out i)) {
				if (i > 0) {
					return i;
				}
			}
			return defaultValue;
		}

		public static bool GetConfigBool(string key, bool defaultValue = false) {
			var config = System.Configuration.ConfigurationManager.AppSettings[key];

			bool b;
			if (!string.IsNullOrEmpty(config) && bool.TryParse(config, out b)) {
				return b;
			}
			return defaultValue;
		}
	}
}
