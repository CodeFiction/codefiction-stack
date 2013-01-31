using System.ComponentModel;

namespace CodeFiction.Stack.Common.Configuration.Fluent
{
    /// <summary>
    /// Interface that is used to build fluent interfaces and hides methods declared by <see cref="object"/> from IntelliSense.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFluentInterface
    {
    }
}
