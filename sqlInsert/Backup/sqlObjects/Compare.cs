using System.Data;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.Collections.Specialized;
using DifferenceEngine;
using System.Collections;

namespace SQLObjects
{
	/// <summary>
	/// Summary description for Compare.
	/// </summary>
	public class Compare
	{
		private Database _leftDataBase;
		private Database _rightDataBase;
		DataTable _dtOverview;
		DataTable _dtDetails;
		public Compare(Database leftDataBase , Database rightDataBase)
		{
			this._leftDataBase = leftDataBase;
			this._rightDataBase = rightDataBase;
		}
		

		/// <summary>
		/// Infomation if table exists
		/// </summary>
		public DataTable Overview
		{
			get
			{
				if(_dtOverview==null)
				{
					_dtOverview = GetOverview();
				}
				return _dtOverview;
			}
		}


		/// <summary>
		/// Infomation if tables have the same structure
		/// checks Column Name, Datatype , Length
		/// </summary>
		public DataTable Details
		{
			get
			{
				if(_dtDetails==null)
				{
					_dtDetails = GetDetails();
				}
				return _dtDetails;
			}
		}

		private DataTable GetDetails()
		{	
			
			//0 exists both
			//1 exists only left
			//2 exists only right
			//3  differs

			DataTable _dtDetails  = new DataTable();
			DataColumn _tableName = new DataColumn("TableName",typeof(string));
			DataColumn _colName = new DataColumn("ColumnName",typeof(string));


			_dtDetails.Columns.Add(new DataColumn("StateTable",typeof(int)));
			_dtDetails.Columns.Add(new DataColumn("StateColumn",typeof(int)));
			_dtDetails.Columns.Add(new DataColumn("TypeColumn",typeof(bool)));
			_dtDetails.Columns.Add(new DataColumn("LenColumn",typeof(bool)));

 
			
			return _dtOverview;

		}

        /// <summary>
        /// Gets the text diff for table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public void GetTableDiffReport(string tableName, out DiffList_TextFile sLF, out DiffList_TextFile dLF, out ArrayList rep)
        {
            TableCollection leftDataBaseTables = Global.Serv1.Databases[Global.SelectedDB].Tables;
            TableCollection rightDataBaseTables = Global.Serv1.Databases[_rightDataBase.Name].Tables;

            Table tableRight = rightDataBaseTables[tableName];
            Table tableLeft = leftDataBaseTables[tableName];


            ScriptingOptions scriptingOptions = new ScriptingOptions();

            scriptingOptions.Add(ScriptOption.NoCollation);
            scriptingOptions.Add(ScriptOption.NoFileGroup);
            scriptingOptions.Add(ScriptOption.NoExecuteAs);

            StringCollection scriptLeft = tableLeft.Script(scriptingOptions);
            StringCollection scriptRight = tableRight.Script(scriptingOptions);

            string leftSql = scriptLeft[scriptLeft.Count-1].Replace("\r", "").Replace("\t", "").ToLower();
            string rightSql = scriptRight[scriptRight.Count-1].Replace("\r", "").Replace("\t", "").ToLower();

            sLF = new DiffList_TextFile(leftSql);
            dLF = new DiffList_TextFile(rightSql);

            rep = TextDiff(sLF, dLF);

        }

        /// <summary>
        /// Use DifferenceEngine to calculate Differences
        /// </summary>
        /// <param name="sFile"></param>
        /// <param name="dFile"></param>
        private ArrayList TextDiff(DiffList_TextFile sLF, DiffList_TextFile dLF)
        {

            double time = 0;
            DiffEngine de = new DiffEngine();
            time = de.ProcessDiff(sLF, dLF, DifferenceEngine.DiffEngineLevel.SlowPerfect);

            ArrayList rep = de.DiffReport();
            return rep;

        }

      

        /// <summary>
        /// Get the diff list for all tables
        /// </summary>
        /// <returns></returns>
		private DataTable GetOverview()
		{
			DataTable _dtOverview  = new DataTable();
			
			DataColumn _colName = new DataColumn("Name",typeof(string));
			DataColumn[] keys = new DataColumn[1];
			keys[0] = _colName;
			_dtOverview.Columns.Add(_colName);
			//0 exists both
			//1 exists only left
			//2 exists only right
			//3 table differs

			_dtOverview.Columns.Add(new DataColumn("StateTable",typeof(int)));
			_dtOverview.PrimaryKey = keys;


            TableCollection tabs = Global.Serv1.Databases[Global.SelectedDB].Tables;

            TableCollection  _leftDataBaseTables = Global.Serv1.Databases[Global.SelectedDB].Tables;
            TableCollection _rightDataBaseTables = Global.Serv1.Databases[_rightDataBase.Name].Tables;

            foreach (Table tableLeft in _leftDataBaseTables)
            {
                if (!tableLeft.IsSystemObject)
                {
                    if (!_rightDataBaseTables.Contains(tableLeft.Name))
                    {
                        DataRow _rowToAdd = _dtOverview.NewRow();
                        _rowToAdd["Name"] = tableLeft.Name;
                        _rowToAdd["StateTable"] = 1;
                        _dtOverview.Rows.Add(_rowToAdd);
                    }
                }
            }

            foreach (Table tableRight in _rightDataBaseTables)
            {
                if (!tableRight.IsSystemObject)
                {
                    if (!_leftDataBaseTables.Contains(tableRight.Name))
                    {
                        DataRow _rowToAdd = _dtOverview.NewRow();
                        _rowToAdd["Name"] = tableRight.Name;
                        _rowToAdd["StateTable"] = 2;
                        _dtOverview.Rows.Add(_rowToAdd);
                    }
                }
            }

            //Table exists in both databases
            foreach (Table tableLeft in _leftDataBaseTables)
            {
                if (!tableLeft.IsSystemObject)
                {
                    if (_rightDataBaseTables.Contains(tableLeft.Name))
                    {
                        Table tableRight = _rightDataBaseTables[tableLeft.Name];

                        DataRow _rowToAdd = _dtOverview.NewRow();
                        ScriptingOptions scriptingOptions = new ScriptingOptions();

                        scriptingOptions.Add(ScriptOption.NoCollation);
                        scriptingOptions.Add(ScriptOption.NoFileGroup);
                        scriptingOptions.Add(ScriptOption.NoExecuteAs);


                        StringCollection  scriptLeft = tableLeft.Script(scriptingOptions);
                        StringCollection scriptRight = tableRight.Script(scriptingOptions);

                        bool hasDiff = false;
                        int index = 0;
                        for (int scriptIndex = 0; scriptIndex <= scriptLeft.Count - 1; scriptIndex++)
                        {
                            string leftSql = scriptLeft[scriptIndex].Replace("\r","").Replace("\t","").ToLower();
                            string rightSql = scriptRight[scriptIndex].Replace("\r", "").Replace("\t", "").ToLower();
                            if (leftSql != rightSql)
                            {
                                hasDiff = true;

                                //TextDiff(leftSql, rightSql);
                            }

                        }


                        _rowToAdd["Name"] = tableLeft.Name;
                        if (hasDiff)
                        {
                            _rowToAdd["StateTable"] = 3;
                        }
                        else
                        {
                            _rowToAdd["StateTable"] = 0;
                        }
                        _dtOverview.Rows.Add(_rowToAdd);
                    }
                }
            }

			
			return _dtOverview;

		}
	}
}
