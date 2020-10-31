using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Boost
{
	public class AsyncHelper
	{
		public static async void InvokeByQueue(IList<Action> Acts)
		{
			new Thread(() => { for (int i = 0; i < Acts.Count; i++) Acts[i].Invoke(); });
		}

		public static void InvokeAll(IList<Action> Acts)
		{
			for (int i = 0; i < Acts.Count; i++)
			{
				Acts[i].BeginInvoke(null, null);
			}
		}
	}
}

