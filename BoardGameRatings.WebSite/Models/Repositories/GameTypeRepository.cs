using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;

namespace BoardGameRatings.WebSite.Models.Repositories
{
    public class GameTypeRepository : IGameTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public GameTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GameType> GetAll()
        {
            return _context.GameTypes;
        }

        public GameType Add(GameType gameType)
        {
            var found = GetBy(gameType.Description);
            if (found != null)
                return found;

            _context.GameTypes.Add(gameType);
            _context.SaveChanges();
            return gameType;
        }

        public void Remove(GameType gameType)
        {
            _context.GameTypes.Remove(gameType);
            _context.SaveChanges();
        }

        public GameType GetBy(int gameTypeId)
        {
            return _context.GameTypes.FirstOrDefault(g => g.Id == gameTypeId);
        }

        public GameType GetBy(string description)
        {
            return
                _context.GameTypes.FirstOrDefault(
                    g => g.Description.Equals(description, StringComparison.CurrentCultureIgnoreCase));
        }

        public void Update(GameType gameType)
        {
            _context.Entry(gameType).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}