using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.VisualStyles;

namespace Boost
{
	public static class Controls
	{
		public static Panel ToVerticalStackPanel(IList<Control> Controls, int space = 0)
		{
			// панель - пустой блок без всего
			Panel Out = new Panel();
			// отсчет отступа меж элементами
			int CurrentHeight = 0;
			// параметры выходной панели
			int OutWid = 0;

			for (int i = 0; i < Controls.Count; i++)
			{
				// Выбор положения на панели
				Controls[i].Location = new Point(0, CurrentHeight);
				// Добавление на панель
				Out.Controls.Add(Controls[i]);
				// Учет старого элемента для определения позиции нового
				CurrentHeight += Controls[i].Height + space;
				// Саммый длинный элемент определяет длинну панели
				if (Controls[i].Width > OutWid) OutWid = Controls[i].Width;
			}
			Out.Height = Controls.Last().Location.Y+ Controls.Last().Height;
			Out.Width = Controls.Max(x=>x.Width);
			Out.BackColor = Color.Transparent;
			return Out;
		}
		public static Panel ToHorizontalStackPanel(IList<Control> Controls, int space = 5)
		{
			// панель - пустой блок без всего
			Panel Out = new Panel();
			// отсчет отступа меж элементами
			int CurrentWidth = 0;
			// параметры выходной панели
			int OutHei = 0;

			for (int i = 0; i < Controls.Count; i++)
			{
				// Выбор положения на панели
				Controls[i].Location = new Point(CurrentWidth, 0);
				// Добавление на панель
				Out.Controls.Add(Controls[i]);
				// Учет старого элемента для определения позиции нового
				CurrentWidth += Controls[i].Width + space;
				// Саммый длинный элемент определяет длинну панели
				if (Controls[i].Height > OutHei) OutHei = Controls[i].Height;
			}
			Out.Width = CurrentWidth;
			Out.Height = OutHei;
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
	class ScrollableStackPanel
	{
		private ScrollableControl MainControl;
		private List<Control> Controls;
		public bool UpToDateRendered;
		public int Width => MainControl.Width;
		public int Height => MainControl.Height;
		public int NumberOfControls => Controls.Count;
		public Point LowestElementLocation
		{
			get
			{
				return Controls.Count == 0 ? new Point(0, 0) : Controls.Last().Location;
			}
		}
		public Size LowestElementSize
		{
			get
			{
				return Controls.Count == 0 ? new Size(0, 0) : Controls.Last().Size;
			}
		}
		public Point LowestPointOfControls
		{
			get
			{
				return Point.Add(LowestElementLocation, LowestElementSize);
			}
		}

		public ScrollableControl ToControl() => this.MainControl;
		public static explicit operator ScrollableControl(ScrollableStackPanel ssp) => ssp.MainControl;

		public ScrollableStackPanel()
		{
			this.MainControl = new ScrollableControl();
			this.Controls = new List<Control>();
		}
		public void AddControl(Control Cntrl, int position = int.MaxValue)
		{
			UpToDateRendered = false;
		}
		public virtual Control GetControl(int Index)
		{
			return Controls[Index];
		}
		public void MoveControl(int OldIndex, int NewIndex)
		{
			if (OldIndex == NewIndex) return;
			if (OldIndex >= NumberOfControls || NewIndex >= NumberOfControls || OldIndex < 0 || NewIndex > 0) throw new IndexOutOfRangeException();
			UpToDateRendered = false;


		}
		public  void Render()
		{

			UpToDateRendered = true;
		}

	}
}
