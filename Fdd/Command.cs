using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fdd
{
	public class Command
	{
		public static Cmd getCommand(string cmd) {
			cmd = cmd == null ? "" : cmd.Trim().ToLower();
			if (cmd.StartsWith("!")) {
				cmd = cmd.Substring(1);
			}

			if (cmd.Equals("x") || cmd.Equals("q") || cmd.Equals("c") || cmd.Equals("exit") || cmd.Equals("quit") || cmd.Equals("close")) {
				return Cmd.Exit;
			}
			else if(cmd.Equals("raw")) {
				return Cmd.FormatRaw;
			}
			else if (cmd.Equals("parse")) {
				return Cmd.FormatParsed;
			}
			else if (cmd.Equals("size")) {
				return Cmd.FileSize;
			}
			else if (cmd.Equals("time")) {
				return Cmd.SearchTime;
			}
			else {
				return Cmd.Help;
			}
		}

		public static string getHelp() {
			var help = new StringBuilder();
			help.AppendLine("----------------- HELP -----------------");
			help.AppendLine("Type a part of the backup file name you want to search.");
			help.AppendLine("Enter * to show all files.");
			help.AppendLine();
			help.AppendLine("Start with an exclamation (!) to launch a command.");
			help.AppendLine("Supported commands as below:");
			help.AppendLine("-----------------  -----------------");
			help.AppendLine("x,q,c:\t Exit this tool. Same as: exit, quit, close");
			help.AppendLine("raw:\t Show name of backup files.");
			help.AppendLine("parse:\t Show parsed information of back files.");
			help.AppendLine("size:\t Show/hide size of back files.");
			help.AppendLine("time:\t Show/hide timestamp of last search on status bar.");
			help.AppendLine("-----------------  -----------------");
			help.AppendLine("Example: !exit");

			return help.ToString();
		}
	}

	public enum Cmd
	{
		Help,
		Exit,
		FormatRaw,
		FormatParsed,
		FileSize,
		SearchTime
	}
}
