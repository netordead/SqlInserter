namespace SQLObjects
{
	/// <summary>
	/// Dataobject that is used to pass Connection Data into a separate thread
	/// </summary>
	public class Connection
	{
		private string _server;
		private string _db;
		private bool _useSQL;
		private string _sqlPwd;
		private string _sqlUser;
		private bool _isCompare;
		public Connection()
		{
	
		}

		public bool IsCompare
		{
			get
			{
				return _isCompare;
			}
			set
			{
				_isCompare = value;
			}
		}
		public string Server
		{
			get
			{
				return _server;
			}
			set
			{
				_server = value;
			}
		}
		public string DB
		{
			get
			{
				return _db;
			}
			set
			{
				_db = value;
			}
		}

		public bool UseSQL
		{
			get
			{
				return _useSQL;
			}
			set
			{
				_useSQL = value;
			}
		}
		public string SqlPwd
		{
			get
			{
				return _sqlPwd;
			}
			set
			{
				_sqlPwd = value;
			}
		}
		public string SqlUser
		{
			get
			{
				return _sqlUser;
			}
			set
			{
				_sqlUser = value;
			}
		}
	}
}
