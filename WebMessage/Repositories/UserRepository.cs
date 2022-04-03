using System.Collections.Generic;
using WebMessage.Models;
using System.IO;
using System.Text.Json;
using System;
using WebMessage.Enums;
using System.Linq;

namespace WebMessage.Repositories
{
    /// <summary>
    /// Репозиторий пользователей.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// Список пользователей.
        /// </summary>
        public static List<User> Users = new();

        /// <summary>
        /// Сериализация пользователей в json-файл.
        /// </summary>
        public void GetUsers()
        {
            Users = CreateUsers();
            WriteUsers();
        }

        /// <summary>
        /// Сериализация пользователей в json-файл.
        /// </summary>
        public void WriteUsers()
        {
            StreamWriter writer = new("users.json", false);
            string usersJson = JsonSerializer.Serialize(Users);
            writer.Write(usersJson);
            writer.Close();
        }

        /// <summary>
        /// Чтение пользователей из json-файла.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        public List<User> ReadUsers()
        {
            StreamReader reader = new("users.json");
            string usersJson = reader.ReadToEnd();
            Users = JsonSerializer.Deserialize<List<User>>(usersJson);
            reader.Close();
            return Users;
        }

        /// <summary>
        /// Создание списка пользователей.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        public static List<User> CreateUsers()
        {
            Random generator = new();
            List<User> userList = new();

            int quantity = generator.Next(4, 11);
            var emails = Enum.GetValues(typeof(Emails)).OfType<Enum>().OrderBy(_ => Guid.NewGuid()).Take(quantity).ToList();

            for (int i = 0; i < quantity; i++)
            {
                string name = Enum.GetValues(typeof(Names)).OfType<Enum>().OrderBy(_ => Guid.NewGuid()).FirstOrDefault().ToString();
                string email = $"{emails[i]}@edu.hse.ru";
                userList.Add(new User(name, email));
                userList = userList.OrderBy(_ => _.Email).ToList();
            }
            return userList;
        }
    }
}
