using System;
using UnityEngine;
using Util;

namespace Level.Grid {
	public abstract class IGrid<T>: GenericGrid1D<T> {

		protected IGrid(
			int width,
			int depth,
			float cellSize,
			Vector3 originPosition,
			Func<GenericGrid1D<T>, int, int, T> createGridObject,
			bool showDebug = false,
			Transform debugTextParent = null) :
			base(
				width,
				depth,
				cellSize,
				originPosition,
				createGridObject,
				showDebug,
				debugTextParent) { }
	}
}