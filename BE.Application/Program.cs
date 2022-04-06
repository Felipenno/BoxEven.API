using BE.Service;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); //resolve dependencia circular
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => x.SwaggerDoc("v1", new OpenApiInfo { Title = "BoxEven API", Version = "V1"}));
builder.Services.AddCors();

InjecaoDependencia.Configurar(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),//habilita requisitar imagens pela url
//    RequestPath = new PathString("/Resources")
//});

app.UseCors(opt => opt.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200"));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
