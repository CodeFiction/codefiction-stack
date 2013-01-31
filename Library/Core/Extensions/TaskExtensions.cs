using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CodeFiction.Stack.Library.Core.Extensions
{
    /// <summary>
    /// Task extensions for 
    /// </summary>
    // TODO : Move to Utilities library
    public static class TaskExtensions
    {
        private static readonly Task<object> emptyTask = GenerateTask<object>(null);

        public static Task EmptyTask
        {
            get { return emptyTask; }
        }

        private static Task<TType> GenerateTask<TType>(TType type)
        {
            return PopulateFromResult<TType>(type);
        }

        public static TTaskType Catch<TTaskType>(this TTaskType task)
            where TTaskType : Task
        {
            return Catch(task, ex => Trace.TraceError("Task exception : '{0}'".FormatText(ex)));
        }

        public static TTaskType Catch<TTaskType>(this TTaskType task, Action<Exception> onException)
            where TTaskType : Task
        {
            if (task == null || task.Status == TaskStatus.RanToCompletion)
            {
                return task;
            }
            task.ContinueWith(task1 =>
            {
                var ex = task1.Exception;
                onException(ex);
            }, TaskContinuationOptions.OnlyOnFaulted);
            return task;
        }

        public static Task<TResultType> Then<TResultType>(this Task task, Func<TResultType> func)
        {
            switch (task.Status)
            {
                case TaskStatus.Faulted:
                    return PopulateFromError<TResultType>(task.Exception);
                case TaskStatus.Canceled:
                    return Canceled<TResultType>();
                case TaskStatus.RanToCompletion:
                    return PopulateFromMethod(func);
                default:
                    return RunTask(task, func);
            }
        }

        internal static Task PopulateFromError(Exception e)
        {
            return PopulateFromError<object>(e);
        }
        internal static Task<TResultType> PopulateFromError<TResultType>(Exception e)
        {
            var tcs = new TaskCompletionSource<TResultType>();
            tcs.SetException(e);
            return tcs.Task;
        }

        internal static Task Canceled()
        {
            return Canceled<object>();
        }
        internal static Task<TResultType> Canceled<TResultType>()
        {
            var tcs = new TaskCompletionSource<TResultType>();
            tcs.SetCanceled();
            return tcs.Task;
        }

        internal static Task PopulateFromMethod(Action then)
        {
            try
            {
                then();
                return EmptyTask;
            }
            catch (Exception ex)
            {
                return PopulateFromError(ex);
            }
        }
        internal static Task<TResultType> PopulateFromMethod<TResultType>(Func<TResultType> func)
        {
            try
            {
                return PopulateFromResult(func());
            }
            catch (Exception ex)
            {
                return PopulateFromError<TResultType>(ex);
            }
        }


        internal static Task<TType> PopulateFromResult<TType>(TType value)
        {
            var tcs = new TaskCompletionSource<TType>();
            tcs.SetResult(value);
            return tcs.Task;
        }

        internal static Task<TResultType> RunTask<TResultType>(Task task, Func<TResultType> then)
        {
            var tcs = new TaskCompletionSource<TResultType>();
            task.ContinueWith(t =>
                                  {
                                      if (t.IsFaulted && t.Exception != null)
                                      {
                                          tcs.SetException(t.Exception);
                                      }
                                      else if (t.IsCanceled)
                                      {
                                          tcs.SetCanceled();
                                      }
                                      else
                                      {
                                          try
                                          {
                                              tcs.SetResult(then());
                                          }
                                          catch (Exception ex)
                                          {
                                              tcs.SetException(ex);
                                          }
                                      }
                                  });
            return tcs.Task;
        }
        internal static Task RunTask<TResultType>(Task<TResultType> task, Action<TResultType> successor)
        {
            var tcs = new TaskCompletionSource<object>();
            task.ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception != null)
                {
                    tcs.SetException(t.Exception);
                }
                else if (t.IsCanceled)
                {
                    tcs.SetCanceled();
                }
                else
                {
                    try
                    {
                        successor(t.Result);
                        tcs.SetResult(null);
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(ex);
                    }
                }
            });

            return tcs.Task;
        }
    }
}
