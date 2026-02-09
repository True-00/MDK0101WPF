using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop7.Interfaces;
using oop7.Models;

namespace oop7.Classes
{
    internal class MessagesContext : IMessages
    {
        private List<Messages> messages = new List<Messages>();
        private int nextId = 1;

        public MessagesContext()
        {
            AddMessage(new Messages(0, 1, "Привет! Как дела?", DateTime.Now.AddMinutes(-30)));
            AddMessage(new Messages(0, 2, "Добрый день!", DateTime.Now.AddMinutes(-25)));
            AddMessage(new Messages(0, 1, "Что нового?", DateTime.Now.AddMinutes(-20)));
        }

        public List<Messages> GetAllMessages()
        {
            return messages.OrderByDescending(m => m.Date).ToList();
        }

        public Messages GetMessageById(int id)
        {
            return messages.FirstOrDefault(m => m.Id == id);
        }

        public void AddMessage(Messages message)
        {
            message.Id = nextId++;
            message.Date = DateTime.Now;
            messages.Add(message);
        }

        public void UpdateMessage(Messages message)
        {
            var existingMessage = GetMessageById(message.Id);
            if (existingMessage != null)
            {
                existingMessage.Message = message.Message;
                existingMessage.ImagePath = message.ImagePath;
            }
        }

        public void DeleteMessage(int id)
        {
            var message = GetMessageById(id);
            if (message != null)
            {
                messages.Remove(message);
            }
        }

        public List<Messages> GetMessagesByUserId(int userId)
        {
            return messages.Where(m => m.UserId == userId)
                          .OrderByDescending(m => m.Date)
                          .ToList();
        }
    }
}
