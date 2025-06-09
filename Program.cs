using controleDeFuncionarios.Models;
using controleDeFuncionarios.Dao;
using controleDeFuncionarios.Rotas;

var builder = WebApplication.CreateBuilder(args);

// Configurar serviços
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllersWithViews(); // Adiciona suporte ao MVC
builder.Services.AddHttpClient(); // Adiciona suporte ao HttpClient para consumir APIs

var app = builder.Build();

// Inicializar banco de dados
InicializarBanco.PopularBancoDeDados(app.Services);

// Configurar middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles(); // Permite servir arquivos estáticos (CSS, JS, etc.)
app.UseRouting(); // Configura o roteamento

// Configurar rotas
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Funcionario}/{action=Index}/{id?}"); // Define rota padrão
});

app.MapGet("/", () => "Hello World!");
app.MapGetRoutes();
app.MapPostRoutes();
app.MapDeleteRoutes();

app.Run();
