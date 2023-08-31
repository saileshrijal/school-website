using SchoolWebsite.Dtos.EventDto;
using SchoolWebsite.Models;
using SchoolWebsite.Repositories.Interface;
using SchoolWebsite.Services.Interface;

namespace SchoolWebsite.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;
        public EventService(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(CreateEventDto createEventDto)
        {
            var eventObj = new Event()
            {
                Title = createEventDto.Title,
                Location = createEventDto.Location,
                Organizer = createEventDto.Organizer,
                StartDate = createEventDto.StartDate,
                EndDate = createEventDto.EndDate,
                ImageUrl = createEventDto.ImageUrl,
                Description = createEventDto.Description,
                Status = true,
                CreatedDate = DateTime.UtcNow,
            };
            await _unitOfWork.CreateAsync(eventObj);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var eventObj = await _eventRepository.GetByIdAsync(id);
            await _unitOfWork.DeleteAsync(eventObj);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditAsync(EditEventDto editEventDto)
        {
            var eventObj = await _eventRepository.GetByIdAsync(editEventDto.Id);
            eventObj.Title = editEventDto.Title;
            eventObj.Location = editEventDto.Location;
            eventObj.Organizer = editEventDto.Organizer;
            eventObj.StartDate = editEventDto.StartDate;
            eventObj.EndDate = editEventDto.EndDate;
            eventObj.ImageUrl = editEventDto.ImageUrl;
            eventObj.Description = editEventDto.Description;
            await _unitOfWork.UpdateAsync(eventObj);
            await _unitOfWork.SaveAsync();
        }

        public async Task ToggleStatusAsync(int id)
        {
            var eventObj = await _eventRepository.GetByIdAsync(id);
            eventObj.Status = !eventObj.Status;
            await _unitOfWork.UpdateAsync(eventObj);
            await _unitOfWork.SaveAsync();
        }
    }
}