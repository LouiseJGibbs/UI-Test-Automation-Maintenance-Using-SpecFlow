using FixTheTests.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace FIxTheTests.Steps
{
    [Binding]
    public sealed class AdminLoginSteps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;

        AdminLoginPage _adminLoginPage = new AdminLoginPage();

        public AdminLoginSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [When(@"I login to the admin section")]
        public void WhenILoginToTheAdminSection()
        {
            _adminLoginPage.ClickAdminLink();
            _adminLoginPage.SubmitUsernameAndPassword();
        }
    }
}
