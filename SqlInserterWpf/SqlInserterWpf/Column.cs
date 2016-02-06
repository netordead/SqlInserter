using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlInserterWpf
{
    public class Column
    {
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



    }
}
