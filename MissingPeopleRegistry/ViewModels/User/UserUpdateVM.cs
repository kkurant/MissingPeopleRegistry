using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MissingPeopleRegistry.ViewModels.User
{
    public class UserUpdateVM
    {
        public UserVM User { get; set; }
        [Required(ErrorMessage = "Pole rola jest wymagane")]
        [DisplayName("Rola")]
        public string Role { get; set; }
    }

    public class UserVM 
    {
        [Required(ErrorMessage = "Pole imię jest wymagane")]
        [DisplayName("Imię")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Pole nazwisko jest wymagane")]
        [DisplayName("Nazwisko")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Pole email jest wymagane")]
        [DisplayName("Email")]
        public string Email { get; set; }
        public Guid Id { get; set; }
    }
}