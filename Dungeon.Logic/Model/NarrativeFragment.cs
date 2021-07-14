namespace Dungeon.Logic.Model
{
    public class NarrativeFragment
    {
        private NarrativeFragment(string text, string keyword)
        {
            Text = text;
            Keyword = keyword;
        }

        public string Text { get; }
        public string Keyword { get; }
        public bool IsLink
        {
            get
            {
                return Keyword != null;
            }
        }

        public static NarrativeFragment PlainText(string text)
        {
            return new NarrativeFragment(text, null);
        }

        public static NarrativeFragment NavLink(string text, string keyword)
        {
            return new NarrativeFragment(text, keyword);
        }

    }
}
