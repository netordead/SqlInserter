using System;
using System.Collections;
using System.Xml.Serialization;

namespace SQLObjects
{
	/// <summary>
	/// Object to store and serialize Data for a Database
	/// </summary>
	///	<remarks>
	/// Database is matched by its name
	///	 </remarks>
	[Serializable]
	public class DBSetting
	{

		private string _orderDirection;
		private string _name;
		private bool _identityInsert;
		private bool _deleteBeforeInsert;
		private bool _scriptToFile;
		private string _fileNameResult;
		private Hashtable _tblSettings;


		/// <summary>
		/// Constructor creates new settings
		/// </summary>
		/// <example> This sample shows how to call the DBSetting constructor.
		/// <code>
		///   class MyClass 
		///   {
		///      public static int Main() 
		///      {
		///         DBSetting _setting = new DBSetting("yourdb");
		///      }
		///   }
		/// </code>
		/// </example>
		public DBSetting(string name)
		{
			this._name  = name;
			_tblSettings = new Hashtable();
		}

		/// <summary>
		/// IdentityInsert
		/// </summary>
		public bool IdentityInsert
		{
			get
			{
				return _identityInsert;
			}
			set
			{
				this._identityInsert = value;	
			}
		}

		/// <summary>ScriptToFile
		/// </summary>
		public bool ScriptToFile
		{
			get
			{
				return _scriptToFile;
			}
			set
			{
				this._scriptToFile = value;	
			}
		}
		
		/// <summary>
		/// FileNameResult
		/// </summary>
		public string FileNameResult
		{
			get
			{
				return _fileNameResult;
			}
			set
			{
				this._fileNameResult = value;	
			}
		}

		/// <summary>
		/// OrderDirection - order names of tables
		/// Ascending || Descending
		/// </summary>
		public string OrderDirection
		{
			get
			{
				return _orderDirection;
			}
			set
			{
				this._orderDirection = value;	
			}
		}

		/// <summary>
		/// IdentityInsert
		/// </summary>
		public bool DeleteBeforeInsert
		{
			get
			{
				return _deleteBeforeInsert;
			}
			set
			{
				this._deleteBeforeInsert = value;	
			}
		}



		/// <summary>
		/// Constructor used for serialization
		/// </summary>
		public DBSetting()
		{
		}


		/// <summary>
		/// only use for serialization
		/// </summary>
		///	<remarks>
		/// There is this extra property because Hashtable in the other property doesnt serialize
		///	 </remarks>

		public TableSetting[] TableSettingsSer
		{
			get
			{
				TableSetting[] _retvalue = new TableSetting[_tblSettings.Count];
				int i =0;
				foreach(TableSetting setting in _tblSettings.Values)
				{
					_retvalue[i] = setting;
					i++;
				}
				return _retvalue;
			}
			set
			{
				_tblSettings = new Hashtable();

				for(int i=0;i<= value.Length-1;i++)
				{
					TableSetting setting = value[i];
					_tblSettings.Add(setting.Name,setting );
				}
			}
		}

		/// <summary>
		/// Name of the Database
		/// </summary>
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				this._name = value;	
			}
		}

		/// <summary>
		/// Use to get Hashtable of all user tables in Database
		/// </summary>
		///	<remarks >
		/// This is not serialzed because Hashtable doesnt serialize
		///	 </remarks>
		[XmlIgnore]
		public Hashtable TableSettings
		{
				get{return _tblSettings;}
			
		}

		/// <summary>
		///Add Setting for one Sql-Server table
		/// </summary>
		///	<remarks>
		/// Check if the setting exists before calling , throws error if setting already exists
		///	 </remarks>
		public TableSetting AddTblSetting( string  tblName)
		{
			if(_tblSettings.ContainsKey(tblName))
			{
				throw new Exception("Table already in Settings: " + tblName);
			}
			SQLObjects.TableSetting _tblSetting = new TableSetting(tblName);
			_tblSettings.Add(tblName,_tblSetting);
			return (TableSetting)_tblSettings[tblName];
		}
		
		/// <summary>
		/// Get settings for one Sql-Server table
		/// </summary>
		public TableSetting GetTblSetting(string  tblName , bool forceCreate)
		{
			
			if(!_tblSettings.ContainsKey(tblName))
			{
				if(!forceCreate)
				{
					throw new Exception("Table not in Settings: " + tblName);
				}
				else
				{
					return AddTblSetting(tblName);
				}
			}
			return (TableSetting)_tblSettings[tblName];
		}

		/// <summary>
		/// Moves Tables in the range start-end one position down
		/// Used for order in scripting
		/// </summary>
		public void MoveRestDown(int startEntry, int endEntry )
		{
			foreach(TableSetting tableSetting in _tblSettings.Values)
			{
				if( 	tableSetting.OrderID >= startEntry && tableSetting.OrderID < endEntry)
				{
					tableSetting.OrderID += 1;
				}
			}
		}

		/// <summary>
		/// Moves Tables in the range start-end one position up
		/// Used for order in scripting
		/// </summary>
		public void MoveRestUp(int startEntry, int endEntry )
		{
			foreach(TableSetting tableSetting in _tblSettings.Values)
			{
				if( 	tableSetting.OrderID <= startEntry && tableSetting.OrderID > endEntry)
				{
					tableSetting.OrderID += -1;
				}
			}
		}
		
		/// <summary>
		/// Get Setting for one Sqlserver at specified position
		/// </summary>
		///	<remarks>
		/// orderID defines where the script appears in Script
		///	 </remarks>
		public TableSetting GetTblSetting(int  orderID )
		{
			foreach(TableSetting tableSetting in _tblSettings.Values)
			{
				if( 	tableSetting.OrderID == orderID)
				{
					return tableSetting;
				}
			}
			return null;
		}


	}
}
