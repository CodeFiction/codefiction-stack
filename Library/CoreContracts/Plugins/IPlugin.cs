namespace CodeFiction.Stack.Library.CoreContracts.Plugins
{
    /// <summary>
    /// Represents the interface for application plugins.
    /// </summary>
    public interface IPlugin
    {
        string Name { get; }

        void LoadPlugin(IDependencyResolver resolver);
    }
}
