using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace SQLObjects
{
	/// <summary>
	/// Settings are serialized for each server u connected to and each db on it
	/// </summary>
	[Serializable]
	public class ApplicationSetting
	{
		private List<ServerSetting> _serverSettings;
		private string selectedDB;
		private string selectedDBCompare;

		public ApplicationSetting()
		{
            _serverSettings = new List<ServerSetting>();
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
				foreach(ServerSetting setting in _serverSettings)
				{
					_retvalue[i] = setting;
					i++;
				}
				return _retvalue;
			}
			set
			{
				_serverSettings = new List<ServerSetting>();

				for(int i=0;i<= value.Length-1;i++)
				{
					ServerSetting setting = value[i];
					_serverSettings.Add(setting );
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
            return  (from setting in _serverSettings where setting.Name == serverName select setting).SingleOrDefault<ServerSetting>()!=null;
		}

		

		public ServerSetting GetSetting(string  serverName)
		{

            return (from setting in _serverSettings where setting.Name == serverName select setting).Single<ServerSetting>();
		}
		
		/// <summary>
		/// List of server names
		/// </summary>
		/// <returns></returns>
        public List<ServerSetting> GetSettings()
		{

            return _serverSettings;
		}

		public ServerSetting AddServerSetting( string  serverName)
		{
            ServerSetting setting = new ServerSetting
            {
                Name = serverName
            };
            _serverSettings.Add(new ServerSetting { Name = serverName });
            return setting;

		}

		public void AddServerSetting( ServerSetting  servSetting)
		{
			

				_serverSettings.Add(servSetting);
			
		}
		public ServerSetting GetServerSetting(string  serverName , bool forceCreate)
		{
            ServerSetting sett = (from setting in _serverSettings where setting.Name == serverName select setting).SingleOrDefault<ServerSetting>();


            if (sett == null)
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
                return sett;

			}
		}

	}
}
