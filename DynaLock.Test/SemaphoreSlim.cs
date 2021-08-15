using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace DynaLock.Test
{
    public class SemaphoreSlim
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
            bool assertResult = true;
            for (int i = 0; i < 3; i++)
            {
                List<Task<bool>> tasks = new List<Task<bool>>();
                for (int j = 0; j < 12; j++)
                {
                    tasks.Add(Process());
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

        public async Task<bool> Process()
        {
            Func<bool> CheckSemaphoreSlimBehavior = () =>
            {
                if (Interlocked.Read(ref _threadsInMethodCount) >= TotalThreadsPermitted)
                {
                    return false;
                }
                else
                {
                    Interlocked.Increment(ref _threadsInMethodCount);
                    Thread.Sleep(8000);
                    Interlocked.Decrement(ref _threadsInMethodCount);
                    return true;
                }
            };

            bool result = false;
            // Only 4 threads could access resource at the same time
            using (var semaphoreSlim = new DynaLock.SemaphoreSlimBuilder()
                .Name("semaphoreSlim_name")
                .InitialCount(TotalThreadsPermitted)
                .MaximumCount(TotalThreadsPermitted)
                .Build())
            {
                do
                {
                    await semaphoreSlim.WaitAsync(60000);
                    await Task.Delay(5000);
                    if (semaphoreSlim.IsLockOwned())
                    {
                        // SemaphoreSlim is taken by thread
                        result = CheckSemaphoreSlimBehavior();
                        break;
                    }
                } while (true);
            }

            return result;
        }
    }
}
