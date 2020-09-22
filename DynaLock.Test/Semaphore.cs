using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DynaLock.Test
{
    public class Semaphore
    {
        private long _threadsInMethodCount = 0;
        private const int TotalThreadsPermitted = 4;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Func<bool> CheckSemaphoreBehavior = () =>
            {
                if (Interlocked.Read(ref _threadsInMethodCount) >= TotalThreadsPermitted)
                {
                    return false;
                }
                else
                {
                    Interlocked.Increment(ref _threadsInMethodCount);
                    Thread.Sleep(10000);
                    Interlocked.Decrement(ref _threadsInMethodCount);
                    return true;
                }
            };

            bool assertResult = true;
            for (int i = 0; i < 3; i++)
            {
                List<Task<bool>> tasks = new List<Task<bool>>();
                for (int j = 0; j < 12; j++)
                {
                    tasks.Add(Task<bool>.Factory.StartNew(() =>
                    {
                        bool result = false;
                        // Only 4 threads could access resource at the same time
                        using (var semaphore = new DynaLock.SemaphoreBuilder()
                            .Name("semaphore_name")
                            .InitialCount(TotalThreadsPermitted)
                            .MaximumCount(TotalThreadsPermitted)
                            .Build())
                        {
                            do
                            {
                                semaphore.WaitOne(60000);
                                if (semaphore.IsLockOwned())
                                {
                                    // Semaphore is taken by thread
                                    result = CheckSemaphoreBehavior();
                                    break;
                                }
                            } while (true);
                        }

                        return result;
                    }));
                }

                Task.WaitAll(tasks.ToArray());

                if (tasks.Any(task => task.Result == false))
                {
                    assertResult = false;
                    break;
                }
            }

            if (assertResult)
                Assert.Pass();
            else
                Assert.Fail();
        }
    }
}
