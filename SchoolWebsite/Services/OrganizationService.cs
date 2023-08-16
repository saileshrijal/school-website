using SchoolWebsite.Dtos;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationService(IOrganizationRepository organizationRepository, IUnitOfWork unitOfWork)
        {
            _organizationRepository = organizationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(OrganizationDto organizationDto)
        {
            var organization = new Organization()
            {
                Name = organizationDto.Name,
                ShortName = organizationDto.ShortName,
                Address = organizationDto.Address,
                Phone = organizationDto.Phone,
                Email = organizationDto.Email,
                ContactPerson = organizationDto.ContactPerson,
                PanNumber = organizationDto.PanNumber,
                FacebookUrl = organizationDto.FacebookUrl,
                TwitterUrl = organizationDto.TwitterUrl,
                InstagramUrl = organizationDto.InstagramUrl,
                YoutubeUrl = organizationDto.YoutubeUrl,
                Description = organizationDto.Description,
                LogoUrl = organizationDto.LogoUrl,
            };
            await _unitOfWork.CreateAsync(organization);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(OrganizationDto organizationDto)
        {
            var organization = await _organizationRepository.GetByIdAsync(organizationDto.Id);
            organization.Name = organizationDto.Name;
            organization.ShortName = organizationDto.ShortName;
            organization.Address = organizationDto.Address;
            organization.Phone = organizationDto.Phone;
            organization.Email = organizationDto.Email;
            organization.ContactPerson = organizationDto.ContactPerson;
            organization.PanNumber = organizationDto.PanNumber;
            organization.FacebookUrl = organizationDto.FacebookUrl;
            organization.TwitterUrl = organizationDto.TwitterUrl;
            organization.InstagramUrl = organizationDto.InstagramUrl;
            organization.YoutubeUrl = organizationDto.YoutubeUrl;
            organization.Description = organizationDto.Description;
            organization.LogoUrl = organizationDto.LogoUrl;
            await _unitOfWork.SaveAsync();
        }
    }
}