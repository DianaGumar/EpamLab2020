using System.Threading.Tasks;
using AQA_demoproj.WebPages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AQA_demoproj.Steps
{
    [Binding]
    public class LoginSteps
    {
        private static HomePage HomePage => PageFactory.Get<HomePage>();
        
        [Given(@"User is on training\.by")]
        public void GivenUserIsOnTraining_By()
        {
            HomePage.Open();
        }

        [When(@"(?:User )?[Cc]licks Login button")]
        public async Task WhenUserClicksLoginButtonAsync()
        {
            await Task.Delay(3000);
            HomePage.SignInButton.Click();
        }

        [When(@"(?:User )?[Cc]licks FinalLogin button")]
        public void WhenUserClicksFinalLoginButton()
        {
            HomePage.FinalLoginButton.Click();
        }

        [When(@"Enters ""(.*)"" to user name input")]
        public void WhenEntersToUserNameInput(string inputString)
        {
            HomePage.EmailInput.SendKeys(inputString);
        }

        [When(@"(?:User )?[Cc]licks Continue button")]
        public void WhenUserClicksContinueButton()
        {
            HomePage.ContinueLoginFormButton.Click();
        }

        [When(@"Enters ""(.*)"" to password field")]
        public void WhenEntersToPasswordField(string inputString)
        {
            HomePage.PasswordInput.SendKeys(inputString);
        }

        [When(@"Clicks Sign In button on login form")]
        public void WhenClicksSignInButtonOnLoginForm()
        {
            HomePage.SubmitLoginFormButton.Click();
        }

        [Then(@"Login form has error ""(.*)""")]
        public void ThenLoginFormHasError(string expectedErrorMsg)
        {
            var errorMsg = "";
            Assert.DoesNotThrow(() => errorMsg = HomePage.LoginFormError(expectedErrorMsg).Text);
            Assert.That(errorMsg, Is.EqualTo(expectedErrorMsg));
        }
    }
}
