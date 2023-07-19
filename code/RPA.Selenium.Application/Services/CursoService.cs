using Microsoft.Extensions.Logging;
using RPA.Selenium.Domain.Entities;
using RPA.Selenium.Domain.Repositories;
using RPA.Selenium.Infrastructure.Crawlers;

namespace RPA.Selenium.Application.Services
{    public class CursoService
    {
        private readonly ICursoRepository _cursoRepository;

        private readonly ILogger<CursoService> _logger;

        public CursoService(ICursoRepository cursoRepository, ILogger<CursoService> logger)
        {
            _cursoRepository = cursoRepository;
            _logger = logger;
        }

        public IEnumerable<Curso> GetAllCursos()
        {
            return _cursoRepository.GetAll();
        }

        public Curso GetCursoById(int id)
        {
            return _cursoRepository.GetById(id);
        }

        public void AddCurso(Curso curso)
        {
            _cursoRepository.Add(curso);
        }

        public void Executar(string termoBusca)
        {
            _logger.LogInformation("Iniciando coleta de dados");

            _cursoRepository.CreateDatabaseAndTable();

            var aluraCrawler = new AluraCrawler();

            var cursosAlura = aluraCrawler.CapturarCursosAlura(termoBusca);

            foreach (var curso in cursosAlura)
            {
                AddCurso(curso);
                _logger.LogInformation($"Salvando curso: {curso.Titulo}");
            }

            aluraCrawler.FecharDriver();

            _logger.LogInformation("Concluíndo coleta.");
        }
    }
}
