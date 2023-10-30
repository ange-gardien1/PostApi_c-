using tweeter_api.Models;
using tweeter_api.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace tweeter_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TweetsController : ControllerBase 
{
    private readonly ILogger<TweetsController> _logger;
    private readonly ITweetRepository _tweetRepository;

    public TweetsController(ILogger<TweetsController> logger, ITweetRepository repository)
    {
        _logger = logger;
        _tweetRepository = repository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Tweet>> GetAllTweets() 
    {
        return Ok(_tweetRepository.GetTweets());
    }

    [HttpGet]
    [Route("{tweetId:int}")]
    public ActionResult<Tweet> GetTweetById(int tweetId) 
    {
        var tweet = _tweetRepository.GetTweetById(tweetId);
        if (tweet == null) {
            return NotFound();
        }
        return Ok(tweet);
    }

    [HttpGet]
    [Route("user/{userId:int}")]
    public ActionResult<Tweet> GetTweetByUserId(int userId) 
    {
        var tweets = _tweetRepository.GetTweetsByUserId(userId);
        return Ok(tweets);
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Tweet> CreateTweet(Tweet tweet) 
    {
        if (!ModelState.IsValid || tweet == null) {
            return BadRequest();
        }
        if (HttpContext.User == null) {
            return Unauthorized();
        }

        var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Tweeter_UserID");
        tweet.UserId = Int32.Parse(userId.Value);

        var newTweet = _tweetRepository.CreateTweet(tweet);
        return Created(nameof(GetTweetById), newTweet);
    }

    [HttpPut]
    [Route("{tweetId:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<Tweet> UpdateTweet(Tweet tweet) 
    {
        if (!ModelState.IsValid || tweet == null) {
            return BadRequest();
        }
        return Ok(_tweetRepository.UpdateTweet(tweet));
    }

    [HttpDelete]
    [Route("{tweetId:int}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult DeleteTweet(int tweetId) 
    {
        _tweetRepository.DeleteTweetById(tweetId); 
        return NoContent();
    }
}
