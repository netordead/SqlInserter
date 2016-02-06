using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlInserterWpf
{
    public class SQLServer
    {
        List<DataBase> dataBases = new List<DataBase>();

        public List<DataBase> DataBases
        {
            get { return dataBases; }
            set { dataBases = value; }
        }

    }
}
