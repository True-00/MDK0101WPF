using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace oop6.Classes
{
    public abstract class Human
    {
        public string Name { get; set; }
        public string Img { get; set; }

        public Human(string name, string img)
        {
            Name = name;
            Img = img;
        }

        public abstract void Speak(Label phraseLabel);
    }
}
