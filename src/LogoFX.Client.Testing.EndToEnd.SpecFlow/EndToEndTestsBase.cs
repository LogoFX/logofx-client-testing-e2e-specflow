﻿using Attest.Testing.Contracts;
using Attest.Testing.Core.FakeData;
using Attest.Testing.EndToEnd;
using Attest.Testing.SpecFlow;
using TechTalk.SpecFlow;

namespace LogoFX.Client.Testing.EndToEnd.SpecFlow
{
    /// <summary>
    /// Base class for SpecFlow bridge.
    /// </summary>    
    public abstract class EndToEndTestsBase :
        Attest.Testing.SpecFlow.EndToEndTestsBase
    {
        private readonly IApplicationFacade _applicationFacade;
        private readonly ScenarioHelper _scenarioHelper;

        /// <summary>
        /// Base class for client End-To-End tests which use fake data providers.
        /// </summary>
        /// <seealso cref="EndToEndTestsBase" />
        public abstract class WithFakeProviders : EndToEndTestsBase
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="EndToEndTestsBase.WithFakeProviders"/> class.
            /// </summary>
            protected WithFakeProviders(IApplicationFacade applicationFacade, ScenarioHelper scenarioHelper, ScenarioContext scenarioContext) 
                : base(applicationFacade, scenarioHelper, scenarioContext)
            {
                scenarioHelper.Add(new StartApplicationService.WithFakeProviders(applicationFacade), typeof(IStartApplicationService));
                scenarioHelper.Add(new BuilderRegistrationService(), typeof(IBuilderRegistrationService));
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
            protected WithRealProviders(IApplicationFacade applicationFacade, ScenarioHelper scenarioHelper,  ScenarioContext scenarioContext) :
                base(applicationFacade, scenarioHelper, scenarioContext)
            {
                scenarioHelper.Add(new StartApplicationService.WithRealProviders(applicationFacade), typeof(IStartApplicationService));
                RegisterScreenObjectsCore();
            }
        }

        /// <summary>
        /// Constructs an instance of <see cref="EndToEndTestsBase" />
        /// </summary>
        /// <param name="applicationFacade">The application facade.</param>
        /// <param name="scenarioHelper">The scenario helper.</param>
        /// <param name="scenarioContext">The scenario context.</param>
        protected EndToEndTestsBase(IApplicationFacade applicationFacade, ScenarioHelper scenarioHelper, ScenarioContext scenarioContext)
        :base(scenarioContext)
        {
            _applicationFacade = applicationFacade;
            _scenarioHelper = scenarioHelper;
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

        /// <inheritdoc />
        protected override void OnAfterTeardown()
        {
            base.OnAfterTeardown();
            _applicationFacade.Stop();
            _scenarioHelper.Clear();
        }
    }
}
