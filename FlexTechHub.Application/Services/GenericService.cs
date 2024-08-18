using AutoMapper;
using InventoryManagement.Application.Interfaces.GenericRepositoryPattern;
using InventoryManagement.Application.Interfaces;

namespace InventoryManagement.Application.Services.Customer
{
    public class GenericService<T, TCreateDto, TUpdateDto, TReadDto> : IGenericService<T, TCreateDto, TUpdateDto, TReadDto> where T : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<T> _repository;

        public GenericService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetRepository<T>();
        }

        public virtual async Task<IEnumerable<TReadDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<TReadDto>>(entities);
        }

        public virtual async Task<TReadDto> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<TReadDto>(entity);
        }

        public virtual async Task AddAsync(TCreateDto createDto)
        {
            var entity = _mapper.Map<T>(createDto);
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task UpdateAsync(Guid id, TUpdateDto updateDto)
        {
            var entity = await _repository.GetByIdAsync(id);
            _mapper.Map(updateDto, entity);
            await _repository.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}


