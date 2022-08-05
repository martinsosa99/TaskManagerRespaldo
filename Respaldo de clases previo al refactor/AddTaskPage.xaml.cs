using System.Text;
using System.Text.Json;

namespace TaskManager;

public partial class AddTaskPage : ContentPage
{

    HttpClient _client;
    JsonSerializerOptions _serializerOptions;

    public UserSessionData UserSessionData { get; set; }

    public AddTaskPage(UserSessionData userSessionData)
    {
        InitializeComponent();
        this.UserSessionData = userSessionData;
    }

    private void OnAddTaskBtnClicked(object sender, EventArgs e)
    {
        this.AddTask();
    }

    public async Task AddTask()
    {
        try
        {
            _client = new HttpClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };

            UserDTO userDTO = new UserDTO();
            userDTO.id = this.UserSessionData.UserId;
            userDTO.emailAddress = "";
            userDTO.password = "";

            TodoTaskDTO todoTaskDTO = new TodoTaskDTO();
            todoTaskDTO.id = 0;
            todoTaskDTO.name = TaskNameEntry.Text;

            int month = TaskNamDatePicker.Date.Month;
            int day = TaskNamDatePicker.Date.Day;
            int year = TaskNamDatePicker.Date.Year;

            todoTaskDTO.date = new DateTime(year, month, day);
            todoTaskDTO.user = userDTO;

            Uri uri = new Uri(string.Format("http://localhost:5000/api/tasks", string.Empty));

            string json = JsonSerializer.Serialize<TodoTaskDTO>(todoTaskDTO, _serializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;

            _client.DefaultRequestHeaders.Add("token", this.UserSessionData.Token);

            response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                TaskNameEntry.Text = "";
                TaskNamDatePicker.Date = DateTime.Today;
                messageLabel.Text = "Task added successfully!";
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var responseDTO = JsonSerializer.Deserialize<ResponseDTO>(responseContent);

                messageLabel.Text = responseDTO.errorMessage;
            }

        }
        catch (Exception ex)
        {
            messageLabel.Text = ex.Message;
        }

    }

}