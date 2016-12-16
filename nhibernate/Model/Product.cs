using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nhibernate.Model
{
    public class Product
    {
        public virtual int id { get; set; }
        public virtual string name { get; set; }
        public virtual string code { get; set; }
    }
}
