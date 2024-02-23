using Newtonsoft.Json;

namespace HackerNews
{
    /* 
     * Class to connect to hacker-news site
     * Gets the best stories and also the individual story based on the Id
     */

    public class HNHttpClient
    {
        private readonly HttpClient _httpClient;
        private const string BEST_STORIES_URL = @"https://hacker-news.firebaseio.com/v0/beststories.json";

        public HNHttpClient(HttpClient httpClient)
		{
            if (httpClient == null)
                throw new ArgumentNullException("Could not initialize http client");

            _httpClient = httpClient;
        }

        public async Task<int[]> GetBestStoriesIdsAsync(int n)
        {
            //the response is an array of story ids (all numeric), there are 200 of them per response
            int[] response = await _httpClient.GetFromJsonAsync<int[]>(BEST_STORIES_URL);
            return response;
        }

        public async Task<NewsStory> GetStoryAsync(int id)
        {
            //https://hacker-news.firebaseio.com/v0/item/39452494.json

            // Use the URL below and passed story id to build the URL for individual story
            string storyUrl = $"https://hacker-news.firebaseio.com/v0/item/{id}.json";

            // Send an HTTP GET request to retrieve the story data
            HttpResponseMessage response = await _httpClient.GetAsync(storyUrl);

            // Check if the response is successful (status code 200)
            if (response.IsSuccessStatusCode)
            {
                // Read the content of the response and deserialize it into a HackerNewsStory object
                string newsStoryJson = await response.Content.ReadAsStringAsync();
                HackerNewsStory _story = JsonConvert.DeserializeObject<HackerNewsStory>(newsStoryJson);

                return _story.getNewsStory(); //return the news story object formatted as per the requirement
            }
            else
            {
                // Error
                string errorMessage = $"Exception caught while retrieving story with ID {id}. Status code: {response.StatusCode}";
                throw new HttpRequestException(errorMessage);
            }
        }
    }
}

