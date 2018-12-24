using System.ComponentModel.DataAnnotations;
using Web.Domain;

namespace Web.Models
{
    public class KitViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter a manufacturer")]
        public string Manufacturer { get; set; }
        [Required(ErrorMessage = "Please enter item")]
        public string Item { get; set; }
        [Required(ErrorMessage = "Please enter size")]
        public Size Size { get; set; }
        [Required(ErrorMessage = "Please define ImageUrl")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Please pick kit type")]
        public KitType KitType { get; set; }
    }
}
