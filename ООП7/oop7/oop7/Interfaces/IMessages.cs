using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop7.Models;

namespace oop7.Interfaces
{
    public interface IMessages
    {
        List<Messages> GetAllMessages();
        Messages GetMessageById(int id);
        void AddMessage(Messages message);
        void UpdateMessage(Messages message);
        void DeleteMessage(int id);
        List<Messages> GetMessagesByUserId(int userId);
    }
}
