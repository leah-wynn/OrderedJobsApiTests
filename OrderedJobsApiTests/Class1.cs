using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using NUnit.Framework;

namespace OrderedJobsApiTests
{
    [TestFixture]
    public class OrderedJobsApiAcceptanceTests
    {
        [Test]
        public void SomeOneCreatesANewTest()
        {
            var client = new HttpClient();
            var url = "http://localhost:5000/tests";
            var body = new StringContent("\"a => |b => a|c =>a\"", Encoding.UTF8, "application/json");
            client.PostAsync(url, body);
            var data = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(data);
            Assert.That(data.Contains("a => |b => a|c =>a"), Is.True);
        }
        
        [Test]
        public void SomeOneClearsTheTests()
        {
            var client = new HttpClient();
            var url = "http://localhost:5000/tests";
            var data = client.DeleteAsync(url).Result.Content.ReadAsStringAsync().Result;
            Assert.That(data, Is.EqualTo("true"));
            var collectionTests = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            Assert.That(collectionTests, Is.EqualTo("[]"));
        }
        [Test]
        public void TestTheJobsAreOrderedCorrectly()
        {
            var client = new HttpClient();
            var url = "http://localhost:5000/tests?url={http://localhost:5000/orderedjobs}";
            var data = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            Assert.That(data, Is.EqualTo("{}"));
        }
        
        [Test]
        public void SomeoneOrdersTheJobs()
        {
            var client = new HttpClient();
            var url = "http://localhost:5000/orderedjobs/a => | b => | c => ";
            var result = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            Assert.That(result, Is.EqualTo("abc"));
        }
        
    }
}