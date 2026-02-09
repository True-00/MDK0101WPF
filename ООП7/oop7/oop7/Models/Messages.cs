using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oop7.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string ImagePath { get; set; }

        public Messages(int id, int userId, string message, DateTime date, string imagePath = null)
        {
            Id = id;
            UserId = userId;
            Message = message;
            Date = date;
            ImagePath = imagePath;
        }

        public override string ToString()
        {
            return $"{Date:HH:mm} - {Message}";
        }
    }
}
