using Microsoft.Extensions.DependencyInjection;
using RPA.Selenium.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using RPA.Selenium.Domain.Repositories;
using RPA.Selenium.Application.Services;

class Program
{
    static void Main(string[] args)
    {
        // Configuração do serviço de injeção de dependência
        var serviceProvider = new ServiceCollection()
            .AddSingleton<ICursoRepository, CursoRepository>(provider => new CursoRepository("Data Source=:memory:;Mode=Memory;Cache=Shared"))
            .AddTransient<CursoService>()
            .AddLogging(builder =>{builder.AddConsole();})
            .BuildServiceProvider();

        var cursoService = serviceProvider.GetService<CursoService>();

        cursoService.Executar("RPA");

    }
}