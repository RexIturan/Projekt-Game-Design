using System;
using System.IO;
using UnityEngine;

namespace SaveSystem {
	public static class FileManager {

		private static string numberPattern = " ({0})";
		private static string fileExtension = ".json";
		private static string levelDirectory = "level";
		
//////////////////////////////////////// Local Functions ///////////////////////////////////////////

		private static string GetSaveDirectory() {
			// var persistantPath = Application.persistentDataPath;
			// var streamingAssets = Application.streamingAssetsPath;
			// var dataAssetPath = Application.dataPath;
			
			return Path.Combine(Application.streamingAssetsPath, levelDirectory);
		}

		private static string GetFullFilename(string filename) {
			return String.Concat(filename, fileExtension); 
		}
		
		/// <summary>
		/// Get Full Path inseide the streaming Assets Folder 
		/// 
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		private static string GetFullPath(string fileName) {
			var fullFilename = GetFullFilename(fileName);			
			var directory = GetSaveDirectory();
			
			return  Path.Combine(directory, fullFilename);
		}

		//https://stackoverflow.com/questions/1078003/c-how-would-you-make-a-unique-filename-by-adding-a-number
		public static string NextAvailableFilename(string path)
		{
			// Short-cut if already available
			if (!File.Exists(path))
				return path;

			// If path has extension then insert the number pattern just before the extension and return next filename
			if (Path.HasExtension(path))
				return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path), StringComparison.Ordinal), numberPattern));

			// Otherwise just append the pattern to the path and return next filename
			return GetNextFilename(path + numberPattern);
		}
		
		private static string GetNextFilename(string pattern)
		{
			string tmp = string.Format(pattern, 1);
			if (tmp == pattern)
				throw new ArgumentException("The pattern must include an index place-holder", "pattern");

			if (!File.Exists(tmp))
				return tmp; // short-circuit if no matches

			int min = 1, max = 2; // min is inclusive, max is exclusive/untested

			while (File.Exists(string.Format(pattern, max)))
			{
				min = max;
				max *= 2;
			}

			while (max != min + 1)
			{
				int pivot = (max + min) / 2;
				if (File.Exists(string.Format(pattern, pivot)))
					min = pivot;
				else
					max = pivot;
			}

			return string.Format(pattern, max);
		}
		
//////////////////////////////////////// Public Functions //////////////////////////////////////////

		public static string[] GetFileNames() {
			var directory = GetSaveDirectory();
			var filesPaths = Directory.GetFiles(directory, $"*{fileExtension}");

			string[] fileNames = new string[filesPaths.Length];

			for ( int i = 0; i < filesPaths.Length; i++ ) {
				fileNames[i] = Path.GetFileNameWithoutExtension(filesPaths[i]);
			}
			
			return fileNames;
		}

		public static bool FileExists(string filename) {
			var fullFilename = GetFullFilename(filename);			
			var directory = GetSaveDirectory();
			var fullpath = Path.Combine(directory, fullFilename);
			
			return File.Exists(fullpath);
		}

		public static void DeleteFile(string filename) {
			var fullFilename = GetFullFilename(filename);			
			var directory = GetSaveDirectory();
			var fullpath = Path.Combine(directory, fullFilename);

			File.Delete(fullpath);
		}

		public static bool WriteToFile(string fileName, string fileContents) {
			var fullPath = NextAvailableFilename(GetFullPath(fileName));

			try {
				File.WriteAllText(fullPath, fileContents);
				return true;
			}
			catch ( Exception e ) {
				Debug.LogError($"Failed to write to {fullPath} with exception {e}");
				return false;
			}
		}

		public static bool ReadFromFile(string fileName, out string result) {
			var fullPath = GetFullPath(fileName);

			try {
				result = File.ReadAllText(fullPath);
				return true;
			}
			catch ( Exception e ) {
				Debug.LogError($"Failed to read from {fullPath} with exception {e}");
				result = "";
				return false;
			}
		}

		public static bool MoveFile(string fileName, string newFileName) {
			var fullPath = GetFullPath(fileName);
			var newFullPath = NextAvailableFilename(GetFullPath(newFileName));

			try {
				if ( File.Exists(newFullPath) ) {
					File.Delete(newFullPath);
				}

				File.Move(fullPath, newFullPath);
			}
			catch ( Exception e ) {
				Debug.LogError($"Failed to move file from {fullPath} to {newFullPath} with exception {e}");
				return false;
			}

			return true;
		}

		
	}
}