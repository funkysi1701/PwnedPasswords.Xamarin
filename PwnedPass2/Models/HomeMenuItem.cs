namespace PwnedPass2.Models
{
    public enum MenuItemType
    {
        EmailSearch,
        PasswordSearch,
        List,
        About,
        Rate
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
