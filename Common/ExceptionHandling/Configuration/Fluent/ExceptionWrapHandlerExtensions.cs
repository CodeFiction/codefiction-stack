using System;
using CfCommerce.Common.ExceptionHandling.Handlers;

namespace CfCommerce.Common.ExceptionHandling.Configuration.Fluent
{
    public static class ExceptionWrapHandlerExtensions
    {
        public static IExceptionWrapHandlerRegistration WrapWith<T>(this IExceptionHandlerRegistration context) where T : Exception
        {
            return ExceptionWrapHandlerExtensions.WrapWith(context, typeof (T));
        }

        public static IExceptionWrapHandlerRegistration WrapWith(this IExceptionHandlerRegistration context, Type wrappingExceptionType)
        {
            if (wrappingExceptionType == null)
            {
                throw new ArgumentNullException("wrappingExceptionType");
            }

            if (!typeof (Exception).IsAssignableFrom(wrappingExceptionType))
            {
                throw new ArgumentException("Type must be derived from Exception", "wrappingExceptionType");
            }

            return new ExceptionWrapHandlerBuilder(context, wrappingExceptionType);
        }

        private class ExceptionWrapHandlerBuilder : ExceptionHandlerBuilderExtension, IExceptionWrapHandlerRegistration
        {
            private readonly CfWrapHandlerData _handlerData;

            public ExceptionWrapHandlerBuilder(IExceptionHandlerRegistration context, Type wrappingExceptionType)
                : base(context)
            {
                _handlerData = new CfWrapHandlerData()
                    {
                        ExceptionType = wrappingExceptionType,
                        HandlerType = typeof(CfWrapExceptionHandler)
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