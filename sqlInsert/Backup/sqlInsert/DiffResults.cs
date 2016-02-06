using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DifferenceEngine;
using System.Collections;

namespace SQLInsert
{
    public partial class DiffResults : Form
    {
        public DiffResults()
        {
            InitializeComponent();
        }
        public DiffResults(DiffList_TextFile source, DiffList_TextFile destination, ArrayList DiffLines, double seconds)
		{
			InitializeComponent();
			this.Text = string.Format("Results: {0} secs.",seconds.ToString("#0.00"));

			ListViewItem lviS;
			ListViewItem lviD;
			int cnt = 1;
			int i;

			foreach (DiffResultSpan drs in DiffLines)
			{				
				switch (drs.Status)
				{
					case DiffResultSpanStatus.DeleteSource:
						for (i = 0; i < drs.Length; i++)
						{
							lviS = new ListViewItem(cnt.ToString("00000"));
							lviD = new ListViewItem(cnt.ToString("00000"));
							lviS.BackColor = Color.Red;
							lviS.SubItems.Add(((TextLine)source.GetByIndex(drs.SourceIndex + i)).Line);
							lviD.BackColor = Color.LightGray;
							lviD.SubItems.Add("");

							lvSource.Items.Add(lviS);
							lvDestination.Items.Add(lviD);
							cnt++;
						}
						
						break;
					case DiffResultSpanStatus.NoChange:
						for (i = 0; i < drs.Length; i++)
						{
							lviS = new ListViewItem(cnt.ToString("00000"));
							lviD = new ListViewItem(cnt.ToString("00000"));
							lviS.BackColor = Color.White;
							lviS.SubItems.Add(((TextLine)source.GetByIndex(drs.SourceIndex+i)).Line);
							lviD.BackColor = Color.White;
							lviD.SubItems.Add(((TextLine)destination.GetByIndex(drs.DestIndex+i)).Line);

							lvSource.Items.Add(lviS);
							lvDestination.Items.Add(lviD);
							cnt++;
						}
						
						break;
					case DiffResultSpanStatus.AddDestination:
						for (i = 0; i < drs.Length; i++)
						{
							lviS = new ListViewItem(cnt.ToString("00000"));
							lviD = new ListViewItem(cnt.ToString("00000"));
							lviS.BackColor = Color.LightGray;
							lviS.SubItems.Add("");
							lviD.BackColor = Color.LightGreen;
							lviD.SubItems.Add(((TextLine)destination.GetByIndex(drs.DestIndex+i)).Line);

							lvSource.Items.Add(lviS);
							lvDestination.Items.Add(lviD);
							cnt++;
						}
						
						break;
					case DiffResultSpanStatus.Replace:
						for (i = 0; i < drs.Length; i++)
						{
							lviS = new ListViewItem(cnt.ToString("00000"));
							lviD = new ListViewItem(cnt.ToString("00000"));
							lviS.BackColor = Color.Red;
							lviS.SubItems.Add(((TextLine)source.GetByIndex(drs.SourceIndex+i)).Line);
							lviD.BackColor = Color.LightGreen;
							lviD.SubItems.Add(((TextLine)destination.GetByIndex(drs.DestIndex+i)).Line);

							lvSource.Items.Add(lviS);
							lvDestination.Items.Add(lviD);
							cnt++;
						}
						
						break;
				}
				
			}
		}

    }
}