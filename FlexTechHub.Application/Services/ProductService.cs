using AutoMapper;
using InventoryManagement.Application.DTO.Product.Request;
using InventoryManagement.Application.DTO.Product.Response;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Application.Interfaces.GenericRepositoryPattern;
using InventoryManagement.Domain.Models;

namespace InventoryManagement.Application.Services.Customer
{
    public class ProductService : GenericService<Product, CreateProductDto, UpdateProductDto, ReadProductDto>, IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> ProductExistsAsync(string name)
        {
            var products = await _unitOfWork.GetRepository<Product>().GetAllAsync();
            return products.Any(p => p.Name == name);
        }

        public override async Task AddAsync(CreateProductDto createDto)
        {
            if (await ProductExistsAsync(createDto.Name))
            {
                throw new InvalidOperationException("Product already exists.");
            }

            await base.AddAsync(createDto);
        }
    }

}
