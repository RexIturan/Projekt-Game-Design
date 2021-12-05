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
			this.pos = new Vector3Int(x, y, z);
			isWalkable = true;
		}

		//todo refactor move to Controller Class and let this class just be a data struct 
		public void SetEdges(bool diagonal, GenericGrid1D<PathNode> grid) {
			// if (!isWalkable) {
			//     return;
			// }

			edges = new List<Edge>();

			var x = this.pos.x;
			var y = this.pos.x;
			
			if ( x - 1 >= 0 ) {
				// Left
				AddEdge(x - 1, y, MoveStraightCost, grid);
				if ( diagonal ) {
					// Left Down
					if ( y - 1 >= 0 ) AddEdge(x - 1, y - 1, MoveDiagonalCost, grid);
					// Left Up
					if ( y + 1 < grid.Depth ) AddEdge(x - 1, y + 1, MoveDiagonalCost, grid);
				}
			}

			if ( x + 1 < grid.Width ) {
				// Right
				AddEdge(x + 1, y, MoveStraightCost, grid);
				if ( diagonal ) {
					// Right Down
					if ( y - 1 >= 0 ) AddEdge(x + 1, y - 1, MoveDiagonalCost, grid);
					// Right Up
					if ( y + 1 < grid.Depth ) AddEdge(x + 1, y + 1, MoveDiagonalCost, grid);
				}
			}

			// Down
			if ( y - 1 >= 0 ) AddEdge(x, y - 1, MoveStraightCost, grid);
			// Up
			if ( y + 1 < grid.Depth ) AddEdge(x, y + 1, MoveStraightCost, grid);
		}

		private void AddEdge(int posX, int posY, int cost, GenericGrid1D<PathNode> grid) {
			if ( grid.GetGridObject(posX, posY).isWalkable ) {
				edges.Add(new Edge(cost * _costFactor, grid.GetGridObject(posX, posY)));
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
	}
}