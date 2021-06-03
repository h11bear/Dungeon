using System;
using Xunit;
using FluentAssertions;

namespace Dungeon.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            1.Should().Be(1);
        }
    }
}
