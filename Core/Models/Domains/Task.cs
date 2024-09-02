using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Core.Models.Domains
{
    public class Task
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Pole tytuł jest wymagane")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Pole kategoria jest wymagane")]
        [Display(Name = "Kategoria")]
        public string Description { get; set; }

        public int CategoryId { get; set; }

        [Display(Name = "Termin")]
        public DateTime? Term { get; set; }

        [Display(Name = "Zrealizowany")]
        public bool IsExecuted { get; set; }

        public string UserId { get; set; }

        public Category Category { get; set; }

        public ApplicationUser User { get; set; }
    }
}
