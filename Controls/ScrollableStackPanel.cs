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
		public class ScrollableStackPanel
		{
			//public readonly ICollection<Control> Controls => this.MainControl.Co
			private readonly ScrollableControl MainControl;
			public ScrollableControl AsControl => this.MainControl;

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

			private Point LowestElementLocation => this.MainControl.Controls.Count == 0 ? new Point(0, 0) : this.MainControl.Controls.Last().Location;
			private Size LowestElementSize => this.MainControl.Controls.Count == 0 ? new Size(0, 0) : this.MainControl.Controls.Last().Size;
			public Point LowestLeftPointOfControls => new Point(0, LowestElementLocation.Y + LowestElementSize.Height);

			public static explicit operator ScrollableControl(ScrollableStackPanel SSP) => SSP.MainControl;
			public static explicit operator Control(ScrollableStackPanel SSP) => SSP.MainControl;

			public ScrollableStackPanel(int height)
			{
				this.MainControl = new ScrollableControl { Height = height, AutoScroll = true };
			}
			public ScrollableStackPanel(Control[] controls, int height)
			{
				foreach (Control c in controls) this.MainControl.Controls.Add(c);
				this.MainControl = new ScrollableControl { Height = height, AutoScroll = true };
			}

			public void AddControl(Control Cntrl)
			{
				Cntrl.Location = this.MainControl.Controls.Count > 0 ? this.LowestLeftPointOfControls : new Point(0, 0);

				Controls.Add(Cntrl);
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
				Controls.Insert(Index, Cntrl);
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

			public void RemoveControl(int Index)
			{

			}

			public Control GetControl(int Index)
			{
				return this.MainControl.Controls[Index];
			}
			private void SetAutoWidthIfNeeded()
			{
				if (this.AutoWidth) this.SetAutoWidth();
			}
			private void SetAutoWidth()
			{
				if (this.MainControl.Controls.Count > 0) this.MainControl.Width = ((ICollection<Control>)this.MainControl.Controls).Max(x => x.Width) + 17;
			}
		}
	}
}
