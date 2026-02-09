using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop7.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Img { get; set; }

        public Users(int id, string name, string surname, string phone, string img)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Phone = phone;
            Img = img;
        }

        public string FullName => $"{Name} {Surname}";
    }
}
