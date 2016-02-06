using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;


namespace SQLInsert
{


	// FireEventArgs: a custom event inherited from EventArgs.

	public class ChangedOrderArgs: EventArgs 
	{
		public ChangedOrderArgs(int from, int to) 
		{
			this.From = from;
			this.To = to;
		}


		public int To;
		public int From;

	}   


	#region derived datagrid
	public class MatGrid : DataGrid
	{
		private int mouseDownRow;
		private int currentMouseRow;
		private bool movingMouseDown;
		private int oldY;
		public DataTable _dataTable;

		private bool okToDrag = false;

		public delegate void ChangedOrderHandler(object sender, ChangedOrderArgs oa);

		public event ChangedOrderHandler ChangedOrderEvent;


		
		#region mouse overrides		
		protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
		{
			if(okToDrag)
			{
				DataGrid.HitTestInfo hti = this.HitTest(e.X, e.Y);
				if(hti.Row != currentMouseRow)
				{
					if(movingMouseDown)
						EraseLine(oldY);
					Console.WriteLine("MouseMove " + hti.Row.ToString());
					currentMouseRow = hti.Row;

					DrawLine(e.Y);
					oldY = e.Y;
					movingMouseDown = true;
					
				}
				return; //no baseclass call
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			DataGrid.HitTestInfo hti = this.HitTest(e.X, e.Y);
			if(hti.Type == DataGrid.HitTestType.RowResize)
			{
				okToDrag = false;
				return;
			}

			okToDrag = hti.Type == DataGrid.HitTestType.RowHeader && e.Button == MouseButtons.Left;
			mouseDownRow = hti.Row;
			currentMouseRow = hti.Row;
			movingMouseDown = false;

			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
		{
			if(okToDrag)
			{
				EraseLine(oldY);
				Console.WriteLine("Dropped " + currentMouseRow.ToString());
				MoveRowFromTo(mouseDownRow, currentMouseRow);
			}
			else
				base.OnMouseUp(e);
			okToDrag = false;
		}
		#endregion

		#region drawing lines
		private void EraseLine(int row)
		{
			this.Refresh();
		}
		private void DrawLine(int row)
		{
			Graphics g = Graphics.FromHwnd(this.Handle); 
			Pen pen = new Pen(Color.Red, 2); 
			g.DrawLine(pen, 0, row, this.Size.Width , row); 
			pen.Dispose();
			g.Dispose();
		}
		#endregion



		private void MoveRowFromTo(int fromRow, int toRow)
		{
			if(fromRow == toRow)return;

			
			System.Diagnostics.Debug.WriteLine(  "move from " + fromRow.ToString() + " to " + toRow.ToString());
			
			if(fromRow > toRow)
			{
				ChangedOrderEvent(this,new ChangedOrderArgs(fromRow  ,toRow + 1));
			}
			else
			{
				ChangedOrderEvent(this,new ChangedOrderArgs(fromRow  ,toRow -1 ));
			}

		}

	}
	#endregion
}
