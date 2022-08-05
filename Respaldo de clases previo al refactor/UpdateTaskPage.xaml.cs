using System.Text;
using System.Text.Json;
using Android.Service.Autofill;

namespace TaskManager;

public partial class UpdateTaskPage : ContentPage
{

    public UserSessionData UserSessionData { get; set; }

    private HttpClient HttpClient;

    private JsonSerializerOptions JsonSerializerOptions;

    public UpdateTaskPage(UserSessionData userSessionData)
    {
        InitializeComponent();
        this.UserSessionData = userSessionData;
    }

    private void OnUpdateTaskBtnClicked(object sender, EventArgs e)
    {
        this.UpdateTask();
    }

    private async Task UpdateTask()
    {
        try
        {
            UserDTO userDTO = this.CreateUserDTO();
            TodoTaskDTO todoTaskDTO = this.CreateTodoTaskDTO(userDTO);

            Uri uri = new Uri(string.Format("http://localhost:5000/api/tasks", string.Empty));

            string json = JsonSerializer.Serialize<TodoTaskDTO>(todoTaskDTO, JsonSerializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            
            HttpClient.DefaultRequestHeaders.Add("token", this.UserSessionData.Token);

            response = await HttpClient.PutAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                TaskIdEntry.Text = "";
                TaskNameEntry.Text = "";
                TaskDateEntry.Date = DateTime.Today;
                MessageLbl.Text = "Task updated successfully!";
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var responseDTO = JsonSerializer.Deserialize<ResponseDTO>(responseContent);
                MessageLbl.Text = responseDTO.errorMessage;
            }
        }
        catch (Exception ex)
        {
            MessageLbl.Text = ex.Message;
        }

    }

    private void SetHttpConnection()
    {
        HttpClient = new HttpClient();
        JsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };
    }

    private UserDTO CreateUserDTO()
    {
        UserDTO userDTO = new UserDTO();
        userDTO.id = this.UserSessionData.UserId;
        userDTO.emailAddress = "";
        userDTO.password = "";

        return userDTO;
    }

    private TodoTaskDTO CreateTodoTaskDTO(UserDTO userDTO)
    {
        TodoTaskDTO todoTaskDTO = new TodoTaskDTO();
        todoTaskDTO.id = Int32.Parse(TaskIdEntry.Text);
        todoTaskDTO.name = TaskNameEntry.Text;
        todoTaskDTO.date = TaskDateEntry.Date;
        todoTaskDTO.user = userDTO;

        return todoTaskDTO;
    }

}