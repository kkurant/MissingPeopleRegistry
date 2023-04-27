using Microsoft.AspNetCore.Identity;
using MissingPeopleRegistry.Models;
using MissingPeopleRegistry.Repositories;
using MissingPeopleRegistry.ViewModels.MissingPeople;

namespace MissingPeopleRegistry.Services
{
    public class MissingPeopleService : IMissingPeopleService
    {
        private readonly IMissingPeopleRepository _missingPeopleRepository;
        private readonly UserManager<User> _userManager;

        public MissingPeopleService(IMissingPeopleRepository missingPeopleRepository, UserManager<User> userManager)
        {
            _missingPeopleRepository = missingPeopleRepository;
            _userManager = userManager;
        }
        public async Task CreateAsync(MissingPersonVM viewmodel, Guid userId)
        {
            var missingPerson = new MissingPerson
            {
                Name = viewmodel.Name,
                LastName = viewmodel.LastName,
                Age = viewmodel.Age,
                Gender = viewmodel.Gender,
                Description = viewmodel.Description,
                MissingDate = viewmodel.MissingDate,
                City = viewmodel.City,
                Country = viewmodel.Country,
                UserId = userId,
                CreateDate = DateTime.Now
            };

            await _missingPeopleRepository.CreateAsync(missingPerson);
        }

        public async Task UpdateAsync(MissingPersonVM viewmodel, Guid userId)
        {
            var missingPerson = await _missingPeopleRepository.GetMissingPersonById(viewmodel.Id);

            missingPerson.Name = viewmodel.Name;
            missingPerson.LastName = viewmodel.LastName;
            missingPerson.Age = viewmodel.Age;
            missingPerson.Gender = viewmodel.Gender;
            missingPerson.Description = viewmodel.Description;
            missingPerson.MissingDate = viewmodel.MissingDate;
            missingPerson.City = viewmodel.City;
            missingPerson.Country = viewmodel.Country;
            missingPerson.ModifyDate = DateTime.Now;
            missingPerson.LastModifierId = userId;

            await _missingPeopleRepository.UpdateAsync(missingPerson);
        }

        public async Task<IEnumerable<MissingPersonVM>> GetAllMissingPeople()
        {
            var missingPeople = await _missingPeopleRepository.GetAllMissingPeople();
            return missingPeople.Select(x => new MissingPersonVM
            {
                Id = x.Id,
                Name = x.Name,
                LastName = x.LastName,
                Age = x.Age,
                Gender = MapGender(x.Gender),
                Description = x.Description,
                MissingDate = x.MissingDate,
                City = x.City,
                Country = x.Country
            });
        }

        public async Task<MissingPersonVM> GetMissingPersonById(Guid id)
        {
            var missingPerson = await _missingPeopleRepository.GetMissingPersonById(id);
            var model = new MissingPersonVM
            {
                Id = missingPerson.Id,
                Name = missingPerson.Name,
                LastName = missingPerson.LastName,
                Age = missingPerson.Age,
                Gender = missingPerson.Gender,
                Description = missingPerson.Description,
                MissingDate = missingPerson.MissingDate,
                City = missingPerson.City,
                Country = missingPerson.Country,
                CreateDate = missingPerson.CreateDate,
                ModifyDate = missingPerson.ModifyDate,
                AuthorName = missingPerson.User.ToString()
            };

            if (missingPerson.LastModifierId.HasValue)
            {
                var user = (await _userManager.FindByIdAsync(missingPerson.LastModifierId.ToString()));
                if(user != null)
                {
                    model.LastModifierName = user.ToString();
                }
                else
                {
                    model.LastModifierName = "użytkownika, który nie istnieje już w systemie";
                }
                
            }

            return model;
        }

        public async Task DeleteMissingPersonById(Guid id)
        {
            await _missingPeopleRepository.DeleteMissingPersonById(id);
        }

        private string MapGender(string gender)
        {
            if(gender == "Male")
            {
                return "Mężczyzna";
            }

            return "Kobieta";
        }
    }
}