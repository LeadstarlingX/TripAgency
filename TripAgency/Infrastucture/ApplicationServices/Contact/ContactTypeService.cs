

using Application.DTOs.Common;
using Application.DTOs.Contact;
using Application.IApplicationServices.Contact;
using Application.IReositosy;
using Domain.Enum;
using Domain.Entities.ApplicationEntities;

namespace Infrastructure.Services.ServicesImplementation
{
    public class ContactTypeService : IContactTypeService
    {
        private readonly IAppRepository<ContactType> _contactTypeRepository;

        public ContactTypeService(IAppRepository<ContactType> contactTypeRepository)
        {
            _contactTypeRepository = contactTypeRepository;
        }

        public async Task<ContactTypeDto> CreateContactAsync(CreateContactTypeDto createContactTypeDto)
        {
            
            var existingContactType =  _contactTypeRepository.Find(ct => ct.Type == createContactTypeDto.Type).FirstOrDefault();

            if (existingContactType is not null)
            {
                throw new Exception("This contact type already exists!");
            }

            var contactType = await _contactTypeRepository.InsertAsync(new ContactType { Type = createContactTypeDto.Type });
            return new ContactTypeDto() { Id = contactType.Id, Type = contactType.Type };
        }

        public async Task DeleteContactAsync(BaseDto<int> baseDto)
        {
            var contactTypeToDelete =  _contactTypeRepository.Find(c => c.Id == baseDto.Id).FirstOrDefault();

            if (contactTypeToDelete is null)
            {
                throw new Exception("This contact type does not exist");
            }

            await _contactTypeRepository.RemoveAsync(contactTypeToDelete);
        }

        public async Task<ContactTypesDto> GetContactTypesAsync() 
        {
            var contactTypesList = await _contactTypeRepository.FindAsync(c => c.Id != 0);
            var contactTypeDtos = contactTypesList.Select(ct => new ContactTypeDto
            {
                Id = ct.Id,
                Type = ct.Type
            }).ToList();

            return new ContactTypesDto
            {
                ContactTypes = contactTypeDtos
            };
        }

        public async Task<ContactType> GetContactTypeByIdAsync(int id)
        {
            var contactType = (await _contactTypeRepository.FindAsync(c => c.Id == id)).FirstOrDefault();

            return (contactType is null) ? throw new Exception("This contact type does not exist") : contactType;
        }

        public async Task UpdateContactAsync(UpdateContactTypeDto updateContactTypeDto)
        {

            var contactTypeToUpdate =await GetContactTypeByIdAsync(updateContactTypeDto.Id);

            if (contactTypeToUpdate is null)
                throw new Exception("This contact type does not exist");            

            if (contactTypeToUpdate.Type != updateContactTypeDto.Type)
            {
                contactTypeToUpdate.Type = updateContactTypeDto.Type;
                contactTypeToUpdate = await _contactTypeRepository.UpdateAsync(contactTypeToUpdate);
            }
        }

        public async Task<ContactType> GetContactByTypeAsync(ContactTypeEnum contactType)
        {
            return (await _contactTypeRepository.FindAsync(c => c.Type == contactType)).FirstOrDefault()
                ??throw new Exception("This contact type does not exist");
        }
    }
}
