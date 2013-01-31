using System;
using CodeFiction.Stack.Common.ExceptionHandling.Handlers;

namespace CodeFiction.Stack.Common.ExceptionHandling.Configuration.Fluent
{
    public static class ExceptionReplaceHandlerExtensions
    {
        public static IExceptionReplaceHandlerRegistration ReplaceWith<T>(this IExceptionHandlerRegistration context) where T : Exception
        {
            return ExceptionReplaceHandlerExtensions.ReplaceWith(context, typeof(T));
        }

        public static IExceptionReplaceHandlerRegistration ReplaceWith(this IExceptionHandlerRegistration context, Type replaceExceptionType)
        {
            if (replaceExceptionType == null)
            {
                throw new ArgumentNullException("replaceExceptionType");
            }

            if (!typeof(Exception).IsAssignableFrom(replaceExceptionType))
            {
                throw new ArgumentException("Type must be derived from Exception", "replaceExceptionType");
            }

            return new ExceptionReplaceHandlerBuilder(context, replaceExceptionType);
        }

        private class ExceptionReplaceHandlerBuilder : ExceptionHandlerBuilderExtension, IExceptionReplaceHandlerRegistration
        {
            private readonly CfReplaceHandlerData _handlerData;

            public ExceptionReplaceHandlerBuilder(IExceptionHandlerRegistration context, Type replaceExceptionType)
                : base(context)
            {
                _handlerData = new CfReplaceHandlerData()
                    {
                        ExceptionType = replaceExceptionType,
                        HandlerType = typeof(CfReplaceExceptionHandler)
                    };

                CurrentExceptionPolicy.ExceptionHandlingPolicy.AddHandlerData(_handlerData);
            }

            public IExceptionHandlerRegistrationForPolicyAndHandler UsingMessage(string message)
            {
                _handlerData.ExceptionMessage = message;
                return this;
            }
        }
    }
}