using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CfCommerce.Common.Configuration;
using CfCommerce.Common.ExceptionHandling;
using CfCommerce.Common.ExceptionHandling.Configuration;
using CfCommerce.Common.ExceptionHandling.Configuration.Fluent;
using CfCommerce.Common.ExceptionHandling.Handlers;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using ExceptionManager = CfCommerce.Common.ExceptionHandling.ExceptionManager;

namespace ExceptionHandlingSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
           ExceptionHandlingConfiguration configuration = new ExceptionHandlingConfiguration();

           configuration.BuildPolicies()
               .AddPolicyWithName("Defualt")
               .ForExceptionType<Exception>()
               //.HandleCustom<ConsoleLoggerHandler>()
                   .HandleCustom(typeof(ConsoleLoggerHandler), new Dictionary<string, object>() {{"Key","Hello World"} })
                   .WrapWith<InvalidOperationException>()
                       .UsingMessage("Hede")
                   .ThenThrowNewException();

            ICfExceptionManager cfExceptionManager = ExceptionManager.Current;

            cfExceptionManager.Configure(configuration);

            try
            {
                bool handleException = cfExceptionManager.HandleException(new Exception("First"), "Defualt");
            }
            catch (Exception ex)
            {
            }

            Console.Read();
        }
    }

    public class ConsoleLoggerHandler : ICfExceptionHandler, IExceptionHandler
    {
        private string _message;

        public ConsoleLoggerHandler(params object[] objects)
        {
            _message = objects[0].ToString();
        }
        public Exception HandleException(Exception exception, Guid handlingInstanceId)
        {
            Console.WriteLine("Exception : {0}\nException Message : {1}\nException Guid : {2}.\nCustom Parameter : {3}", exception.GetType().FullName, exception.Message, handlingInstanceId, _message);

            return exception;
        }
    }
}
