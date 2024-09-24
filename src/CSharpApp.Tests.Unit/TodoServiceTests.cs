namespace CSharpApp.Tests.Unit;

public class TodoServiceTests
{
    private readonly TodoService _sut;
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly IOptions<ApiSettings> _apiSettingsOptions;

    public TodoServiceTests()
    {
        _httpClientWrapper = Substitute.For<IHttpClientWrapper>();
        var apiSettings = new ApiSettings { BaseUrl = "https://jsonplaceholder.typicode.com" };
        _apiSettingsOptions = Substitute.For<IOptions<ApiSettings>>();
        _apiSettingsOptions.Value.Returns(apiSettings);
        _sut = new TodoService(_httpClientWrapper, _apiSettingsOptions);
    }
    
    [Fact]
    public async Task GetTodoById_ShouldReturnTodoRecordResponse()
    {
        // Arrange
        var todoId = 1;
        var expectedTodo = new TodoRecordResponse(1, todoId, 
            "delectus aut autem", true);
        
        _httpClientWrapper.GetAsync<TodoRecordResponse>($"https://jsonplaceholder.typicode.com/todos/{todoId}")
            .Returns(expectedTodo);

        // Act
        var result = await _sut.GetTodoById(todoId);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(expectedTodo.UserId);
        result.Id.Should().Be(expectedTodo.Id);
        result.Title.Should().Be(expectedTodo.Title);
        result.Completed.Should().Be(expectedTodo.Completed);
    }
    
    [Fact]
    public async Task GetAllTodos_ShouldReturnReadOnlyCollectionOfTodoRecordResponse()
    {
        // Arrange
        var expectedTodos = new List<TodoRecordResponse>
        {
            new TodoRecordResponse(1, 1, "delectus aut autem", true),
            new TodoRecordResponse(1, 2, "quis ut nam facilis et officia qui", false),
            new TodoRecordResponse(1, 3, "fugiat veniam minus", false),
        };
        
        _httpClientWrapper.GetAsync<List<TodoRecordResponse>>("https://jsonplaceholder.typicode.com/todos")
            .Returns(expectedTodos);

        // Act
        var result = await _sut.GetAllTodos();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ReadOnlyCollection<TodoRecordResponse>>();
        result.Should().HaveCount(expectedTodos.Count);

        
        for (int i = 0; i < expectedTodos.Count; i++)
        {
            result[i].UserId.Should().Be(expectedTodos[i].UserId);
            result[i].Id.Should().Be(expectedTodos[i].Id);
            result[i].Title.Should().Be(expectedTodos[i].Title);
            result[i].Completed.Should().Be(expectedTodos[i].Completed);
        }
    }
}