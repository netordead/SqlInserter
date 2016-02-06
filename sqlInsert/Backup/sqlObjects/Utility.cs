using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using log4net;

using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;


namespace SQLObjects
{
	/// <summary>
	/// Summary description for Utility.
	/// </summary>
	public class Utility
	{
		public Utility()
		{

		}

		private static readonly ILog log = LogManager.GetLogger(typeof(Utility));
		/// <summary>
		/// Returns  List of tables ordered to allow inserts
		/// without violating constraints
		/// </summary>
		/// <param name="unorderedTables"></param>
		/// <returns></returns>
		public static SortedList OrderTablesByDependency( TableCollection unorderedTables)
		{
			ArrayList _referencedTables = new ArrayList();
			SortedList _sortedTables = new SortedList();

			//create a list of tables that are refernced by other tables
			foreach(Table tableObject in unorderedTables  )
			{
				if(tableObject.IsSystemObject) continue;
				
				foreach(string tableName in GetTableDependencies(tableObject))
				{
					if(!_referencedTables.Contains(tableName))
					{
						_referencedTables.Add(tableName);
					}
				}
			}
			//tables that are referenced have to be filled first
			while(true)
			{
				UpdateNonViolatingList(ref _referencedTables , ref _sortedTables ,ref unorderedTables);
				if(_referencedTables.Count == 0) break;
			}
			//tables that are not referenced at end
			foreach(Table tableObject in unorderedTables  )
			{
				if(tableObject.IsSystemObject) continue;

				if(!_sortedTables.ContainsValue(tableObject.Name))
				{
					_sortedTables.Add(_sortedTables.Count + 1 ,tableObject.Name );
				}
			}
			return _sortedTables;
		}
		
		/// <summary>
		/// Returns  List of tables ordered to allow inserts
		/// without violating constraints
		/// </summary>
		/// <param name="unorderedTables"></param>
		/// <returns></returns>
		public static SortedList OrderTablesByName( TableCollection unorderedTables , bool Ascending )
		{
			ArrayList tableList = new ArrayList();
			SortedList sortedTables = new SortedList();
			int counter = 0;

			foreach(Table tableObject in unorderedTables  )
			{
				if(tableObject.IsSystemObject) continue;
				tableList.Add(  tableObject.Name);
			}

			tableList.Sort();
	
			foreach(string tableName in tableList)
			{
				if(Ascending)
				{
					sortedTables.Add(counter  + 1 ,tableName );
				}
				else
				{
					sortedTables.Add( ((tableList.Count - 1) - counter ) + 1 ,tableName );	
				}
				counter++;
			}

			return sortedTables;
		}
		/// <summary>
		/// List of Tables this Tbale depends on
		/// </summary>
		/// <param name="tableObject"></param>
		/// <returns></returns>
		public static ArrayList GetTableDependencies(Table tableObject)
		{
			ArrayList referencedTables = new ArrayList();

            foreach (ForeignKey key in tableObject.ForeignKeys)
            {
                referencedTables.Add(key.ReferencedTable);
            }

			return referencedTables;
		}


		/// <summary>
		/// checks potentially violating tables for contsraints and if no violations happen
		/// adds them to the list of tables to script
		/// </summary>
		/// <param name="tableList"></param>
		/// <param name="sortedTables"></param>
		private static void UpdateNonViolatingList(   ref ArrayList  tableList , ref SortedList sortedTables , ref TableCollection unorderedTables )
		{
			foreach(string potentiallyViolatingTableName in tableList)
			{
				Table potentiallyViolatingTable = unorderedTables[potentiallyViolatingTableName];

				bool hasViolation = false;

		
				if(!hasViolation) 
				{
					sortedTables.Add( sortedTables.Count + 1  , potentiallyViolatingTableName);
					tableList.Remove(potentiallyViolatingTableName);
					break;
				}
			}
		}

		public static void SaveAppSettings( string path , ApplicationSetting settings)
		{
			try
			{
				XmlSerializer _xmlSer = new XmlSerializer(typeof(ApplicationSetting));
				FileStream	_stream = new FileStream(path, FileMode.Create);
				_xmlSer.Serialize(_stream, settings);
				_stream.Close();

			}
			catch(Exception ex)
			{
				throw ex;
			}
	
		}

		public static ApplicationSetting LoadAppSettings( string path )
		{
			FileStream fs =null;
			XmlReader reader =null;

			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(ApplicationSetting));
				fs = new FileStream(path, FileMode.Open);
				reader = new XmlTextReader(fs);
				ApplicationSetting _setting;
				_setting = (ApplicationSetting) serializer.Deserialize(reader);
				return _setting;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			finally
			{
				if(fs!=null)
				{
					fs.Close();				
				}
				if(reader!=null)
				{
					reader.Close();						
				}
			}
		}

		public static string BinToString(Byte[] binValue)
		{		
			char[] hexCode = {'0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'};

			StringBuilder sb = new StringBuilder();

			foreach (byte b in binValue)
			{
				sb.Append(Convert.ToString(hexCode[b >> 4]));
				sb.Append(Convert.ToString(hexCode[b & 0xF]));
			}

			return "0x" + sb.ToString();
		}
	}
}
