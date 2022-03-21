using UnityEditor.GraphToolsFoundation.Overdrive;

namespace _Structure._GraphView.LevelGraph.Util {
	public class LevelGraph_PortType : PortType {
		
		/// <summary>
		/// The port is used for the bothway connections.
		/// </summary>
		public static readonly PortType Connection = new LevelGraph_PortType(3, nameof(Connection));

		protected LevelGraph_PortType(int id, string name) : base(id, name) { }
	}
}