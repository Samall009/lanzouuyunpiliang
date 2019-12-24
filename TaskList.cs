using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace 蓝奏云批量上传
{
    class TaskList
    {
        public static void Test()
        {
            Task task1 = new Task(() => {
                // 写入控制台
                Console.WriteLine("线程ID:{0},开始执行", Thread.CurrentThread.ManagedThreadId);
                // 写入控制台
                Console.WriteLine("线程ID:{0},执行完成", Thread.CurrentThread.ManagedThreadId);
            });

            Task task2 = new Task(() => {
                // 写入控制台
                Console.WriteLine("线程ID:{0},开始执行", Thread.CurrentThread.ManagedThreadId);
                // 写入控制台
                Console.WriteLine("线程ID:{0},执行完成", Thread.CurrentThread.ManagedThreadId);
            });

            List<Task> listTask = new List<Task>();
            listTask.Add(task1);
            listTask.Add(task2);

            foreach (Task task in listTask)
            {
                task.Start();
            }

            Task.WaitAll(listTask.ToArray());
            Console.WriteLine("所有线程执行完成。");
            Console.ReadLine();
        }

        // 对象序列
        private static List<Task> ListTask = new List<Task>();


        /*
         * 添加任务
         */
        public static void push(Action action)
        {
            Task task = new Task(action);

            ListTask.Add(task);
        }

        // 任务执行中
        private static List<int> WorkList = new List<int>();

        public static void work()
        {
            while (true)
            {
                if (WorkList.Count <= 2)
                {
                    // 执行新的任务
                }


            }
        }
    }
}
