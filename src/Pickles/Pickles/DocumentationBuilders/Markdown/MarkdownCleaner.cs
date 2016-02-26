namespace PicklesDoc.Pickles.DocumentationBuilders.Markdown
{
    public static class MarkdownCleaner
    {
        public static string CleanContent(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }

            return s.Replace("<", "&lt;").Replace(">", "&gt;");
        }
    }
}
