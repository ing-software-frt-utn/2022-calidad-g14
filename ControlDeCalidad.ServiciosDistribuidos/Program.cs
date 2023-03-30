using ControlDeCalidad.ServiciosDistribuidos.Semaforo;
using ControlDeCalidad.ServiciosDistribuidos.Setup;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("ControlDeCalidad");
builder.Services.Configurar(connectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
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
    endpoints.MapHub<SemaforoHub>("SemaforoHub");
});

app.MapControllers();

app.Run();
