using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;

namespace SqlInserterWpf
{
 

    public class Table
    {
        public Table(string name)
        {
            this.name = name;
            Columns = new ColumnList();
        }
        public Table()
        {
            Columns = new ColumnList();
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

        public ColumnList Columns
        {
            get;
            set;
        }
    }

}
