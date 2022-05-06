var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AppAutomapperProfile));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:AsyncProcessingTestDBConnectionString"]);
});


builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddAutoMapper(typeof(AppAutomapperProfile));
builder.Services.AddSingleton<ITaskService, TaskService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
SeedData.EnsurePopulated(app);
app.Run();

