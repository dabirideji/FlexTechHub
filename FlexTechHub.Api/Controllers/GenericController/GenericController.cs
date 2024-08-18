namespace InventoryManagement.Api.Controllers

{

    [ApiController]
    [Route("api/[controller]/[action]")]

    public abstract class GenericController<T, TCreateDto, TUpdateDto, TReadDto> : ControllerBase
            where T : class
    {
        private readonly IGenericService<T, TCreateDto, TUpdateDto, TReadDto> _service;

        protected GenericController(IGenericService<T, TCreateDto, TUpdateDto, TReadDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TCreateDto createDto)
        {
            await _service.AddAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = createDto }, createDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TUpdateDto updateDto)
        {
            await _service.UpdateAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }

}

