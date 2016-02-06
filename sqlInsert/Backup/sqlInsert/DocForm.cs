using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Threading;
using SQLObjects;
using WeifenLuo.WinFormsUI;
using System.Globalization;
using log4net;

using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace SQLInsert
{
    /// <summary>
    /// Summary description for DummyPropertyGrid.
    /// </summary>
    public class DocForm : DockContent
    {
        private System.Windows.Forms.Label lblRows;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RichTextBox txtResult;
        private bool stopped = false;


        delegate void ScriptCallback(string script);
        ScriptCallback setScript;

        delegate void BooleanCallback(Boolean enable);
        BooleanCallback enableControls;

        delegate void MaximumTableCallback(int max);
        MaximumTableCallback setMaximumTable;

        delegate void SetInt(int value);
        delegate void SetString(string str);

        delegate void MsgCallback(string msg);
        MsgCallback setMessage;
        private System.Windows.Forms.StatusBar statBar;
        private System.Windows.Forms.Button btnCancel;
        private static readonly ILog log = LogManager.GetLogger(typeof(DocForm));
        /// <summary>
        /// Required designer variable.
        /// </summary>


        public DocForm()
        {

            InitializeComponent();
            // Cache a delegate for repeated reuse 
            enableControls = new BooleanCallback(EnableControls);
            setMessage = new MsgCallback(SetMessage);
            setScript = new ScriptCallback(SetScript);
            setMaximumTable = new MaximumTableCallback(SetMaximumTable);

        }
        void SetMessage(string msg)
        {
            // makes sure that it is being called on 
            // the GUI thread using InvokeRequired and BeginInvoke
            if (InvokeRequired)
            {
                BeginInvoke(setMessage, new Object[] { msg });
                return;
            }
            Thread.Sleep(100);
            this.statBar.Text = msg;


        }
        void EnableControls(Boolean enable)
        {

            // EnableControls makes sure that it is being called on 
            // the GUI thread using InvokeRequired and BeginInvoke
            if (InvokeRequired)
            {
                BeginInvoke(enableControls, new Object[] { enable });
                return;
            }
        }
        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DocForm));
            this.lblRows = new System.Windows.Forms.Label();
            this.lblTable = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.statBar = new System.Windows.Forms.StatusBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblRows
            // 
            this.lblRows.Location = new System.Drawing.Point(8, 48);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(148, 23);
            this.lblRows.TabIndex = 62;
            // 
            // lblTable
            // 
            this.lblTable.Location = new System.Drawing.Point(8, 16);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(148, 23);
            this.lblTable.TabIndex = 61;
            this.lblTable.Text = "Total Progress";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(160, 48);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(752, 23);
            this.progressBar2.TabIndex = 60;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(160, 16);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(752, 23);
            this.progressBar1.TabIndex = 59;
            // 
            // txtResult
            // 
            this.txtResult.DetectUrls = false;
            this.txtResult.Location = new System.Drawing.Point(16, 120);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(904, 408);
            this.txtResult.TabIndex = 63;
            this.txtResult.Text = "";
            // 
            // statBar
            // 
            this.statBar.Location = new System.Drawing.Point(0, 518);
            this.statBar.Name = "statBar";
            this.statBar.Size = new System.Drawing.Size(992, 22);
            this.statBar.TabIndex = 64;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(24, 88);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(124, 23);
            this.btnCancel.TabIndex = 65;
            this.btnCancel.Text = "Stop";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // DocForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(992, 542);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.statBar);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.lblRows);
            this.Controls.Add(this.lblTable);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft)
                | WeifenLuo.WinFormsUI.DockAreas.DockRight)
                | WeifenLuo.WinFormsUI.DockAreas.DockTop)
                | WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
            this.DockPadding.Bottom = 2;
            this.DockPadding.Top = 2;
            this.HideOnClose = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocForm";
            this.ShowHint = WeifenLuo.WinFormsUI.DockState.DockTopAutoHide;
            this.Text = "SQL Script";
            this.ToolTipText = "Run this Script to insert the data";
            this.Resize += new System.EventHandler(this.DocForm_Resize);
            this.ResumeLayout(false);

        }




        public void HandleScript(bool script)
        {
            stopped = false;
            this.txtResult.Text = "";
            //enableControls(false);

            ArrayList _tablesList = new ArrayList();



            foreach (TableSetting tableSetting in Global.DBSetting.TableSettings.Values)
            {
                if (tableSetting.Script)
                {
                    _tablesList.Add(tableSetting);
                }
            }
            _tablesList.Sort();

            this.progressBar1.Maximum = _tablesList.Count;

            WaitCallback async = new WaitCallback(HandleTables);
            ThreadPool.QueueUserWorkItem(async, _tablesList);
        }
        /// <summary>
        /// Loops through Tables and calls for ScriptTable()  
        /// for Tables that are to be scripted
        /// </summary>
        /// <param name="param">Array of all Tables</param>
        private void HandleTables(Object param)
        {
            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-us");


                string script = "";


                ArrayList _tablesList = (ArrayList)param;
                int i = 0;

                if (Global.DBSetting.ScriptToFile)
                {
                    File.Delete(Global.DBSetting.FileNameResult);
                }

                if (Global.DBSetting.DeleteBeforeInsert)
                {
                    IComparer comparerBackwards = new ComparerDeleteSequence();

                    _tablesList.Sort(comparerBackwards);

                    foreach (TableSetting currentTableSetting in _tablesList)
                    {
                        string _retScript = " DELETE FROM  [" + currentTableSetting.Name + "] \n ";
                        if (!Global.DBSetting.ScriptToFile)
                        {
                            SetScript(_retScript);
                        }
                        else
                        {
                            AppendToFileScript(_retScript);
                        }
                    }

                }
                _tablesList.Sort();
                foreach (TableSetting currentTableSetting in _tablesList)
                {
                    //stop if user canceled
                    if (stopped) return;
                    ReportTable(currentTableSetting.Name);
                    script = ScriptTable(currentTableSetting.Name);



                    if (!Global.DBSetting.ScriptToFile)
                    {
                        SetScript(script);
                    }
                    else
                    {
                        AppendToFileScript(script);
                    }
                    ReportProgress(++i);
                }



                if (Global.DBSetting.ScriptToFile)
                {
                    SetScript("run this from command prompt \n  osql -E -S " + Global.ServSettings.Name + " < " + Global.DBSetting.FileNameResult + " -d " + Global.DBSetting.Name + " -x 10000000 ");
                }
                enableControls(true);
            }
            catch (System.Exception ex)
            {
                this.SetMessage(ex.Message);
                log.Error(ex);
            }
        }


        /// <summary>
        /// Option to write insert script to file
        /// </summary>
        private void AppendToFileScript(string script)
        {
            FileStream _fsSQL = null;
            StreamWriter _sw = null;

            try
            {
                _fsSQL = new FileStream(Global.DBSetting.FileNameResult, FileMode.Append);
                _sw = new StreamWriter(_fsSQL);
                _sw.Write(script);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                _sw.Close();
                _fsSQL.Close();
            }
        }

        void SetScript(string script)
        {
            // makes sure that it is being called on 
            // the GUI thread using InvokeRequired and BeginInvoke
            if (InvokeRequired)
            {
                BeginInvoke(setScript, new Object[] { script });
                return;
            }

            this.txtResult.Text += script + "\n";
        }


        /// <summary>
        /// Set the Maximum for TableRowa Progress
        /// </summary>
        /// <param name="max"></param>
        void SetMaximumTable(int max)
        {
            // makes sure that it is being called on 
            // the GUI thread using InvokeRequired and BeginInvoke
            if (InvokeRequired)
            {
                BeginInvoke(setMaximumTable, new Object[] { max });
                return;
            }

            this.progressBar2.Maximum = max;
        }

        /// <summary>
        /// Progresbar Total
        /// </summary>
        /// <param name="val"></param>
        void ReportProgress(int val)
        {
            ISynchronizeInvoke synchronizer = progressBar1;
            if (synchronizer.InvokeRequired == false)
            {
                SetProgress(val);
                return;
            }
            SetInt delProg = new SetInt(SetProgress);
            try
            {
                synchronizer.Invoke(delProg, new object[] { val });
            }
            catch
            {
            }
        }

        /// <summary>
        /// Progresbar Table
        /// </summary>
        /// <param name="val"></param>
        void ReportProgressTable(int val)
        {
            ISynchronizeInvoke synchronizer = progressBar2;
            if (synchronizer.InvokeRequired == false)
            {
                SetProgressTable(val);
                return;
            }
            SetInt delProg = new SetInt(SetProgressTable);
            try
            {
                synchronizer.Invoke(delProg, new object[] { val });
            }
            catch
            {
            }
        }
        void SetProgress(int value)
        {
            progressBar1.Value = value;
        }
        void SetProgressTable(int value)
        {
            progressBar2.Value = value;
        }
        void SetTable(string value)
        {
            this.lblRows.Text = value;
        }
        /// <summary>
        /// Creates Script for 1 given Table
        /// </summary>
        /// <param name="_table">Name of Table</param>
        /// <returns>Scrip to Insert values</returns>
        private string ScriptTable(string table)
        {

            try
            {
                log.Debug("Scripting table: " + table);
                Table currentTable = new Table();
                if (Global.Serv1.Databases[Global.SelectedDB].Tables.Contains(table))
                {
                    //In some Server Settings / Firewall settings this doesnt work
                    currentTable = Global.Serv1.Databases[Global.SelectedDB].Tables[table];
                }
                else
                {
                    foreach (Table tableScan in Global.Serv1.Databases[Global.SelectedDB].Tables)
                    {
                        if (tableScan.Name == table)
                        {
                            currentTable = tableScan;
                        }
                    }
                }

                ColumnCollection cols = currentTable.Columns;
                string tblWithSchema = "[" + currentTable.Schema + "].[" + table + "]";


                string _templateScript = "INSERT INTO " + tblWithSchema + " (";
                bool _firstCol = true;
                string _valuesTemplate = ") VALUES ( ";
                string _columnList = "";
                string _retScript = "";

                DBSetting _dbSettings = Global.ServSettings.GetDBSettings(Global.SelectedDB, true);
                TableSetting _tblSettings = _dbSettings.GetTblSetting(table, true);
                // Create Template for INSERT - Statement
                foreach (Column col in cols)
                {
                    if (!_tblSettings.GetColSetting(col.Name, true).Script) continue;
                    //no inserts for timestamp - explicit insert doesnt exist
                    if (col.DataType.Name.ToLower() == "timestamp") continue;

                    if (_firstCol)
                    {
                        _firstCol = false;
                    }
                    else
                    {
                        _columnList += ",";
                        _valuesTemplate += ",";
                    }
                    _columnList += "[" + col.Name + "] ";

                    if (
                        col.DataType.Name.ToLower() == "bigint" ||
                        col.DataType.Name.ToLower() == "bit" ||
                        col.DataType.Name.ToLower() == "decimal" ||
                        col.DataType.Name.ToLower() == "float" ||
                        col.DataType.Name.ToLower() == "int" ||
                        col.DataType.Name.ToLower() == "money" ||
                        col.DataType.Name.ToLower() == "real" ||
                        col.DataType.Name.ToLower() == "smallint" ||
                        col.DataType.Name.ToLower() == "smallmoney" ||
                        col.DataType.Name.ToLower() == "tinyint" ||
                        col.DataType.Name.ToLower() == "image" ||
                        col.DataType.Name.ToLower() == "bytearray" ||
                       col.DataType.Name.ToLower() == "geography"

                        )
                    {
                        _valuesTemplate += "###" + col.Name + "###";
                    }
                    else
                    {
                        _valuesTemplate += "'###" + col.Name + "###'";

                    }
                }
                _templateScript += _columnList + _valuesTemplate + " )";
                string _top = "";

                if (_tblSettings.Top != -1)
                {
                    _top = " TOP " + _tblSettings.Top.ToString() + " ";
                }


                string _sql = "SELECT " + _top + _columnList + " FROM " + tblWithSchema + " ";
                _sql += " WHERE 1=1 ";


                if (_tblSettings.Filter != "")
                {
                    _sql += " AND ( " + _tblSettings.Filter + " ) ";
                }

                log.Debug(_sql);
                DataTable dt = Global.Serv1.Databases[Global.SelectedDB].ExecuteWithResults(_sql).Tables[0];

                // Create INSERT Script for each row in Table
                int _rowCounter = 0;
                if (dt.Rows.Count == 0)
                {
                    return "-- No rows for table: " + table + "\n";
                }
                else
                {
                    _retScript += "--Insert Script for " + dt.Rows.Count.ToString() + " rows of table: " + tblWithSchema + "\n";
                }

                SetMaximumTable(dt.Rows.Count);
                foreach (DataRow row in dt.Rows)
                {
                    // stop if user canceled
                    if (stopped) return _retScript + "--...Canceled";
                    //Report Progress of Selected Table
                    _rowCounter++;
                    ReportProgressTable(_rowCounter);

                    string _rowScript = _templateScript;
                    //check each COLUMN if it is included in Script
                    foreach (DataColumn col in dt.Columns)
                    {
                        //user disabled scrpting for column
                        if (!_tblSettings.GetColSetting(col.ColumnName, true).Script) continue;

                        // handle null values
                        if (row[col.ColumnName] == DBNull.Value)
                        {
                            _rowScript = _rowScript.Replace("'###" + col.ColumnName + "###'", "Null");
                            _rowScript = _rowScript.Replace("###" + col.ColumnName + "###", "Null");
                            continue;
                        }

                        if (col.DataType == typeof(bool))
                        {
                            _rowScript = _rowScript.Replace("###" + col.ColumnName + "###", (bool)row[col.ColumnName] ? "1" : "0");
                        }
                        else if (col.DataType == typeof(DateTime))
                        {
                            _rowScript = _rowScript.Replace("###" + col.ColumnName + "###", ((DateTime)row[col.ColumnName]).ToString("s"));
                        }
                        else if (col.DataType == typeof(System.Byte[]))
                        {
                            Byte[] _fileData = (System.Byte[])row[col.ColumnName];
                            string _hexString = Utility.BinToString(_fileData);
                            _rowScript = _rowScript.Replace("###" + col.ColumnName + "###", _hexString);
                        }
                        else if (col.DataType.ToString() == "Microsoft.SqlServer.Types.SqlGeography")
                        {
                            string geodata = string.Format("(geography::STGeomFromText('{0}', 4326))", row[col.ColumnName].ToString());
                            _rowScript = _rowScript.Replace("###" + col.ColumnName + "###", geodata);
                        }
                        else
                        {
                            _rowScript = _rowScript.Replace("###" + col.ColumnName + "###", row[col.ColumnName].ToString().Replace("'", "''"));
                        }
                    }
                    _retScript += _rowScript + "\n";
                    if (Global.DBSetting.ScriptToFile)
                    {
                        _retScript = _retScript + " GO \n";
                    }
                }
                if (Global.DBSetting.IdentityInsert)
                {
                    bool _hasIdentity = false;

                    foreach (Column col in cols)
                    {
                        if (col.Identity)
                        {
                            _hasIdentity = true;
                            break;
                        }
                    }
                    if (_hasIdentity)
                    {
                        _retScript = "SET IDENTITY_INSERT [" + table + "] ON \n" + _retScript;
                        _retScript = _retScript + " SET IDENTITY_INSERT [" + table + "] OFF \n";
                    }
                }



                return _retScript;
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
                throw;
            }
        }

        void ReportTable(string val)
        {
            ISynchronizeInvoke synchronizer = lblRows;
            if (synchronizer.InvokeRequired == false)
            {
                SetTable(val);
                return;
            }
            SetString delTab = new SetString(SetTable);
            try
            {
                synchronizer.Invoke(delTab, new object[] { val });
            }
            catch
            {
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.stopped = true;
        }

        private void DocForm_Resize(object sender, System.EventArgs e)
        {
            this.txtResult.Width = this.Width - 40;


            this.progressBar1.Width = this.Width - 180;
            this.progressBar2.Width = this.Width - 180;
        }

    }
}
