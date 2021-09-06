using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.Primitives;

namespace Boost
{
	public partial class Controls
	{
		public class CheckTextBoxPanel
		{
			private readonly Panel MainPanel;
			private readonly CheckBox CheckBox;
			private readonly RadioButton RadioButton;
			private readonly TextBox TextBox;

			public enum BUTTON_TYPE
			{
				CHECK,
				RADIO
			}
			private BUTTON_TYPE _ButtonType;
			public BUTTON_TYPE ButtonType
			{
				get => this._ButtonType;
				set
				{
					if (value != _ButtonType)
					{
						if (value == BUTTON_TYPE.CHECK)
						{
							this.CheckBox.Visible = true;
							this.RadioButton.Visible = false;
							_ButtonType = value;
						}

						if (value == BUTTON_TYPE.RADIO)
						{
							this.CheckBox.Visible = false;
							this.RadioButton.Visible = true;
							_ButtonType = value;
						}
					}
				}
			}

			public bool Checked
			{
				get
				{
					if (this.ButtonType == BUTTON_TYPE.CHECK) return this.CheckBox.Checked;
					if (this.ButtonType == BUTTON_TYPE.RADIO) return this.RadioButton.Checked;
					return false; // ??
				}
			}
			public string Text
			{
				get => this.TextBox.Text;
				set => this.TextBox.Text = value;
			}

			public Panel AsControl =>this.MainPanel;


			public CheckTextBoxPanel(int TextBoxWidth = 100)
			{
				this.TextBox = new TextBox { Width = TextBoxWidth, Location = new Point(Consts.CheckBoxWidth, 0) };
				this.CheckBox = new CheckBox { Width = Consts.CheckBoxWidth, Height = this.TextBox.Height, Text = string.Empty };
				this.RadioButton = new RadioButton {Width = Consts.CheckBoxWidth, Height = this.TextBox.Height, Text = string.Empty};
				this.MainPanel = new Panel { AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };

				this.MainPanel.Controls.Add(this.CheckBox);
				this.MainPanel.Controls.Add(this.RadioButton);
				this.MainPanel.Controls.Add(this.TextBox);
			}
		}
	}
}