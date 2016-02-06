using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SqlInserterWpf
{
    public class DataBaseList : ObservableCollection<DataBase>
    {
        public DataBaseList()
            : base()
        {
            DataBase d;
            Table t;

            d = new DataBase { Name = "DB1" };
            Add(d);
            t = new Table { Name = "Table1" };
            d.Tables.Add(t);
            t.Columns.Add(new Column { Name = "col1" });
            t.Columns.Add(new Column { Name = "col2" });

            d = new DataBase { Name = "DB2" };
            Add(d);
            t = new Table { Name = "Table2" };
            d.Tables.Add(t);
            t.Columns.Add(new Column { Name = "col3" });
            t.Columns.Add(new Column { Name = "col2" });
            t.Columns.Add(new Column { Name = "col4" });
            t.Columns.Add(new Column { Name = "col6" });

            t = new Table { Name = "Table3" };
            d.Tables.Add(t);
            t.Columns.Add(new Column { Name = "col3" });
            t.Columns.Add(new Column { Name = "col6" });
        }
    }

}
