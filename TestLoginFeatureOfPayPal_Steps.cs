using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework;

namespace AutomationTestDingSpecflow
{
    [Binding]
    public class TestLoginFeatureOfPayPal_Steps
    {
        public IWebDriver driver;
        WebDriverWait wait;
        [BeforeScenario]
        public void Setup()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
        }

             
        [Given(@"user navigate to login page")]
        public void GivenUserNavigateToLoginPage()
        {
            driver.Url = "https://www.sandbox.paypal.com/us/signin";
        }
        
        [When(@"username ""(.*)"" is entered")]
        public void WhenUsernameIsEntered(string username)
        {
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("email"))).SendKeys(username);
            driver.FindElement(By.Id("btnNext")).Click();
        }
        
        [When(@"password ""(.*)"" is entered")]
        public void WhenPasswordIsEntered(string password)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("password"))).SendKeys(password);
            driver.FindElement(By.Id("btnLogin")).Click();
        }
        
        [Then(@"invalid message should be prompted")]
        public void ThenInvalidMessageShouldBePrompted()
        {
            string actualMessage = driver.FindElement(By.CssSelector("p.notification.notification-critical")).Text;
            Assert.IsTrue(actualMessage.Equals("Some of your info isn't correct. Please try again."));
            
        }

        [Then(@"required message should be prompted")]
        public void ThenRequiredMessageShouldBePrompted()
        {
            string actualMessage = driver.FindElement(By.Id("passwordErrorMessage")).Text;
            Assert.IsTrue(actualMessage.Equals("Required"));

        }

        
        [Then(@"invalid mobile number message should be prompted")]
        public void ThenInvalidMobileNumberMessageShouldBePrompted()
        {
            string actualMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("p.notification.notification-warning"))).Text;
            Assert.IsTrue(actualMessage.Equals("You haven’t confirmed your mobile yet. Use your email for now."));
        }

        [AfterScenario]
        public void close()
        {
            driver.Close();
        }
    }
}
