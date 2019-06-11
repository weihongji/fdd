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
			else if (cmd.Equals("parsed")) {
				return Cmd.FormatParsed;
			}
			else if (cmd.Equals("showsize")) {
				return Cmd.ShowSize;
			}
			else if (cmd.Equals("hidesize")) {
				return Cmd.HideSize;
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
			help.AppendLine("x,q,c:\t\t Exit this tool. Same as: exit, quit, close");
			help.AppendLine("raw:\t\t Show name of backup files.");
			help.AppendLine("parsed:\t\t Show parsed information of back files.");
			help.AppendLine("showsize:\t Show size of back files.");
			help.AppendLine("hidesize:\t Hide size of back files.");
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
		ShowSize,
		HideSize,
	}
}
