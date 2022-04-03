using System.Collections.Generic;
using WebMessage.Models;

namespace WebMessage.Repositories
{
    /// <summary>
    /// Интерфейс, хранящий абстрактные методы для репозитория соообщенийй.
    /// </summary>
    public interface IMessageRepository
    {
        /// <summary>
        /// Сериализация сообщений в json-файл.
        /// </summary>
        abstract void GetMessages();

        /// <summary>
        /// Сериализация сообщений в json-файл.
        /// </summary>
        abstract void WriteMessages();

        /// <summary>
        /// Чтение сообщений из json-файла.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        abstract List<MessageInfo> ReadMessages();
    }
}
