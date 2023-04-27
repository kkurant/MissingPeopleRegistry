using Microsoft.AspNetCore.Identity;

namespace MissingPeopleRegistry.Models
{
    public class User: IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<MissingPerson> MissingPeople { get; set; }
        public override string ToString()
        {
            return Name + " " + LastName;
        }

        public bool Blocked { get; set; }
    }

    public enum Role
    {
        User,
        Admin
    }
}