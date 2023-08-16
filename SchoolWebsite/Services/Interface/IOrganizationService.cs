using SchoolWebsite.Dtos;

namespace SchoolWebsite.Services.Interface
{
    public interface IOrganizationService
    {
        Task CreateAsync(OrganizationDto organizationDto);
        Task EditAsync(OrganizationDto organizationDto);
    }
}