using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AG.Models
{
    public class deviceTokenModel
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
