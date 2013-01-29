using System;
using System.Collections.Generic;

namespace CfCommerce.Common.ExceptionHandling.Configuration.Fluent
{
    public static class ExceptionCustomHandlerExtensions
    {
        public static IExceptionHandlerRegistrationForPolicyAndHandler HandleCustom(this IExceptionHandlerRegistration context, Type customHandlerType)
        {
            return HandleCustom(context, customHandlerType, new Dictionary<string, object>());
        }

        public static IExceptionHandlerRegistrationForPolicyAndHandler HandleCustom<T>(this IExceptionHandlerRegistration context) where T : ICfExceptionHandler
        {
            return HandleCustom(context, typeof(T));
        }

        public static IExceptionHandlerRegistrationForPolicyAndHandler HandleCustom(this IExceptionHandlerRegistration context, Type customHandlerType, Dictionary<string,object> parameters)
        {
            if (customHandlerType == null)
            {
                throw new ArgumentNullException("customHandlerType");
            }

            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            if (!typeof(ICfExceptionHandler).IsAssignableFrom(customHandlerType))
            {
                throw new ArgumentException("Type must be derived from ICfExceptionHandler", "customHandlerType");
            }

            return new ExceptionCustomHandlerBuilder(context, customHandlerType, parameters);
        }

        private class ExceptionCustomHandlerBuilder : ExceptionHandlerBuilderExtension
        {
            public ExceptionCustomHandlerBuilder(IExceptionHandlerRegistration context, 
                                                 Type customHandlerType,
                                                 Dictionary<string, object> parameters)
                : base(context)
            {
                BaseHandlerData baseHandlerData = new BaseHandlerData
                    {
                        HandlerType = customHandlerType, HandlerData = parameters
                    };    

                CurrentExceptionPolicy.ExceptionHandlingPolicy.AddHandlerData(baseHandlerData);
            }
        }
         
    }
}