using BakokiWeb.Server.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
string? connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

// Add services to the container.


try
{
	if (connection != null)
	{
        builder.Services.AddDbContext<DataContext>(options =>
			options.UseSqlServer(connection, options => options.EnableRetryOnFailure()));
	}
	else
	{
		throw new Exception("Conection string Null.");
	}
}
catch(Exception ex)
{
	Console.WriteLine(ex.ToString());
}

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	//app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

//app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
