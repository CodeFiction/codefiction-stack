using System;

namespace CodeFiction.Stack.Library.AspectCore.Attributes
{
    internal enum ExecutionOrder
    {
        Before,
        After,
        Exception
    }

    /// <summary>
    /// Base class for Aspect attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class AspectAttributeBase : Attribute
    {
        /// <summary>
        /// Initializes a new instance of AspectAttributeBase.
        /// </summary>
        /// <param name="order">Execution order of the current attribute 
        /// among all the other attributes used on the same method.</param>
        protected AspectAttributeBase(byte order)
        {
            Order = order;
        }

        /// <summary>
        /// Initializes a new instance of AspectAttributeBase.
        /// </summary>
        protected AspectAttributeBase()
            : this(0)
        {
        }

        /// <summary>
        /// Gets or sets the execution order of the current attribute 
        /// among all the other attributes used on the same method.
        /// </summary>
        public byte Order { get; set; }
    }
}