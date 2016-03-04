namespace BoardGameRatings.WebSite.Models.Extensions
{
    public static class PlayerExtension
    {
        public static string GetFullName(this Player player)
        {
            if (string.IsNullOrWhiteSpace(player.FirstName) && string.IsNullOrWhiteSpace(player.LastName))
                return string.Empty;
            return string.Concat(player.FirstName, " ", player.LastName);
        }
    }
}