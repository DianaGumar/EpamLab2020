using System.Threading.Tasks;
using AQATM.WebPages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AQATM.Steps
{
    [Binding]
    public class TMRegistrateSteps
    {
        private static string _password = "";

        private static TMEventModelsIndexPage TMEventIndexPage => PageFactory.Get<TMEventModelsIndexPage>();

        private string SuggestValidPassword()
        {
            if (string.IsNullOrEmpty(_password))
            {
                /////_password = System.Web.Security.Membership.GeneratePassword(25, 10)
                _password = "x6@9hkrmWZNjmzY";
            }

            return _password;
        }

        [Given(@"User is on TM")]
        public void GivenUserIsOnTM()
        {
            TMEventIndexPage.Open();
        }

        [When(@"User clicks Register button")]
        public async Task WhenUserClicksRegisterButtonAsync()
        {
            await Task.Delay(3000);
            TMEventIndexPage.RegisterButton.Click();
        }

        [When(@"Enters ""(.*)"" to user Email input")]
        public void WhenEntersToUserEmailInput(string p0)
        {
            TMEventIndexPage.EmailInput.SendKeys(p0);
        }

        [When(@"Enters ""(.*)"" to Password field")]
        public void WhenEntersToPasswordField(string p0)
        {
            _ = p0;
            TMEventIndexPage.PasswordInput.SendKeys(SuggestValidPassword());
        }

        [When(@"Enters ""(.*)"" to ConfirmPassword field")]
        public void WhenEntersToConfirmPasswordField(string p0)
        {
            _ = p0;
            TMEventIndexPage.ConfirmPasswordInput.SendKeys(SuggestValidPassword());
        }

        [When(@"User clicks FinalRegister button")]
        public async Task WhenUserClicksFinalRegisterButtonAsync()
        {
            await Task.Delay(3000);
            TMEventIndexPage.FinalRegisterButton.Click();
        }

        [Then(@"Register form has error ""(.*)""")]
        public void ThenRegisterFormHasError(string expectedErrorMsg)
        {
            var errorMsg = "";
            Assert.DoesNotThrow(() => errorMsg = TMEventIndexPage.RegisterFormError(expectedErrorMsg).Text);
            Assert.That(errorMsg, Is.EqualTo(expectedErrorMsg));
        }
    }
}
