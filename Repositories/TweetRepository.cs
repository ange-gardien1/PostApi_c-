using Microsoft.EntityFrameworkCore;
using tweeter_api.Migrations;
using tweeter_api.Models;

namespace tweeter_api.Repositories;

public class TweetRepository : ITweetRepository 
{
    private readonly TweeterDbContext _context;

    public TweetRepository(TweeterDbContext context)
    {
        _context = context;
    }

    public Tweet CreateTweet(Tweet newTweet)
    {
        _context.Tweets.Add(newTweet);
        _context.SaveChanges();
        return newTweet;
    }

    public void DeleteTweetById(int tweetId)
    {
        var tweet = _context.Tweets.Find(tweetId);
        if (tweet != null) {
            _context.Tweets.Remove(tweet); 
            _context.SaveChanges(); 
        }
    }

    public IEnumerable<Tweet> GetTweets()
    {
        return _context.Tweets
            .Include(t => t.User)
            .ToList();
    }

    public Tweet? GetTweetById(int tweetId)
    {
        return _context.Tweets.Include(t => t.User).SingleOrDefault(c => c.TweetId == tweetId);
    }

    public IEnumerable<Tweet>? GetTweetsByUserId(int userId)
    {
        return _context.Tweets.Include(t => t.User).Where(c => c.UserId == userId);
    }

    public Tweet? UpdateTweet(Tweet newTweet)
    {
        var originalTweet = _context.Tweets.Find(newTweet.TweetId);
        if (originalTweet != null) {
            originalTweet.Message = newTweet.Message;
            _context.SaveChanges();
        }
        return originalTweet;
    }
}