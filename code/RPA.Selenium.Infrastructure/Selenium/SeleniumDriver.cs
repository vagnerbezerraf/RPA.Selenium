using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace RPA.Selenium.Infrastructure.Selenium
{
    /// <summary>
    /// Encapsulmento do Selenium Driver para tratar as Exceptions mais comuns de navegação
    /// </summary>
    public class SeleniumDriver : IDisposable
    {
        private IWebDriver _driver;

        public SeleniumDriver()
        {
            _driver = new ChromeDriver();
        }

        public void NavigateToUrl(string url)
        {
            try
            {
                _driver.Navigate().GoToUrl(url);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            catch (Exception ex)
            {
                SeleniumExceptionHelper.HandleSeleniumException(ex);
            }
        }

        public IWebElement FindElement(By by)
        {
            try
            {
                return _driver.FindElement(by);
            }
            catch (Exception ex)
            {
                SeleniumExceptionHelper.HandleSeleniumException(ex);
                return null; // Ou lançar uma exceção ou retornar null, dependendo do seu caso de uso
            }
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            try
            {
                return _driver.FindElements(by);
            }
            catch (Exception ex)
            {
                SeleniumExceptionHelper.HandleSeleniumException(ex);
                return null; // Ou lançar uma exceção ou retornar null, dependendo do seu caso de uso
            }
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
