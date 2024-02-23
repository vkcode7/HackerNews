# HackerNews
## HackerNews API Project Requirements

Using ASP.NET Core, implement a RESTful API to retrieve the details of the best n stories from the Hacker News API, as determined by their score, where n is specified by the caller to the API.

The Hacker News API is documented here: https://github.com/HackerNews/API.

The IDs for the stories can be retrieved from this URI: https://hacker-news.firebaseio.com/v0/beststories.json .

The details for an individual story ID can be retrieved from this URI: https://hacker-news.firebaseio.com/v0/item/21233041.json (in this case for the story with ID 21233041 )

The API should return an array of the best n stories as returned by the Hacker News API in descending order of score, in the form:

```json
[
{
"title": "A uBlock Origin update was rejected from the Chrome Web Store",
"uri": "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
"postedBy": "ismaildonmez",
"time": "2019-10-12T13:43:01+00:00",
"score": 1716,
"commentCount": 572
},
{ ... },
{ ... },
{ ... },
...
]
```

In addition to the above, your API should be able to efficiently service large numbers of requests without risking overloading of the Hacker News API.

You should share a public repository with us, that should include a README.md file which describes how to run the application, any assumptions you have made, and any enhancements or changes you would make, given the time.

## Notes and Assumptions
- The commentCount attribute is no longer available in the response from hacker-news site, so it is set to a default value of 0 in that case
- The code uses in memory caching where in the best stories data is cached for 2 minutes and individual stories are cached for 60 minutes
- The URL path is http://localhost:port/api/v1/news/get-best-stories/10
- If the story count (n) is omitted, the API defaults to n=10

## How to Run the Application (bash shell)

1. Clone the repository:
   ```bash
   git clone https://github.com/vkcode7/HackerNews.git
   cd HackerNews

2. Restore dependencies and build the project:
   ```bash
   dotnet restore
   dotnet build

2. Run the application:
   ```bash
   dotnet run

This will spawn the web server and API can be accessed using URL (fetch 25 best stories) in browser or Postman:

http://localhost:5131/api/v1/news/get-best-stories/25

## Running in Visual Studio

1. Open the HackerNews.csproj file in VS 2022 and build the project
2. Build the project and launch it
3. Access the API using URL provided in previous section

## Dependencies
Add latest version of following NuGet packages if needed:
- Microsoft.AspNetCore.Mvc.Versioning
- Newtonsoft.Json
