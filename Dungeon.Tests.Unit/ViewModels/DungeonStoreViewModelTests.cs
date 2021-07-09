using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Dungeon.Logic.Model;

namespace Dungeon.Tests.Unit.ViewModels
{
    public class DungeonStoryViewModelTests
    {
        [Fact]
        public void ParseStoryFragmentsUsingKeywords()
        {
            var entrance = new Room("entrance", "my entrance, pursue the monster?");
            entrance.AddExit(new RoomExit("pursue", "monsterRoom"));
            
            NarrativeParser narrativeParser = new NarrativeParser();

            List<StoryFragment> fragments = narrativeParser.Parse(entrance);

            fragments.Count.Should().Be(3);
            fragments[0].FragmentText.Should().Be("my entrance, ");
            fragments[0].IsLink.Should().BeFalse();
            fragments[1].FragmentText.Should().Be("pursue");
            fragments[1].IsLink.Should().BeTrue();
            fragments[2].FragmentText.Should().Be(" the monster?");
        }
    }
}