using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlInserterWpf
{
    public class DataBase
    {
        public DataBase(string name)
        {
            this.name = name;
            this.Tables = new TableList();
        }
        public DataBase()
        {

            this.Tables = new TableList();
        }

        string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name == value) { return; }
                this.name = value;
            
            }
        }


        public TableList Tables
        {
            get;
            set;
        }
    }
}
