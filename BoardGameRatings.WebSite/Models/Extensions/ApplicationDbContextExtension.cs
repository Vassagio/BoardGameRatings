using System.Linq;

namespace BoardGameRatings.WebSite.Models.Extensions
{
    public static class ApplicationDbContextExtension
    {
        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            if (!context.AllMigrationsApplied()) return;

            if (context.Games.Any())
            {
                context.Games.RemoveRange(context.Games);
                context.SaveChanges();
            }
            var battlestarGalactica = new Game
            {
                Name = "Battlestar Galactica",
                Description =
                    "An exciting game of mistrust, intrigue, and the struggle for survival. Based on the epic and widely-acclaimed Sci Fi Channel series, Battlestar Galactica: The Board Game puts players in the role of one of ten of their favorite characters from the show. Each playable character has their own abilities and weaknesses, and must all work together in order for humanity to have any hope of survival. However, one or more players in every game secretly side with the Cylons. Players must attempt to expose the traitor while fuel shortages, food contaminations, and political unrest threatens to tear the fleet apart.\n\nAfter the Cylon attack on the Colonies, the battered remnants of the human race are on the run, constantly searching for the next signpost on the road to Earth.They face the threat of Cylon attack from without, and treachery and crisis from within.Humanity must work together if they are to have any hope of survival…but how can they, when any of them may, in fact, be a Cylon agent?"
            };
            var settlersOfCatan = new Game
            {
                Name = "Settlers Of Catan",
                Description =
                    "Players assume the roles of settlers, each attempting to build and develop holdings while trading and acquiring resources. Players are rewarded points as their settlements grow; the first to reach a set number of points, typically 10, is the winner."
            };
            var lastNightOnEarth = new Game
            {
                Name = "Last Night On Earth",
                Description =
                    "Players can play on the Hero team or as the Zombies. A modular board randomly determines the layout of the town at the start of each game and there are several different scenarios to play."
            };
            context.Games.Add(battlestarGalactica);
            context.Games.Add(settlersOfCatan);
            context.Games.Add(lastNightOnEarth);

            if (context.Players.Any())
            {
                context.Players.RemoveRange(context.Players);
                context.SaveChanges();
            }
            var brandonLamkey = new Player {FirstName = "Brandon", LastName = "Lamkey"};
            var charlieKohlhaas = new Player {FirstName = "Charlie", LastName = "Kohlhaas"};
            var derekKohlhagen = new Player {FirstName = "Derek", LastName = "Kohlhagen"};
            var johnDavidson = new Player {FirstName = "John", LastName = "Davidson"};
            var ritchKing = new Player {FirstName = "Ritch", LastName = "King"};
            var williamChronowski = new Player {FirstName = "William", LastName = "Chronowski"};
            context.Players.Add(brandonLamkey);
            context.Players.Add(charlieKohlhaas);
            context.Players.Add(derekKohlhagen);
            context.Players.Add(johnDavidson);
            context.Players.Add(ritchKing);
            context.Players.Add(williamChronowski);
            context.SaveChanges();
        }
    }
}