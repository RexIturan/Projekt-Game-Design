using System.Collections.Generic;
using UnityEditor.GraphToolsFoundation.Overdrive;

namespace _Structure._GraphView.LevelGraph.Core {
	public class LevelGraph_ToolbarProvider : IToolbarProvider {
		private static readonly string[] buttonNames = new[] {
			MainToolbar.NewGraphButton,
			MainToolbar.SaveAllButton,
			MainToolbar.BuildAllButton,
			MainToolbar.ShowMiniMapButton,
			MainToolbar.ShowBlackboardButton,
			MainToolbar.EnableTracingButton,
			MainToolbar.OptionsButton
		};

		private readonly Dictionary<string, bool> showButtonDict;

		public LevelGraph_ToolbarProvider(string[] enabledButtons) {
			showButtonDict = new Dictionary<string, bool>();

			foreach ( var name in buttonNames ) {
				showButtonDict.Add(name, false);
			}
			
			foreach ( var str in enabledButtons ) {
				showButtonDict[str] = true;
			}
		}
		
		/// <inheritdoc />
		public bool ShowButton(string buttonName) {
			return showButtonDict.ContainsKey(buttonName) && showButtonDict[buttonName];
		}
	}
}