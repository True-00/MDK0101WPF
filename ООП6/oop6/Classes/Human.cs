using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace oop6.Classes
{
    abstract class Human
    {
        public string Name { get; set; }

        public string Img { get; set; }

        public Human(string name, string img)
        {
            this.Name = name;
            this.Img = img;
        }

        public abstract void Speak(Label Phrase);
    }
}
