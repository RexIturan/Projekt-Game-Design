﻿using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.UI.Helper {
	static class NodeUI_Helper {

		static string _ussFilter = "stateMachineStylesBase t:StyleSheet";
		
		static string StylesheetPath {
			get {
				var ussGuid = AssetDatabase.FindAssets(_ussFilter);
				var ussPath = AssetDatabase.GUIDToAssetPath(ussGuid.Length > 0 ? ussGuid[0] : "");
				return Path.GetDirectoryName(ussPath);
			}
		}

		internal static void AddStylesheet(this VisualElement ve, string stylesheetName)
		{
			StyleSheet stylesheet = null;

			string path = StylesheetPath;
			
			// ReSharper disable once ConditionIsAlwaysTrueOrFalse
			if (stylesheet == null)
			{
				stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(path + "/" + stylesheetName);
			}

			if (stylesheet != null)
			{
				ve.styleSheets.Add(stylesheet);
			}
			else
			{
				Debug.Log("Failed to load stylesheet " + path + stylesheetName);
			}
		}
	}
}