using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFiction.Stack.Library.Core.Castle.Common
{
    internal sealed class AttributeContext<TAttributeType>
        where TAttributeType : Attribute
    {
        public AttributeContext()
            : this(default(TAttributeType))
        {
        }

        public AttributeContext(TAttributeType attribute)
        {
            AttributeInstance = attribute;
        }

        public TAttributeType AttributeInstance { get; private set; }


        public bool HasAttribute { get; set; }
    }
}
