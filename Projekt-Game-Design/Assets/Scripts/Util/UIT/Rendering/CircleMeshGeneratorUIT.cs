using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace GDP01.Util.Util.UIT.Rendering {
	public static class CircleMeshGeneratorUIT {
		public const int MinRings = 1;
		public const int MinSections = 3;

		private const float PI = Mathf.PI;

		private static Vector2 GetPointOnCircle(float angle, float radius, Vector2 center) {
			return new Vector2 {
				x = radius * Mathf.Cos(angle) + center.x,
				y = radius * Mathf.Sin(angle) + center.y
			};
		}
		
		private static void CreateCircleCenter(MeshWriteData mwd, ushort sections, float radius, Vector2 center, Color32 color, ushort lastIndex) {

			float sectionAngle = Mathf.Deg2Rad * 360f / sections;

			//place vertices
			for ( int i = 0; i < sections; i++ ) {
				mwd.SetNextVertex( new Vertex { position = GetPointOnCircle(sectionAngle * i, radius, center), tint = color });	
			}
			mwd.SetNextVertex( new Vertex { position = center, tint = color });
			
			var x2 = lastIndex + sections;
			for ( ushort i = 0; i < sections; i++ ) {
				var x0 = lastIndex + i;
				var x1 = lastIndex + i + 1;

				if ( i + 1 == sections ) {
					x1 = lastIndex;
				}

				if ( i % 2 == 0 ) {
					mwd.SetNextIndex((ushort) x2);
					mwd.SetNextIndex((ushort) x0);
					mwd.SetNextIndex((ushort) x1);
				}
				else {
					mwd.SetNextIndex((ushort) x0);
					mwd.SetNextIndex((ushort) x1);
					mwd.SetNextIndex((ushort) x2);
				}
			}
		}
		
		private static int CreateCircleRing(MeshWriteData mwd, int sections, int rings, float ringWidth, float radius, Vector2 center, Color32 color, ushort lastIndex) {

			float sectionAngle = Mathf.Deg2Rad * 360f / sections;
			
			var newRadius = radius;
			
			//set vertices
			for ( int i = 0; i < rings; i++ ) {
				newRadius -= i * ringWidth;
				for ( int j = 0; j < sections; j++ ) {
					mwd.SetNextVertex( new Vertex { position = GetPointOnCircle(sectionAngle * j, newRadius, center), tint = color });	
				}
			}

			//set indices
			for ( int i = 0; i < rings -1; i++ ) {
				for ( int j = 0; j < sections; j++ ) {
					var x0 = lastIndex + j + sections * i;
					var x1 = j + 1 == sections ? lastIndex + sections * i : x0 + 1;
					var x3 = x0 + sections;
					var x2 = x1 + sections;
					
					mwd.SetNextIndex((ushort) x0);
					mwd.SetNextIndex((ushort) x1);
					mwd.SetNextIndex((ushort) x2);
				
					mwd.SetNextIndex((ushort) x2);
					mwd.SetNextIndex((ushort) x3);
					mwd.SetNextIndex((ushort) x0);
				}
			}

			return ( rings - 1 ) * sections * 2 + lastIndex;
		}
		
		public static void CreateCircle(this MeshGenerationContext mgc, int sections, int rings, float radius, Vector2 center, Color32 color, bool filled, float ringWidth) {
			Assert.IsFalse(sections < MinSections);
			Assert.IsFalse(rings < MinRings);

			int vertexCount;
			int triangleCount;
			int indixCount;
			MeshWriteData mwd;			

			if ( filled ) {
				
				if ( rings == 1 ) {
					vertexCount = 1 + sections;
					triangleCount = sections;
					indixCount = triangleCount * 3;
					mwd = mgc.Allocate(vertexCount, indixCount, null);
					CreateCircleCenter(mwd, (ushort) sections, radius, center, color, 0);
				}
				else {
					vertexCount = 1 + sections + sections * rings;
					//todo share vertices between both?
					triangleCount = sections + ( rings -1) * sections * 2;
					indixCount = triangleCount * 3;
					mwd = mgc.Allocate(vertexCount, indixCount, null);
					
					var lastIndex = CreateCircleRing(mwd, (ushort) sections, rings, ringWidth, radius, center, color, 0);
					var newRadius = radius - ( ( rings - 1 ) * ringWidth );
					CreateCircleCenter(mwd, (ushort) sections, newRadius, center, color, (ushort) lastIndex);
				}	
			}
			else {
				var actualRings = rings > 1 ? rings : 2;
				
				vertexCount = sections * actualRings;
				triangleCount = ( actualRings - 1 ) * sections * 2;
				indixCount = triangleCount * 3;
				mwd = mgc.Allocate(vertexCount, indixCount, null);
				
				CreateCircleRing(mwd, (ushort) sections, actualRings, ringWidth, radius, center, color, 0);				
			}
		}
	}
}