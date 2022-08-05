using TaskManager.Domain;

namespace TaskManager.IDataAccess;

public interface ITodoTaskDataAccess
{

    public void AddTodoTask(TodoTask todoTask);

    public List<TodoTask> TodoTasks();

    public void DeleteTodoTask(TodoTask todoTask);

    public void UpdateTodoTask(TodoTask todoTask);

}