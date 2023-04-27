using System.ComponentModel.DataAnnotations;

namespace MissingPeopleRegistry.Models
{
    public class MissingPerson
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public DateTime MissingDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public Guid? LastModifierId { get; set; }
    }
}