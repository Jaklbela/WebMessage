using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebMessage.Models;
using WebMessage.Repositories;

namespace WebMessage.Controllers
{
    /// <summary>
    /// Контроллер с запросами для сообщений.
    /// </summary>
    [Route("/api/[controller]")]
    public class MessageController : Controller
    {
        private readonly IMessageRepository _messageRepository;

        /// <summary>
        /// Получение списка всех сообщений.
        /// </summary>
        /// <returns>Список сообщений.</returns>
        [HttpGet]
        public IEnumerable<MessageInfo> Get() => _messageRepository.ReadMessages();

        /// <summary>
        /// Получение списка сообщений по идентификаторам(адресу почты) отправителя и получателя.
        /// </summary>
        /// <param name="sender">Почта отправителя.</param>
        /// <param name="receiver">Почта получателя.</param>
        /// <returns></returns>
        [HttpGet("sender_and_receiver")]
        public IActionResult Get([Required] string sender, [Required] string receiver)
        {
            List<MessageInfo> messages = MessageRepository.Messages.Where(p => p.SenderId == sender && p.ReceiverId == receiver).ToList();

            if (messages.Count == 0 || messages == null)
            {
                return new NotFoundObjectResult("There is no such messages.");
            }
            return new OkObjectResult(messages);
        }

        /// <summary>
        /// Получение списка сообщений по идентификатору(адресу почты) отправителя.
        /// </summary>
        /// <param name="sender">Почта отправителя.</param>
        /// <returns>Результат операции.</returns>
        [HttpGet("sender")]
        public IActionResult GetBySender([Required] string sender)
        {
            List<MessageInfo> messages = MessageRepository.Messages.Where(p => p.SenderId == sender).ToList();

            if (messages.Count == 0 || messages == null)
            {
                return new NotFoundObjectResult("There is no such messages.");
            }
            return new OkObjectResult(messages);
        }

        /// <summary>
        /// Получение списка сообщений по идентификатору(адресу почты) получателя.
        /// </summary>
        /// <param name="receiver">Почта получателя.</param>
        /// <returns>Результат операции.</returns>
        [HttpGet("receiver")]
        public IActionResult GetByReceiver([Required] string receiver)
        {
            List<MessageInfo> messages = MessageRepository.Messages.Where(p => p.ReceiverId == receiver).ToList();

            if (messages.Count == 0 || messages == null)
            {
                return new NotFoundObjectResult("There is no such messages.");
            }
            return new OkObjectResult(messages);
        }

        /// <summary>
        /// Добавление нового сообщения.
        /// </summary>
        /// <param name="subject">Тема сообщения</param>
        /// <param name="text">Текст сообщения</param>
        /// <param name="senderId">Почта отправителя.</param>
        /// <param name="receiverId">Почта получателя.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost("createmessage")]
        public IActionResult Create(string subject, [Required] string text, [Required] string senderId, [Required] string receiverId)
        {
            if (!UserRepository.Users.Any(_ => _.Email == senderId))
                return new ConflictObjectResult("Sender does not exist.");
            if (!UserRepository.Users.Any(_ => _.Email == receiverId))
                return new ConflictObjectResult("Receiver does not exist.");
            if (subject == null)
            {
                MessageInfo messageNull = new("", text, senderId, receiverId);
                MessageRepository.Messages.Add(messageNull);
                _messageRepository.WriteMessages();
                return new OkObjectResult("Message was sended successfully!");
            }
            MessageInfo message = new(subject, text, senderId, receiverId);
            MessageRepository.Messages.Add(message);
            _messageRepository.WriteMessages();
            return new OkObjectResult("Message was sended successfully!");
        }

        /// <summary>
        /// Конструктор для объявления объектов интерфейса.
        /// </summary>
        /// <param name="messageRepository">Репозиторий сообщений.</param>
        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
    }
}
