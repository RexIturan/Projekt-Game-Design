using System.Collections.Generic;
using UnityEngine;


namespace Util {
	[System.Serializable]
	public class PathNode {
		public struct Edge {
			public Edge(int cost, PathNode target) {
				this.cost = cost;
				this.target = target;
			}

			public readonly int cost;
			public readonly PathNode target;
		}

		// private GenericGrid1D<PathNode> _grid;
		public Vector3Int pos;

		public int gCost;
		public int hCost;
		public int fCost;

		private const int MoveStraightCost = 10;
		private const int MoveDiagonalCost = 14;

		private int _costFactor = 2;

		public List<Edge> edges;

		public int dist;

		public bool isWalkable;
		public PathNode parentNode;

		public PathNode(Vector3Int pos) : this(pos.x, pos.y, pos.z) { }

		public PathNode(int x, int y, int z) {
			pos = new Vector3Int(x, y, z);
			isWalkable = true;
		}

		//todo refactor move to Controller Class and let this class just be a data struct 
		public void SetEdges(bool diagonal, GenericGrid1D<PathNode> grid) {
			// if (!isWalkable) {
			//     return;
			// }

			edges = new List<Edge>();

			var x = pos.x;
			var z = pos.z;
			
			if ( x - 1 >= 0 ) {
				// Left
				AddEdge(x - 1, z, MoveStraightCost, grid);
				if ( diagonal ) {
					// Left Down
					if ( z - 1 >= 0 ) AddEdge(x - 1, z - 1, MoveDiagonalCost, grid);
					// Left Up
					if ( z + 1 < grid.Depth ) AddEdge(x - 1, z + 1, MoveDiagonalCost, grid);
				}
			}

			if ( x + 1 < grid.Width ) {
				// Right
				AddEdge(x + 1, z, MoveStraightCost, grid);
				if ( diagonal ) {
					// Right Down
					if ( z - 1 >= 0 ) AddEdge(x + 1, z - 1, MoveDiagonalCost, grid);
					// Right Up
					if ( z + 1 < grid.Depth ) AddEdge(x + 1, z + 1, MoveDiagonalCost, grid);
				}
			}

			// Down
			if ( z - 1 >= 0 ) AddEdge(x, z - 1, MoveStraightCost, grid);
			// Up
			if ( z + 1 < grid.Depth ) AddEdge(x, z + 1, MoveStraightCost, grid);
		}

		private void AddEdge(int posX, int posZ, int cost, GenericGrid1D<PathNode> grid) {
			if ( grid.GetGridObject(posX, posZ).isWalkable ) {
				edges.Add(new Edge(cost * _costFactor, grid.GetGridObject(posX, posZ)));
			}
		}

		public void CalculateFCost() {
			fCost = gCost + hCost;
		}

		public void SetIsWalkable(bool value) {
			isWalkable = value;
			// _grid.TriggerGridObjectChanged(x, y);
		}

		public override string ToString() {
			if ( isWalkable ) {
				return pos + " +";
			}
			else {
				return pos + " -";
			}
		}

		/// <summary>
		/// Converts a list of pathnodes to a list of game grid coordinates. 
		/// </summary>
		/// <param name="pathNodes">List that is being converted </param>
		/// <returns>List of grid coordinates corresponding the pathnodes </returns>
		public static List<Vector3Int> ConvertPathNodeListToVector3IntList(List<PathNode> pathNodes) {
			List<Vector3Int> convertedList = new List<Vector3Int>();
			foreach(PathNode node in pathNodes) {
				convertedList.Add(node.pos);
			}
			return convertedList;
		}
	}
}