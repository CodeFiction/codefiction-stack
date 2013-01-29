using System;
using System.Collections.Generic;
using System.Linq;
using CfCommerce.Common.Configuration.Fluent;
using CfCommerce.Common.ExceptionHandling.Handlers;

namespace CfCommerce.Common.ExceptionHandling.Configuration
{
    public interface IExceptionPolicyBuilder
    {

    }

    public class ExceptionPolicyBuilder : IExceptionPolicyBuilder, 
                                          IPolicyRegistration, 
                                          IExceptionPolicyPostHandlingActionRegistration, 
                                          IExceptionPolicyRegistration, 
                                          IExceptionHandlerRegistration,
                                          IExceptionHandlerExtension
    {
        private readonly IExceptionHandlingConfiguration _exceptionHandlingConfiguration;

        private Policy _currentPolicy;
        private ExceptionPolicy _currentExceptionPolicy;

        internal ExceptionPolicyBuilder(IExceptionHandlingConfiguration exceptionHandlingConfiguration)
        {
            _exceptionHandlingConfiguration = exceptionHandlingConfiguration;
        }

        IExceptionPolicyRegistration IPolicyRegistration.AddPolicyWithName(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentException("Policy name cannot be null or empty", "name");
            }

            _currentPolicy = new Policy(name);
            _exceptionHandlingConfiguration.AddPolicy(_currentPolicy);
            return this;
        }

        IExceptionHandlerRegistration IExceptionPolicyRegistration.ForExceptionType(Type exceptionType)
        {
            if (exceptionType == null)
                throw new ArgumentNullException("exceptionType");

            if (!typeof(Exception).IsAssignableFrom(exceptionType))
            {
                throw new ArgumentException("Type must be derived from Exception", "exceptionType");
            }
            _currentExceptionPolicy = new ExceptionPolicy(exceptionType);
            _currentPolicy.AddExceptionHandlingPolicy(_currentExceptionPolicy);
            return this;
        }

        IExceptionHandlerRegistration IExceptionPolicyRegistration.ForExceptionType<T>()
        {
            return ((IExceptionPolicyRegistration) this).ForExceptionType(typeof (T));
        }

        IExceptionPolicyRegistration IExceptionPolicyPostHandlingActionRegistration.ThenDoNothing()
        {
            _currentExceptionPolicy.ExceptionHandlingPolicy.PostHandlingAction = PostHandlingAction.None;

            return this;
        }

        IExceptionPolicyRegistration IExceptionPolicyPostHandlingActionRegistration.ThenNotifyRethrow()
        {
            _currentExceptionPolicy.ExceptionHandlingPolicy.PostHandlingAction = PostHandlingAction.NotifyRethrow;

            return this;
        }

        IExceptionPolicyRegistration IExceptionPolicyPostHandlingActionRegistration.ThenThrowNewException()
        {
            _currentExceptionPolicy.ExceptionHandlingPolicy.PostHandlingAction = PostHandlingAction.ThrowNewException;

            return this;
        }

        ExceptionPolicy IExceptionHandlerExtension.CurrentExceptionPolicy
        {
            get { return _currentExceptionPolicy; }
        }
    }

    public class ExceptionHandlingConfiguration : IExceptionHandlingConfiguration
    {
        private readonly List<Policy> _policies;

        public Policy[] Policies { get { return _policies.ToArray(); } }

        public ExceptionHandlingConfiguration()
        {
            _policies = new List<Policy>();
        }

        //public void AddPolicy(Policy[] exceptionPolicies)
        //{
        //    // TODO : throw libraray specific Exception

        //    if (exceptionPolicies.IsNullOrEmpty())
        //    {
        //        throw new InvalidOperationException("Must be at least one policy");
        //    }

        //    if (exceptionPolicies.Any(registration => registration == null))
        //    {
        //        throw new InvalidOperationException("Policies can not be null");
        //    }

        //    foreach (var policyRegistration in exceptionPolicies)
        //    {
        //        ExceptionHandlingPolicyContainer container = policyRegistration.ExceptionHandlingPolicyContainer;

        //        if (container.Name.IsNullOrEmpty())
        //        {
        //            throw new InvalidOperationException("Policy must have name");
        //        }

        //        if (container.ExceptionHandlingPolicies.IsNullOrEmpty())
        //        {
        //            throw new InvalidOperationException(String.Format("Must be at least one exception handling policy. Policy Name {0}",policyRegistration.ExceptionHandlingPolicyContainer.Name));
        //        }

        //        if (container.ExceptionHandlingPolicies.Select(policy => policy.ExceptionType.FullName).Count() 
        //            != container.ExceptionHandlingPolicies.Select(policy => policy.ExceptionType.FullName).Distinct().Count())
        //        {
        //            throw new InvalidOperationException(String.Format("Policy can not contain more than one same type of exception.  Policy Name {0}", policyRegistration.ExceptionHandlingPolicyContainer.Name));
        //        }
        //    }

        //    if (exceptionPolicies.Select(registration => registration.ExceptionHandlingPolicyContainer.Name).Count()
        //            != exceptionPolicies.Select(registration => registration.ExceptionHandlingPolicyContainer.Name).Distinct().Count())
        //    {
        //        throw new InvalidOperationException("Can not have more than one policy with the same name.");
        //    }

        //    _policies = exceptionPolicies;
        //}

        public void AddPolicy(Policy policy)
        {
            _policies.Add(policy);
        }

        public IPolicyRegistration BuildPolicies()
        {
            return new ExceptionPolicyBuilder(this);
        }
    }

    public interface IExceptionHandlingConfiguration
    {
        void AddPolicy(Policy policy);
        Policy[] Policies { get; }
    }

    public interface IPolicyRegistration : IFluentInterface
    {
        IExceptionPolicyRegistration AddPolicyWithName(string name);
    }

    public interface IExceptionPolicyRegistration : IPolicyRegistration
    {
        IExceptionHandlerRegistration ForExceptionType(Type exceptionType);
        IExceptionHandlerRegistration ForExceptionType<T>() where T : Exception;
    }

    public interface IExceptionPolicyPostHandlingActionRegistration : IFluentInterface
    {
        /// <summary>
        /// End the current exception handling chain by doing nothing more.
        /// </summary>
        /// <returns></returns>
        IExceptionPolicyRegistration ThenDoNothing();

        /// <summary>
        /// End the current exception handling chain by notifying the caller that an exception should be rethrown.
        /// </summary>
        /// <returns></returns>
        IExceptionPolicyRegistration ThenNotifyRethrow();

        /// <summary>
        /// End the current exception handling chain by throwing a new exception.
        /// </summary>
        /// <returns></returns>
        IExceptionPolicyRegistration ThenThrowNewException();
    }

    public interface IExceptionHandlerRegistration : IExceptionPolicyPostHandlingActionRegistration, IFluentInterface
    {
        
    }

    public interface IExceptionWrapHandlerRegistration : IExceptionHandlerMessage
    {
        
    }

    public interface IExceptionHandlerMessage : IExceptionHandlerRegistrationForPolicyAndHandler
    {
        IExceptionHandlerRegistrationForPolicyAndHandler UsingMessage(string message);
    }

    public interface IExceptionHandlerRegistrationForPolicyAndHandler : IPolicyRegistration, IExceptionPolicyPostHandlingActionRegistration, IExceptionHandlerRegistration
    {

    }

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

                CurrentExceptionPolicy.ExceptionHandlingPolicy.AddHandler(_handlerData);
            }

            public IExceptionHandlerRegistrationForPolicyAndHandler UsingMessage(string message)
            {
                _handlerData.ExceptionMessage = message;
                return this;
            }
        }
    }

    public abstract class ExceptionHandlerBuilderExtension : IExceptionHandlerRegistrationForPolicyAndHandler, IExceptionHandlerExtension
    {
        protected IExceptionHandlerRegistrationForPolicyAndHandler Context { get; private set; }

        protected ExceptionHandlerBuilderExtension(IExceptionHandlerRegistration context)
        {
            Context = (IExceptionHandlerRegistrationForPolicyAndHandler)context;
        }

        public ExceptionPolicy CurrentExceptionPolicy
        {
            get
            {
               return ((IExceptionHandlerExtension)Context).CurrentExceptionPolicy;
            }
        }

        IExceptionPolicyRegistration IPolicyRegistration.AddPolicyWithName(string name)
        {
           return Context.AddPolicyWithName(name);
        }

        IExceptionPolicyRegistration IExceptionPolicyPostHandlingActionRegistration.ThenDoNothing()
        {
            return Context.ThenDoNothing();
        }

        IExceptionPolicyRegistration IExceptionPolicyPostHandlingActionRegistration.ThenNotifyRethrow()
        {
            return Context.ThenNotifyRethrow();
        }

        IExceptionPolicyRegistration IExceptionPolicyPostHandlingActionRegistration.ThenThrowNewException()
        {
            return Context.ThenThrowNewException();
        }
    }

    public interface IExceptionHandlerExtension : IFluentInterface
    {
        ExceptionPolicy CurrentExceptionPolicy { get; }
    }

    public interface IExceptionReplaceHandlerRegistration : IExceptionHandlerMessage
    {

    }

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

                 CurrentExceptionPolicy.ExceptionHandlingPolicy.AddHandler(_handlerData);
             }

             public IExceptionHandlerRegistrationForPolicyAndHandler UsingMessage(string message)
             {
                 _handlerData.ExceptionMessage = message;
                 return this;
             }
         }
     }

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

                CurrentExceptionPolicy.ExceptionHandlingPolicy.AddHandler(baseHandlerData);
            }
        }
         
    }

}
