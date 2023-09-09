using System.ComponentModel.DataAnnotations;

namespace CopiloDemoWebApp.Models
{
    public class CategoryModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required"), 
         MaxLength(50, ErrorMessage = "You exceeded 50 characters."), 
         MinLength(3, ErrorMessage = "Name must have atleast 3 characters.")]
        public required string Name { get; set; }
        public string? Description { get; set; }


    }
}
