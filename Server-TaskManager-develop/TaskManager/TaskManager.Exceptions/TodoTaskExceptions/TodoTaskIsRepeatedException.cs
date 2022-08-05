namespace TaskManager.Exceptions.TodoTaskExceptions;

public class TodoTaskIsRepeatedException : TodoTaskException
{
    public TodoTaskIsRepeatedException(string mensaje) : base(mensaje)
    {
    }
}

