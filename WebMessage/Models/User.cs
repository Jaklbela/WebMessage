using System.ComponentModel.DataAnnotations;

namespace WebMessage.Models
{
    /// <summary>
    /// Модель пользователя.
    /// </summary>
    /// <param name="UserName">Никнейм пользователя.</param>
    /// <param name="Email">Электронная почта пользователя.</param>
    public record User([Required] string UserName, [Required] string Email);
}
