namespace tweeter_api.Models;

public class Tweet {
    public int TweetId { get; set; }
    public string? Message { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public DateTime CreatedOn { get; set; }
} 