namespace TaskManager.Exceptions.TodoTaskExceptions;

public class TodoTaskIsNullException : TodoTaskException
{

    public TodoTaskIsNullException(string mensaje) : base(mensaje)
    {

    }

}