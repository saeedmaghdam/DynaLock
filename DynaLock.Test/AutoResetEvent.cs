using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SystemAutoResetEvent = System.Threading.AutoResetEvent;

namespace DynaLock.Test
{
    public class AutoResetEvent
    {
        private class TestContext{
            public int BankAccount;
            public DynaLock.Context.Context<SystemAutoResetEvent> DynaLockContext;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Action<TestContext> Deposit = (TestContext testContext) => {
                using(var autoResetEvent = new DynaLock.AutoResetEventBuilder()
                    .Name("bankaccount_locker")
                    .Context(testContext.DynaLockContext)
                    .Build()
                ){
                    autoResetEvent.WaitOne();
                    if (autoResetEvent.IsLockOwned())
                        testContext.BankAccount++;
                }
            };

            Action<TestContext> Withdraw = (TestContext testContext) => {
                using (var autoResetEvent = new DynaLock.AutoResetEventBuilder()
                    .Name("bankaccount_locker")
                    .Context(testContext.DynaLockContext)
                    .Build()
                )
                {
                    autoResetEvent.WaitOne();
                    if (autoResetEvent.IsLockOwned())
                        testContext.BankAccount--;
                }
            };

            TestContext p1Context = new TestContext(){
                DynaLockContext = new DynaLock.Context.Context<SystemAutoResetEvent>()
            };
            TestContext p2Context = new TestContext(){
                DynaLockContext = new DynaLock.Context.Context<SystemAutoResetEvent>()
            };
            TestContext p3Context = new TestContext();
            TestContext p4Context = new TestContext(){
                DynaLockContext = new DynaLock.Context.Context<SystemAutoResetEvent>()
            };

            var tasks = new List<Task>();

            // Person 1 account
            for(int i = 0; i < 10000; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => {
                    Deposit(p1Context);
                }));
            }

            for(int i = 0; i < 10000; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => {
                    Withdraw(p1Context);
                }));
            }

            // Person 2 account
            for(int i = 0; i < 10000; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => {
                    Deposit(p2Context);
                }));
            }

            for(int i = 0; i < 10000; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => {
                    Withdraw(p2Context);
                }));
            }

            // Person 3 account
            for(int i = 0; i < 10000; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => {
                    Deposit(p3Context);
                }));
            }

            for(int i = 0; i < 10000; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => {
                    Withdraw(p3Context);
                }));
            }

            // Person 4 account
            for(int i = 0; i < 10000; i++)
            {
                tasks.Add(Task.Factory.StartNew(() => {
                    Deposit(p4Context);
                }));
            }

            for (int i = 0; i < 10000; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    Withdraw(p4Context);
                }));
            }

            Task.WaitAll(tasks.ToArray());

            if (p1Context.BankAccount != 0)
                Assert.Fail($"Person 1 bank account is not balance, balance value is: {p1Context.BankAccount}");

            if (p2Context.BankAccount != 0)
                Assert.Fail($"Person 2 bank account is not balance, balance value is: {p2Context.BankAccount}");

            if (p3Context.BankAccount != 0)
                Assert.Fail($"Person 3 bank account is not balance, balance value is: {p3Context.BankAccount}");

            if (p4Context.BankAccount != 0)
                Assert.Fail($"Person 4 bank account is not balance, balance value is: {p4Context.BankAccount}");

            Assert.Pass();
        }
    }
}