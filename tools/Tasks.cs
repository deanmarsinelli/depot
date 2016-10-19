// Tasks.cs
// Dean Marsinelli
// Learning async tasks in C#

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task> tasks = new List<Task>();

            // immediately start a task using lambda syntax
            // (input params) => { body }
            for (int i = 0; i < 25; i++)
            {
                tasks.Add(Task.Factory.StartNew((n) =>
                {
                    Thread t = Thread.CurrentThread;
                    Console.WriteLine("Async Thread " + n + " -- ID: " + t.ManagedThreadId);
                }, i + 1));
            }
            
            Thread thread = Thread.CurrentThread;
            Console.WriteLine("Main Thread ID: " + thread.ManagedThreadId);

            // wait on all tasks to finish before continuing
            Task.WaitAll(tasks.ToArray());
        }
    }
}
