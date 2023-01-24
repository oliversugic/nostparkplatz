using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Comments;
using MongoDBDemoApp.Core.Workloads.Posts;
using NSubstitute;
using Xunit;

namespace MongoDBDemoApp.Test;

public sealed class PostServiceTests
{
    [Fact]
    public async Task TestGetAllPosts()
    {
        Post[] expectedPosts = {new Post(), new Post()};
        var repoMock = Substitute.For<IPostRepository>();
        repoMock.GetAllPosts().Returns(expectedPosts);

        var service = new PostService(Substitute.For<IDateTimeProvider>(), repoMock,
            Substitute.For<ICommentRepository>(), Substitute.For<ILogger<PostService>>());
        IReadOnlyCollection<Post> posts = await service.GetAllPosts();

        await repoMock.Received(1).GetAllPosts();
        posts.Should().NotBeNullOrEmpty()
            .And.HaveCount(2)
            .And.Contain(expectedPosts);
    }

    [Fact]
    public async Task TestGetPostById()
    {
        var id = new ObjectId();
        var repoMock = Substitute.For<IPostRepository>();
        repoMock.GetPostById(Arg.Any<ObjectId>()).Returns(ci => new Post
        {
            Id = ci.Arg<ObjectId>()
        });

        var service = new PostService(Substitute.For<IDateTimeProvider>(), repoMock,
            Substitute.For<ICommentRepository>(), Substitute.For<ILogger<PostService>>());
        var post = await service.GetPostById(id);

        await repoMock.Received(1).GetPostById(Arg.Is(id));
        post.Should().NotBeNull();
        post!.Id.Should().Be(id);
    }

    [Fact]
    public async Task TestAddPost()
    {
        var expectedPost = new Post
        {
            Id = new ObjectId(),
            Author = "Horst",
            Published = new DateTime(2020, 08, 20, 18, 31, 05),
            Text = "Foo",
            Title = "Bar",
            UpVotes = 0
        };
        var repoMock = Substitute.For<IPostRepository>();
        repoMock.AddPost(Arg.Any<Post>()).Returns(c =>
        {
            var p = c.ArgAt<Post>(0);
            p.Id = expectedPost.Id;
            return p;
        });
        var dtMock = Substitute.For<IDateTimeProvider>();
        dtMock.Now.ReturnsForAnyArgs(expectedPost.Published);

        var service = new PostService(dtMock, repoMock,
            Substitute.For<ICommentRepository>(), Substitute.For<ILogger<PostService>>());
        var post = await service.AddPost(expectedPost.Title, expectedPost.Author, expectedPost.Text);

        await repoMock.Received(1).AddPost(Arg.Any<Post>());
        post.Should().NotBeNull();
        post.Should().BeEquivalentTo(expectedPost);
    }

    [Fact]
    public async Task TestDeletePost()
    {
        var postId = new ObjectId();
        var post = new Post
        {
            Id = postId,
            Author = "Horst",
            Published = new DateTime(2020, 08, 20, 18, 31, 05),
            Text = "Foo",
            Title = "Bar",
            UpVotes = 3
        };
        var comments = new List<Comment>
        {
            new Comment
            {
                PostId = postId,
                Id = new ObjectId()
            },
            new Comment
            {
                PostId = postId,
                Id = new ObjectId()
            }
        };
        var repoMock = Substitute.For<IPostRepository>();
        repoMock.DeletePost(Arg.Is(postId)).Returns(Task.CompletedTask);
        repoMock.GetPostWithComments(Arg.Is(postId)).Returns((post.Id, comments.Select(c => c.Id).ToList()));
        var commentRepoMock = Substitute.For<ICommentRepository>();
        commentRepoMock.DeleteCommentsByPost(Arg.Is(postId)).Returns(Task.CompletedTask);

        var service = new PostService(Substitute.For<IDateTimeProvider>(), repoMock,
            commentRepoMock, Substitute.For<ILogger<PostService>>());
        await service.DeletePost(postId);

        await repoMock.Received(1).GetPostWithComments(Arg.Is(postId));
        await commentRepoMock.Received(1).DeleteCommentsByPost(Arg.Is(postId));
        await repoMock.Received(1).DeletePost(Arg.Is(postId));
    }
}