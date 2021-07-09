namespace Dungeon.Logic.Model
{
    public class StoryFragment
    {
        public StoryFragment(string fullText, bool isLink, int startIndex, int endIndex)
        {
            FullText = fullText;
            IsLink = isLink;
            StartIndex = startIndex;
            EndIndex = endIndex;
        }

        public string FullText { get; }
        public int StartIndex { get; }
        public int EndIndex { get; }
        
        public string FragmentText { get; }
        public bool IsLink { get; }

    }
}
