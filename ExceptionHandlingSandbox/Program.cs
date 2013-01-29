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
                   .ThenThrowNewException()
                .ForExceptionType<CustomException>()
                    .HandleCustom(typeof(ConsoleLoggerHandler), new Dictionary<string, object>() { { "Key", "Hello World" } })
                    .ReplaceWith<CustomReplaceException>()
                        .UsingMessage("Replaced")
                    .ThenThrowNewException();

            ICfExceptionManager cfExceptionManager = ExceptionManager.Current;

            cfExceptionManager.Configure(configuration);

            Console.WriteLine("Scenario  1 Started...\n");

            try
            {
                bool rethrow = cfExceptionManager.HandleException(new Exception("First"), "Defualt");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Catch :\n Exception : {0}\nException Message : {1}", ex.GetType().FullName, ex.Message);
            }

            Console.WriteLine("Scenario 1 Ended...\n");
            Console.WriteLine("Scenario 2 Started...\n");

            try
            {
                bool rethrow = cfExceptionManager.HandleException(new CustomException("Custom Exception throw"), "Defualt");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Catch :\n Exception : {0}\nException Message : {1}", ex.GetType().FullName, ex.Message);
            }

            Console.WriteLine("Scenario 2 Ended...\n");
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

    public class CustomException : Exception
    {
        public CustomException(string message, Exception ex)
            : base(message, ex)
        {

        }

        public CustomException(string message)
            : base(message)
        {

        }
    }


    public class CustomReplaceException : Exception
    {
        public CustomReplaceException(string message, Exception ex)
            : base(message,ex)
        {
            
        }

        public CustomReplaceException(string message)
            : base(message)
        {
            
        }
    }
}
