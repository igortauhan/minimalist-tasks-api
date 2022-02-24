namespace MinimalistTasks.Exceptions;

public class ObjectNotFoundException : Exception
{
    public ObjectNotFoundException(string message) : base(message)
    {
    }
}