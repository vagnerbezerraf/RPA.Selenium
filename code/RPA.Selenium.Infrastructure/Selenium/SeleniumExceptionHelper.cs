using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPA.Selenium.Infrastructure.Selenium
{
    public static class SeleniumExceptionHelper
    {
        public static void HandleSeleniumException(Exception ex)
        {
            if (ex is NoSuchElementException)
            {
                // Trate a exceção específica de elemento não encontrado (NoSuchElementException) aqui
                Console.WriteLine("Elemento não encontrado: " + ex.Message);
            }
            else if (ex is WebDriverException)
            {
                // Trate outras exceções do WebDriver aqui
                Console.WriteLine("Exceção do WebDriver: " + ex.Message);
            }
            else
            {
                // Trate outras exceções do Selenium aqui
                Console.WriteLine("Exceção do Selenium: " + ex.Message);
            }

            // Você pode adicionar outros casos de exceções comuns do Selenium conforme necessário
            // Ou então, lançar a exceção novamente se não souber como lidar com ela aqui
        }
    }
}
