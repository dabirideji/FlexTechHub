namespace InventoryManagement.Application.Interfaces
{
    public interface IGenericService<T, TCreateDto, TUpdateDto, TReadDto>
    {
        Task<IEnumerable<TReadDto>> GetAllAsync();
        Task<TReadDto> GetByIdAsync(Guid id);
        Task AddAsync(TCreateDto createDto);
        Task UpdateAsync(Guid id, TUpdateDto updateDto);
        Task DeleteAsync(Guid id);
    }
}

