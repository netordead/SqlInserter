using System;
using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data;
using SQLObjects;
using WeifenLuo.WinFormsUI;
using log4net;

namespace SQLInsert
{
	/// <summary>
	/// Summary description for BaseForm.
	/// </summary>
	public class BaseForm : DockContent
	{
		public System.Windows.Forms.Label testLabel;
		Pen statusPen = new Pen(Color.Red);
		Pen okPen = new Pen(Color.Red);

		Pen bgPen = new Pen(Color.Silver);
		SolidBrush statusBrushFontBrush = new SolidBrush(Color.Red);
		SolidBrush okBrushFontBrush = new SolidBrush(Color.Green);
		
		SolidBrush bgBrush = new SolidBrush(Color.Silver);
		public System.Windows.Forms.StatusBarPanel msgPanel;
		public System.Windows.Forms.StatusBar statBar;

		public BaseForm()
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.testLabel = new System.Windows.Forms.Label();
			testLabel.Text = "test";
			this.statBar = new System.Windows.Forms.StatusBar();
			this.msgPanel = new System.Windows.Forms.StatusBarPanel();
			((System.ComponentModel.ISupportInitialize)(this.msgPanel)).BeginInit();
			this.SuspendLayout();
			this.statBar.Location = new System.Drawing.Point(0, 400);
			this.statBar.Name = "statBar";
			this.statBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																					   this.msgPanel
																					   });

			this.statBar.ShowPanels = true;
			this.statBar.Size = new System.Drawing.Size(536, 22);
			this.statBar.TabIndex = 0;
			this.statBar.DrawItem += new System.Windows.Forms.StatusBarDrawItemEventHandler(this.statBar_DrawItem);
			this.Controls.Add(this.statBar);
			// 
			// msgPanel
			// 
			this.msgPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.msgPanel.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw;
			this.msgPanel.Width = 341;
			this.Controls.Add(this.statBar);
			((System.ComponentModel.ISupportInitialize)(this.msgPanel)).EndInit();
			this.ResumeLayout(false);
		}
		public void WriteSuccess( string msg)
		{
			this.statBar.Text = msg;
			this.statBar.ForeColor = Color.Green;
			statBar.Invalidate();
		}

		/// <summary>
		/// inform user about errot
		/// </summary>
		/// <param name="msg"></param>
		public void WriteError( string msg)
		{
			this.statBar.Text = msg;
			this.statBar.ForeColor = Color.Red;
			statBar.Invalidate();
		}

		public void statBar_DrawItem(object sender, StatusBarDrawItemEventArgs sbdevent)
		{
			Graphics g = sbdevent.Graphics;
			StatusBar sb = (StatusBar)sender;
			RectangleF rectf = new RectangleF(sbdevent.Bounds.X, sbdevent.Bounds.Y, sbdevent.Bounds.Width, sbdevent.Bounds.Height);
			bgBrush.Color = sb.BackColor;
			statusPen.Color = sb.BackColor ;
			g.DrawRectangle(this.statusPen, sbdevent.Bounds);
			sbdevent.Graphics.FillRectangle(this.bgBrush , sbdevent.Bounds);
			statusBrushFontBrush.Color = sb.ForeColor;
			g.DrawString( sb.Text , sb.Font, this.statusBrushFontBrush, rectf);
		}

	}
}
