using AQATM.WebPages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AQATM.Steps
{
    [Binding]
    public class AutorizationSteps
    {
        private static TMAutorizedPage AutorizedPage => PageFactory.Get<TMAutorizedPage>();

        private static TMRegistratePage TMRegPage => PageFactory.Get<TMRegistratePage>();

        [BeforeScenario("user_account_exist")]
        public void CreateUserAccount()
        {
            TMRegPage.Open();
            TMRegPage.RegisterButton.Click();
            TMRegPage.EmailInput.SendKeys("emexample@gmail.com");
            TMRegPage.PasswordInput.SendKeys("x6@9hkrmWZNjmzY34");
            TMRegPage.ConfirmPasswordInput.SendKeys("x6@9hkrmWZNjmzY34");
            TMRegPage.FinalRegisterButton.Click();
            TMRegPage.Open();
        }

        [When(@"User clicks Login button")]
        public void WhenUserClicksLoginButton()
        {
            AutorizedPage.LogInButton.Click();
        }

        [When(@"Enters user ""(.*)"" into autorizeinput")]
        public void WhenEntersUserIntoAutorizeinput(string p0)
        {
            AutorizedPage.EmailInput.SendKeys(p0);
        }

        [When(@"Enters ""(.*)"" to Password autorizefield")]
        public void WhenEntersToPasswordAutorizefield(string p0)
        {
            AutorizedPage.PasswordInput.SendKeys(p0);
        }

        [When(@"User clicks FinalLogin button")]
        public void WhenUserClicksFinalLoginButton()
        {
            AutorizedPage.FinalLogInButton.Click();
        }

        [Then(@"login email form has error ""(.*)""")]
        public void ThenLoginEmailFormHasError(string expectedErrorMsg)
        {
            var errorMsg = "";
            Assert.DoesNotThrow(() => errorMsg = AutorizedPage.LogInEmailFormError(expectedErrorMsg).Text);
            Assert.That(errorMsg, Is.EqualTo(expectedErrorMsg));
        }

        [Then(@"login password form has error ""(.*)""")]
        public void ThenLoginPasswordFormHasError(string expectedErrorMsg)
        {
            var errorMsg = "";
            Assert.DoesNotThrow(() => errorMsg = AutorizedPage.LogInPasswordFormError(expectedErrorMsg).Text);
            Assert.That(errorMsg, Is.EqualTo(expectedErrorMsg));
        }

        [Then(@"login form has error ""(.*)""")]
        public void ThenLoginFormHasError(string expectedErrorMsg)
        {
            var errorMsg = "";
            Assert.DoesNotThrow(() => errorMsg = AutorizedPage.LogInFormError(expectedErrorMsg).Text);
            Assert.That(errorMsg, Is.EqualTo(expectedErrorMsg));
        }
    }
}
