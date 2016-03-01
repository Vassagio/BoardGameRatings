using System.Collections.Generic;
using BoardGameRatings.WebSite.Models;
using BoardGameRatings.WebSite.Models.Repositories;
using Moq;

namespace BoardGameRatings.WebSite.Tests.Mocks
{
    public class MockPlayerRepository : IPlayerRepository
    {
        private readonly Mock<IPlayerRepository> _mock;

        public MockPlayerRepository()
        {
            _mock = new Mock<IPlayerRepository>();
        }

        public IEnumerable<Player> GetAll()
        {
            return _mock.Object.GetAll();
        }

        public Player Add(Player player)
        {
            return _mock.Object.Add(player);
        }

        public void Remove(Player player)
        {
            _mock.Object.Remove(player);
        }

        public Player GetById(int id)
        {
            return _mock.Object.GetById(id);
        }

        public void Update(Player player)
        {
            _mock.Object.Update(player);
        }
    }
}