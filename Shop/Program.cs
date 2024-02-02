using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PMSDbContext>(options =>
    options.UseSqlServer("Data Source=hopelessabitpc;Initial Catalog=shop;User ID=nmdp;Password=123;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(AccountService), typeof(AccountService));
builder.Services.AddTransient(typeof(CategoryService), typeof(CategoryService));
builder.Services.AddTransient(typeof(CustomerService), typeof(CustomerService));
builder.Services.AddTransient(typeof(OrderDetailService), typeof(OrderDetailService));
builder.Services.AddTransient(typeof(OrderService), typeof(OrderService));
builder.Services.AddTransient(typeof(ProductService), typeof(ProductService));
builder.Services.AddTransient(typeof(SupplierService), typeof(SupplierService));

var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new AutoMapperProfile());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    //pattern: "{controller=Account}/{action=TestPage}/{id?}");
    pattern: "{controller=Account}/{action=TestPage}");

app.Run();
