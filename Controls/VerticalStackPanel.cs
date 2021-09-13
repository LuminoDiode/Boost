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
		public class VerticalStackPanel: IScrollableStackPanel
		{
			private bool _AutoWidth;
			public bool AutoWidth
			{
				get => this._AutoWidth;
				set
				{
					if (value == true)
					{
						this.SetAutoWidth();
						this._AutoWidth = value;
					}
					else
					{
						this.MainControl.Width = this._UserWidth;
					}
				}
			}

			private int _UserWidth;
			public int Width
			{
				get => this.MainControl.Width;
				set
				{
					this._UserWidth = value;
					if (!AutoWidth) this.MainControl.Width = value;
				}
			}
			public int Height
			{
				get => this.MainControl.Height;
				set => this.MainControl.Height = value;
			}

			public static explicit operator ScrollableControl(VerticalStackPanel SSP) => SSP.MainControl;
			public static explicit operator Control(VerticalStackPanel SSP) => SSP.MainControl;

			public VerticalStackPanel(int height)
			{
				this.MainControl = new ScrollableControl { Height = height, AutoScroll = true,Visible=true };
			}
			public VerticalStackPanel(IEnumerable<Control> controls, int height)
			{
				this.MainControl = new ScrollableControl { Height = height, AutoScroll = true,Visible=true };
				foreach (Control c in controls) {
					this.AddControl(c);
				}
			}

			public void AddControl(Control Cntrl)
			{
				Cntrl.Location = this.MainControl.Controls.Count > 0 ? new Point(0,this.LowestRightPointOfControls.Y) : new Point(0, 0);
				Console.WriteLine($"Added control to {Cntrl.Location}");
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
					this.MainControl.Controls[i].Location = new Point(0,RemovedSize.Height);
				}
			}

			

			
			private void SetAutoWidthIfNeeded()
			{
				if (this.AutoWidth) this.SetAutoWidth();
			}
			private void SetAutoWidth()
			{
				if (this.MainControl.Controls.Count > 0) this.MainControl.Width = Boost.Controls.MaxBy(this.MainControl.Controls,x=>x.Width).Width+17;
			}
		}
	}
}
