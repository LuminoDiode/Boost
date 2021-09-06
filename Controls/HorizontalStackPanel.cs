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
		public class HorizontalStackPanel: IScrollableStackPanel
		{
			

			//mode to Height
			private bool _AutoHeight;
			//mode to Height
			public bool AutoHeight
			{
				get => this._AutoHeight;
				set
				{
					if (value == true)
					{
						this.SetAutoHeight();
						this._AutoHeight = value;
					}
					else
					{
						this.MainControl.Width = this._UserHeight;
					}
				}
			}
			//mode to height
			private int _UserHeight;
			//mode to height
			public int Height
			{
				get => this.MainControl.Width;
				set
				{
					this._UserHeight = value;
					if (!AutoHeight) this.MainControl.Width = value;
				}
			}
			//mode to width
			public int Width
			{
				get => this.MainControl.Width;
				set => this.MainControl.Width = value;
			}

			//to new class name
			public static explicit operator ScrollableControl(HorizontalStackPanel SSP) => SSP.MainControl;
			//to new class name
			public static explicit operator Control(HorizontalStackPanel SSP) => SSP.MainControl;


			public HorizontalStackPanel(int width)
			{
				this.MainControl = new ScrollableControl { Width = width, AutoScroll = true };
			}
			public HorizontalStackPanel(IEnumerable<Control> controls, int width)
			{
				foreach (Control c in controls) this.MainControl.Controls.Add(c);
				this.MainControl = new ScrollableControl { Width = width, AutoScroll = true };
			}

			public void AddControl(Control Cntrl)
			{
				Cntrl.Location = this.MainControl.Controls.Count > 0 ? new Point(this.RightestLowPointOfControls.X,0) : new Point(0, 0);

				this.MainControl.Controls.Add(Cntrl);
				SetAutoWidthIfNeeded();
			}
			public void AddControl(Control Cntrl, int Index)
			{
				if (Index > (this.MainControl.Controls.Count - 1))
				{
					this.AddControl(Cntrl);
					return;
				}
				Cntrl.Location = this.MainControl.Controls[Index].Location;
				this.MainControl.Controls.Add(Cntrl);
				for (int i = Index + 1; i < this.MainControl.Controls.Count; i++)
				{
					this.MainControl.Controls[i].Location = Point.Add(this.MainControl.Controls[i].Location, new Size(0, Cntrl.Size.Height));
				}
			}
			/// <summary>
			/// Removes the first occurrence
			/// </summary>
			/// <param name="Cntrl"></param>
			public void RemoveControl(Control Cntrl)
			{
				var RemovedSize = Cntrl.Size;
				var RemovedIndex = this.MainControl.Controls.IndexOf(Cntrl);
				this.MainControl.Controls.Remove(Cntrl);
				for (int i = RemovedIndex; i < this.MainControl.Controls.Count; i++)
				{
					this.MainControl.Controls[i].Location = new Point(0, RemovedSize.Height);
				}
			}

			public void RemoveControl(int Index)
			{

			}

			public Control GetControl(int Index)
			{
				return this.MainControl.Controls[Index];
			}
			private void SetAutoWidthIfNeeded()
			{
				if (this.AutoHeight) this.SetAutoHeight();
			}
			private void SetAutoHeight()
			{
				if (this.MainControl.Controls.Count > 0) this.MainControl.Width = Boost.Controls.MaxBy(this.MainControl.Controls, x => x.Width).Width + 17;
			}
		}
	}
}