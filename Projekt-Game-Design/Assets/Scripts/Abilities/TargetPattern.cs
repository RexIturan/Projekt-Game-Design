using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ability
{
		[System.Serializable]
		public class TargetPattern
		{
				[SerializeField] private TargetPattern_Line[] rows;
				[SerializeField] private Vector2Int anchor;

				private int height;
				private int width;

				[System.Serializable]
				private class TargetPattern_Line
				{
						[SerializeField] public bool[] line;
				}

				/**
				 * A pattern needs to be rectangular; 
				 * Each row needs to be the same length. 
				 * There must be at least one row and its length must be at least one. 
				 * The anchor must be within the bounds of the pattern. 
				 */
				public bool Init()
				{
						bool isValid = true;

						isValid &= rows != null;

						isValid &= rows.Length > 0;

						isValid &= rows[0] != null;

						isValid &= rows[0].line != null;

						width = -1;
						if ( isValid )
						{
								width = rows[0].line.Length;
								isValid = width > 0;
						}
						
						for ( int row = 1; isValid && row < rows.Length; row++ )
						{
								isValid = rows[row].line != null && rows[row].line.Length == width;
						}

						if(isValid)
						{
								height = rows.Length;
								width = rows[0].line.Length;
						}

						isValid &= anchor.x >= 0 && anchor.y >= 0
								&& anchor.x < width && anchor.y < height;

						return isValid;
				}

				public void SetSingleTarget()
				{
						rows = new TargetPattern_Line[1];
						rows[0] = new TargetPattern_Line();
						rows[0].line = new bool[1];
						anchor = new Vector2Int(0, 0);
						height = 1;
						width = 1;
				}

				public bool[][] GetPattern()
				{
						return GetPattern(0);
				}

				public bool[][] GetPattern(int rotations90DegRight)
				{
						rotations90DegRight %= 4;

						if ( rotations90DegRight >= 0 && rotations90DegRight <= 3 ) {
								bool[][] pattern;
								int patternWidth;
								int patternHeight;
								
								// if the pattern is rotated an uneven amount of times
								// the dimensions swap
								if ( rotations90DegRight % 2 == 0 )
								{
										patternWidth = width;
										patternHeight = height;
								}
								else
								{
										patternWidth = height;
										patternHeight = width;
								}

								pattern = new bool[patternWidth][];
								for ( int x = 0; x < pattern.Length; x++ )
								{
										pattern[x] = new bool[patternHeight];
										for ( int y = 0; y < pattern[x].Length; y++ )
										{
												// the correspondense between the new pattern and the original depends on the rotation
												switch ( rotations90DegRight )
												{
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

				public Vector2Int GetAnchor()
				{
						return GetAnchor(0);
				}

				public Vector2Int GetAnchor(int rotations90DegRight)
				{
						rotations90DegRight %= 4;
						switch (rotations90DegRight)
						{
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

				public static string PrintPattern(bool[][] pattern)
				{
						string str = "";

						if ( pattern != null && pattern.Length >= 1 ) {
								for ( int y = 0; y < pattern[0].Length; y++ )
								{
										for ( int x = 0; x < pattern.Length; x++ )
										{
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
		}
}