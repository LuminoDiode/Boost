using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Boost
{
	partial class Graph
	{
		/*
		class Scheme
		{
			public List<Vertex> Vertices;

			public void AddVertex(Vertex Vrtx)
			{
				if (!Vertices.Contains(Vrtx))
				{
					if (!Vertices.Any(x => x.Equals(Vrtx))) Vertices.Add(Vrtx);
					else throw new ArgumentException("Equal Vertex is already exists in this Scheme");
				}
				else throw new ArgumentException("Passed Vertex is already exists in this Scheme");
			}
			public void RemoveVertex(Vertex Vrtx)
			{
				Vertices.RemoveAll(x => x.Equals(Vrtx));
			}

			public static int GetShortestWayP(Scheme Scheme, Vertex StartVertex, Vertex DestinationVertex)
			{
				var Visited = new List<Vertex>(); Visited.Add(StartVertex);
				return GetShortestWay(Scheme, StartVertex, DestinationVertex, 0, Visited);
			}
		*/

		public enum VERTEX_COMPARISON_MODES
		{
			ByRef = 0,
			ByEquality = 1,
			ByName = 2
		}

		public static int GetShortestWay(
		in Vertex CurrentVertex,
		in Vertex DestinationVertex,
		int OnGoing=0,
		List<Vertex> Visited=null,
		VERTEX_COMPARISON_MODES VertexComparisonMode = 0)
		{
			if (Visited == null) Visited = new List<Vertex>();

			if (CurrentVertex.ConnectedVertices.Count == 0) return int.MaxValue;

			var VertexEnum = CurrentVertex.ConnectedVertices.GetEnumerator();
			
			Vertex NowTo;
			int CurrentWayLength, MinDist = int.MaxValue;

			for(int i = 0; i< CurrentVertex.ConnectedVertices.Count; i++) { 
				NowTo = CurrentVertex.ConnectedVertices[i];
				if (NowTo == null) throw new Exception("n");
				if (!Visited.Contains(NowTo))
				{
					if (CompareVertices(NowTo, DestinationVertex, VertexComparisonMode))
					{
						CurrentWayLength = OnGoing + CurrentVertex.GetDistanceToVertex(NowTo);
					}
					else
					{
						Visited.Add(NowTo);
						CurrentWayLength = GetShortestWay(NowTo, DestinationVertex, OnGoing + CurrentVertex.GetDistanceToVertex(NowTo), Visited,VertexComparisonMode);
						Visited.Remove(NowTo);
					}

					if (CurrentWayLength < MinDist) MinDist = CurrentWayLength;
				}
			} //while (VertexEnum.MoveNext());

			return MinDist;
		}

		private static bool CompareVertices(Vertex v1, Vertex v2, VERTEX_COMPARISON_MODES VertexComparisonMode = 0)
		{
			return
				VertexComparisonMode == VERTEX_COMPARISON_MODES.ByRef ? v1 == v2 :
				VertexComparisonMode == VERTEX_COMPARISON_MODES.ByEquality ? v1.Equals(v2) :
				VertexComparisonMode == VERTEX_COMPARISON_MODES.ByName ? v1.Name == v2.Name : false;
		}


		public class Vertex
		{
			public int ID { get; set; }
			public string Name { get; set; }
			private Dictionary<Vertex, int> DistanceToConnectedVertices = new Dictionary<Vertex, int>();
			public List<Vertex> ConnectedVertices => DistanceToConnectedVertices.Keys.ToList();

			public int GetDistanceToVertex(Vertex Vrtx)
			{
				try
				{
					return DistanceToConnectedVertices[Vrtx];
				}
				catch (KeyNotFoundException)
				{
					throw new ArgumentException("No connection to selected Vertex found");
				}
			}
			public void SetDistanceToVertex(Vertex Vrtx, int NewDistance)
			{
				try
				{
					DistanceToConnectedVertices[Vrtx] = NewDistance;
				}
				catch (KeyNotFoundException)
				{
					DistanceToConnectedVertices.Add(Vrtx, NewDistance);
				}
			}

			public bool Equals(Vertex Vrtx)
			{
				if (Vrtx == this) return true;

				return
					Vrtx.DistanceToConnectedVertices.Count == this.DistanceToConnectedVertices.Count
					&& Vrtx.DistanceToConnectedVertices.Keys.All(x => Vrtx.DistanceToConnectedVertices[x] == this.DistanceToConnectedVertices[x]);
			}
		}
	}
}