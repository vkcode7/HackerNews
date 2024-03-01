using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HackerNews.Controllers
{
    /*
     * API contrioller class
     */

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly HNHttpClient _HNHttpClient;
        private readonly IMemoryCache _cache; //local in memory cache; avoid hitting the Hacker-News server
        // we are using 2 minutes time out cache for best stories
        // 1 hour for caching individual story
        private const int DEFAULT_STORIES_COUNT = 10;
        private const int BEST_STORIES_CACHE_TIMEOUT_MINUTES = 2;
        private const int NEWS_STORY_CACHE_TIMEOUT_MINUTES = 60;

        public NewsController(HNHttpClient client, IMemoryCache cache)
        {
            _HNHttpClient = client;

            if (cache == null)
                throw new ArgumentNullException("Could not initialize MemoryCache");

            _cache = cache;
        }

        [HttpGet("get-best-stories")]
        public async Task<ActionResult<IEnumerable<NewsStory>>> GetBestStoriesDdefault()
        {
            return await GetBestStories(DEFAULT_STORIES_COUNT);
        }

        [HttpGet("get-best-stories/{n}")]
        public async Task<ActionResult<IEnumerable<NewsStory>>> GetBestStories(int n)
        {
            if (n < 0)
                n = Math.Abs(n);

            // Lets check in the cache first; if not in cache, add
            if (!_cache.TryGetValue("BestHNStoryIds", out int[] Ids))
            {
                // If Ids are not cached fetch and cache them for a time span of 2 minutes
                Ids = await _HNHttpClient.GetBestStoriesIdsAsync();
                _cache.Set("BestHNStoryIds", Ids, TimeSpan.FromMinutes(BEST_STORIES_CACHE_TIMEOUT_MINUTES));
            }

            var news_stories = new List<NewsStory>();
            if (Ids.Length > n) //we fetched more stories than requested
                Ids = Ids[..n];

            foreach (var id in Ids)
            {

                // Lets check in cache first, if unavailable, add
                if (!_cache.TryGetValue($"NewsStory_{id}", out NewsStory news_story))
                {
                    // we didnt get it from the cache, so fetch it from hacker news site
                    news_story = await _HNHttpClient.GetStoryAsync(id);

                    // add it to cache with a time out of 1 hour
                    _cache.Set($"NewsStory_{id}", news_story, TimeSpan.FromMinutes(NEWS_STORY_CACHE_TIMEOUT_MINUTES));
                }

                news_stories.Add(news_story);
            }
            news_stories = news_stories.OrderByDescending(s => s.Score).ToList();
            return Ok(news_stories);
        }
    }
}

