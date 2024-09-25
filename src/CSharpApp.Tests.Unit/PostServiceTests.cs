namespace CSharpApp.Tests.Unit;

public class PostServiceTests
{
    private readonly PostService _sut;
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly IOptions<ApiSettings> _apiSettingsOptions;

    public PostServiceTests()
    {
        _httpClientWrapper = Substitute.For<IHttpClientWrapper>();
        var apiSettings = new ApiSettings { BaseUrl = "https://jsonplaceholder.typicode.com" };
        _apiSettingsOptions = Substitute.For<IOptions<ApiSettings>>();
        _apiSettingsOptions.Value.Returns(apiSettings);
        _sut = new PostService(_httpClientWrapper, _apiSettingsOptions);
    }
    
    [Fact]
    public async Task GetPostById_ShouldReturnPostRecordResponse()
    {
        // Arrange
        var postId = 1;
        var expectedPost = new PostRecordResponse(1, postId, 
            "sunt aut facere repellat provident occaecati excepturi optio reprehenderit", 
            "quia et suscipit\\nsuscipit recusandae consequuntur expedita et cum\\nreprehenderit molestiae ut ut quas totam\\nnostrum rerum est autem sunt rem eveniet architecto");
        
        _httpClientWrapper.GetAsync<PostRecordResponse>($"https://jsonplaceholder.typicode.com/posts/{postId}")
            .Returns(expectedPost);

        // Act
        var result = await _sut.GetPostById(postId);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(expectedPost.UserId);
        result.Id.Should().Be(expectedPost.Id);
        result.Title.Should().Be(expectedPost.Title);
        result.Body.Should().Be(expectedPost.Body);
    }
    
    [Fact]
    public async Task GetPostById_ShouldReturnNull_WhenPostDoesNotExist()
    {
        // Arrange
        var postId = 999;
        PostRecordResponse? expectedPost = null;
        
        _httpClientWrapper.GetAsync<PostRecordResponse>($"https://jsonplaceholder.typicode.com/posts/{postId}")
            .Returns(expectedPost);

        // Act
        var result = await _sut.GetPostById(postId);

        // Assert
        result.Should().BeNull();
    }

    
    [Fact]
    public async Task GetAllPosts_ShouldReturnReadOnlyCollectionOfPostRecordResponse()
    {
        // Arrange
        var expectedPosts = new List<PostRecordResponse>
        {
            new PostRecordResponse(1, 1, 
                "sunt aut facere repellat provident occaecati excepturi optio reprehenderit", 
                "quia et suscipit\\nsuscipit recusandae consequuntur expedita et cum\\nreprehenderit molestiae ut ut quas totam\\nnostrum rerum est autem sunt rem eveniet architecto")
            ,
            new PostRecordResponse(1, 2, 
                "qui est esse", 
                "est rerum tempore vitae\\nsequi sint nihil reprehenderit dolor beatae ea dolores neque\\nfugiat blanditiis voluptate porro vel nihil molestiae ut reiciendis\\nqui aperiam non debitis possimus qui neque nisi nulla")
            ,
            new PostRecordResponse(1, 3, 
                "ea molestias quasi exercitationem repellat qui ipsa sit aut", 
                "et iusto sed quo iure\\nvoluptatem occaecati omnis eligendi aut ad\\nvoluptatem doloribus vel accusantium quis pariatur\\nmolestiae porro eius odio et labore et velit aut")
            ,
        };
        
        _httpClientWrapper.GetAsync<List<PostRecordResponse>>("https://jsonplaceholder.typicode.com/posts")
            .Returns(expectedPosts);

        // Act
        var result = await _sut.GetAllPosts();

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<ReadOnlyCollection<PostRecordResponse>>();
        result.Should().HaveCount(expectedPosts.Count);
        
        for (int i = 0; i < expectedPosts.Count; i++)
        {
            result[i].UserId.Should().Be(expectedPosts[i].UserId);
            result[i].Id.Should().Be(expectedPosts[i].Id);
            result[i].Title.Should().Be(expectedPosts[i].Title);
            result[i].Body.Should().Be(expectedPosts[i].Body);
        }
    }
    
    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedPostRecordResponse()
    {
        // Arrange
        var newPost = new PostRecordRequest(
            UserId: 1,
            Title: "New Post Title",
            Body: "This is the body of the new post."
        );

        var createdPostResponse = new PostRecordResponse(1, 1, newPost.Title, newPost.Body);
        
        _httpClientWrapper.PostAsync<PostRecordResponse>("posts", newPost)
            .Returns(createdPostResponse);

        // Act
        var result = await _sut.CreateAsync(newPost);

        // Assert
        result.Should().NotBeNull();
        result.UserId.Should().Be(createdPostResponse.UserId);
        result.Id.Should().Be(createdPostResponse.Id);
        result.Title.Should().Be(createdPostResponse.Title);
        result.Body.Should().Be(createdPostResponse.Body);
    }
    
    [Fact]
    public async Task DeleteByIdAsync_ShouldReturnTrue_WhenPostIsDeleted()
    {
        // Arrange
        var postId = 1;
        
        _httpClientWrapper.DeleteAsync($"posts/{postId}")
            .Returns(true);

        // Act
        var result = await _sut.DeleteByIdAsync(postId);

        // Assert
        result.Should().BeTrue();
    }
}