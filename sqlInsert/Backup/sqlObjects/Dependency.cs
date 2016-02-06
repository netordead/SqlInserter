using System;

namespace SQLObjects
{
	[Serializable]
	public class Dependency
	{
		private string _tableName;
		private string _primaryKey;
		private string _foreignKey;


		public Dependency()
		{
			
		}


		public string TableName
		{
			get
			{
				return 	_tableName;
			}
			set
			{
				_tableName = value;
			}
		}
		public string PrimaryKey
		{
			get
			{
				return 	_primaryKey;
			}
			set
			{
				_primaryKey = value;
			}
		}
		public string ForeignKey
		{
			get
			{
				return 	_foreignKey;
			}
			set
			{
				_foreignKey = value;
			}
		}
	}
}
