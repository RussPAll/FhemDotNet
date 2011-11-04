// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.6.1.0
//      SpecFlow Generator Version:1.6.0.0
//      Runtime Version:4.0.30319.235
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
namespace FhemDotNet.UI.Specs.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.6.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Configure Thermostats")]
    [NUnit.Framework.CategoryAttribute("WebDriver")]
    public partial class ConfigureThermostatsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "ConfigureThemostats.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Configure Thermostats", "In order to change the system state\nAs a user\nI want to be able to configure ther" +
                    "mostat behaviour", GenerationTargetLanguage.CSharp, new string[] {
                        "WebDriver"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Thermostats allow temperature to be set")]
        public virtual void ThermostatsAllowTemperatureToBeSet()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Thermostats allow temperature to be set", ((string[])(null)));
#line 7
this.ScenarioSetup(scenarioInfo);
#line 8
 testRunner.Given("I am on the \"thermostat list\" page");
#line 9
 testRunner.Then("I should see a list of thermostats");
#line 10
  testRunner.And("I can input a desired temperature \"5\"");
#line 11
  testRunner.And("I can submit the page");
#line 12
  testRunner.And("I should be on the \"thermostat list\" page");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Thermostats allow mode to be set to manual")]
        public virtual void ThermostatsAllowModeToBeSetToManual()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Thermostats allow mode to be set to manual", ((string[])(null)));
#line 14
this.ScenarioSetup(scenarioInfo);
#line 15
 testRunner.Given("I am on the \"thermostat list\" page");
#line 16
 testRunner.Then("I should see a list of thermostats");
#line 17
  testRunner.And("I can set the thermostat mode to \"auto\"");
#line 18
  testRunner.And("I can submit the page");
#line 19
  testRunner.And("I can set the thermostat mode to \"manual\"");
#line 20
  testRunner.And("I can submit the page");
#line 21
  testRunner.And("I should be on the \"thermostat list\" page");
#line hidden
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Changes are reflected in the UI")]
        public virtual void ChangesAreReflectedInTheUI()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Changes are reflected in the UI", ((string[])(null)));
#line 23
this.ScenarioSetup(scenarioInfo);
#line 24
 testRunner.Given("I am on the \"thermostat list\" page");
#line 25
 testRunner.When("I have set the thermostat \"Bathroom\" to temperature \"5\"");
#line 26
  testRunner.And("I have submitted the page");
#line 27
 testRunner.Then("the \"Bathroom\" thermostat should show a pending desired temperature \"5\"");
#line hidden
            testRunner.CollectScenarioErrors();
        }
    }
}
#endregion
