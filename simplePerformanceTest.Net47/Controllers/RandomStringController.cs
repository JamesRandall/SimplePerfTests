using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace simplePerformanceTest.Net47.Controllers
{
    public class RandomStringController : Controller
    {
        private static readonly Random Random = new Random();

        [HttpGet("randomString")]
        public IActionResult GetRandomString()
        {
            string base64EncodedString = GenerateRandomHashedString();
            return Ok(base64EncodedString);
        }

        [HttpGet("randomStringx20")]
        public IActionResult GetRandomStrings()
        {
            List<string> strings = new List<string>();
            for (int counter = 0; counter < 20; counter++)
            {
                string base64EncodedString = GenerateRandomHashedString();
                strings.Add(base64EncodedString);
            }

            return Ok(strings);
        }

        [HttpGet("baseline")]
        public IActionResult GetBaseline()
        {
            return Ok();
        }

        private static string GenerateRandomHashedString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string randomString = new string(Enumerable.Repeat(chars, 128).Select(s => s[Random.Next(s.Length)]).ToArray());
            byte[] bytes = Encoding.UTF8.GetBytes(randomString);
            SHA256Managed sha256Managed = new SHA256Managed();
            byte[] hash = sha256Managed.ComputeHash(bytes);
            string base64EncodedString = Convert.ToBase64String(hash);
            return base64EncodedString;
        }
    }
}
