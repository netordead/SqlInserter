using System;
using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data;
using SQLObjects;
using WeifenLuo.WinFormsUI;
using log4net;

using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace SQLInsert
{
	/// <summary>
	/// Summary description for DummyPropertyGrid.
	/// </summary>
	public class TableForm : BaseForm
	{
		public delegate void ScriptHandler(object sender, ScriptArgs ar);
		public event ScriptHandler ScriptEvent;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtTop;
		private System.Windows.Forms.TextBox txtFilter;
		private SQLInsert.MatGrid dtgTables;
		private System.Windows.Forms.CheckedListBox chkCols;

		private string SelectedTable;

		private System.Windows.Forms.Button btnCreate;
		private System.Windows.Forms.Button btnAll;
		private System.Windows.Forms.Button btnUnselect;
		private System.Windows.Forms.GroupBox grpTable;
		private System.Windows.Forms.GroupBox grpFilter;
		private System.Windows.Forms.Label lblCol;
		private System.Windows.Forms.Label lblTable;
		private System.Windows.Forms.Button btnOrder;


		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
		private System.Windows.Forms.DataGridBoolColumn Script;
		private System.Windows.Forms.DataGridTextBoxColumn TableName;
		private static readonly ILog log = LogManager.GetLogger(typeof(TableForm));

		public TableForm()
		{

			InitializeComponent();


		}

        /// <summary>
        /// Loads Tables from database into UI
        /// </summary>
        /// <param name="mode"></param>
		public void SetUIFromSettings(Global.RefreshMode mode)
		{
			//SqlTableCollection tabs = Global.Serv.Databases[Global.SelectedDB].Tables;
            TableCollection tabs = Global.Serv1.Databases[Global.SelectedDB].Tables;

			//oh what a name!
			// DataTable that contains a list of SqlServer Tables

			DataTable _tablesTable = new DataTable();
			_tablesTable.Columns.Add(new DataColumn("OrderID",typeof(int)));
			_tablesTable.Columns.Add(new DataColumn("TableName",typeof(string)));
			_tablesTable.Columns.Add(new DataColumn("Script",typeof(bool)));
			
			int _rowcounter = 0;
			foreach(Table tab in tabs)
			{
				if(!tab.IsSystemObject)
				{
					_rowcounter ++;

					DataRow _row  = _tablesTable.NewRow();
					if(Global.DBSetting.GetTblSetting(tab.Name,true).OrderID == 0)
					{
						Global.DBSetting.GetTblSetting(tab.Name,true).OrderID = _rowcounter;
					}
					_row["OrderID"]= Global.DBSetting.GetTblSetting(tab.Name,true).OrderID;
					_row["TableName"] = tab.Name;
					_row["Script"]= Global.DBSetting.GetTblSetting(tab.Name,true).Script;
					_tablesTable.Rows.Add(_row);
				}
			}

			_tablesTable.TableName = "Tables";
			DataView vu = _tablesTable.DefaultView;
			vu.Sort = "OrderID";
			_tablesTable.Columns["OrderID"].ColumnMapping = MappingType.Hidden; 

			this.dtgTables.DataSource = _tablesTable;
		}

		


		

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TableForm));
			this.lblCol = new System.Windows.Forms.Label();
			this.lblTable = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtTop = new System.Windows.Forms.TextBox();
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.dtgTables = new SQLInsert.MatGrid();
			this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
			this.TableName = new System.Windows.Forms.DataGridTextBoxColumn();
			this.Script = new System.Windows.Forms.DataGridBoolColumn();
			this.chkCols = new System.Windows.Forms.CheckedListBox();
			this.btnCreate = new System.Windows.Forms.Button();
			this.btnAll = new System.Windows.Forms.Button();
			this.btnUnselect = new System.Windows.Forms.Button();
			this.grpTable = new System.Windows.Forms.GroupBox();
			this.btnOrder = new System.Windows.Forms.Button();
			this.grpFilter = new System.Windows.Forms.GroupBox();
			((System.ComponentModel.ISupportInitialize)(this.msgPanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dtgTables)).BeginInit();
			this.grpTable.SuspendLayout();
			this.grpFilter.SuspendLayout();
			this.SuspendLayout();
			// 
			// testLabel
			// 
			this.testLabel.Name = "testLabel";
			// 
			// msgPanel
			// 
			this.msgPanel.Width = 1016;
			// 
			// statBar
			// 
			this.statBar.Location = new System.Drawing.Point(0, 622);
			this.statBar.Name = "statBar";
			this.statBar.Size = new System.Drawing.Size(1032, 22);
			// 
			// lblCol
			// 
			this.lblCol.Location = new System.Drawing.Point(328, 24);
			this.lblCol.Name = "lblCol";
			this.lblCol.Size = new System.Drawing.Size(72, 20);
			this.lblCol.TabIndex = 50;
			this.lblCol.Text = "Columns";
			// 
			// lblTable
			// 
			this.lblTable.Location = new System.Drawing.Point(16, 24);
			this.lblTable.Name = "lblTable";
			this.lblTable.Size = new System.Drawing.Size(40, 20);
			this.lblTable.TabIndex = 49;
			this.lblTable.Text = "Table";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(24, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 24);
			this.label4.TabIndex = 46;
			this.label4.Text = "Top x (ie 10)";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(232, 16);
			this.label3.TabIndex = 45;
			this.label3.Text = "WHERE Clause (ie CategoryID = 1)";
			// 
			// txtTop
			// 
			this.txtTop.Location = new System.Drawing.Point(128, 112);
			this.txtTop.Name = "txtTop";
			this.txtTop.Size = new System.Drawing.Size(60, 20);
			this.txtTop.TabIndex = 44;
			this.txtTop.Text = "";
			this.txtTop.TextChanged += new System.EventHandler(this.txtTop_TextChanged);
			// 
			// txtFilter
			// 
			this.txtFilter.Location = new System.Drawing.Point(24, 48);
			this.txtFilter.Multiline = true;
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtFilter.Size = new System.Drawing.Size(760, 56);
			this.txtFilter.TabIndex = 43;
			this.txtFilter.Text = "";
			this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
			// 
			// dtgTables
			// 
			this.dtgTables.AllowSorting = false;
			this.dtgTables.DataMember = "";
			this.dtgTables.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dtgTables.Location = new System.Drawing.Point(8, 48);
			this.dtgTables.Name = "dtgTables";
			this.dtgTables.Size = new System.Drawing.Size(208, 288);
			this.dtgTables.TabIndex = 39;
			this.dtgTables.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
																								  this.dataGridTableStyle1});
			this.dtgTables.ChangedOrderEvent += new SQLInsert.MatGrid.ChangedOrderHandler(this.ChangeOrder);
			this.dtgTables.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dtgTables_MouseUp);
			// 
			// dataGridTableStyle1
			// 
			this.dataGridTableStyle1.DataGrid = this.dtgTables;
			this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
																												  this.TableName,
																												  this.Script});
			this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.dataGridTableStyle1.MappingName = "Tables";
			this.dataGridTableStyle1.PreferredColumnWidth = 300;
			// 
			// TableName
			// 
			this.TableName.Format = "";
			this.TableName.FormatInfo = null;
			this.TableName.HeaderText = "Table";
			this.TableName.MappingName = "TableName";
			this.TableName.ReadOnly = true;
			this.TableName.Width = 50;
			// 
			// Script
			// 
			this.Script.FalseValue = false;
			this.Script.HeaderText = "Script ? ";
			this.Script.MappingName = "Script";
			this.Script.NullValue = ((object)(resources.GetObject("Script.NullValue")));
			this.Script.TrueValue = true;
			this.Script.Width = 50;
			// 
			// chkCols
			// 
			this.chkCols.BackColor = System.Drawing.Color.WhiteSmoke;
			this.chkCols.CheckOnClick = true;
			this.chkCols.Location = new System.Drawing.Point(240, 48);
			this.chkCols.Name = "chkCols";
			this.chkCols.Size = new System.Drawing.Size(164, 289);
			this.chkCols.TabIndex = 42;
			this.chkCols.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkCols_ItemCheck);
			// 
			// btnCreate
			// 
			this.btnCreate.Location = new System.Drawing.Point(16, 8);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.Size = new System.Drawing.Size(156, 23);
			this.btnCreate.TabIndex = 61;
			this.btnCreate.Text = "Script";
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// btnAll
			// 
			this.btnAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAll.BackgroundImage")));
			this.btnAll.Location = new System.Drawing.Point(64, 24);
			this.btnAll.Name = "btnAll";
			this.btnAll.Size = new System.Drawing.Size(20, 20);
			this.btnAll.TabIndex = 64;
			this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
			// 
			// btnUnselect
			// 
			this.btnUnselect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnUnselect.BackgroundImage")));
			this.btnUnselect.Location = new System.Drawing.Point(88, 24);
			this.btnUnselect.Name = "btnUnselect";
			this.btnUnselect.Size = new System.Drawing.Size(20, 20);
			this.btnUnselect.TabIndex = 63;
			this.btnUnselect.Click += new System.EventHandler(this.btnUnselect_Click);
			// 
			// grpTable
			// 
			this.grpTable.Controls.Add(this.btnOrder);
			this.grpTable.Controls.Add(this.lblTable);
			this.grpTable.Controls.Add(this.dtgTables);
			this.grpTable.Controls.Add(this.chkCols);
			this.grpTable.Controls.Add(this.btnAll);
			this.grpTable.Controls.Add(this.btnUnselect);
			this.grpTable.Controls.Add(this.lblCol);
			this.grpTable.Location = new System.Drawing.Point(16, 192);
			this.grpTable.Name = "grpTable";
			this.grpTable.Size = new System.Drawing.Size(448, 360);
			this.grpTable.TabIndex = 65;
			this.grpTable.TabStop = false;
			this.grpTable.Text = "Table and Column to Script";
			// 
			// btnOrder
			// 
			this.btnOrder.Location = new System.Drawing.Point(120, 24);
			this.btnOrder.Name = "btnOrder";
			this.btnOrder.Size = new System.Drawing.Size(136, 23);
			this.btnOrder.TabIndex = 69;
			this.btnOrder.Text = "Order by Dependency";
			this.btnOrder.Click += new System.EventHandler(this.btnOrder_Click);
			// 
			// grpFilter
			// 
			this.grpFilter.Controls.Add(this.label4);
			this.grpFilter.Controls.Add(this.label3);
			this.grpFilter.Controls.Add(this.txtTop);
			this.grpFilter.Controls.Add(this.txtFilter);
			this.grpFilter.Location = new System.Drawing.Point(16, 32);
			this.grpFilter.Name = "grpFilter";
			this.grpFilter.Size = new System.Drawing.Size(928, 144);
			this.grpFilter.TabIndex = 67;
			this.grpFilter.TabStop = false;
			this.grpFilter.Text = "Optional: Limit scripted Data to Filter";
			// 
			// TableForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(1032, 646);
			this.Controls.Add(this.grpFilter);
			this.Controls.Add(this.grpTable);
			this.Controls.Add(this.btnCreate);
			this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft) 
				| WeifenLuo.WinFormsUI.DockAreas.DockRight) 
				| WeifenLuo.WinFormsUI.DockAreas.DockTop) 
				| WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
			this.DockPadding.Bottom = 2;
			this.DockPadding.Top = 2;
			this.HideOnClose = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TableForm";
			this.ShowHint = WeifenLuo.WinFormsUI.DockState.DockTopAutoHide;
			this.Text = "Table Form";
			this.ToolTipText = "Choose which Tables and Columns to script";
			this.Resize += new System.EventHandler(this.TableForm_Resize);
			this.Controls.SetChildIndex(this.btnCreate, 0);
			this.Controls.SetChildIndex(this.grpTable, 0);
			this.Controls.SetChildIndex(this.grpFilter, 0);
			this.Controls.SetChildIndex(this.statBar, 0);
			((System.ComponentModel.ISupportInitialize)(this.msgPanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dtgTables)).EndInit();
			this.grpTable.ResumeLayout(false);
			this.grpFilter.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private void btnCreate_Click(object sender, System.EventArgs e)
		{
			if( Global.IsConnected)
			{
				ScriptEvent(this,new ScriptArgs(true));
			}
			else
			{
				this.statBar.Text = "Not connected to a Database";	
			}
		}


		private void chkCols_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			SetColumnScripting( SelectedTable ,chkCols.Items[e.Index].ToString(),e.NewValue == CheckState.Checked);
		}
		private void SetColumnScripting(string table, string column, bool script )
		{
			TableSetting _tblSettings = Global.DBSetting.GetTblSetting( table ,true);
			_tblSettings.GetColSetting( column ,true).Script = script;
		}

		private void ChangeOrder(object sender,  ChangedOrderArgs e)
		{
			TableSetting _settingMoved =Global.DBSetting.GetTblSetting(e.From + 1 );
			if(e.From > e.To)
			{
				Global.DBSetting.MoveRestDown(e.To + 1 , e.From + 1 );
				_settingMoved.OrderID = e.To + 1 ;
			}
			if(e.From < e.To)
			{
				Global.DBSetting.MoveRestUp(e.To + 1 , e.From + 1 );
				_settingMoved.OrderID = e.To + 1  ;
			}

			SetUIFromSettings(Global.RefreshMode.DataBase);
		}

		private void LoadTableSettings(string tableName )
		{
			TableSetting _tblSettings = Global.DBSetting.GetTblSetting(tableName,true);
			
			if(_tblSettings.Top==-1)
			{
				this.txtTop.Text = "";
			}
			else
			{
				this.txtTop.Text = _tblSettings.Top.ToString();
			}

			if(_tblSettings.Filter=="")
			{
				this.txtFilter.Text = "";
			}
			else
			{
				this.txtFilter.Text = _tblSettings.Filter;
			}
			//SqlAdmin.SqlColumnCollection cols = Global.Serv.Databases[Global.DBSetting.Name].Tables[tableName].Columns;
            if (Global.Serv1.Databases[Global.DBSetting.Name].Tables.Contains(tableName))
            {
                ColumnCollection cols = Global.Serv1.Databases[Global.DBSetting.Name].Tables[tableName].Columns;
                chkCols.Items.Clear();

                foreach (Column col in cols)
                {
                    this.chkCols.Items.Add(col.Name, _tblSettings.GetColSetting(col.Name, true).Script);
                }
            }
            else
            {
                foreach (Table table in Global.Serv1.Databases[Global.DBSetting.Name].Tables)
                {
                    if (table.Name == tableName)
                    {
                        ColumnCollection cols = table.Columns;
                        chkCols.Items.Clear();
                        foreach (Column col in cols)
                        {
                            this.chkCols.Items.Add(col.Name, _tblSettings.GetColSetting(col.Name, true).Script);
                        }
                    }
                }
            }

		}

		private void dtgTables_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e) 
		{ 

			try 
			{ 
				log.Debug("dtgTables_MouseUp");
				DataGrid.HitTestInfo hti = this.dtgTables.HitTest(e.X, e.Y); 
				
				//order by name
				if( hti.Type == DataGrid.HitTestType.ColumnHeader && 
					hti.Column == 0 )
				{
					log.Debug("Hit first Header");
					string orderDirection = Global.DBSetting.OrderDirection;
					SortedList _sortedTableList = null;
					if(orderDirection == "Ascending")
					{
						Global.DBSetting.OrderDirection = "Descending";
						log.Debug("Order Names Descending");
						_sortedTableList =  Utility.OrderTablesByName(Global.Serv1.Databases[Global.DBSetting.Name].Tables , false);
					}
					else
					{
						Global.DBSetting.OrderDirection = "Ascending";
						log.Debug("Order Names Ascending");
						_sortedTableList =  Utility.OrderTablesByName(Global.Serv1.Databases[Global.DBSetting.Name].Tables , true);
					}

					TableCollection tabs = Global.Serv1.Databases[Global.SelectedDB].Tables;
					foreach( DictionaryEntry orderedEntry in _sortedTableList   )
					{
						Global.DBSetting.GetTblSetting((string)orderedEntry.Value,true).OrderID = (int)orderedEntry.Key;
					}
					this.SetUIFromSettings(Global.RefreshMode.DataBase);
				}

				if( hti.Type == DataGrid.HitTestType.Cell && 
					hti.Column == 1)
				{ 
					log.Debug("Hit first Column");

					string tblName  = (string)this.dtgTables[hti.Row, 0];
					this.dtgTables[hti.Row , 1] = ! (bool) this.dtgTables[hti.Row, 1];

					if(log.IsDebugEnabled)
					{
						log.Debug("Changed Table:" + tblName + " script: " + ((bool)this.dtgTables[hti.Row, 1]).ToString() );
					}
					
					//Global.DBSetting.GetTblSetting(hti.Row + 1).Script = (bool)this.dtgTables[hti.Row, 1];
					Global.DBSetting.GetTblSetting(tblName,false ).Script = (bool)this.dtgTables[hti.Row, 1];
				} 
				if( hti.Type == DataGrid.HitTestType.Cell && 
					(hti.Column == 0 || hti.Column == 1) )
				{
					SelectedTable = (string)this.dtgTables[hti.Row, 0];
					log.Debug("SelectedTable: " + SelectedTable);
					LoadTableSettings(SelectedTable);
				}
			}                                                                                                    
			catch(Exception ex) 
			{ 
				log.Error(ex);
				base.WriteError(ex.ToString());
			}
		} 
		private void txtFilter_TextChanged(object sender, System.EventArgs e)
		{
			if(this.SelectedTable == null) return;

			if(txtFilter.Text.Trim()!="")
			{
				Global.DBSetting.GetTblSetting(this.SelectedTable,true).Filter = txtFilter.Text.Trim();
			}
			else
			{
				Global.DBSetting.GetTblSetting(this.SelectedTable,true).Filter = "";
			}
		}
		private void txtTop_TextChanged(object sender, System.EventArgs e)
		{
			if(this.SelectedTable == null) return;

			if(txtTop.Text.Trim()!="")
			{
				try
				{
					Global.DBSetting.GetTblSetting(this.SelectedTable,true).Top = int.Parse(txtTop.Text.Trim());
				}
				catch
				{
					this.statBar.Text = "Please check the Top Value. Must be a number greater zero";
					return;
				}
			}
			else
			{
				Global.DBSetting.GetTblSetting(this.SelectedTable,true).Top = -1;
			}
			this.statBar.Text = "";
		}

		





		private void btnAll_Click(object sender, System.EventArgs e)
		{
			setAll(true);
		}

		private void btnUnselect_Click(object sender, System.EventArgs e)
		{
			setAll(false);
		}
		private void setAll(bool selected)
		{
			foreach( TableSetting tableSetting in Global.DBSetting.TableSettings.Values)
			{
				tableSetting.Script = selected;
			}
			SetUIFromSettings(Global.RefreshMode.DataBase);
		}

		private void TableForm_Resize(object sender, System.EventArgs e)
		{
			Redraw();
		}
		
		private void Redraw()
		{
			this.grpFilter.Width = this.ClientRectangle.Width - 30;
			this.grpTable.Width =  this.ClientRectangle.Width - 40;	
			this.grpTable.Height = this.ClientRectangle.Height - this.grpFilter.Height - 70 ;
			this.txtFilter.Width = this.grpFilter.Width -40;
			
			//Table Group
			this.dtgTables.Width = ((grpTable.Width  )/ 2) -10;
			this.dtgTables.Height = grpTable.Height - 70;
			this.chkCols.Width = this.dtgTables.Width - 5 ;
			this.chkCols.Height = this.dtgTables.Height ;
			this.chkCols.Location = new Point(grpTable.Location.X + (grpTable.Width / 2) - 5,chkCols.Location.Y);
			this.lblCol.Location = new Point(this.chkCols.Location.X,lblCol.Location.Y);
			
			this.dataGridTableStyle1.GridColumnStyles[0].Width = this.dtgTables.Width -100;
			this.dataGridTableStyle1.GridColumnStyles[1].Width = 40 ;
		}

		

		/// <summary>
		/// Order Tables to avoid Constraint Violations
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnOrder_Click(object sender, System.EventArgs e)
		{
			SortedList _sortedTableList =  Utility.OrderTablesByDependency(Global.Serv1.Databases[Global.DBSetting.Name].Tables);
			foreach( DictionaryEntry orderedEntry in _sortedTableList   )
			{
				Global.DBSetting.GetTblSetting((string)orderedEntry.Value,true).OrderID = (int)orderedEntry.Key;
			}
			this.SetUIFromSettings(Global.RefreshMode.DataBase);
		}

	}
}
