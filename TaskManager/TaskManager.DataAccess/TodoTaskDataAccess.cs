using Microsoft.EntityFrameworkCore;
using TaskManager.Domain;
using TaskManager.IDataAccess;

namespace TaskManager.DataAccess;

public class TodoTaskDataAccess : ITodoTaskDataAccess
{
    private DataBaseContext DataBaseContext { get; set; }

    public TodoTaskDataAccess(DataBaseContext dataBaseContext)
    {
        this.DataBaseContext = dataBaseContext;
    }

    public void AddTodoTask(TodoTask todoTask)
    {
        var userOfTodoTask = this.DataBaseContext.Set<User>().FirstOrDefault<User>(systemUser => systemUser.Id == todoTask.User.Id);
        todoTask.User = userOfTodoTask;

        this.DataBaseContext.Attach(todoTask.User);
        this.DataBaseContext.Set<TodoTask>().Add(todoTask);
        this.DataBaseContext.SaveChanges();
    }

    public void DeleteTodoTask(TodoTask todoTask)
    {
        this.DataBaseContext.Set<TodoTask>().Remove(todoTask);
        this.DataBaseContext.SaveChanges();
    }

    public List<TodoTask> TodoTasks()
    {
        return this.DataBaseContext.Set<TodoTask>().Include(todoTask => todoTask.User).ToList<TodoTask>();
    }

    public void UpdateTodoTask(TodoTask todoTask)
    {
        var userOfTask = this.DataBaseContext.Set<User>().FirstOrDefault<User>(systemUser => systemUser.Id == todoTask.User.Id);
        todoTask.User = userOfTask;

        var todoTaskToUpdate = this.DataBaseContext.Set<TodoTask>().FirstOrDefault<TodoTask>(systemTodoTask => systemTodoTask.Id == todoTask.Id);
        todoTaskToUpdate.Name = todoTask.Name;
        todoTaskToUpdate.Date = todoTask.Date;
        todoTaskToUpdate.User = todoTask.User;

        this.DataBaseContext.SaveChanges();
    }

}