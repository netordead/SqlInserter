using System;



namespace SQLObjects
{
	/// <summary>
	/// Summary description for ColumnSetting.
	/// </summary>
	/// 
	[Serializable]
	public class ColumnSetting
	{
		private bool _script;
		private string _name;

		public ColumnSetting(string name)
		{
			_script = true;
			_name  = name;
		}

		public ColumnSetting()
		{

		}

		public bool Script
		{
			get
			{
				return _script;
			}
			set
			{
				_script = value;
			}
		}
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
	}
}
