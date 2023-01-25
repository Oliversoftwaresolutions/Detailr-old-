using System.ComponentModel.DataAnnotations;

namespace Detailr.MVC.Models
{
    public class CreateBusinessVM
    {
        [Required]
        public string Name { get; set; }

        [Display(Name = "A little bit About your Business")]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
