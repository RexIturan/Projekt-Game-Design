using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Util.Extensions;

namespace Ability {
	[System.Serializable]
	public class TargetPattern {
		
		[System.Serializable]
		private class TargetPattern_Line {
			[SerializeField] public bool[] line;
		}
		
///// NEW
		private char hitSymbole = 'x';
		private char emptySymbole = '-';
		private char seperatorSymbole = ' ';
		private char[] anchorSymbole = {'[', ']'};
		[TextArea(1,10)] [SerializeField] private string stringPattern;
		private bool[,] pattern;

///// OLD		
		[SerializeField] private TargetPattern_Line[] rows;
		[SerializeField] private Vector2Int anchor;

		private int height;
		private int width;

		/**
				 * A pattern needs to be rectangular; 
				 * Each row needs to be the same length. 
				 * There must be at least one row and its length must be at least one. 
				 * The anchor must be within the bounds of the pattern. 
				 */
		public bool IsValid() {
			bool isValid = true;

			isValid = isValid && rows != null;
			isValid = isValid && rows.Length > 0;
			isValid = isValid && rows[0] != null;
			isValid = isValid && rows[0].line != null;

			width = -1;
			if ( isValid ) {
				width = rows[0].line.Length;
				isValid = width > 0;
			}

			for ( int row = 1; isValid && row < rows.Length; row++ ) {
				isValid = rows[row].line != null && rows[row].line.Length == width;
			}

			if ( isValid ) {
				height = rows.Length;
				width = rows[0].line.Length;
			}

			isValid &= anchor.x >= 0 && anchor.y >= 0
			                         && anchor.x < width && anchor.y < height;

			return isValid;
		}

		public void SetSingleTarget() {
			rows = new TargetPattern_Line[1];
			rows[0] = new TargetPattern_Line();
			rows[0].line = new bool[1];
			anchor = new Vector2Int(0, 0);
			height = 1;
			width = 1;
		}

		public bool[][] GetPattern() {
			return GetPattern(0);
		}

		//todo why no direction enum?
		public bool[][] GetPattern(int rotations90DegRight) {
			rotations90DegRight %= 4;

			if ( rotations90DegRight >= 0 && rotations90DegRight <= 3 ) {
				bool[][] pattern;
				int patternWidth;
				int patternHeight;

				// if the pattern is rotated an uneven amount of times
				// the dimensions swap
				if ( rotations90DegRight % 2 == 0 ) {
					patternWidth = width;
					patternHeight = height;
				}
				else {
					patternWidth = height;
					patternHeight = width;
				}

				pattern = new bool[patternWidth][];
				for ( int x = 0; x < pattern.Length; x++ ) {
					pattern[x] = new bool[patternHeight];
					for ( int y = 0; y < pattern[x].Length; y++ ) {
						// the correspondense between the new pattern and the original depends on the rotation
						switch ( rotations90DegRight ) {
							case 0:
								// no rotation: the x coordinate of the new pattern corresponds to
								// the row index, the y coordinate corresponds to the line index
								pattern[x][y] = rows[y].line[x];
								break;
							case 1:
								// one rotation to the right: the x coordinate of the new pattern corresponds 
								// to the negative row index of the original pattern (first row->last x; last row->first x). 
								// The y coordinate of the new pattern corresponds to the line index of the original pattern
								// (the first in a line -> first y; last in a line -> last y)
								pattern[x][y] = rows[height - x - 1].line[y];
								break;
							case 2:
								// two rotations to the right: the x coordinate corresponds to the negative line index 
								// (first in line -> last x; last in line -> first x). The y coordinate corresponds to the
								// negative row index (first row -> last y; last row -> first y)
								pattern[x][y] = rows[height - y - 1].line[width - x - 1];
								break;
							case 3:
								// three rotations to the right (or one left): the x coordinate corresponds to the row index
								// The y coordinate corresponds to the negative line index
								pattern[x][y] = rows[x].line[width - y - 1];
								break;
						}
					}
				}


				return pattern;
			}
			else
				return null;
		}

		public Vector2Int GetAnchor() {
			return GetAnchor(0);
		}

		public Vector2Int GetAnchor(int rotations90DegRight) {
			rotations90DegRight %= 4;
			switch ( rotations90DegRight ) {
				case 0:
					return anchor;
				case 1:
					return new Vector2Int(height - anchor.y - 1, anchor.x);
				case 2:
					return new Vector2Int(width - anchor.x - 1, height - anchor.y - 1);
				case 3:
					return new Vector2Int(anchor.y, width - anchor.x - 1);
				default:
					return new Vector2Int(0, 0);
			}
		}

		public static string PrintPattern(bool[][] pattern) {
			string str = "";

			if ( pattern != null && pattern.Length >= 1 ) {
				for ( int y = 0; y < pattern[0].Length; y++ ) {
					for ( int x = 0; x < pattern.Length; x++ ) {
						if ( pattern[x][y] )
							str += "X";
						else
							str += "O";
					}

					str += "\n";
				}
			}

			return str;
		}
		
///// NEW CODE

		public bool InitFromStringPattern() {

			var rows = stringPattern.Split(new []{'\n'});
			List<string[]> sPattern = new List<string[]>();
			
			if (
				!( stringPattern.Contains(anchorSymbole[0].ToString())
				   && stringPattern.Contains(anchorSymbole[1].ToString()) )
			) {
				return false;
			}
			
			var tempAnchor = GetAnchorFromStringPattern(rows);
			
			for ( int i = 0; i < rows.Length; i++ ) {
				rows[i] = RemoveAnchorSymbol(rows[i]);
				rows[i] = rows[i].Trim(new[] { seperatorSymbole });
			}

			foreach ( var row in rows ) {
				sPattern.Add(row.Split(seperatorSymbole));
			}
			
			var tempHeight = sPattern.Count;
			var tempWidth = sPattern[0].Length;

			if ( sPattern.Any(row => row.Length != tempWidth) ) return false;

			width = tempWidth;
			height = tempHeight;
			anchor = tempAnchor;
			
			this.pattern = new bool[width, height];

			for ( int y = 0; y < height; y++ ) {
				for ( int x = 0; x < width; x++ ) {
					var chr = sPattern[y][x][0];
					if ( chr.Equals(hitSymbole) ) {
						this.pattern[x, y] = true;
					}
					else {
						this.pattern[x, y] = false;
					}
				}
			}

			// PrintPattern(pattern);
			// PrintPattern(RotatePattern(pattern, true));
			// Debug.Log(RotateAnchor(pattern, anchor, true));
			// PrintPattern(FlipPattern(pattern, true));
			// Debug.Log(FlipAnchor(pattern, anchor, true));
			// PrintPattern(RotatePattern(pattern, false));
			// Debug.Log(RotateAnchor(pattern, anchor, false));
			pattern = FlipPattern(pattern, false);
			return true;
		}

		public void PrintPattern(bool[,] arr) {
			int width = arr.GetLength(0);
			int height = arr.GetLength(1);
			
			var s = "";
			for ( int y = 0; y < height; y++ ) {
				for ( int x = 0; x < width; x++ ) {
					if ( arr[x,y] ) {
						s += hitSymbole;
					}
					else {
						s += emptySymbole;
					}

					s += seperatorSymbole;
				}
				s += "\n";
			}
			Debug.Log("PrintPattern\n" + s);
		}
		
		private string RemoveSeperator(string str) {
			var s = str.Remove(seperatorSymbole);
			return s;
		}
		
		private string RemoveAnchorSymbol(string str) {
			var s = str;
			StringBuilder sb = new StringBuilder (s);
			
			if ( s.Length == 0 ) {
				return "";
			}
			
			if ( s[0].Equals(anchorSymbole[0]) ) {
				s.Remove(0);
			} else if ( s[s.Length - 1].Equals(anchorSymbole[1]) ) {
				s.Remove(s.Length-1);
			}

			sb.Replace(anchorSymbole[0], seperatorSymbole);
			sb.Replace(anchorSymbole[1], seperatorSymbole);
			sb.Replace($"{seperatorSymbole}{seperatorSymbole}", $"{seperatorSymbole}");
	
			return sb.ToString();
		} 
		
		private Vector2Int GetAnchorFromStringPattern(string[] rows) {
			Vector2Int pos = new Vector2Int();
			
			for ( int i = 0; i < rows.Length; i++ ) {
				if ( rows[i].Contains(anchorSymbole[0].ToString()) ) {
					pos.y = rows.Length-1 - i;
					var index = rows[i].IndexOf(anchorSymbole[0]);
					pos.x = GetAnchorXPos(rows[i], index);
				}
			}

			return pos;
		}

		private int GetAnchorXPos(string row, int index) {
			if ( row.Length == 0 ) {
				return 0;
			}
			var s = row.Substring(0, index);
			s = String.Join("", s.Split(seperatorSymbole));
			return s.Length;
		}

		private bool[,] RotatePattern(bool[,] arr, bool clockwise) {
			int width = arr.GetLength(0);
			int height = arr.GetLength(1);
			
			var arr_rotated = new bool[height, width];

			if (clockwise) {
				for (int i = 0; i < width; i++){
					for (int j = height -1; j >= 0; j--){
						arr_rotated[height-1 -j, i] = arr[i, j];
					}
				}
			} else {
				for (int i = width-1; i >= 0; i--){
					for (int j = 0; j < height; j++){
						arr_rotated[j, width-1-i] = arr[i, j];
					}
				}
			}

			return arr_rotated;
		}
		
		private bool[,] FlipPattern(bool[,] arr, bool horizontal) {
			int width = arr.GetLength(0);
			int height = arr.GetLength(1);
			
			var arr_flipped = new bool[width, height];

			if (horizontal) {
				for (var y = 0; y < height; y++) {
					for (var x = 0; x < width; x++) {
						arr_flipped[x, height-1 -y] = arr[x, y];
					}
				}
			} else {
				for (var y = 0; y < height; y++) {
					for (var x = 0; x < width; x++) {
						arr_flipped[width-1-x, y] = arr[x, y];
					}
				}
			}
			
			return arr_flipped;
		}

		private Vector2Int RotateAnchor(bool[,] arr, Vector2Int pos, bool clockwise) {
			int width = arr.GetLength(0);
			int height = arr.GetLength(1);
			Vector2Int rotatedPos = new Vector2Int();
			
			if (clockwise) {
				rotatedPos.x = pos.y;
				rotatedPos.y = width-1 - pos.x;
			} else {
				rotatedPos.x = height - 1 - pos.y;
				rotatedPos.y = pos.x;
			}

			return rotatedPos;
		}
		
		private Vector2Int FlipAnchor(bool[,] arr, Vector2Int pos, bool horizontal) {
			int width = arr.GetLength(0);
			int height = arr.GetLength(1);
			Vector2Int flippedPos = new Vector2Int();
			
			if (horizontal) {
				flippedPos.x = pos.x;
				flippedPos.y = height-1 - pos.y;
			} else {
				flippedPos.x = width -1 -pos.x;
				flippedPos.y = pos.y;
			}

			return flippedPos;
		}

		public bool[,] GetRotatedPattern(int rot) {
			return GetRotatedPattern(pattern, rot);
		}

		public bool[,] GetRotatedPattern(bool[,] arr, int rot) {
			rot %= 4;
			bool[,] p;
			switch ( rot ) {
				
				case 1:
					p = RotatePattern(arr, true);
					break;
				case 2:
					p =  FlipPattern(FlipPattern(arr, true), false);
					break;
				case 3:
					p = RotatePattern(arr, false);
					break;
				case 0:
				default:
					p = arr;
					break;
			}

			return p;
		}

		public Vector2Int GetRotatedAnchor(int rot) {
			return GetRotatedAnchor(pattern, rot);
		}
		
		public Vector2Int GetRotatedAnchor(bool[,] arr, int rot) {
			rot %= 4;
			
			Vector2Int p;
			switch ( rot ) {
				
				case 1:
					p = RotateAnchor(arr, anchor, true);
					break;
				case 0:
					p = FlipAnchor(arr,FlipAnchor(arr, anchor, true), false);
					break;
				case 3:
					p = RotateAnchor(arr, anchor, false);
					break;
				case 2:
				default:
					p = anchor;
					break;
			}

			return p;
		}
		
		//todo why no direction enum?
		public List<Vector3Int> GetTargetedTiles(Vector3Int pos, int rot) {
			var pattern = GetRotatedPattern(this.pattern, rot);
			var targetAnchor = GetRotatedAnchor(rot);
			int targetWidth = pattern.GetLength(0);
			int targetHeight = pattern.GetLength(1);

			List<Vector3Int> targets = new List<Vector3Int>();
			
			Vector2Int offset = pos.XZ() - targetAnchor;
			
			for ( int y = 0; y < targetHeight; y++ ) {
				for ( int x = 0; x < targetWidth; x++ ) {
					if ( pattern[x, y] ) {

						var tx = x + offset.x; 
						var tz = y + offset.y;
						Vector3Int targetPos = new Vector3Int(tx, pos.y, tz);
						
						targets.Add(targetPos);
					}
				}
			}

			return targets;
		}
	}
}