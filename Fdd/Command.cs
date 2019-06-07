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
			else {
				return Cmd.Help;
			}
		}

		public static string getHelp() {
			var help = new StringBuilder();
			help.AppendLine("----------------- HELP -----------------");
			help.AppendLine("x,q,c:\t Exit this tool. Same as: exit, quit, close");
			help.AppendLine("raw:\t Show name of backup files.");
			help.AppendLine("parsed:\t Show parsed information of back files.");

			help.AppendLine();
			help.AppendLine("Notes: Enter ! in Filter field to launch a command. Example: !exit");

			return help.ToString();
		}
	}

	public enum Cmd
	{
		Help,
		Exit,
		FormatRaw,
		FormatParsed
	}
}
