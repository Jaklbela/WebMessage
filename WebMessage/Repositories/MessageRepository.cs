using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using WebMessage.Enums;
using WebMessage.Models;

namespace WebMessage.Repositories
{
    /// <summary>
    /// Репозиторий сообщений.
    /// </summary>
    public class MessageRepository : IMessageRepository
    {
        /// <summary>
        /// Список сообщений.
        /// </summary>
        public static List<MessageInfo> Messages = new();

        /// <summary>
        /// Получение и сериализация сообщений в json-файл.
        /// </summary>
        public void GetMessages()
        {
            Messages = CreateMessages();
            WriteMessages();
        }

        /// <summary>
        /// Сериализация сообщений в json-файл.
        /// </summary>
        public void WriteMessages()
        {
            StreamWriter writer = new("messages.json", false);
            string messagesJson = JsonSerializer.Serialize(Messages);
            writer.Write(messagesJson);
            writer.Close();
        }

        /// <summary>
        /// Чтение сообщенийиз json-файла.
        /// </summary>
        /// <returns>Список сообщений.</returns>
        public List<MessageInfo> ReadMessages()
        {
            StreamReader reader = new("messages.json");
            string messagesJson = reader.ReadToEnd();
            Messages = JsonSerializer.Deserialize<List<MessageInfo>>(messagesJson);
            reader.Close();
            return Messages;
        }

        /// <summary>
        /// Создание списка сообщений.
        /// </summary>
        /// <returns>Список сообщений.</returns>
        public static List<MessageInfo> CreateMessages()
        {
            Random generator = new();
            int quantity = generator.Next(4, 11);
            int words = generator.Next(2, 8);
            List<MessageInfo> messageList = new();
            for (int i = 0; i < quantity; i++)
            {
                string subject = Enum.GetValues(typeof(Subjects)).OfType<Enum>().OrderBy(_ => Guid.NewGuid()).FirstOrDefault().ToString();
                var messages = Enum.GetValues(typeof(Messages)).OfType<Enum>().OrderBy(_ => Guid.NewGuid()).Take(words).ToList();
                string message = "";
                foreach (var word in messages) message += word.ToString();
                string senderId = UserRepository.Users[generator.Next(UserRepository.Users.Count)].Email;
                string receiverId = UserRepository.Users[generator.Next(UserRepository.Users.Count)].Email;
                messageList.Add(new MessageInfo(subject, message, senderId, receiverId));
            }
            return messageList;
        }
    }
}
