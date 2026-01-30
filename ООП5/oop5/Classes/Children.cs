using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop5.Classes
{
    class Children : Shop
    {
        public int Age;

        public Children(string name, int price, int age) :
            base(name, price)
        {
            this.Age = age;
        }
    }
}
