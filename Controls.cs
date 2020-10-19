using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Boost
{
	public class Controls
	{
		public static Panel ToVerticalLinedView(IList<Control> Controls, int space = 5)
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
				Controls[i].Location = new Point(0, CurrentHeight + ((i == 0 || i == Controls.Count - 1) ? 0 : 2 * space)); 
				// Добавление на панель
				Out.Controls.Add(Controls[i]);
				// Учет старого элемента для определения позиции нового
				CurrentHeight += Controls[i].Height;
				// Саммый длинный элемент определяет длинну панели
				if (Controls[i].Width > OutWid) OutWid = Controls[i].Width;
			}
			Out.Height = CurrentHeight;
			Out.Width = OutWid;
			Out.BackColor = Color.Transparent;
			return Out;
		}
		public static Panel ToHorizontalLinedView(IList<Control> Controls, int space = 5)
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
				Controls[i].Location = new Point(CurrentWidth + ((i == 0 || i == Controls.Count - 1) ? 0 : 2 * space),0);
				// Добавление на панель
				Out.Controls.Add(Controls[i]);
				// Учет старого элемента для определения позиции нового
				CurrentWidth += Controls[i].Width;
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
			int MaxWidth=0;
			for (int i = 0; i < Controls.Count; i++) if (Controls[i].Width > MaxWidth) MaxWidth = Controls[i].Width;
			for (int i = 0; i < Controls.Count; i++) Controls[i].Width = MaxWidth;
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
				return ToHorizontalLinedView(new Control[] { label, EqualsSymb, textbox });
			}
		}

	}
}
