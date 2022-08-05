using TaskManager.Domain;
using TaskManager.IBusinessLogic;
using TaskManager.IDataAccess;
using TaskManager.Validators;
using TaskManager.Exceptions.UsersExceptions;
using TaskManager.Exceptions.TodoTaskExceptions;
using TaskManager.Exceptions.AuthenticationExceptions;

namespace TaskManager.BusinessLogic.Services.Tasks;

delegate TodoTask TaskIsRepeated(TodoTask todoTask, List<TodoTask> todoTasks);

public class TodoTaskService : ITodoTaskService
{

    public IUserDataAccess UserDataAccess { get; set; }

    public IAuthenticationDataAccess AuthenticationDataAccess { get; set; }

    public ITodoTaskDataAccess TodoTaskDataAccess { get; set; }

    public TodoTaskService(IUserDataAccess userDataAccess, ITodoTaskDataAccess todoTaskDataAccess,
        IAuthenticationDataAccess authenticationDataAccess)
    {
        this.UserDataAccess = userDataAccess;
        this.TodoTaskDataAccess = todoTaskDataAccess;
        this.AuthenticationDataAccess = authenticationDataAccess;
    }

    public void AddTodoTask(TodoTask todoTask)
    {
        this.ValidateTodoTaskAttributes(todoTask);
        this.ValidateThatTodoTaskUserExists(todoTask.User);

        TaskIsRepeated taskIsRepeated = new TaskIsRepeated(this.CheckIfTaskIsRepeatedComparingByEquals);
        this.ValidateThatTodoTaskIsNotRepeated(todoTask, taskIsRepeated);

        this.TodoTaskDataAccess.AddTodoTask(todoTask);
    }

    public void DeleteTodoTask(int todoTaskId, string usersToken)
    {
        this.ValidateToken(usersToken);

        TodoTask todoTask = this.ValidateThatTodoTaskExists(todoTaskId);

        this.ValidateThatTodoTaskBelongsToUser(todoTask, usersToken);

        this.TodoTaskDataAccess.DeleteTodoTask(todoTask);
    }

    public void UpdateTodoTask(TodoTask todoTask, string usersToken)
    {
        this.ValidateToken(usersToken);

        this.ValidateTodoTaskAttributes(todoTask);
        TodoTask todoTasksThatExists = this.ValidateThatTodoTaskExists(todoTask.Id);
        this.ValidateThatTodoTaskUserExists(todoTask.User);

        TaskIsRepeated taskIsRepeated = new TaskIsRepeated(this.CheckIfTaskIsRepeatedComparingByIdAndEquals);
        this.ValidateThatTodoTaskIsNotRepeated(todoTask, taskIsRepeated);

        this.ValidateThatTodoTaskBelongsToUser(todoTasksThatExists, usersToken);

        this.TodoTaskDataAccess.UpdateTodoTask(todoTask);
    }

    public List<TodoTask> TodoTasks(string usersToken)
    {
        this.ValidateToken(usersToken);

        List<TodoTask> systemTodoTasks = this.TodoTaskDataAccess.TodoTasks();
        List<TodoTask> usersTodoTasks = new List<TodoTask>();

        User todoTaskOwner = this.AuthenticationDataAccess.GetUserByToken(usersToken);

        for (int index = 0; index < systemTodoTasks.Count; index++)
        {
            TodoTask currentTodoTask = systemTodoTasks[index];

            if (currentTodoTask.User.Equals(todoTaskOwner))
            {
                usersTodoTasks.Add(currentTodoTask);
            }
        }

        usersTodoTasks.Sort();

        return usersTodoTasks;
    }

    private void ValidateTodoTaskAttributes(TodoTask todoTask)
    {
        TodoTaskValidator todoTaskValidator = new TodoTaskValidator();
        todoTaskValidator.ValidateTodoTask(todoTask);
    }

    private void ValidateThatTodoTaskUserExists(User todoTaskUser)
    {
        List<User> systemUsers = this.UserDataAccess.Users();

        User user = systemUsers.FirstOrDefault(currentUser => currentUser.Id == todoTaskUser.Id);

        if (user == null)
            throw new UserIsNullException("User doesn't exist.");
    }

    private TodoTask CheckIfTaskIsRepeatedComparingByEquals(TodoTask todoTask, List<TodoTask> todoTasks)
    {
        TodoTask todoTaskToReturn = todoTasks.FirstOrDefault(currentTask => currentTask.Equals(todoTask));

        return todoTaskToReturn;
    }

    private void ValidateThatTodoTaskIsNotRepeated(TodoTask todoTask, TaskIsRepeated taskIsRepeated)
    {
        List<TodoTask> todoTasks = this.TodoTaskDataAccess.TodoTasks();

        TodoTask repeatedTodoTask = taskIsRepeated(todoTask, todoTasks);

        if (repeatedTodoTask != null)
            throw new TodoTaskIsRepeatedException("Task is repeated.");
    }

    private void ValidateToken(string usersToken)
    {
        if (String.IsNullOrWhiteSpace(usersToken))
            throw new InvalidTokenException("Token is not valid.");
    }

    private TodoTask ValidateThatTodoTaskExists(int todoTaskId)
    {
        List<TodoTask> systemTodoTasks = this.TodoTaskDataAccess.TodoTasks();

        TodoTask todoTask = systemTodoTasks.FirstOrDefault(currentTodoTask => currentTodoTask.Id == todoTaskId);

        if (todoTask == null)
            throw new TodoTaskIsNullException("Task doesn't exist.");

        return todoTask;
    }

    private TodoTask CheckIfTaskIsRepeatedComparingByIdAndEquals(TodoTask todoTask, List<TodoTask> todoTasks)
    {
        TodoTask todoTaskToReturn = todoTasks.FirstOrDefault(currentTask => (currentTask.Id != todoTask.Id) && (currentTask.Equals(todoTask)));

        return todoTaskToReturn;

    }

    private void ValidateThatTodoTaskBelongsToUser(TodoTask todoTask, string usersToken)
    {
        User todoTaskOwner = this.AuthenticationDataAccess.GetUserByToken(usersToken);

        if (todoTaskOwner == null || todoTask.User.Id != todoTaskOwner.Id)
            throw new UserNotAllowedToProceedException("This task doesn't belong to you!");
    }

}
