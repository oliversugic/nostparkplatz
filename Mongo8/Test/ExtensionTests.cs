using FluentAssertions;
using MongoDBDemoApp.Core.Util;
using Xunit;

namespace MongoDBDemoApp.Test;

public sealed class ExtensionTests
{
    private const int Truncate_Length = 10;

    [Fact]
    public void TestTruncateNullOrEmptyString()
    {
        string? s1 = null;
        string s2 = string.Empty;
        string s3 = " ";

        s1!.Truncate(Truncate_Length).Should().BeNull();
        s2.Truncate(Truncate_Length).Should().Be(string.Empty);
        s3.Truncate(Truncate_Length).Should().BeEquivalentTo(s3);
    }

    [Fact]
    public void TestTruncateShortString()
    {
        string s1 = "123456789";
        string s2 = "1234567890";

        s1.Truncate(Truncate_Length).Should().BeEquivalentTo(s1);
        s2.Truncate(Truncate_Length).Should().BeEquivalentTo(s2);
    }

    [Fact]
    public void TestTruncateLongString()
    {
        string s = "123456789_10";

        s.Truncate(Truncate_Length).Should().BeEquivalentTo("1234567...");
    }
}