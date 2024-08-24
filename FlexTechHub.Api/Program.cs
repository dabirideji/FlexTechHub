var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddDbContext<InventoryManagementDbContext>(options =>
//    options.UseSqlite(builder.Configuration.GetConnectionString("OFFLINE"),
//        b => b.MigrationsAssembly("FlexTechHub.Api")));

builder.Services.AddDbContext<InventoryManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ONLINE"),
        b => b.MigrationsAssembly("FlexTechHub.Api")));

// Register Unit of Work and other services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeacherCourseService, TeacherCourseService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));
builder.Services.AddScoped<IProductService, ProductService>();

// Register Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlexTechHub API", Version = "v1" });
});

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Use Middleware
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlexTechHub API v1"));
app.UseStaticFiles();
app.UseHttpsRedirection();

// Use CORS policy
app.UseCors("AllowAll"); // Apply the CORS policy before any other middleware

app.UseAuthorization();
app.UseMiddleware<ResponseWrapperMiddleware>();

app.MapControllers();

app.Run();
