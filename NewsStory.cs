
namespace HackerNews
{
    /* Copied from the requirements. 
     {
    "title": "A uBlock Origin update was rejected from the Chrome Web Store",
    "uri": "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
    "postedBy": "ismaildonmez",
    "time": "2019-10-12T13:43:01+00:00",
    "score": 1716,
    "commentCount": 572
    },

    NOTE: The CommentCount is no longer in the response retrieved from HackerNews site
     */

    /*
     * This is what we return to clients
     */
    public class NewsStory
    {
        public NewsStory(
            string title,
            string uri,
            string postedBy,
            DateTime time,
            int score,
            int commentCount)
        {
            Title = title;
            Uri = uri;
            PostedBy = postedBy;
            Time = time;
            Score = score;
            CommentCount = commentCount;
        }

        public string Title { get; set; }
        public string Uri { get; set; }
        public string PostedBy { get; set; }
        public DateTime Time { get; set; }
        public int Score { get; set; }
        public int CommentCount { get; set; } = 0;
    }

    /*
     * Used for the purpose of deserialization of json data to story object
     */
    public class HackerNewsStory
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string By { get; set; }
        public long Time { get; set; }
        public int Score { get; set; }
        public int CommentCount { get; set; } = 0;

        public NewsStory getNewsStory()
        {
            //The time in response is in Unix time format, so added a converter here
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(Time);
            return new NewsStory(Title, Url, By, dateTimeOffset.UtcDateTime, Score, CommentCount);
        }
    }
}



