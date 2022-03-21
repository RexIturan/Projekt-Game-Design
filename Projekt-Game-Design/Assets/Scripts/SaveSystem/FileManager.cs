using System;
using System.IO;
using UnityEngine;

namespace SaveSystem {
	public static class FileManager {

		private static string numberPattern = " ({0})";
		private static string jsonFileExtension = ".json";
		private const string levelDirectory = "level";
		
//////////////////////////////////////// Local Functions ///////////////////////////////////////////

		private static string GetSaveDirectory() {
			//todo -> Application.persistentDataPath
			return Application.streamingAssetsPath;
		}
		
		private static string GetSaveDirectory(string subDirectory) {
			return Path.Combine(GetSaveDirectory(), subDirectory);
		}

		private static string GetFullFilename(string filename) {
			return String.Concat(filename, jsonFileExtension); 
		}
		
		private static string GetFullPath(string fileName, string subDirectory) {
			var fullFilename = GetFullFilename(fileName);
			var directory = GetSaveDirectory(subDirectory); 
			
			return Path.Combine(directory, fullFilename);
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

		public static string[] GetFileNames(string subDirectory = levelDirectory) {
			var directory = GetSaveDirectory(subDirectory);
			var filesPaths = Directory.GetFiles(directory, $"*{jsonFileExtension}");

			string[] fileNames = new string[filesPaths.Length];

			for ( int i = 0; i < filesPaths.Length; i++ ) {
				fileNames[i] = Path.GetFileNameWithoutExtension(filesPaths[i]);
			}
			
			return fileNames;
		}

		public static bool FileExists(string filename, string subDirectory = levelDirectory) {
			var fullpath = GetFullPath(filename, subDirectory);
			
			return File.Exists(fullpath);
		}

		public static void DeleteFile(string filename, string subDirectory = levelDirectory) {
			var fullpath = GetFullPath(filename, subDirectory);

			File.Delete(fullpath);
		}

		public static bool WriteToFile(string fileContents, string fileName, bool overrideFile = false, string subDirectory = levelDirectory) {
			var path = GetFullPath(fileName, subDirectory);
			var fullPath = overrideFile ? path : NextAvailableFilename(path);

			try {
				File.WriteAllText(fullPath, fileContents);
				return true;
			}
			catch ( Exception e ) {
				Debug.LogError($"Failed to write to {fullPath} with exception {e}");
				return false;
			}
		}

		public static bool ReadFromFile(string fileName, out string result, string directory = levelDirectory) {
			//todo check if it exists first?
			
			var fullPath = GetFullPath(fileName, directory);

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

		public static bool MoveFile(string fileName, string newFileName, string directory = levelDirectory, string newDirectory = levelDirectory) {
			var fullPath = GetFullPath(fileName, directory);
			var newFullPath = NextAvailableFilename(GetFullPath(newFileName, newDirectory));

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