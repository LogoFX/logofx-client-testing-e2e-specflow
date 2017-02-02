using Attest.Testing.Core;
using LogoFX.Client.Testing.Contracts;
using LogoFX.Client.Testing.EndToEnd.FakeData;
using LogoFX.Client.Testing.EndToEnd.White;
using ScenarioContext = TechTalk.SpecFlow.ScenarioContext;

namespace LogoFX.Client.Testing.EndToEnd.SpecFlow
{
    /// <summary>
    /// Base class for SpecFlow bridge.
    /// </summary>    
    public abstract class EndToEndTestsBase :
        Attest.Testing.SpecFlow.EndToEndTestsBase
    {
        /// <summary>
        /// Base class for client End-To-End tests which use fake data providers.
        /// </summary>
        /// <seealso cref="EndToEndTestsBase" />
        public abstract class WithFakeProviders : EndToEndTestsBase
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="EndToEndTestsBase.WithFakeProviders"/> class.
            /// </summary>
            protected WithFakeProviders(ScenarioContext scenarioContext) : base(scenarioContext)
            {
                ScenarioHelper.Add(new StartApplicationService.WithFakeProviders(), typeof(IStartApplicationService));
                ScenarioHelper.Add(new BuilderRegistrationService(), typeof(IBuilderRegistrationService));
                RegisterScreenObjectsCore();
            }
        }

        /// <summary>
        /// Base class for client End-To-End tests which use real data providers.
        /// </summary>
        /// <seealso cref="EndToEndTestsBase" />
        public abstract class WithRealProviders : EndToEndTestsBase
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="EndToEndTestsBase.WithRealProviders"/> class.
            /// </summary>
            protected WithRealProviders(ScenarioContext scenarioContext) : base(scenarioContext)
            {
                ScenarioHelper.Add(new StartApplicationService.WithRealProviders(), typeof(IStartApplicationService));
                RegisterScreenObjectsCore();
            }
        }

        protected EndToEndTestsBase(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        /// <summary>
        /// Override this method to register the screen objects.
        /// </summary>
        protected virtual void RegisterScreenObjects()
        {

        }

        internal void RegisterScreenObjectsCore()
        {
            RegisterScreenObjects();
        }

        /// <summary>
        /// Called when the teardown finishes
        /// </summary>
        protected override void OnAfterTeardown()
        {
            base.OnAfterTeardown();
            if (ApplicationContext.Application != null)
            {
                ApplicationContext.Application.Dispose();
            }
            ScenarioHelper.Clear();
        }
    }
}
