using System;
using System.Collections.Generic;

namespace BoardGameRatings.WebSite.Models.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        Game GetBy(string name);
        void AddElectedCategory(int gameId, int categoryId);
        IEnumerable<Category> GetAllCategoriesBy(int gameId);
        GameCategory GetGameCategoryBy(int gameId, int categoryId);
        void RemoveElectedCategory(int gameId, int categoryId);
        IEnumerable<GamePlayedDate> GetAllPlayedDatesBy(int gameId);
        void AddPlayedDate(int gameId, DateTime playedDate);
        GamePlayedDate GetGamePlayedDateBy(int gameId, DateTime playedDate);
        void RemovePlayedDate(int gameId, DateTime playedGame);
    }
}