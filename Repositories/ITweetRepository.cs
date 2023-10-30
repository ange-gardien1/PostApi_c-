using tweeter_api.Models;

namespace tweeter_api.Repositories;
public interface ITweetRepository
{
    IEnumerable<Tweet> GetTweets();
    Tweet? GetTweetById(int tweetId);
    IEnumerable<Tweet>? GetTweetsByUserId(int userId);
    Tweet CreateTweet(Tweet newTweet);
    Tweet? UpdateTweet(Tweet newTweet);
    void DeleteTweetById(int tweetId);
}