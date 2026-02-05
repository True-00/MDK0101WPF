using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop6.Classes
{
    public class Phrase
    {
        public string Text { get; set; }
        public string AudioPath { get; set; }

        public Phrase(string text, string audioPath)
        {
            Text = text;
            AudioPath = audioPath;
        }
    }
}
