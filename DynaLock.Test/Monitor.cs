using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DynaLock.Test
{
    public class Monitor
    {
        private class TestContext{
            public int BankAccount;
            public DynaLock.Context.Monitor DynaLockContext;
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Action<TestContext> Deposit = (TestContext testContext) => {
                using(var monitor = new DynaLock.Monitor(testContext.DynaLockContext, "bankaccount_locker")){
                    monitor.Enter();
                    if (monitor.IsLockOwned())
                        testContext.BankAccount++;
                }
            };

            Action<TestContext> Withdraw = (TestContext testContext) => {
                using(var monitor = new DynaLock.Monitor(testContext.DynaLockContext, "bankaccount_locker")){
                    monitor.Enter();
                    if (monitor.IsLockOwned())
                        testContext.BankAccount--;
                }
            };

            TestContext p1Context = new TestContext(){
                DynaLockContext = new DynaLock.Context.Monitor()
            };
            TestContext p2Context = new TestContext(){
                DynaLockContext = new DynaLock.Context.Monitor()
            };
            TestContext p3Context = new TestContext();
            TestContext p4Context = new TestContext(){
                DynaLockContext = new DynaLock.Context.Monitor()
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

            // for(int i = 0; i < 10000; i++)
            // {
            //     tasks.Add(Task.Factory.StartNew(() => {
            //         Withdraw(p4Context);
            //     }));
            // }

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