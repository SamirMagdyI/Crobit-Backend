using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AG.Models
{
    public class MapLink
    {
        [Key]
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string link { get; set; }

      
    }
}
