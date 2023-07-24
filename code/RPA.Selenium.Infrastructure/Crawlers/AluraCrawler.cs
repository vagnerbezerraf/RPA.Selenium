using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using RPA.Selenium.Domain.Entities;
using System.Xml.Linq;
using RPA.Selenium.Infrastructure.Selenium;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;

namespace RPA.Selenium.Infrastructure.Crawlers
{
    public class AluraCrawler
    {
        private readonly SeleniumDriver _driver;

        public AluraCrawler()
        {
            _driver = new SeleniumDriver();
        }

        public IEnumerable<Curso> CapturarCursosAlura(string termoBusca)
        {
            _driver.NavigateToUrl("https://www.alura.com.br/");

            // Encontrar o campo de busca e inserir o termo
            var campoBusca = _driver.FindElement(By.CssSelector(".header__nav--busca-input"));
            campoBusca.SendKeys(termoBusca);

            // Encontrar e clicar no botão de busca
            var botaoBusca = _driver.FindElement(By.CssSelector(".header__nav--busca-submit"));
            botaoBusca.Click();

            //Inicializa a lista de cursos que vamos colher os dados
            var cursos = new List<Curso>();

            //Busca pelos resultados da busca
            var elementosCursos = _driver.FindElements(By.CssSelector(".busca-resultado"));

            foreach (var elemento in elementosCursos)
            {
                var titulo = elemento.FindElement(By.CssSelector(".busca-resultado-nome")).Text;
                var descricao = elemento.FindElement(By.CssSelector(".busca-resultado-descricao")).Text;
                var url = elemento.FindElement(By.CssSelector(".busca-resultado-link"))?.GetAttribute("href");
                
                if (url is not null && (titulo.StartsWith("Curso")||titulo.StartsWith("Formação")))
                {
                    var curso = new Curso
                    {
                        Titulo = titulo,
                        Descricao = descricao,
                        Url = url
                    };

                    cursos.Add(curso);
                }
            }

            return CapturarDadosPorCurso(cursos);
        }

        private IEnumerable<Curso> CapturarDadosPorCurso(IEnumerable<Curso> cursos)
        {
            cursos.ToList().ForEach((curso) => {
                _driver.NavigateToUrl(curso.Url);
                curso.Professor = _driver.FindElement(By.CssSelector(".instructor-title--name"))?.Text;
                curso.QuantidadeHoras = _driver.FindElement(By.CssSelector(".courseInfo-card-wrapper-infos"))?.Text;

                //faz uma busca extendida no layout de formação
                if(curso.Professor is null && curso.QuantidadeHoras is null)
                    CapturaDadosLayoutFormacao(curso);
            });
            
            return cursos;
        }

        private void CapturaDadosLayoutFormacao(Curso curso)
        {
            var professores = _driver.FindElements(By.CssSelector(".formacao-instrutor-nome")).Select(a=>a.Text).Distinct();         
            curso.Professor = string.Join("; ",professores);

            var dadosHoras = _driver.FindElements(By.CssSelector(".formacao__info-destaque")).Where(a=>a.Text.Contains("h"));
            if(dadosHoras is not null)
                curso.QuantidadeHoras = dadosHoras.FirstOrDefault()?.Text;
        }

        public void FecharDriver()
        {
            _driver.Dispose();
        }
    }
}
