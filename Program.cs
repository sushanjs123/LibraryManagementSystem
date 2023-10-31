using LibraryManagement.Data.Repository;
using LibraryManagementSystem.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LibraryDbContext>(options =>
{
    options.UseInMemoryDatabase("LibraryContext");
});
builder.Services.AddTransient<LibraryManagementSystem.Data.Interfaces.ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<LibraryManagementSystem.Data.Interfaces.IAuthorRepository, AuthorRepository>();
builder.Services.AddTransient<LibraryManagementSystem.Data.Interfaces.IBookRepository, BookRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

initdb.Seed(app);
app.Run();
