using System;
using System.Collections;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Boost
{
	public static class Program
	{
		[DllImport("kernel32.dll")]
		static extern bool AllocConsole();
		public static void Main()
		{
			Graph.Vertex v1 = new Graph.Vertex();
			Graph.Vertex v2 = new Graph.Vertex();
			Graph.Vertex v3 = new Graph.Vertex();

			v1.SetDistanceToVertex(v2, 100);
			v1.SetDistanceToVertex(v3, 300);
			v2.SetDistanceToVertex(v3, 50);

			Console.WriteLine(Graph.GetShortestWay(v1, v3));
		}
	}
}
