using System;

namespace TaskManager.Domain;

public class TodoTask : IComparable
{

    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime Date { get; set; }

    public User User { get; set; }

    public TodoTask()
    {
        this.Name = "";
        this.Date =  DateTime.Today;
    }

    public override bool Equals(Object obj)
    {
        bool equals = false;

        if (obj == null)
        {
            equals = false;
        }
        else if (!(obj is TodoTask))
        {
            equals = false;
        }
        else
        {
            TodoTask todoTask = ((TodoTask)obj);

            equals = this.Name == todoTask.Name
                && this.Date.Day == todoTask.Date.Day
                && this.Date.Month == todoTask.Date.Month
                && this.Date.Year == todoTask.Date.Year
                && this.User.Id == todoTask.User.Id;
        }

        return equals;
    }

    public int CompareTo(object obj)
    {
        int result = 0;

        if (obj == null)
            return 1;

        TodoTask secondTodoTask = obj as TodoTask;

        if (this.Id < secondTodoTask.Id )
            result = -1;
        else if (this.Id == secondTodoTask.Id)
            result = 0;
        else if (this.Id > secondTodoTask.Id)
            result = 1;

        return result;

    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

}
