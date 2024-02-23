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
- The HackerNews API for best stories only returns 200 story ids, so API is limited to only 200 results. If n > 200, than 200 news stories will be returned back by the API instead of an error message indicating that n is over 200.

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

## API Overview

Title: API to get best stories from the Hacker News API<br>
Version: 1.0<br>
Server base URL: http://localhost:5131/api<br>

GET /api/v1/news/get-best-stories<br>
GET /api/v1/news/get-best-stories/n

Content-Type: application/json <br>
Responses:<br>
   HTTP 200 OK<br>
   Response Body Format<br>
```json
[
    {
        "title": The title of the story, poll or job,
        "uri": The URL of the story,
        "postedBy": The username of the item's author,
        "time": Creation date of the item,
        "score": The story's score, or the votes for a pollopt,
        "commentCount": this data is no longer available, so defdaulted to 0
    },
    {...},
    {...}
]
```
### Example Request/Response
To get 4 best stories:<br>
http://localhost:5131/api/v1/news/get-best-stories/4

```json
[
    {
        "title": "Insecure vehicles should be banned, not security tools like the Flipper Zero",
        "uri": "https://saveflipper.ca/",
        "postedBy": "pabs3",
        "time": "2024-02-21T11:20:49Z",
        "score": 1456,
        "commentCount": 0
    },
    {
        "title": "Keep your phone number private with Signal usernames",
        "uri": "https://signal.org/blog/phone-number-privacy-usernames/",
        "postedBy": "Josely",
        "time": "2024-02-20T18:01:05Z",
        "score": 1405,
        "commentCount": 0
    },
    {
        "title": "The killer app of Gemini Pro 1.5 is using video as an input",
        "uri": "https://simonwillison.net/2024/Feb/21/gemini-pro-video/",
        "postedBy": "simonw",
        "time": "2024-02-21T19:23:06Z",
        "score": 1103,
        "commentCount": 0
    },
    {
        "title": "Gemma: New Open Models",
        "uri": "https://blog.google/technology/developers/gemma-open-models/",
        "postedBy": "meetpateltech",
        "time": "2024-02-21T13:03:53Z",
        "score": 1101,
        "commentCount": 0
    }
]
```
