using System;
using BoardGameRatings.WebSite.ViewModels;

namespace BoardGameRatings.WebSite.Contexts
{
    public interface IGameContext
    {
        GameViewModel BuildViewModel(int? id);
        void Save(GameViewModel model);
        void AddElectedCategory(int gameId, int categoryId);
        void RemoveElectedCategory(int gameId, int categoryId);
        void AddPlayedDate(int gameId, DateTime playedDate);
        void RemovePlayedDate(int gameId, DateTime playedDate);
    }
}