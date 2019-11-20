using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alading.Core.Interface;
using Alading.Entity;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Data.Objects;
using Alading.Core.Enum;
using System.Linq.Expressions;
using Alading.Core;
namespace Alading.DataProvider
{
   
    public partial class DataProviderClass : IAlading
    {
        public ReturnType AddTask(Task task)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {

                    alading.AddToTask(task);
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    else
                    {
                        return ReturnType.PropertyExisted;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }

        }

        public ReturnType AddTask(List<Task> taskList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    foreach (Task task in taskList)
                    {
                        alading.AddToTask(task);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }

        }

        public ReturnType RemoveAllTask()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Task> list = alading.Task.ToList();
                    foreach (Task task in list)
                    {
                        alading.DeleteObject(task);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;

                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public ReturnType RemoveTask(Func<Task, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Task> list = alading.Task.Where(func).ToList();
                    foreach (Task task in list)
                    {
                        alading.DeleteObject(task);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }

            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public List<Task> GetTask(List<string> taskCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Task.Where(BuildWhereInExpression<Task, int>(v => v.TaskID, taskIDList));*/
                    var result = alading.Task.Where(BuildWhereInExpression<Task, string>(v => v.TaskCode, taskCodeList));
                    return result.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

        public ReturnType RemoveTask(List<string> taskCodeList)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Task.Where(BuildWhereInExpression<Task, int>(v => v.TaskID, taskIDList));*/
                    var result = alading.Task.Where(BuildWhereInExpression<Task, string>(v => v.TaskCode, taskCodeList));
                    foreach (Task s in result)
                    {
                        alading.DeleteObject(s);
                    }
                    alading.SaveChanges();
                    return ReturnType.Success;
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }


        public ReturnType RemoveTask(string taskCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Task> list = alading.Task.Where(p => p.TaskID == taskID).ToList();*/
                    List<Task> list = alading.Task.Where(p => p.TaskCode == taskCode).ToList();
                    if (list.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }
                    else
                    {
                        Task sy = list.First();
                        alading.DeleteObject(sy);
                        alading.SaveChanges();
                        return ReturnType.Success;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public ReturnType UpdateTask(Task task)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*Task result = alading.Task.Where(p => p.TaskID == task.TaskID).FirstOrDefault();*/
                    Task result = alading.Task.Where(p => p.TaskCode == task.TaskCode).FirstOrDefault();
                    if (result == null)
                    {
                        return ReturnType.NotExisted;
                    }
                    #region   Using Attach() Function Update,Default USE;
                    alading.Attach(result);
                    alading.ApplyPropertyChanges("Task", task);
                    #endregion

                    #region    Using All Items Replace To Update ,Default UnUse
                    /*		
                        result.TaskCode = task.TaskCode;
                        result.Type = task.Type;
                        result.Desc = task.Desc;
                        result.StartTime = task.StartTime;
                        result.EndTime = task.EndTime;
                        result.Creator = task.Creator;
                        result.Total = task.Total;
                        result.Done = task.Done;
			
                    */
                    #endregion
                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    else
                    {
                        return ReturnType.OthersError;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public ReturnType UpdateTask(string taskCode, Task task)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*var result = alading.Task.Where(p => p.TaskID == taskID).ToList();*/
                    var result = alading.Task.Where(p => p.TaskCode == taskCode).ToList();
                    if (result.Count == 0)
                    {
                        return ReturnType.NotExisted;
                    }

                    Task ob = result.First();
                    ob.TaskCode = task.TaskCode;
                    ob.Type = task.Type;
                    ob.Desc = task.Desc;
                    ob.StartTime = task.StartTime;
                    ob.EndTime = task.EndTime;
                    ob.Creator = task.Creator;
                    ob.Total = task.Total;
                    ob.Done = task.Done;

                    if (alading.SaveChanges() == 1)
                    {
                        return ReturnType.Success;
                    }
                    else
                    {
                        return ReturnType.OthersError;
                    }
                }
            }
            catch (SqlException sex)
            {
                return ReturnType.ConnFailed;
            }
            catch (System.Exception ex)
            {
                return ReturnType.OthersError;
            }
        }

        public List<Task> GetAllTask()
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Task> list = alading.Task.ToList();
                    return list;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Task> GetTask(Func<Task, bool> func)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    List<Task> list = alading.Task.Where(func).ToList();
                    return list;
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public Task GetTask(string taskCode)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    /*List<Task> list = alading.Task.Where(p => p.TaskID == taskID).ToList();*/
                    List<Task> list = alading.Task.Where(p => p.TaskCode == taskCode).ToList();
                    if (list.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return list.First();
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Task> GetTask(int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    var ob = (from u in alading.Task orderby u.TaskID descending select u).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                    rowCount = alading.Task.Count();
                    return ob.ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public List<Task> GetTask(Func<Task, bool> func, int pageIndex, int pageSize, out int rowCount)
        {
            try
            {
                using (AladingEntities alading = new AladingEntities(AppSettings.GetConnectionString()))
                {
                    IOrderedEnumerable<Task> list = alading.Task.Where(func).OrderByDescending(a => a.TaskID);
                    rowCount = list.Count();
                    return list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}

