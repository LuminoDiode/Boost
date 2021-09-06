using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace Boost
{
	public partial class Controls
	{
		/// <summary>
		/// VerticalStackPanel of HorizontalStackPanel
		/// </summary>
		public abstract class IStackPanel
		{
			public Control.ControlCollection Controls => this.MainControl.Controls;
			protected ScrollableControl MainControl;
			public ScrollableControl AsControl => this.MainControl;

			/// <summary>
			/// Для самого низкорасположенного контрола возвращает нижний правый угол
			/// </summary>
			public Point LowestRightPointOfControls
			{
				get
				{
					if (this.MainControl.Controls.Count == 0) return new Point(0, 0);
					var LowestControl = Boost.Controls.MaxBy(this.Controls, x => x.Location.Y + x.Size.Height);
					return Point.Add(LowestControl.Location, LowestControl.Size);
				}
			}
			/// <summary>
			/// Для самого праворасположенного контрола возвращает нижний правый угод
			/// </summary>
			public Point RightestLowPointOfControls
			{
				get
				{
					if (this.MainControl.Controls.Count == 0) return new Point(0, 0);
					var RightestControl = Boost.Controls.MaxBy(this.Controls, x => x.Location.X + x.Size.Width);
					return Point.Add(RightestControl.Location, RightestControl.Size);
				}
			}

			public Control GetControl(int Index)
			{
				return this.MainControl.Controls[Index];
			}
		}
		public abstract class IScrollableStackPanel:IStackPanel
		{
			protected bool _Scrollable;
			public bool Scrollable
			{
				get => this.MainControl.AutoScroll;
				set
				{
					this.MainControl.AutoScroll = value;
				}
			}
		}
	}
}