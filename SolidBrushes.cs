using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Boost
{
	public class SolidBrushes{
		private const byte MaxByte = byte.MaxValue;
		private const byte LightColor = byte.MaxValue - 20;

		public static readonly SolidColorBrush LightRed = new SolidColorBrush(Color.FromRgb(MaxByte, LightColor, LightColor));
		public static readonly SolidColorBrush LightGreen = new SolidColorBrush(Color.FromRgb(LightColor, MaxByte, LightColor));
		public static readonly SolidColorBrush LightBlue = new SolidColorBrush(Color.FromRgb(LightColor, LightColor, MaxByte));
	}
}
