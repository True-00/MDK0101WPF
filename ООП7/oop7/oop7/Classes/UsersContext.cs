using oop7.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using oop7.Models;
using oop7.Interfaces;

namespace oop7.Classes
{
    internal class UsersContext : IUsers
    {
        private List<Users> users = new List<Users>();
        private int nextId = 1;

        public UsersContext()
        {
            AddUser(new Users(0, "Иван", "Иванов", "+7-999-123-45-67", "Images/user1.jpg"));
            AddUser(new Users(0, "Мария", "Петрова", "+7-999-987-65-43", "Images/user2.jpg"));
            AddUser(new Users(0, "Алексей", "Сидоров", "+7-999-555-44-33", "Images/user3.jpg"));
        }

        public List<Users> GetAllUsers()
        {
            return users;
        }

        public Users GetUserById(int id)
        {
            return users.FirstOrDefault(u => u.Id == id);
        }

        public void AddUser(Users user)
        {
            user.Id = nextId++;
            users.Add(user);
        }

        public void UpdateUser(Users user)
        {
            var existingUser = GetUserById(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Surname = user.Surname;
                existingUser.Phone = user.Phone;
                existingUser.Img = user.Img;
            }
        }

        public void DeleteUser(int id)
        {
            var user = GetUserById(id);
            if (user != null)
            {
                users.Remove(user);
            }
        }
    }
}
