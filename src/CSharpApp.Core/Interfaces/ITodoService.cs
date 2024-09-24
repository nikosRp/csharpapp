namespace CSharpApp.Core.Interfaces;

public interface ITodoService
{
    Task<TodoRecordResponse?> GetTodoById(int id);
    Task<ReadOnlyCollection<TodoRecordResponse>> GetAllTodos();
}