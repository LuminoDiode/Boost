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
		/// Панель, размещающая прямоугольные элементы максимально компактно.
		/// </summary>
		public class StackPanel : IStackPanel
		{
			private bool[][] PointIsFree;
			private Size _PanelSize;
			public Size PanelSize
			{
				get => _PanelSize;
				set
				{
					if (value != this._PanelSize){
						this._PanelSize = value;
						this.Relocate();
					}
				}
			}
			public StackPanel(Size PanelSize)
			{
				this.PointIsFree = Boost.Matrix.Generate<bool>(PanelSize.Width, PanelSize.Height);

			}
			private void Relocate()
			{
				var AllControls = Boost.Controls.ControlCollectionToArray(this.Controls).OrderBy(x => x.Width + x.Height);
				Stack<Control> ControlsToLocateOrderedBySize = new Stack<Control>(AllControls);
				
				
			}
			private bool CanInsertInPoint(Point Pt, Size CtrlSize)
			{
				return (Matrix.GetSubMatrix(this.PointIsFree, Pt, Point.Add(Pt, CtrlSize)).All(x => x.All(y => y == true))) ;
			}
			private void SetBitmapToTrueAfterControlInsertion(Point Pt, Size CtrlSize)
			{
				Matrix.SetSubMatrix(this.PointIsFree, Pt, Point.Add(Pt, CtrlSize),true);
			}
			public Point FindPointToInsert(Size CtrlSize)
			{
				for (int raw = 0; raw < this.PointIsFree.Length; raw++)
					for (int col = 0; col < this.PointIsFree[raw].Length; col++)
						if (CanInsertInPoint(new Point(raw, col), CtrlSize)) return new Point(raw, col);
				throw new System.AggregateException("No enough space found to insert control with size of " + CtrlSize.ToString());
			}
		}
	}
}