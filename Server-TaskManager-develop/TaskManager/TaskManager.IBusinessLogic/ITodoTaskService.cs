using TaskManager.Domain;

namespace TaskManager.IBusinessLogic;

public interface ITodoTaskService
{

    public void AddTodoTask(TodoTask todoTask);

    public void DeleteTodoTask(int todoTaskId, string usersToken);

    public void UpdateTodoTask(TodoTask todoTask, string usersToken);

    public List<TodoTask> TodoTasks(string usersToken);

}