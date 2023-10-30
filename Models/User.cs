using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace tweeter_api.Models;

public class User 
{
    public int UserId { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
    [Required]
    public string? City { get; set; }
    [Required]
    public string? State { get; set; }
    public DateTime? CreatedOn {get; set;}
    [JsonIgnore]
    public IEnumerable<Tweet>? Tweets { get; set; }
}