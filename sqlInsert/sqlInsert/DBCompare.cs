using System.Data;
using System.Drawing;
using SQLObjects;
using System.Windows.Forms;
using log4net;
using System.Collections;
using DifferenceEngine;
namespace SQLInsert
{
	/// <summary>
	/// Summary description for DummyPropertyGrid.
	/// </summary>
	public class DBCompare : DBConnection
	{
		private System.Windows.Forms.DataGrid dtgDataBaseDiff;
		private System.Windows.Forms.GroupBox grpCompare;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private DataGridColoredTextBoxColumn dataGridTextBoxColumn2;
		private System.Windows.Forms.Label lblLeftDatabase;
		private System.Windows.Forms.Label lblRightDatabase;
		private System.Windows.Forms.Button btnCompare;
        private Label lblTablesDiffer;

        private static readonly ILog log = LogManager.GetLogger(typeof(TableForm));


		public DBCompare():base()
		{
			
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DBCompare));
            this.btnCompare = new System.Windows.Forms.Button();
            this.dtgDataBaseDiff = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new SQLInsert.DataGridColoredTextBoxColumn();
            this.grpCompare = new System.Windows.Forms.GroupBox();
            this.lblRightDatabase = new System.Windows.Forms.Label();
            this.lblLeftDatabase = new System.Windows.Forms.Label();
            this.lblTablesDiffer = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.msgPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDataBaseDiff)).BeginInit();
            this.grpCompare.SuspendLayout();
            this.SuspendLayout();
            // 
            // statBar
            // 
            this.statBar.Location = new System.Drawing.Point(0, 542);
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(8, 16);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(232, 23);
            this.btnCompare.TabIndex = 34;
            this.btnCompare.Text = "Compare (takes some time...)";
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // dtgDataBaseDiff
            // 
            this.dtgDataBaseDiff.DataMember = "";
            this.dtgDataBaseDiff.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dtgDataBaseDiff.Location = new System.Drawing.Point(8, 48);
            this.dtgDataBaseDiff.Name = "dtgDataBaseDiff";
            this.dtgDataBaseDiff.Size = new System.Drawing.Size(544, 152);
            this.dtgDataBaseDiff.TabIndex = 35;
            this.dtgDataBaseDiff.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            this.dtgDataBaseDiff.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgDataBaseDiff_MouseUp);
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.dtgDataBaseDiff;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn2});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Table name";
            this.dataGridTextBoxColumn1.MappingName = "Name";
            this.dataGridTextBoxColumn1.Width = 75;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.MappingName = "StateTable";
            this.dataGridTextBoxColumn2.Width = 75;
            // 
            // grpCompare
            // 
            this.grpCompare.Controls.Add(this.lblTablesDiffer);
            this.grpCompare.Controls.Add(this.lblRightDatabase);
            this.grpCompare.Controls.Add(this.lblLeftDatabase);
            this.grpCompare.Controls.Add(this.btnCompare);
            this.grpCompare.Controls.Add(this.dtgDataBaseDiff);
            this.grpCompare.Location = new System.Drawing.Point(8, 336);
            this.grpCompare.Name = "grpCompare";
            this.grpCompare.Size = new System.Drawing.Size(888, 208);
            this.grpCompare.TabIndex = 36;
            this.grpCompare.TabStop = false;
            this.grpCompare.Text = "Compare result";
            // 
            // lblRightDatabase
            // 
            this.lblRightDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lblRightDatabase.Location = new System.Drawing.Point(616, 72);
            this.lblRightDatabase.Name = "lblRightDatabase";
            this.lblRightDatabase.Size = new System.Drawing.Size(176, 16);
            this.lblRightDatabase.TabIndex = 38;
            // 
            // lblLeftDatabase
            // 
            this.lblLeftDatabase.ForeColor = System.Drawing.Color.Red;
            this.lblLeftDatabase.Location = new System.Drawing.Point(616, 48);
            this.lblLeftDatabase.Name = "lblLeftDatabase";
            this.lblLeftDatabase.Size = new System.Drawing.Size(176, 16);
            this.lblLeftDatabase.TabIndex = 37;
            // 
            // lblTablesDiffer
            // 
            this.lblTablesDiffer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblTablesDiffer.Location = new System.Drawing.Point(616, 101);
            this.lblTablesDiffer.Name = "lblTablesDiffer";
            this.lblTablesDiffer.Size = new System.Drawing.Size(176, 16);
            this.lblTablesDiffer.TabIndex = 39;
            this.lblTablesDiffer.Text = "Tables differ";
            // 
            // DBCompare
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(904, 566);
            this.Controls.Add(this.grpCompare);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DBCompare";
            this.Text = "Compare Databases";
            this.Resize += new System.EventHandler(this.DBCompare_Resize);
            this.Controls.SetChildIndex(this.statBar, 0);
            this.Controls.SetChildIndex(this.grpCompare, 0);
            ((System.ComponentModel.ISupportInitialize)(this.msgPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDataBaseDiff)).EndInit();
            this.grpCompare.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion



		private void btnCompare_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.lblLeftDatabase.Text = " Table exists only in " + Global.SelectedDB;
				this.lblRightDatabase.Text = " Table exists only in " + Global.SelectedDBCompare ;

				Compare comparer = new Compare(Global.Serv1.Databases[Global.SelectedDB],Global.Serv1.Databases[Global.SelectedDBCompare]);
				DataTable compareTable = comparer.Overview;

                DataView vuCompare = new DataView(compareTable, "", "Name", DataViewRowState.CurrentRows);
				this.dtgDataBaseDiff.DataSource = vuCompare;
				statBar.Text = "";
			}
			catch(System.Exception ex)
			{
				statBar.Text = ex.ToString();
			}
		}


		private void DBCompare_Resize(object sender, System.EventArgs e)
		{
			try
			{
				this.grpCompare.Height =  this.Height -350;
				this.grpCompare.Width =  this.Width -40;

				this.dtgDataBaseDiff.Width  = grpCompare.Width - 280;
				this.dtgDataBaseDiff.Height  = grpCompare.Height -60;
			
				dtgDataBaseDiff.TableStyles[0].GridColumnStyles[0].Width = ( this.dtgDataBaseDiff.Width - 60)  /2;
				dtgDataBaseDiff.TableStyles[0].GridColumnStyles[1].Width = ( this.dtgDataBaseDiff.Width -60) /2;

				this.lblLeftDatabase.Location  = new Point(this.dtgDataBaseDiff.Location.X + dtgDataBaseDiff.Width + 20 ,this.lblLeftDatabase.Location.Y );
                this.lblRightDatabase.Location = new Point(lblLeftDatabase.Location.X, this.lblLeftDatabase.Location.Y + 40 );
                this.lblTablesDiffer.Location = new Point(lblLeftDatabase.Location.X, this.lblLeftDatabase.Location.Y + 80);
			}
			catch(System.Exception ex)
			{
				statBar.Text = ex.ToString();
			}

		}

		private void btnCompareDetail_Click(object sender, System.EventArgs e)
		{
		
		}

        private void dtgDataBaseDiff_Navigate(object sender, System.Windows.Forms.NavigateEventArgs ne)
        {

        }

      

        private void dtgDataBaseDiff_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            try
            {
                log.Debug("dtgTables_MouseUp");
                DataGrid.HitTestInfo hti = this.dtgDataBaseDiff.HitTest(e.X, e.Y);
                string selectedTable = "";

                if (hti.Type == DataGrid.HitTestType.Cell &&(hti.Column == 0 || hti.Column == 1))
                {
                    selectedTable = (string)this.dtgDataBaseDiff[hti.Row, 0];
                    log.Debug("SelectedTable: " + selectedTable);

                    Compare comparer = new Compare(Global.Serv1.Databases[Global.SelectedDB], Global.Serv1.Databases[Global.SelectedDBCompare]);
                    ArrayList rep = new ArrayList();
                    DiffList_TextFile sourceText = null;
                    DiffList_TextFile targetText = null;

                    comparer.GetTableDiffReport(selectedTable,out sourceText,out targetText,out rep);

                    DiffResults results = new DiffResults(sourceText, targetText, rep, 0);
                    results.ShowDialog();
                    results.Dispose();
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
            }
       
        }






	}
}
