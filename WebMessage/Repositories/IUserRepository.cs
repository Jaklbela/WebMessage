using System.Collections.Generic;
using WebMessage.Models;

namespace WebMessage.Repositories
{
    /// <summary>
    /// Интерфейс, хранящий абстрактные методы для репозитория пользователей.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получение и сериализация пользователей в json-файл.
        /// </summary>
        abstract void GetUsers();

        /// <summary>
        /// Сериализация пользователей в json-файл.
        /// </summary>
        abstract void WriteUsers();

        /// <summary>
        /// Чтение пользователей из json-файла.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        abstract List<User> ReadUsers();
    }
}
