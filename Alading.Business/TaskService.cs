using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.DataProvider;
using Alading.Core.Enum;
using Alading.Entity;

namespace Alading.Business
{
    public static class TaskService
    {
        public static ReturnType AddTask(Task task)
        {
            return DataProviderClass.Instance().AddTask(task);
        }

        public static ReturnType AddTask(List<Task> taskList)
        {
            return DataProviderClass.Instance().AddTask(taskList);
        }

        public static ReturnType RemoveAllTask()
        {
            return DataProviderClass.Instance().RemoveAllTask();
        }

        public static ReturnType RemoveTask(Func<Task, bool> func)
        {
            return DataProviderClass.Instance().RemoveTask(func);
        }

        public static ReturnType RemoveTask(string taskCode)
        {
            return DataProviderClass.Instance().RemoveTask(taskCode);
        }

        /*
        public static ReturnType RemoveTask(int taskID)
        {
            return DataProviderClass.Instance().RemoveTask(taskID);
        }
        */

        public static ReturnType RemoveTask(List<string> taskCodeList)
        {
            return DataProviderClass.Instance().RemoveTask(taskCodeList);
        }

        /*
        public static ReturnType RemoveTask(List<int> taskIDList)
        {
            return DataProviderClass.Instance().RemoveTask(taskIDList);
        }
        */

        public static ReturnType UpdateTask(Task task)
        {
            return DataProviderClass.Instance().UpdateTask(task);
        }

        public static ReturnType UpdateTask(string taskCode, Task task)
        {
            return DataProviderClass.Instance().UpdateTask(taskCode, task);
        }

        /*
        public static ReturnType UpdateTask(int taskID, Task task)
        {
            return DataProviderClass.Instance().UpdateTask(taskID, task);
        }
        */

        public static List<Task> GetAllTask()
        {
            return DataProviderClass.Instance().GetAllTask();
        }

        public static List<Task> GetTask(Func<Task, bool> func)
        {
            return DataProviderClass.Instance().GetTask(func);
        }

        public static Task GetTask(string taskCode)
        {
            return DataProviderClass.Instance().GetTask(taskCode);
        }

        /*
        public static Task GetTask(int taskID)
        {
            return DataProviderClass.Instance().GetTask(taskID);
        }
        */

        public static List<Task> GetTask(List<string> taskCodeList)
        {
            return DataProviderClass.Instance().GetTask(taskCodeList);
        }

        /*
        public static List<Task> GetTask(List<int> taskIDList)
        {
            return DataProviderClass.Instance().GetTask(taskIDList);
        }
        */

        public static List<Task> GetTask(int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTask(pageIndex, pageSize, out rowCount);
        }

        public static List<Task> GetTask(Func<Task, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            return DataProviderClass.Instance().GetTask(func, pageIndex, pageSize, out rowCount);
        }
    }
}
