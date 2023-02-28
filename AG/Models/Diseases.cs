using System.ComponentModel.DataAnnotations;

namespace AG.Models
{
    public class Diseases
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SugestedTreatment { get; set; }
    }
}