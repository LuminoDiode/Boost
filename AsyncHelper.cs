using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Boost
{
	public class AsyncHelper
	{
		public static void InvokeByQueue(IList<Action> Acts)
		{
			Trace.WriteLine("InvokeByQueue Called");
			Thread thr = new Thread(() => { for (int i = 0; i < Acts.Count; i++) Acts[i](); Trace.WriteLine("InvokeByQueue Invoke Called"); });
			thr.Start();
		}

		public static void InvokeAll(IList<Action> Acts)
		{
			Trace.WriteLine("InvokeAll Called");
			for (int i = 0; i < Acts.Count; i++)
			{
				Thread thr = new Thread(()=>Acts[i]());
				thr.Start();
				Trace.WriteLine("InvokeAll Invoke Called");
			}
		}
	}
}

