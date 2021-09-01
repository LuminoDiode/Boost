using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Boost
{
	class AdvancedConsoleManager
	{
		private const string DefaultConsoleTraceListener = "DefaultConsoleTraceListener";

		public enum MESSAGE_TYPE
		{
			TRACE,
			DEBUG,
			WARNING,
			ERROR
		}

		private bool[] MessagesToPrint = new bool[4] { true, true, true, true };
		public bool PrintTrace
		{
			get { return MessagesToPrint[(int)MESSAGE_TYPE.TRACE]; }
			set { MessagesToPrint[(int)MESSAGE_TYPE.TRACE] = value; }
		}
		public bool PrintDebug
		{
			get { return MessagesToPrint[(int)MESSAGE_TYPE.DEBUG]; }
			set { MessagesToPrint[(int)MESSAGE_TYPE.DEBUG] = value; }
		}
		public bool PrintWarning
		{
			get { return MessagesToPrint[(int)MESSAGE_TYPE.WARNING]; }
			set { MessagesToPrint[(int)MESSAGE_TYPE.WARNING] = value; }
		}
		public bool PrintError
		{
			get { return MessagesToPrint[(int)MESSAGE_TYPE.ERROR]; }
			set { MessagesToPrint[(int)MESSAGE_TYPE.ERROR] = value; }
		}
		public bool PrintNothing = false;

		private bool _ConsoleListeningTrace;
		public bool ConsoleListeningTrace
		{
			get
			{
				return _ConsoleListeningTrace;
			}
			set
			{
				if (_ConsoleListeningTrace == value) return;
				if (value == false) { Trace.Listeners.Remove(DefaultConsoleTraceListener); _ConsoleListeningTrace = value;  return; }
				if (value == true) { Trace.Listeners.Add(new ConsoleTraceListener { Name = DefaultConsoleTraceListener }); _ConsoleListeningTrace = value; return; }
			}
		}


		public void Write(string Message)
		{
			if (PrintNothing) return;
			Console.Write(Message);
			return;
		}

		public void Write(string Message, ConsoleColor ForegroundColor)
		{
			if (PrintNothing) return;
			var CurrentColor = Console.ForegroundColor;
			Console.ForegroundColor = ForegroundColor;
			Console.Write(Message);
			Console.ForegroundColor = CurrentColor;
			return;
		}

		public void Write(string Message, MESSAGE_TYPE MessageType)
		{
			if (!MessagesToPrint[(int)MessageType]) return;
			Console.Write(Message);
			return;
		}

		public void Write(string Message, ConsoleColor ForegroundColor, MESSAGE_TYPE MessageType)
		{
			if (!MessagesToPrint[(int)MessageType]) return;
			var CurrentColor = Console.ForegroundColor;
			Console.ForegroundColor = ForegroundColor;
			Console.Write(Message);
			Console.ForegroundColor = CurrentColor;
			return;
		}

		public string MagentaDebug => PrintMagentaDebugAndReturnEmpty();
		private string PrintMagentaDebugAndReturnEmpty()
		{
			this.Write("Debug", ConsoleColor.Magenta);
			return string.Empty;
		}
		public string GreenTrace => PrintGreenTraceAndReturnEmpty();
		private string PrintGreenTraceAndReturnEmpty()
		{
			this.Write("Trace", ConsoleColor.Green);
			return string.Empty;
		}

		public string YellowWarning=> PrintYellowWarningAndReturnEmpty();
		private string PrintYellowWarningAndReturnEmpty()
		{
			this.Write("Warning", ConsoleColor.Yellow);
			return string.Empty;
		}
		public string RedError=> PrintRedErrorAndReturnEmpty();
		private string PrintRedErrorAndReturnEmpty()
		{
			this.Write("Error", ConsoleColor.Red);
			return string.Empty;
		}

		
	}
}
