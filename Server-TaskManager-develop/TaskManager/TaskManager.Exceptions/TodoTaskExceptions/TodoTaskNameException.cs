namespace TaskManager.Exceptions.TodoTaskExceptions;

public class TodoTaskNameException : TodoTaskException
{
    public TodoTaskNameException(string mensaje) : base(mensaje)
    {
    }

}