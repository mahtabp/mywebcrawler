using System;
using Crawler.Services;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;

namespace CrawlerTests
{
    [TestFixture]    
    public class LookupServiceTest
    {
        [Test]
        public async Task GetOccurrence_KeywordExistsOnce_ReturnLocation()
        {
            string target = "www.smokeball.com";
            string keyword = "conveyancing software";
            string searchResult = @"</span><cite class=\\\UdQCqe\\\ style=\\\max-width:400px\\\>www.<b>ballball.com</b>.au/book-a-demo</cite><div class=\\\Pj9hGd\\\><cite class=UdQCqe>www.kelada<b>conveyancing</b>.com.au/</cite><div class=Pj9hGd><div style=display:inline onclick=google.sham(this); aria-expanded=false aria-haspopup=true tabindex=0 data-ved=0ahUKEwiD2_2Lt4LdAhXLEZAKHRYzAgsQ7B0I8wQ><span class<div style=\\\display:inline\\\ onclick=\\\google.sham(this);\\\</span><cite class=\\\UdQCqe\\\ style=\\\max-width:400px\\\>www.<b>ballball.com</b>.au/book-a-demo</cite><div class=\\\Pj9hGd\\\><div style=\\\display:inline\\\ onclick=\\\google.sham(this);\\\</span><cite class=UdQCqe>www.kelada<b>conveyancing</b>.com.au/</cite><div class=Pj9hGd><div style=display:inline onclick=google.sham(this); aria-expanded=false aria-haspopup=true tabindex=0 data-ved=0ahUKEwiD2_2Lt4LdAhXLEZAKHRYzAgsQ7B0I8wQ><span class<cite class=\\\UdQCqe\\\ style=\\\max-width:400px\\\>www.<b>lastball.com</b>.au/book-a-demo</cite><div class=\\\Pj9hGd\\\><div style=\\\display:inline\\\ onclick=\\\google.sham(this);\\\</span><cite class=\\\UdQCqe\\\ style=\\\max-width:400px\\\>www.<b>smokeball.com</b>.au/book-a-demo</cite><div class=\\\Pj9hGd\\\><div style=\\\display:inline\\\ onclick=\\\google.sham(this);\\\";
            var httpClient = new Mock<IGoogleHttpClient>();
            httpClient.Setup(x => x.GetSearchResultFromGoogle(keyword)).Returns(Task.FromResult<string>(searchResult));

            var service = new LookupService(httpClient.Object);

            var actual = await service.GetOccurrence(keyword, target);

            Assert.AreEqual("5", actual);
        }


        [Test]
        public async Task GetOccurrence_KeywordDoesNotExist_Return0()
        {
            string target = "www.ballsmoke.com";
            string keyword = "conveyancing software";
            string searchResult = @"</span><cite class=\\\UdQCqe\\\ style=\\\max-width:400px\\\>www.<b>smokeball.com</b>.au/book-a-demo</cite><div class=\\\Pj9hGd\\\><cite class=UdQCqe>www.kelada<b>conveyancing</b>.com.au/</cite><div class=Pj9hGd><div style=display:inline onclick=google.sham(this); aria-expanded=false aria-haspopup=true tabindex=0 data-ved=0ahUKEwiD2_2Lt4LdAhXLEZAKHRYzAgsQ7B0I8wQ><span class<div style=\\\display:inline\\\ onclick=\\\google.sham(this);\\\</span><cite class=\\\UdQCqe\\\ style=\\\max-width:400px\\\>www.<b>smokeball.com</b>.au/book-a-demo</cite><div class=\\\Pj9hGd\\\><div style=\\\display:inline\\\ onclick=\\\google.sham(this);\\\</span><cite class=UdQCqe>www.kelada<b>conveyancing</b>.com.au/</cite><div class=Pj9hGd><div style=display:inline onclick=google.sham(this); aria-expanded=false aria-haspopup=true tabindex=0 data-ved=0ahUKEwiD2_2Lt4LdAhXLEZAKHRYzAgsQ7B0I8wQ><span class<cite class=\\\UdQCqe\\\ style=\\\max-width:400px\\\>www.<b>smokeball.com</b>.au/book-a-demo</cite><div class=\\\Pj9hGd\\\><div style=\\\display:inline\\\ onclick=\\\google.sham(this);\\\</span><cite class=\\\UdQCqe\\\ style=\\\max-width:400px\\\>www.<b>smokeball.com</b>.au/book-a-demo</cite><div class=\\\Pj9hGd\\\><div style=\\\display:inline\\\ onclick=\\\google.sham(this);\\\";
            var httpClient = new Mock<IGoogleHttpClient>();
            httpClient.Setup(x => x.GetSearchResultFromGoogle(keyword)).Returns(Task.FromResult<string>(searchResult));

            var service = new LookupService(httpClient.Object);

            var actual = await service.GetOccurrence(keyword, target);

            Assert.AreEqual("0", actual);
        }

        [Test]
        public async Task GetOccurrence_KeywordExistMultipleTimes_ReturnAllLocations()
        {
            string target = "www.smokeball.com";
            string keyword = "conveyancing software";
            string searchResult = @"</span><cite class=\\\UdQCqe\\\ style=\\\max-width:400px\\\>www.<b>smokeball.com</b>.au/book-a-demo</cite><div class=\\\Pj9hGd\\\><cite class=UdQCqe>www.kelada<b>conveyancing</b>.com.au/</cite><div class=Pj9hGd><div style=display:inline onclick=google.sham(this); aria-expanded=false aria-haspopup=true tabindex=0 data-ved=0ahUKEwiD2_2Lt4LdAhXLEZAKHRYzAgsQ7B0I8wQ><span class<div style=\\\display:inline\\\ onclick=\\\google.sham(this);\\\</span><cite class=\\\UdQCqe\\\ style=\\\max-width:400px\\\>www.<b>smokeball.com</b>.au/book-a-demo</cite><div class=\\\Pj9hGd\\\><div style=\\\display:inline\\\ onclick=\\\google.sham(this);\\\</span><cite class=UdQCqe>www.kelada<b>conveyancing</b>.com.au/</cite><div class=Pj9hGd><div style=display:inline onclick=google.sham(this); aria-expanded=false aria-haspopup=true tabindex=0 data-ved=0ahUKEwiD2_2Lt4LdAhXLEZAKHRYzAgsQ7B0I8wQ><span class<cite class=\\\UdQCqe\\\ style=\\\max-width:400px\\\>www.<b>smokeball.com</b>.au/book-a-demo</cite><div class=\\\Pj9hGd\\\><div style=\\\display:inline\\\ onclick=\\\google.sham(this);\\\</span><cite class=\\\UdQCqe\\\ style=\\\max-width:400px\\\>www.<b>smokeball.com</b>.au/book-a-demo</cite><div class=\\\Pj9hGd\\\><div style=\\\display:inline\\\ onclick=\\\google.sham(this);\\\";
            var httpClient = new Mock<IGoogleHttpClient>();
            httpClient.Setup(x => x.GetSearchResultFromGoogle(keyword)).Returns(Task.FromResult<string>(searchResult));

            var service = new LookupService(httpClient.Object);

            var actual = await service.GetOccurrence(keyword, target);

            Assert.AreEqual("0 2 4 5", actual);
        }

        [Test]
        public async Task GetOccurrence_GoogleClientFindsNothing_Return0()
        {
            string target = "www.smokeball.com";
            string keyword = "conveyancing software";

            var httpClient = new Mock<IGoogleHttpClient>();
            httpClient.Setup(x => x.GetSearchResultFromGoogle(keyword)).Returns(Task.FromResult<string>(""));

            var service = new LookupService(httpClient.Object);

            var actual = await service.GetOccurrence(keyword, target);

            Assert.AreEqual("0", actual);
        }
    }
}
