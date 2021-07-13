using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using Dungeon.Logic.Model;

namespace Dungeon.Tests.Unit.ViewModels
{
    public class NarrativeFragmentTests
    {
        [Fact]
        public void ParseNarrativeWithExactKeyworkMatches()
        {
            var entrance = new Room("entrance", "my entrance, pursue the monster?");
            entrance.AddExit(new RoomExit("pursue", "monsterRoom"));
            
            NarrativeParser narrativeParser = new NarrativeParser();

            List<NarrativeFragment> fragments = narrativeParser.Parse(entrance);

            fragments[0].Text.Should().Be("my entrance, ");
            fragments[0].IsLink.Should().BeFalse();
            fragments[1].Text.Should().Be("pursue");
            fragments[1].IsLink.Should().BeTrue();
            fragments[2].Text.Should().Be(" the monster?");
        }

        [Fact]
        public void ParseNarrativeWithNoKeywords()
        {
             var happyEnding = new Room("happyEnding", "Blue sky and sunny day, yay!");
            
            NarrativeParser narrativeParser = new NarrativeParser();

            List<NarrativeFragment> fragments = narrativeParser.Parse(happyEnding);

            fragments[0].Text.Should().Be("Blue sky and sunny day, yay!");
        }

        [Fact]
        public void KeywordEndsSentence()
        {
            var entrance = new Room("entrance", "my entrance, pursue");
            entrance.AddExit(new RoomExit("pursue", "monsterRoom"));
            
            NarrativeParser narrativeParser = new NarrativeParser();

            List<NarrativeFragment> fragments = narrativeParser.Parse(entrance);

            fragments[0].Text.Should().Be("my entrance, ");
            fragments[1].Text.Should().Be("pursue");
            fragments.Count.Should().Be(2);
        }

        //TODO: next test we extend keyword match until space, punctuation, or end of word
    }
}