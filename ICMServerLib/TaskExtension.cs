using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer
{
    public static class TaskExtension
    {
        /// <summary>
        /// Takes a collection of tasks and completes the returned task when all tasks have completed. If completion
        /// takes a while a progress lambda is called where all tasks can be observed for their status.
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="reportProgressAction"></param>
        /// <returns></returns>
        public static async Task WhenAll(ICollection<Task> tasks, Action<ICollection<Task>> reportProgressAction)
        {
            // get Task which completes when all 'tasks' have completed
            var whenAllTask = Task.WhenAll(tasks);
            for (;;)
            {
                // get Task which completes after 250ms
                var timer = Task.Delay(250); // you might want to make this configurable
                                             // Wait until either all tasks have completed OR 250ms passed
                await Task.WhenAny(whenAllTask, timer);
                // if all tasks have completed, complete the returned task
                if (whenAllTask.IsCompleted)
                {
                    return;
                }
                // Otherwise call progress report lambda and do another round
                reportProgressAction(tasks);
            }
        }
    }
}
