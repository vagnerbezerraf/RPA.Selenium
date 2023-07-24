using System.Data;
using System.Data.SQLite;
using Dapper;
using RPA.Selenium.Domain.Entities;
using RPA.Selenium.Domain.Repositories;

namespace RPA.Selenium.Infrastructure.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly IDbConnection _dbConnection;

        public CursoRepository(string connectionString)
        {
            _dbConnection = new SQLiteConnection(connectionString);
        }

        public void CreateDatabaseAndTable()
        {
            _dbConnection.Open();

            // Criar a tabela Cursos, se ela não existir
            string createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Cursos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Titulo TEXT NULL,
                Professor TEXT NULL,
                QuantidadeHoras INTEGER NULL,
                Descricao TEXT NULL
            );";

            _dbConnection.Execute(createTableQuery);
        }

        public IEnumerable<Curso> GetAll()
        {
            return _dbConnection.Query<Curso>("SELECT * FROM Cursos");
        }

        public Curso GetById(int id)
        {
            return _dbConnection.QuerySingleOrDefault<Curso>("SELECT * FROM Cursos WHERE Id = @Id", new { Id = id });
        }

        public void Add(Curso curso)
        {
            string insertQuery = "INSERT INTO Cursos (Titulo, Professor, QuantidadeHoras, Descricao) " +
                                 "VALUES (@Titulo, @Professor, @QuantidadeHoras, @Descricao)";
            _dbConnection.Execute(insertQuery, curso);
        }
    }
}
