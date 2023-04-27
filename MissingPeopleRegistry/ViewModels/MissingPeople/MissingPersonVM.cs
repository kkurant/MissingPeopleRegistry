using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MissingPeopleRegistry.ViewModels.MissingPeople
{
    public class MissingPersonVM
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Pole imię jest wymagane")]
        [DisplayName("Imię osoby zaginionej")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Pole nazwisko jest wymagane")]
        [DisplayName("Nazwisko osoby zaginionej")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Pole płeć jest wymagane")]
        [DisplayName("Płeć osoby zaginionej")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Pole wiek jest wymagane")]
        [DisplayName("Wiek osoby zaginionej")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Pole opis osoby zaginionej jest wymagane")]
        [DisplayName("Opis osoby zaginionej")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Pole daty zaginięcia jest wymagane")]
        [DisplayName("Data zaginięcia")]
        public DateTime MissingDate { get; set; }
        [Required(ErrorMessage = "Pole miasto zaginięcia jest wymagane")]
        [DisplayName("Miasto")]
        public string City { get; set; }
        [Required(ErrorMessage = "Pole kraj jest wymagane")]
        [DisplayName("Kraj")]
        public string Country { get; set; }
        public string LastModifierName { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}