// // var builder = WebApplication.CreateBuilder(args);

// // // Add services to the container.

// // builder.Services.AddControllers();
// // // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// // builder.Services.AddEndpointsApiExplorer();
// // builder.Services.AddSwaggerGen();

// // var app = builder.Build();

// // // Configure the HTTP request pipeline.
// // if (app.Environment.IsDevelopment())
// // {
// //     app.UseSwagger();
// //     app.UseSwaggerUI();
// // }

// // app.UseHttpsRedirection();

// // app.UseAuthorization();

// // app.MapControllers();

// // app.Run();















// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddControllers();
// builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// // builder.Services.AddDbContext<InventoryManagementDbContext>(options =>
// //     options.UseSqlite(builder.Configuration.GetConnectionString("OFFLINE"),
// //         b => b.MigrationsAssembly("FlexTechHub.Api")));


// builder.Services.AddDbContext<InventoryManagementDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("ONLINE"),
//         b => b.MigrationsAssembly("FlexTechHub.Api")));

// // Register Unit of Work
// builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// builder.Services.AddScoped<IUserService, UserService>();
// builder.Services.AddScoped<ITeacherCourseService, TeacherCourseService>();
// builder.Services.AddScoped<ICourseService, CourseService>();

// // Register Generic Repositories
// builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// // Register Generic Services
// builder.Services.AddScoped(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));

// // Register Specific Services
// builder.Services.AddScoped<IProductService, ProductService>();

// // Register Swagger
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlexTechHub API", Version = "v1" });
// });

// var app = builder.Build();

// // Use the ResponseWrapperMiddleware
// app.UseMiddleware<ResponseWrapperMiddleware>();


//     app.UseDeveloperExceptionPage();
//     app.UseSwagger();
//     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlexTechHub API v1"));


// app.UseHttpsRedirection();
// app.UseAuthorization();
// app.MapControllers();

// app.Run();














//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddDbContext<InventoryManagementDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ONLINE"),
//        b => b.MigrationsAssembly("FlexTechHub.Api")));

//// Register Unit of Work and other services
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<ITeacherCourseService, TeacherCourseService>();
//builder.Services.AddScoped<ICourseService, CourseService>();
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));
//builder.Services.AddScoped<IProductService, ProductService>();

//// Register Swagger
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlexTechHub API", Version = "v1" });
//});

//// Add CORS services
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});

//var app = builder.Build();

//// Use Middleware
//app.UseMiddleware<ResponseWrapperMiddleware>();
//app.UseDeveloperExceptionPage();
//app.UseSwagger();
//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlexTechHub API v1"));

//app.UseHttpsRedirection();

//// Use CORS policy
//app.UseCors("AllowAll");

//app.UseAuthorization();

//app.MapControllers();

//app.Run();







//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllers();
//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddDbContext<InventoryManagementDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ONLINE"),
//        b => b.MigrationsAssembly("FlexTechHub.Api")));

//// Register Unit of Work and other services
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<ITeacherCourseService, TeacherCourseService>();
//builder.Services.AddScoped<ICourseService, CourseService>();
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));
//builder.Services.AddScoped<IProductService, ProductService>();

//// Register Swagger
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlexTechHub API", Version = "v1" });
//});

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//    options.AddPolicy("AllowAll", builder =>
//    {
//        builder.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader();
//    });
//});
//var app = builder.Build();

//// Use Middleware
//app.UseDeveloperExceptionPage();
//app.UseSwagger();
//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FlexTechHub API v1"));

//app.UseHttpsRedirection();

//// Use CORS policy
//app.UseCors();
////app.UseCors("AllowAll");

//app.UseAuthorization();
//app.UseMiddleware<ResponseWrapperMiddleware>();

//app.MapControllers();

//app.Run();






var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<InventoryManagementDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("OFFLINE"),
        b => b.MigrationsAssembly("FlexTechHub.Api")));

// builder.Services.AddDbContext<InventoryManagementDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("ONLINE"),
//         b => b.MigrationsAssembly("FlexTechHub.Api")));

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
