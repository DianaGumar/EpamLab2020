using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace AQATM.WebPages
{
    public class TMSetPricePage : AbstractPage
    {
        private const int DefaultWaitingInterval = 1;

        public TMSetPricePage(IWebDriver driver)
            : base(driver)
        {
        }

        public IWebElement FinalSetPriseButton => FindByCss("input[class$='set-price-btn']", DefaultWaitingInterval);

        public IWebElement BackToListButton => FindByCss("a[id='tmsetseveralpricebacktolist']", DefaultWaitingInterval);

        public IWebElement PriceField => FindByCss("input[class*='set-price']", DefaultWaitingInterval);

        public List<IWebElement> GetSetPriseFields()
        {
            return Driver.FindElements(By.CssSelector("input[id='Price']")).ToList();
        }

        public List<IWebElement> GetSetPriseBtns()
        {
            return Driver.FindElements(By.CssSelector("input[class$='set-price-btn']")).ToList();
        }
    }
}
