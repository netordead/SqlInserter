using System;
using System.Collections;



namespace SQLObjects
{
	/// <summary>
	/// Settings for one Sql-Server
	/// </summary>
	///	<remarks>
	/// Server is matched by its name (localhost) and . are different servers here
	///	 </remarks>
	[Serializable]
	public class ServerSetting
	{
		private string _name;
		private Hashtable _dbSettings;

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public ServerSetting(string name)
		{
			this._name = name;
			_dbSettings = new Hashtable();

		}
		/// <summary>
		/// only use for serialization
		/// </summary>
		public DBSetting[] DBSettings
		{
			get
			{
				DBSetting[] _retvalue = new DBSetting[_dbSettings.Count];
				int i =0;
				foreach(DBSetting setting in _dbSettings.Values)
				{
					_retvalue[i] = setting;
					i++;
				}
				return _retvalue;
			}
			set
			{
				_dbSettings = new Hashtable();

				for(int i=0;i<= value.Length-1;i++)
				{
					DBSetting setting = value[i];
					_dbSettings.Add(setting.Name,setting );
				}
			}
		}



		public ServerSetting()
		{
		}

		public DBSetting AddDBSetting( string  dbName)
		{
			if(_dbSettings.ContainsKey(dbName))
			{
				throw new Exception("DB already in store " + dbName );
			}
			else
			{
				SQLObjects.DBSetting _dbSetting = new DBSetting(dbName);
				_dbSettings.Add(dbName,_dbSetting);
				return (DBSetting)_dbSettings[dbName];
			}
		}
		
		public void UpdateDBSetting( DBSetting  updateSetting)
		{
			if(_dbSettings.ContainsKey(updateSetting.Name))
			{
				_dbSettings.Remove(updateSetting.Name);
			}

			_dbSettings.Add(updateSetting.Name,updateSetting);
			
		}

		public DBSetting GetDBSettings(string  dbName , bool forceCreate)
		{
			if(!_dbSettings.ContainsKey(dbName) )
			{
				if(forceCreate)
				{
					return AddDBSetting(dbName);
				}
				else
				{
					throw new Exception("DB not in store " + dbName );
				}
			}
			else
			{
				return  (DBSetting)_dbSettings[dbName];
			}
		}

	}
}
