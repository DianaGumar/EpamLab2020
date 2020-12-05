using System;
using OpenQA.Selenium;

namespace AQATM.WebPages
{
    public class HomePage : AbstractPage
    {
        private const int DefaultWaitingInterval = 1;

        public HomePage(IWebDriver driver)
            : base(driver)
        {
        }

        public string BannerTitle => FindByClassName("hero-banner__heading", DefaultWaitingInterval).Text;

        public IWebElement SelectLanguageButton => FindByClassName("location-selector__globe", DefaultWaitingInterval);

        public IWebElement SignInButton => FindByClassName("header-auth__signin-icon", DefaultWaitingInterval);

        public IWebElement FinalLoginButton => FindByCss("input[id='kc-login']", DefaultWaitingInterval);

        public IWebElement EmailInput => FindByCss("input[id='username']", DefaultWaitingInterval);

        public IWebElement PasswordInput => FindByCss("input[id='password']", DefaultWaitingInterval);

        public IWebElement ContinueLoginFormButton => FindByCss("button[id='kc-login-next']", DefaultWaitingInterval);

        public IWebElement SubmitLoginFormButton => FindByCss("input[type='submit']", DefaultWaitingInterval);

        public IWebElement LoginFormError(string textPresent) => FindByCssWithText("div[id='alert-error']", textPresent, DefaultWaitingInterval);

        public void Open()
        {
            Driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            Driver.Url = "http://www.training.by/?lang=en";
        }

        public void SelectLanguageFromDropDown(string language)
        {
            var xpath = $".//*[contains(@class,'location-selector__item')]//a[contains(text(),'{language}')]";
            Driver.FindElement(By.XPath(xpath)).Click();
        }
    }
}
