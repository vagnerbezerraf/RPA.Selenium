
using RPA.Selenium.Domain.Entities;

namespace RPA.Selenium.Domain.Repositories
{
    public interface ICursoRepository
    {
        IEnumerable<Curso> GetAll();
        Curso GetById(int id);
        void Add(Curso curso);
        void CreateDatabaseAndTable();
    }
}
