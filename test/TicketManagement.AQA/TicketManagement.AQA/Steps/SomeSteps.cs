using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AQA_demoproj.Steps
{
    [Binding]
    public class SomeSteps
    {
        [Given(@"Some step")]
        public void GivenSomeStep()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"Something happens")]
        public void ThenSomethingHappens()
        {
            ScenarioContext.Current.Pending();
        }



    }
}
