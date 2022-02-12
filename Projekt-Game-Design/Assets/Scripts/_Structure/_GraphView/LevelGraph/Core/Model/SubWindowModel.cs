using System;
using _Structure._GraphView.LevelGraph.Core.Model.Interfaces;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;
using UnityEngine;
using static UnityEditor.GraphToolsFoundation.Overdrive.Capabilities;

namespace _Structure._GraphView.LevelGraph.Core.Model {
	[Serializable]
	public class SubWindowModel : GraphElementModel, ISubWindowModel {
		[SerializeField] string m_Title;
		[SerializeField] Rect m_Position;

		/// <inheritdoc />
		public Rect PositionAndSize
		{
			get => m_Position;
			set
			{
				var r = value;
				if (!this.IsResizable())
					r.size = m_Position.size;

				if (!this.IsMovable())
					r.position = m_Position.position;

				m_Position = r;
			}
		}
		
		/// <inheritdoc />
		public Vector2 Position
		{
			get => PositionAndSize.position;
			set
			{
				if (!this.IsMovable())
					return;

				PositionAndSize = new Rect(value, PositionAndSize.size);
			}
		}
		
		/// <inheritdoc />
		public string Title {
			get => m_Title;
			set { if (value != null && m_Title != value) m_Title = value; }
		}
		
		/// <inheritdoc />
		public string DisplayTitle => Title;
		
		/// <inheritdoc />
		public void Move(Vector2 delta) {
			if (!this.IsMovable())
				return;

			Position += delta;
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="SubWindowModel"/> class.
		/// </summary>
		public SubWindowModel()
		{
			m_Capabilities.AddRange(new[]
			{
				Selectable,
				Movable,
				Resizable
			});
			Title = string.Empty;
			// PositionAndSize = Rect.zero;
		}
	}
}