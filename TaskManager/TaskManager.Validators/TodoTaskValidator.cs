using TaskManager.Domain;
using TaskManager.Exceptions.TodoTaskExceptions;
using TaskManager.Exceptions.UsersExceptions;

namespace TaskManager.Validators;

public class TodoTaskValidator
{

    public void ValidateTodoTask(TodoTask todoTask)
    {
        this.ValidateIfTodoTaskIsNull(todoTask);
        this.ValidateName(todoTask.Name);
        this.ValidateDate(todoTask.Date);
        this.ValidateIfUserIsNull(todoTask.User);
    }

    private void ValidateIfTodoTaskIsNull(TodoTask todoTask)
    {
        if (todoTask == null)
            throw new TodoTaskIsNullException("Task can not be empty.");
    }

    private void ValidateName(string todoTaskName)
    {
        if (String.IsNullOrWhiteSpace(todoTaskName))
            throw new TodoTaskNameException("Task's name can't be empty.");
    }

    private void ValidateDate(DateTime todoTaskDate)
    {
        if (DateTime.Compare(todoTaskDate, DateTime.Today) < 0)
            throw new TodoTaskDateException("Task's date must be a future date, or today.");
    }

    private void ValidateIfUserIsNull(User user)
    {
        if (user == null)
            throw new UserIsNullException("User can not be empty.");
    }

}