using TaskManager.Domain;

namespace TaskManager.DTO;

public class TodoTaskDTO
{

    public int id { get; set; }

    public string name { get; set; }

    public DateTime date { get; set; }

    public UserDTO user { get; set; }

    public TodoTaskDTO()
    {
    }

    public static TodoTask TodoTaskDTOToTodoTask(TodoTaskDTO todoTaskDTO)
    {
        TodoTask todoTask = new TodoTask();

        todoTask.Id = todoTaskDTO.id;
        todoTask.Name = todoTaskDTO.name;
        todoTask.Date = todoTaskDTO.date;
        todoTask.User = UserDTO.UserDTOToUser(todoTaskDTO.user);

        return todoTask;
    }

    public static TodoTaskDTO TodoTaskToTodoTaskDTO(TodoTask todoTask)
    {
        TodoTaskDTO todoTaskDTO = new TodoTaskDTO();

        todoTaskDTO.id = todoTask.Id;
        todoTaskDTO.name = todoTask.Name;
        todoTaskDTO.date = todoTask.Date;
        todoTaskDTO.user = UserDTO.UserToUserDTO(todoTask.User);

        return todoTaskDTO;
    }

    public static List<TodoTaskDTO> TodoTasksToTodoTasksDTO(List<TodoTask> todoTasks)
    {
        List<TodoTaskDTO> todoTasksDTO = new List<TodoTaskDTO>();

        for (int index = 0; index < todoTasks.Count; index++)
        {
            TodoTaskDTO todoTaskDTO = TodoTaskDTO.TodoTaskToTodoTaskDTO(todoTasks[index]);
            todoTasksDTO.Add(todoTaskDTO);
        }

        return todoTasksDTO;
    }

}