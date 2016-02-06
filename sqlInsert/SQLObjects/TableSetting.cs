using System;
using System.Collections;
using System.Xml.Serialization;

namespace SQLObjects
{
	/// <summary>
	/// Summary description for TableSetting.
	/// </summary>
	/// 
	[Serializable]
	public class TableSetting: IComparable
	{
		private string _name;
		private Hashtable _colSettings;
		private bool _script;
		// 1 based
		private int _orderID;
		private string _filter;
		private int _top;
		private string _sqlClause;
		private string _filterIncludingConstraint;

		public TableSetting(string tblName)
		{
			this._name = tblName;
			this._script = true;
			this._colSettings  = new Hashtable();
			_filter = "";
			_top = -1;

		}

		public TableSetting()
		{
		}
		


		/// <summary>
		/// only use for serialization
		/// </summary>
		public ColumnSetting[] ColumnSettings
		{
			get
			{
				ColumnSetting[] _retvalue = new ColumnSetting[_colSettings.Count];
				int i =0;
				foreach(ColumnSetting setting in _colSettings.Values)
				{
					_retvalue[i] = setting;
					i++;
				}
				return _retvalue;
			}
			set
			{
				_colSettings = new Hashtable();

				for(int i=0;i<= value.Length-1;i++)
				{
					ColumnSetting setting = value[i];
					_colSettings.Add(setting.Name,setting );
				}
			}
		}




		//if a dependend Table needs to be filetered use this sql
		public string SqlClause
		{
			get { return _sqlClause; }
			set { _sqlClause = value; }
		}

		[XmlIgnore]
		public string FilterIncludingConstraint
		{
		
			get
			{
				return _filterIncludingConstraint;	
			}
			set
			{
				_filterIncludingConstraint = value;	
			}
		}

		public string Filter
		{
		
			get
			{
				return _filter;	
			}
			set
			{
				_filter = value;	
			}
		}

		public  int Top
		{
			set
			{
				this._top = value;
			}
			get
			{
				return this._top;
			}
		}

		public bool Script
		{
			set
			{
				this._script = value;
			}
			get
			{
				return this._script;
			}
		}

		public  string Name
		{
			set
			{
				this._name = value;
			}
			get
			{
				return this._name;
			}
		}
		public  int OrderID
		{
			set
			{
				this._orderID = value;
			}
			get
			{
				return this._orderID;
			}
		}
		
		public ColumnSetting GetColSetting(string colName , bool forceCreate)
		{
			if(_colSettings.ContainsKey(colName))
			{
				return (ColumnSetting)_colSettings[colName];
			}
			else
			{
				if(!forceCreate)
				{
					throw new Exception("Key not found for " + colName);
				}
				else
				{
					return AddColSetting(colName);
				}
			}
		}
		public void ClearColSetting( )
		{
			_colSettings.Clear();
		}

		public ColumnSetting AddColSetting( string  colName)
		{
			if(_colSettings.ContainsKey(colName))
			{
				throw new Exception("Key already there " + colName);
			}
			else
			{
				SQLObjects.ColumnSetting _colSetting = new ColumnSetting(colName);
				_colSettings.Add(colName,_colSetting);
				return _colSetting;
			}
		}
		#region IComparable Members

		public int CompareTo(object obj)
		{
			if(  this.OrderID >  ((TableSetting)obj).OrderID)
			{
				return 1;	
			}
			if(  this.OrderID <  ((TableSetting)obj).OrderID)
			{
				return -1;	
			}
			return 0;
		}

		#endregion
	}
}
