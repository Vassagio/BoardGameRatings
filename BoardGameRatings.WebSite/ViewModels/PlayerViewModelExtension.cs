namespace BoardGameRatings.WebSite.ViewModels
{
    public static class PlayerViewModelExtension
    {
        public static string GetFullName(this PlayerViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.FirstName) && string.IsNullOrWhiteSpace(model.LastName))
                return string.Empty;
            return string.Concat(model.FirstName, " ", model.LastName);
        }
    }
}