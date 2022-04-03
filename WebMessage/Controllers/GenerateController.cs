using Microsoft.AspNetCore.Mvc;
using WebMessage.Repositories;

namespace WebMessage.Controllers
{
    /// <summary>
    /// Контроллер для генерации пользователей и сообщений.
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    public class GenerateController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;

        /// <summary>
        /// Конструктор для объявления объектов интерфейса.
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей.</param>
        /// <param name="messageRepository">Репозиторий сообщений.</param>
        public GenerateController(IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        /// <summary>
        /// Генерация пользователей и сообщений.
        /// </summary>
        /// <returns>Результат операции.</returns>
        [HttpPost]
        public IActionResult GenerateAll()
        {
            _userRepository.GetUsers();
            _messageRepository.GetMessages();
            return new OkObjectResult("Users and messages were created successfully!");
        }
    }
}
