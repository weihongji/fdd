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
	}
}
