using System;
using System.Collections;



namespace SQLObjects
{
	/// <summary>
	/// Settings are serialized for each server u connected to and each db on it
	/// </summary>
	[Serializable]
	public class ApplicationSetting
	{
		private Hashtable _serverSettings;
		private string selectedDB;
		private string selectedDBCompare;

		public ApplicationSetting()
		{
			_serverSettings = new Hashtable();
		}
		

		/// <summary>
		/// only use for serialization
		/// </summary>
		public ServerSetting[] ServerSettings
		{
			get
			{
				ServerSetting[] _retvalue = new ServerSetting[_serverSettings.Count];
				int i =0;
				foreach(ServerSetting setting in _serverSettings.Values)
				{
					_retvalue[i] = setting;
					i++;
				}
				return _retvalue;
			}
			set
			{
				_serverSettings = new Hashtable();

				for(int i=0;i<= value.Length-1;i++)
				{
					ServerSetting setting = value[i];
					_serverSettings.Add(setting.Name,setting );
				}
			}
		}

		public string SelectedDB
		{
			get
			{
				return selectedDB;
			}
			set
			{
				selectedDB = value;
			}
		}
		public string SelectedDBCompare
		{
			get
			{
				return selectedDBCompare;
			}
			set
			{
				selectedDBCompare = value;
			}
		}
		public bool HasSetting(string  serverName)
		{
			return _serverSettings.ContainsKey(serverName);
		}

		

		public ServerSetting GetSetting(string  serverName)
		{
			if(!_serverSettings.ContainsKey(serverName))
			{
				throw new Exception("Server not in store " + serverName );
			}
			else
			{
				return (ServerSetting)_serverSettings[serverName];
			}
		}
		
		/// <summary>
		/// List of server names
		/// </summary>
		/// <returns></returns>
		public ArrayList GetSettings()
		{
			ArrayList keys = new ArrayList();
			foreach( string key in  _serverSettings.Keys)
			{
				keys.Add(key);
			}
			return keys;
		}

		public ServerSetting AddServerSetting( string  serverName)
		{
			if(_serverSettings.ContainsKey(serverName))
			{
				throw new Exception("Server already in store " + serverName );
			}
			else
			{
				SQLObjects.ServerSetting _serverSetting = new ServerSetting(serverName);
				_serverSettings.Add(serverName,_serverSetting);
				return (ServerSetting)_serverSettings[serverName];
			}
		}
		public void AddServerSetting( ServerSetting  servSetting)
		{
			if(_serverSettings.ContainsKey(servSetting.Name))
			{
				throw new Exception("Server already in store " + servSetting.Name );
			}
			else
			{
				_serverSettings.Add(servSetting.Name,servSetting);
			}
		}
		public ServerSetting GetServerSetting(string  serverName , bool forceCreate)
		{
			if(!_serverSettings.ContainsKey(serverName) )
			{
				if(forceCreate)
				{
					return AddServerSetting(serverName);
				}
				else
				{
					throw new Exception("Server not in store " + serverName );
				}
			}
			else
			{
				return  (ServerSetting)_serverSettings[serverName];
			}
		}

	}
}
