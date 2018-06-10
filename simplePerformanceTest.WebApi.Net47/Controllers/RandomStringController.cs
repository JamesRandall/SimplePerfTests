using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace simplePerformanceTest.WebApi.Net47.Controllers
{
    public class RandomStringController : ApiController
    {
        private static readonly Random Random = new Random();

        // GET api/values
        [HttpGet]
        [Route("baseline")]
        public IHttpActionResult GetBaseline()
        {
            return Ok();
        }

        // GET api/values
        [HttpGet]
        [Route("randomString")]
        public string Get()
        {
            string base64EncodedString = GenerateRandomHashedString();
            return base64EncodedString;
        }

        // GET api/values
        [HttpGet]
        [Route("randomStringx20")]
        public IEnumerable<string> GetStrings()
        {
            List<string> strings = new List<string>();
            for (int counter = 0; counter < 20; counter++)
            {
                string base64EncodedString = GenerateRandomHashedString();
                strings.Add(base64EncodedString);
            }

            return strings;
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
