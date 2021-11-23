using System.Text;
using UnityEngine;

namespace Util.Logger {
	public class CustomLogger {
		
		private const string CHECK_MARK = "\u2714";
		private const string UNCHECK_MARK = "\u2718";
		private const string THICK_ARROW = "\u279C";
		private const string SHARP_ARROW = "\u27A4";

		private readonly StringBuilder _logBuilder;

		private string origin;
		private string msg;
		
		public CustomLogger() {
			_logBuilder = new StringBuilder();
		}

		public void NewLog(string from) {
			_logBuilder.Clear();
			origin = from;
		}

		public void Log(string message) {
			this.msg = message; 
		}
		
		public void PrintDebugLog() {
			_logBuilder.Clear();
			_logBuilder.Append(origin);
			_logBuilder.AppendLine();
			_logBuilder.Append($"{THICK_ARROW} {msg}");

			Debug.Log(_logBuilder.ToString());
		}
	}
}