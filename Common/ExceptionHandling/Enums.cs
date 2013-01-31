namespace CodeFiction.Stack.Common.ExceptionHandling
{
    public enum ExceptionManagers : byte
    {
        EnterpriseLibrary = 0
    }

    public enum PostHandlingAction : byte
    {
        None = 0,
        ThrowNewException = 1,
        NotifyRethrow =2
    }
}
