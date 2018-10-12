using Attest.Testing.Contracts;
using Attest.Testing.Core;
using Attest.Testing.Core.FakeData;
using Attest.Testing.EndToEnd;

namespace LogoFX.Client.Testing.EndToEnd.SpecFlow
{
    /// <summary>
    /// Base class for SpecFlow bridge.
    /// </summary>    
    public abstract class EndToEndTestsBase :
        Attest.Testing.SpecFlow.EndToEndTestsBase
    {
        private readonly IApplicationFacade _applicationFacade;

        /// <summary>
        /// Base class for client End-To-End tests which use fake data providers.
        /// </summary>
        /// <seealso cref="EndToEndTestsBase" />
        public abstract class WithFakeProviders : EndToEndTestsBase
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="EndToEndTestsBase.WithFakeProviders"/> class.
            /// </summary>
            protected WithFakeProviders(IApplicationFacade applicationFacade) : base(applicationFacade)
            {
                ScenarioHelper.Add(new StartApplicationService.WithFakeProviders(applicationFacade), typeof(IStartApplicationService));
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
            protected WithRealProviders(IApplicationFacade applicationFacade) : base(applicationFacade)
            {
                ScenarioHelper.Add(new StartApplicationService.WithRealProviders(applicationFacade), typeof(IStartApplicationService));
                RegisterScreenObjectsCore();
            }
        }

        /// <summary>
        /// Constructs an instance of <see cref="EndToEndTestsBase" />
        /// </summary>
        /// <param name="applicationFacade">The application facade.</param>
        protected EndToEndTestsBase(IApplicationFacade applicationFacade)
        {
            _applicationFacade = applicationFacade;
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
            _applicationFacade.Stop();
            ScenarioHelper.Clear();
        }
    }
}
