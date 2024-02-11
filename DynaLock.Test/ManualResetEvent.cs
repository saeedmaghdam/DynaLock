using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SystemManualResetEvent = System.Threading.ManualResetEvent;

namespace DynaLock.Test
{
    public class ManualResetEvent
    {
        private class TestContext
        {
            public int BankAccount;
            public DynaLock.Context.Context<SystemManualResetEvent> DynaLockContext;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Action<TestContext> LockSharedLockAction = (TestContext testContext) =>
            {
                using (var manualResetEvent = new DynaLock.ManualResetEventBuilder()
                    .Name("bankaccount_locker")
                    .Context(testContext.DynaLockContext)
                    .InitialState(false)
                    .Build()
                )
                {
                    Console.WriteLine("Shared lock is taken by me!");
                    System.Threading.Thread.Sleep(5000);
                    manualResetEvent.Set();
                }
            };

            Action<TestContext> WaitForSharedLockAction = (TestContext testContext) =>
            {
                using (var manualResetEvent = new DynaLock.ManualResetEventBuilder()
                    .Name("bankaccount_locker")
                    .Context(testContext.DynaLockContext)
                    .InitialState(false)
                    .Build()
                )
                {
                    Console.WriteLine("Waiting for the shared lock to be released!");
                    manualResetEvent.WaitOne();
                    Console.WriteLine("I've access to shared resource!");
                }
            };

            TestContext p1Context = new TestContext()
            {
                DynaLockContext = new DynaLock.Context.Context<SystemManualResetEvent>()
            };

            var tasks = new List<Task>();

            tasks.Add(Task.Factory.StartNew(() =>
            {
                LockSharedLockAction(p1Context);
            }));

            for (int i = 0; i < 1000; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    WaitForSharedLockAction(p1Context);
                }));
            }

            Task.WaitAll(tasks.ToArray());

            Console.WriteLine("Congratulations! No deadlock happened!");

            Assert.Pass();
        }
    }
}