using System.Net;
using IFS.ApiTests.Models;

namespace IFS.ApiTests.Tests
{
    [TestFixture]
    public class PostsTests : BaseTest
    {
        [Test]
        public async Task GetAllPosts_Returns200OK()
        {
            var (statusCode, _) = await Client.GetAsync<List<Post>>("/posts");

            Assert.That(statusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task GetAllPosts_Returns100Posts()
        {
            var (_, posts) = await Client.GetAsync<List<Post>>("/posts");

            Assert.That(posts!.Count, Is.EqualTo(100));
        }

        [Test]
        public async Task GetAllPosts_PostHasRequiredFields()
        {
            var (_, posts) = await Client.GetAsync<List<Post>>("/posts");
            var firstPost = posts!.First();

            Assert.Multiple(() =>
            {
                Assert.That(firstPost.Id, Is.GreaterThan(0));
                Assert.That(firstPost.UserId, Is.GreaterThan(0));
                Assert.That(firstPost.Title, Is.Not.Empty);
                Assert.That(firstPost.Body, Is.Not.Empty);
            });
        }

        [Test]
        public async Task GetPostById_ReturnsCorrectPost()
        {
            var (statusCode, post) = await Client.GetAsync<Post>("/posts/1");

            Assert.Multiple(() =>
            {
                Assert.That(statusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(post!.Id, Is.EqualTo(1));
            });
        }

        [Test]
        public async Task GetPostById_NonExistentId_Returns404()
        {
            var (statusCode, _) = await Client.GetAsync<Post>("/posts/99999");

            Assert.That(statusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task CreatePost_Returns201WithCorrectData()
        {
            var newPost = new { userId = 1, title = "Test Title", body = "Test Body" };

            var (statusCode, post) = await Client.PostAsync<Post>("/posts", newPost);

            Assert.Multiple(() =>
            {
                Assert.That(statusCode, Is.EqualTo(HttpStatusCode.Created));
                Assert.That(post!.Title, Is.EqualTo("Test Title"));
                Assert.That(post.Body, Is.EqualTo("Test Body"));
            });
        }

        [Test]
        public async Task UpdatePost_Returns200WithUpdatedData()
        {
            var updatedPost = new { id = 1, userId = 1, title = "Updated Title", body = "Updated Body" };

            var (statusCode, post) = await Client.PutAsync<Post>("/posts/1", updatedPost);

            Assert.Multiple(() =>
            {
                Assert.That(statusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(post!.Title, Is.EqualTo("Updated Title"));
                Assert.That(post.Body, Is.EqualTo("Updated Body"));
            });
        }

        [Test]
        public async Task DeletePost_Returns200OK()
        {
            var statusCode = await Client.DeleteAsync("/posts/1");

            Assert.That(statusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}