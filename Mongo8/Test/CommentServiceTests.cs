using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using MongoDB.Bson;
using MongoDBDemoApp.Core.Util;
using MongoDBDemoApp.Core.Workloads.Comments;
using MongoDBDemoApp.Core.Workloads.Posts;
using NSubstitute;
using Xunit;

namespace MongoDBDemoApp.Test;

public sealed class CommentServiceTests
{
    [Fact]
    public async Task TestGetPostById()
    {
        var id = new ObjectId();
        var repoMock = Substitute.For<ICommentRepository>();
        repoMock.GetCommentById(Arg.Any<ObjectId>()).Returns(ci => new Comment
        {
            Id = ci.Arg<ObjectId>()
        });

        var service = new CommentService(Substitute.For<IDateTimeProvider>(), repoMock);
        var comment = await service.GetCommentById(id);

        await repoMock.Received(1).GetCommentById(Arg.Is(id));
        comment.Should().NotBeNull();
        comment!.Id.Should().Be(id);
    }

    [Fact]
    public async Task TestAddPost()
    {
        var postId = new ObjectId();
        var expectedPost = new Comment
        {
            Id = new ObjectId(),
            Name = "Horst",
            Created = new DateTime(2020, 08, 20, 18, 31, 05),
            Text = "Foo",
            Mail = "Bar@Baz.com",
            PostId = postId,
            Approved = false
        };
        var repoMock = Substitute.For<ICommentRepository>();
        repoMock.AddComment(Arg.Any<Comment>()).Returns(ci =>
        {
            var c = ci.ArgAt<Comment>(0);
            c.Id = expectedPost.Id;
            return c;
        });
        var dtMock = Substitute.For<IDateTimeProvider>();
        dtMock.Now.ReturnsForAnyArgs(expectedPost.Created);

        var service = new CommentService(dtMock, repoMock);
        var comment = await service.AddComment(new Post {Id = postId}, expectedPost.Name, expectedPost.Mail,
            expectedPost.Text);

        await repoMock.Received(1).AddComment(Arg.Any<Comment>());
        comment.Should().NotBeNull();
        comment.Should().BeEquivalentTo(expectedPost);
    }

    [Fact]
    public async Task TestGetCommentsForPost()
    {
        var postId = new ObjectId();
        Comment[] expectedComments =
        {
            new Comment
            {
                Id = new ObjectId(),
                Created = new DateTime(2020, 08, 20, 18, 31, 05),
                Mail = "foo@bar.com",
                Name = "Horst",
                PostId = postId,
                Text = "baz",
                Approved = true
            },
            new Comment
            {
                Id = new ObjectId(),
                Created = new DateTime(2020, 08, 20, 18, 38, 02),
                Mail = "foo2@bar.com",
                Name = "Sepp",
                PostId = postId,
                Text = "foobar",
                Approved = true
            }
        };
        var repoMock = Substitute.For<ICommentRepository>();
        repoMock.GetCommentsForPost(Arg.Is(postId)).Returns(expectedComments);

        var service = new CommentService(Substitute.For<IDateTimeProvider>(), repoMock);
        IReadOnlyCollection<Comment> comments = await service.GetCommentsForPost(new Post {Id = postId});

        await repoMock.Received(1).GetCommentsForPost(Arg.Is(postId));
        comments.Should().NotBeNullOrEmpty()
            .And.HaveCount(2)
            .And.Contain(expectedComments);
    }

    [Fact]
    public async Task TestGetUnapprovedComments()
    {
        var c1 = new Comment
        {
            Id = new ObjectId(),
            Created = new DateTime(2020, 08, 20, 18, 31, 05),
            Mail = "foo@bar.com",
            Name = "Horst",
            PostId = new ObjectId(),
            Text = "baz",
            Approved = false
        };
        var repoMock = Substitute.For<ICommentRepository>();
        repoMock.GetUnapprovedComments().Returns(new[] {c1});

        var service = new CommentService(Substitute.For<IDateTimeProvider>(), repoMock);
        IReadOnlyCollection<Comment> comments = await service.GetUnapprovedComments();

        await repoMock.Received(1).GetUnapprovedComments();
        comments.Should().NotBeNullOrEmpty()
            .And.HaveCount(1)
            .And.Contain(c1);
    }

    [Fact]
    public async Task TestDeleteComment()
    {
        var c1 = new Comment
        {
            Id = new ObjectId(),
            Created = new DateTime(2020, 08, 20, 18, 31, 05),
            Mail = "foo@bar.com",
            Name = "Horst",
            PostId = new ObjectId(),
            Text = "baz",
            Approved = false
        };
        var repoMock = Substitute.For<ICommentRepository>();
        repoMock.DeleteComment(Arg.Is(c1.Id)).Returns(true);

        var service = new CommentService(Substitute.For<IDateTimeProvider>(), repoMock);
        var result = await service.DeleteComment(c1);

        await repoMock.Received(1).DeleteComment(Arg.Is(c1.Id));
        result.Should().BeTrue();
    }

    [Fact]
    public async Task TestApproveComment()
    {
        var c1 = new Comment
        {
            Id = new ObjectId(),
            Created = new DateTime(2020, 08, 20, 18, 31, 05),
            Mail = "foo@bar.com",
            Name = "Horst",
            PostId = new ObjectId(),
            Text = "baz",
            Approved = false
        };
        var repoMock = Substitute.For<ICommentRepository>();
        repoMock.ApproveComment(Arg.Is(c1.Id)).Returns(true);

        var service = new CommentService(Substitute.For<IDateTimeProvider>(), repoMock);
        var result = await service.ApproveComment(c1);

        await repoMock.Received(1).ApproveComment(Arg.Is(c1.Id));
        result.Should().BeTrue();
    }

    [Fact]
    public async Task TestApproveCommentAlreadyApproved()
    {
        var c1 = new Comment
        {
            Id = new ObjectId(),
            Created = new DateTime(2020, 08, 20, 18, 31, 05),
            Mail = "foo@bar.com",
            Name = "Horst",
            PostId = new ObjectId(),
            Text = "baz",
            Approved = true
        };
        var repoMock = Substitute.For<ICommentRepository>();
        repoMock.ApproveComment(Arg.Any<ObjectId>()).Returns(false);

        var service = new CommentService(Substitute.For<IDateTimeProvider>(), repoMock);
        var result = await service.ApproveComment(c1);

        await repoMock.Received(0).ApproveComment(Arg.Any<ObjectId>());
        result.Should().BeTrue();
    }
}