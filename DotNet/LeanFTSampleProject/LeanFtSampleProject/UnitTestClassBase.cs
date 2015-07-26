using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using HP.LFT.Report;
using HP.LFT.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeanFtSampleProject
{
    public abstract class UnitTestClassBase<TDerive> : UnitTestBase where TDerive : UnitTestClassBase<TDerive>, new()
    {
        private static readonly UnitTestClassBase<TDerive> Instance = new TDerive();
        private static TestContext _testContext;

        public TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }

        [ClassInitialize]
        public static void GlobalSetup(TestContext context)
        {
            _testContext = context;
            Instance.UnitTestBaseSetup();
        }

        private void UnitTestBaseSetup()
        {
            Instance.TestSuiteSetup();
        }

        [ClassCleanup]
        public static void GlobalTearDown()
        {
            Instance.TestSuiteTearDown();
            Instance.Reporter.GenerateReport();
        }

        [TestInitialize]
        public void BasicSetUp()
        {
            Instance.TestSetUp();
        }

        [TestCleanup]
        public void BasicTearDown()
        {
            Instance.TestTearDown();
        }

        protected override string GetTestName()
        {
            return TestContext.TestName;
        }

        protected override string GetClassName()
        {
            return _testContext.FullyQualifiedTestClassName;
        }

        protected override Status GetFrameworkTestResult()
        {
            switch (_testContext.CurrentTestOutcome)
            {
                case UnitTestOutcome.Error:
                case UnitTestOutcome.Failed:
                    return Status.Failed;
                case UnitTestOutcome.Passed:
                    return Status.Passed;
                case UnitTestOutcome.Inconclusive:
                case UnitTestOutcome.Unknown:
                    return Status.Warning;
                default:
                    return Status.Passed;
            }
        }


    }
}
