using SchoolWebsite.Dtos.EventDto;

namespace SchoolWebsite.Services.Interface
{
    public interface IEventService
    {
        Task CreateAsync(CreateEventDto createEventDto);
        Task EditAsync(EditEventDto editEventDto);
        Task DeleteAsync(int id);
        Task ToggleStatusAsync(int id);
    }
}