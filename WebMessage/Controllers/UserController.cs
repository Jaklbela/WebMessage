using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebMessage.Enums;
using WebMessage.Models;
using WebMessage.Repositories;

namespace WebMessage.Controllers
{
    /// <summary>
    /// Контроллер с запросами для пользователей.
    /// </summary>
    [Route("/api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Получение списка всех пользователей.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        [HttpGet]
        public IEnumerable<User> Get() => _userRepository.ReadUsers();

        /// <summary>
        /// Получение данных о пользователе по идентификатору(адресу почты).
        /// </summary>
        /// <param name="email">Почта пользователя.</param>
        /// <returns>Результат операции.</returns>
        [HttpGet("id")]
        public IActionResult Get([Required] string email)
        {
            User user = UserRepository.Users.SingleOrDefault(p => p.Email == email);

            if (user == null)
            {
                return new NotFoundObjectResult("This user does not exist.");
            }
            return new OkObjectResult(user);
        }

        /// <summary>
        /// Добавление нового пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="email">Почта пользователя.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost("createuser")]
        public IActionResult Create([Required] string userName, [Required] string email)
        {
            if (UserRepository.Users.Any(_ => _.Email == email))
                return new ConflictObjectResult("User with this email already exists.");
            User user = new(userName, email);
            UserRepository.Users.Add(user);
            _userRepository.WriteUsers();
            return new OkObjectResult("User was created successfully!");
        }

        /// <summary>
        /// Конструктор для объявления объектов интерфейса.
        /// </summary>
        /// <param name="userRepository"></param>
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
    }
}
