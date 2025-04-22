using Application.DTOs.Common;
using Application.DTOs.Contact;
using DataAccessLayer.Enum;
using Domain.Entities;
using Domain.Entities.ApplicationEntities;

namespace Application.IApplicationServices.Contact
{
    public interface IContactTypeService
    {
        Task<ContactTypeDto> CreateContactAsync(CreateContactTypeDto createAddressDto);
        Task UpdateContactAsync(UpdateContactTypeDto updateAddressDto);
        Task DeleteContactAsync(BaseDto<int> baseDto);
        Task<ContactTypesDto> GetContactTypesAsync();
        Task<ContactType> GetContactTypeByIdAsync(int id);
        Task<ContactType> GetContactByTypeAsync(ContactTypeEnum contactType);
    }
}
