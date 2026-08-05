using System.Text;
using System.Text.Json;
using TaskTracker.Client.Models;

namespace TaskTracker.Client;

class Program
{
    private static HttpClient client = new HttpClient();
    private static string baseUrl = "http://localhost:5000/api/tasks";

    static async Task Main(string[] args)
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.WriteLine("\n=== TASK TRACKER ===");
            Console.WriteLine("1. Show All Tasks");
            Console.WriteLine("2. Get Task By ID");
            Console.WriteLine("3. Add New Task");
            Console.WriteLine("4. Update Existing Task");
            Console.WriteLine("5. Delete Task");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option (1-6): ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                await GetAllTasks();
            }
            else if (choice == "2")
            {
                await GetTaskById();
            }
            else if (choice == "3")
            {
                await AddTask();
            }
            else if (choice == "4")
            {
                await UpdateTask();
            }
            else if (choice == "5")
            {
                await DeleteTask();
            }
            else if (choice == "6")
            {
                keepRunning = false;
                Console.WriteLine("Goodbye!");
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
    }

    static async Task GetAllTasks()
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync(baseUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<TaskItem> tasks = JsonSerializer.Deserialize<List<TaskItem>>(jsonString, options);

                Console.WriteLine("--- TASK LIST ---");
                if (tasks != null && tasks.Count > 0)
                {
                    foreach (var task in tasks)
                    {
                        string status = task.IsCompleted ? "[Done]" : "[Pending]";
                        Console.WriteLine($"ID: {task.Id} | {status} | Priority: {task.Priority} | {task.Title}");
                    }
                }
                else
                {
                    Console.WriteLine("No tasks found.");
                }
            }
            else
            {
                Console.WriteLine("Error fetching tasks. Status: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error connecting to API. Make sure the API project is running!");
            Console.WriteLine("Message: " + ex.Message);
        }
    }

    static async Task GetTaskById()
    {
        Console.Write("Enter Task ID: ");
        string idInput = Console.ReadLine();

        if (int.TryParse(idInput, out int id))
        {
            HttpResponseMessage response = await client.GetAsync(baseUrl + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                TaskItem task = JsonSerializer.Deserialize<TaskItem>(jsonString, options);

                if (task != null)
                {
                    Console.WriteLine($"\n--- TASK {task.Id} DETAILS ---");
                    Console.WriteLine("Title: " + task.Title);
                    Console.WriteLine("Description: " + task.Description);
                    Console.WriteLine("Priority: " + task.Priority);
                    Console.WriteLine("Completed: " + task.IsCompleted);
                }
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID number.");
        }
    }

    static async Task AddTask()
    {
        Console.Write("Enter Title: ");
        string title = Console.ReadLine();

        Console.Write("Enter Description: ");
        string description = Console.ReadLine();

        Console.Write("Enter Priority (Low/Medium/High): ");
        string priority = Console.ReadLine();

        TaskItem newTask = new TaskItem
        {
            Title = title,
            Description = description,
            Priority = priority,
            IsCompleted = false
        };

        string json = JsonSerializer.Serialize(newTask);
        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync(baseUrl, content);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Task added successfully!");
        }
        else
        {
            Console.WriteLine("Failed to add task.");
        }
    }

    static async Task UpdateTask()
    {
        Console.Write("Enter Task ID to update: ");
        string idInput = Console.ReadLine();

        if (int.TryParse(idInput, out int id))
        {
            Console.Write("Enter New Title: ");
            string title = Console.ReadLine();

            Console.Write("Enter New Description: ");
            string description = Console.ReadLine();

            Console.Write("Enter New Priority: ");
            string priority = Console.ReadLine();

            Console.Write("Is completed? (y/n): ");
            string completedInput = Console.ReadLine();
            bool isCompleted = completedInput.ToLower() == "y";

            TaskItem updatedTask = new TaskItem
            {
                Id = id,
                Title = title,
                Description = description,
                Priority = priority,
                IsCompleted = isCompleted
            };

            string json = JsonSerializer.Serialize(updatedTask);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync(baseUrl + "/" + id, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Task updated successfully!");
            }
            else
            {
                Console.WriteLine("Failed to update task.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID number.");
        }
    }

    static async Task DeleteTask()
    {
        Console.Write("Enter Task ID to delete: ");
        string idInput = Console.ReadLine();

        if (int.TryParse(idInput, out int id))
        {
            HttpResponseMessage response = await client.DeleteAsync(baseUrl + "/" + id);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Task deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete task.");
            }
        }
        else
        {
            Console.WriteLine("Invalid ID number.");
        }
    }
}