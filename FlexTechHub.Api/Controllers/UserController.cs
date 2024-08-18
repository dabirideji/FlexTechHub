namespace InventoryManagement.Api.Controllers
{
        public class UserController : GenericController<User, CreateUserDto, UpdateUserDto, ReadUserDto>
    {
        private readonly IUserService _service;

        public UserController(IUserService service) : base(service)
        {
            this._service = service;
        }
        [HttpPost]
        public async Task<ActionResult> Login(UserLoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _service.UserLogin(dto);
            if (result is null)
            {
                return BadRequest(dto);
            }
            return Ok(result);
        }

    }

}
