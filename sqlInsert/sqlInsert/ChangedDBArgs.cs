namespace SQLInsert
{
	/// <summary>
	/// ChnagedDBArgs.
	/// </summary>
	public class ChangedDBArgs
	{
		public ChangedDBArgs(string dbName)
		{
			DBName = dbName;


		}
		public ChangedDBArgs( )
		{
		}
		public string  DBName;
		public string ServerName;
	}
}