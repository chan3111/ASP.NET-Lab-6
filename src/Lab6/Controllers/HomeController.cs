using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Lab6.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Lab6.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var result = await client.GetAsync("http://cst8359lab6api.azurewebsites.net/api/Twitter/GetLast100Tweets");

            var content = await result.Content.ReadAsStringAsync();

            var tweets = JsonConvert.DeserializeObject<List<Tweet>>(content);

            return View(tweets);
        }

        public async Task<IActionResult> PostTweet()
        {
            var tweet = new Tweet();
            tweet.Username = HttpContext.Request.Form["username"];
            tweet.Content = HttpContext.Request.Form["content"];

            var json = JsonConvert.SerializeObject(tweet);

            var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var client = new HttpClient();
            await client.PostAsync("http://cst8359lab6api.azurewebsites.net/api/Twitter/Add", httpContent);

            return RedirectToAction("Index");
        }
    }
}
