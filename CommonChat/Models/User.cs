using System.ComponentModel.DataAnnotations;

namespace CommonChat.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(10)] // мах длина имени
        public string? Name { get; set; }

        public virtual ICollection<Message>? FromMessages { get; set; }
        public virtual ICollection<Message>? ToMessages { get; set; }
    }
}
