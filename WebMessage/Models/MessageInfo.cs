using System.ComponentModel.DataAnnotations;

namespace WebMessage.Models
{
    /// <summary>
    /// Модель сообщения.
    /// </summary>
    /// <param name="Subject">Тема сообщения.</param>
    /// <param name="Meesage">Текст сообщения.</param>
    /// <param name="SenderId">ID отправителя.</param>
    /// <param name="ReceiverId">ID получателя.</param>
    public record MessageInfo([Required] string Subject, [Required] string Meesage, [Required] string SenderId, [Required] string ReceiverId);
}
