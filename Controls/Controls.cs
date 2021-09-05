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
		public static Panel ToVerticalStackPanel(IList<Control> Controls)
		{
			Panel Out = new Panel();
			if (Controls.Count > 0)
			{
				int CurrentHeight = 0;

				for (int i = 0; i < Controls.Count; i++)
				{
					Controls[i].Location = new Point(0, CurrentHeight);
					Out.Controls.Add(Controls[i]);
					CurrentHeight += Controls[i].Height;
				}

				Out.Height = Controls.Last().Location.Y + Controls.Last().Height;
				Out.Width = Controls.Max(x => x.Width);
				Out.BackColor = Color.Transparent;
			}
			return Out;
		}
		public static Panel ToHorizontalStackPanel(IList<Control> Controls, int space = 0)
		{
			Panel Out = new Panel();
			int CurrentWidth = 0;
			
			for (int i = 0; i < Controls.Count; i++)
			{
				Controls[i].Location = new Point(CurrentWidth + (i==0? 0:space), 0);
				Out.Controls.Add(Controls[i]);
				CurrentWidth += Controls[i].Width;
			}

			Out.Width = CurrentWidth;
			Out.Height = Controls.Max(x => x.Height);
			Out.BackColor = Color.Transparent;

			return Out;
		}

		public static void ToSameWidth(IList<Control> Controls)
		{
			int MaxWidth = 0;
			for (int i = 0; i < Controls.Count; i++) if (Controls[i].Width > MaxWidth) MaxWidth = Controls[i].Width;
			for (int i = 0; i < Controls.Count; i++) Controls[i].Width = MaxWidth;
		}
		public static void ToSameWidth(IList<Control> Controls, int Width)
		{
			for (int i = 0; i < Controls.Count; i++) Controls[i].Width = Width;
		}
		public static void ToSameWidth(params Control[] Controls)
		{
			ToSameWidth(Controls as IList<Control>);
		}

		class VarInputField
		{
			private static readonly Label EqualsSymb = new Label { Text = "=" };
			private Label label = new Label();
			private TextBox textbox = new TextBox();

			public string Name
			{
				get => label.Text;
				set => label.Text = value;
			}

			public string Value
			{
				get => textbox.Text;
				set => textbox.Text = value;
			}

			public VarInputField(string Name, int TextBoxWidth)
			{
				label.Text = Name;
				textbox.Width = TextBoxWidth;
			}

			public Panel GetControl()
			{
				return ToHorizontalStackPanel(new Control[] { label, EqualsSymb, textbox });
			}
		}

		public static ScrollableControl ToScrollableControl(IList<Control> Controls, Size ScrollableControlSize, int space = 5, int AddSpacesAtEnd = 1)
		{
			ScrollableControl Out = new ScrollableControl();
			Out.Size = ScrollableControlSize;
			Out.AutoScroll = true;

			int CurHei = space, CurWid = space;
			for (int i = 0; i < Controls.Count; i++)
			{
				var temp = Controls[i];
				temp.Location = new Point(CurWid, CurHei); CurHei += 5 + temp.Height;
				Out.Controls.Add(temp);
			}
			Out.Controls.Add(new Panel { Size = new Size(0, AddSpacesAtEnd * space), Location = new Point(CurWid, CurHei) });
			return Out;
		}

	}
	
}
