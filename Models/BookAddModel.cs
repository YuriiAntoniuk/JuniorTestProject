using System.ComponentModel.DataAnnotations;

namespace JuniorTestProject.Models
{
    public class BookAddModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
    }
}
