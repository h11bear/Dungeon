namespace Dungeon.Logic.Model
{
    public class NarrativeFragment
    {
        public NarrativeFragment(string text, bool isLink)
        {
            Text = text;
            IsLink = isLink;
        }

        public string Text { get; }
        public bool IsLink { get; }

    }
}
