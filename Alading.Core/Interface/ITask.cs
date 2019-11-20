using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Entity;
using Alading.Core.Enum;

namespace Alading.Core.Interface
{
    public interface ITask
    {
        ReturnType AddTask(Task task);

        ReturnType AddTask(List<Task> taskList);

        ReturnType RemoveAllTask();

        ReturnType RemoveTask(Func<Task, bool> func);

        ReturnType RemoveTask(string taskCode);

        ReturnType RemoveTask(List<string> taskCodeList);

        ReturnType UpdateTask(Task task);

        ReturnType UpdateTask(string taskCode, Task task);

        List<Task> GetAllTask();

        List<Task> GetTask(Func<Task, bool> func);

        List<Task> GetTask(List<string> taskCodeList);

        List<Task> GetTask(int pageIndex, int pageSize, out int rowCount);

        List<Task> GetTask(Func<Task, bool> func, int pageIndex, int pageSize, out int rowCount);
    }
}
