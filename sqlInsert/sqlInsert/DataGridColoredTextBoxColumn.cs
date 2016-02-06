using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SQLInsert
{
	public class DataGridColoredTextBoxColumn : DataGridTextBoxColumn 
 
	{ 
 
		protected override void Paint(System.Drawing.Graphics g, 
 
			System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager 
 
			source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush 
 
			foreBrush, bool alignToRight) 
 
		{ 
 
			// the idea is to conditionally set the foreBrush and/or backbrush 
			// depending upon some crireria on the cell value 
 
			try
			{ 
 
				object o = this.GetColumnValueAtRow(source, rowNum); 
 
				if( o!= null) 
 
				{

                    if (o.GetType() != typeof(int)) return;
					int _val = Convert.ToInt32(o);

					if(_val==0)
					{
 
						backBrush = new SolidBrush(Color.White); 
						foreBrush = new SolidBrush(Color.Black); 
					}
					else if(_val==1)
					{
 
						backBrush = new SolidBrush(Color.Red); 
						foreBrush = new SolidBrush(Color.Black); 
					}
					else if(_val==2)
					{
 
						backBrush = new SolidBrush(Color.Green); 
						foreBrush = new SolidBrush(Color.Black); 
					}
					else if(_val==3)
					{
 
						backBrush = new SolidBrush(Color.Pink); 
						foreBrush = new SolidBrush(Color.Black); 
					}
				} 
 
			} 
 
			catch{


                throw;
            } 
 
			finally
			{ 
 
				// make sure the base class gets called to do the drawing with 
 
				// the possibly changed brushes 
 
				base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight); 
 
			} 
 
		} 
 
	} 
 

}
