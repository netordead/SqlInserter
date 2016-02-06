using SQLObjects;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace SQLObjects
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global
	{
		public  enum RefreshMode{Application,Server,DataBase,Table};

		public static bool IsConnected = false;
		public static ApplicationSetting AppSettings
		{
			get { return _appSettings; }
			set { _appSettings = value; }
		}

		public static ServerSetting ServSettings
		{
			get { return _servSettings; }
			set { _servSettings = value; }
		}

		public static DBSetting DBSetting
		{
			get { return _dbSetting; }
			set { _dbSetting = value; }
		}


        public static Server Serv1
        {
            get { return serv1; }
            set { serv1 = value; }
        }


        public static Server ServCompare1
        {
            get { return servCompare1; }
            set { servCompare1 = value; }
        }


        private static Server serv1;
        private static Server servCompare1;

		private  static DBSetting _dbSetting ;
		private static SQLObjects.ServerSetting _servSettings  ;
		private static SQLObjects.ApplicationSetting _appSettings  ;

		public static string SelectedDB
		{
			get { return _selectedDB; }
			set { _selectedDB = value; }
		}
		public static string SelectedDBCompare
		{
			get { return _selectedDBCompare; }
			set { _selectedDBCompare = value; }
		}
		private static string _selectedDB  ;
		private static string _selectedDBCompare  ;
		
		public Global()
		{

		}
	}
}
