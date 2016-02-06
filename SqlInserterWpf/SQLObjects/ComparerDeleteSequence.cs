using System;
using System.Collections;

namespace SQLObjects
{
	/// <summary>
	/// Summary description for ComparerDeleteSequence.
	/// </summary>
	public class ComparerDeleteSequence:IComparer
	{

		public int Compare(object obj1, object obj2)
		{
			TableSetting tab1 = (TableSetting) obj1;
			TableSetting tab2 = (TableSetting) obj2;

			return tab2.OrderID.CompareTo(tab1.OrderID);
		}

	}
}
