using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CfCommerce.Common.Configuration;
using CfCommerce.Common.ExceptionHandling;
using CfCommerce.Common.ExceptionHandling.Configuration;
using CfCommerce.Common.ExceptionHandling.Handlers;

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
                .ReplaceWith<Exception>()
                .UsingMessage("Hede")
                .ThenThrowNewException();

            Policy[] policies = configuration.Policies;
        }
    }
}
