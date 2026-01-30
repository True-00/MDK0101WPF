using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop6.Classes
{
    class Phrase
    {
        public string _Phrase { get; set; }
        public string Src { get; set; }

        public Phrase(string phrase, string src)
        {
            this._Phrase = phrase;
            this.Src = src;
        }
    }
}
