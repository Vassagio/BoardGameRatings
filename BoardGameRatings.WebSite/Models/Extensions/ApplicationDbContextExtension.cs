using System.Linq;

namespace BoardGameRatings.WebSite.Models.Extensions
{
    public static class ApplicationDbContextExtension
    {
        private static readonly Category Competitive = new Category {Description = "Competitive"};
        private static readonly Category Cooperative = new Category {Description = "Cooperative"};
        private static readonly Category OneVs = new Category {Description = "1 vs."};
        private static readonly Category Teams = new Category {Description = "Teams"};
        private static readonly Category Traitor = new Category {Description = "Traitor"};

        private static readonly Game BattlestarGalactica = new Game
        {
            Name = "Battlestar Galactica",
            Description =
                "An exciting game of mistrust, intrigue, and the struggle for survival. Based on the epic and widely-acclaimed Sci Fi Channel series, Battlestar Galactica: The Board Game puts players in the role of one of ten of their favorite characters from the show. Each playable character has their own abilities and weaknesses, and must all work together in order for humanity to have any hope of survival. However, one or more players in every game secretly side with the Cylons. Players must attempt to expose the traitor while fuel shortages, food contaminations, and political unrest threatens to tear the fleet apart.\n\nAfter the Cylon attack on the Colonies, the battered remnants of the human race are on the run, constantly searching for the next signpost on the road to Earth.They face the threat of Cylon attack from without, and treachery and crisis from within.Humanity must work together if they are to have any hope of survival…but how can they, when any of them may, in fact, be a Cylon agent?"
        };

        private static readonly Game SettlersOfCatan = new Game
        {
            Name = "Settlers Of Catan",
            Description =
                "Players assume the roles of settlers, each attempting to build and develop holdings while trading and acquiring resources. Players are rewarded points as their settlements grow; the first to reach a set number of points, typically 10, is the winner."
        };

        private static readonly Game LastNightOnEarth = new Game
        {
            Name = "Last Night On Earth",
            Description =
                "Players can play on the Hero team or as the Zombies. A modular board randomly determines the layout of the town at the start of each game and there are several different scenarios to play."
        };

        private static readonly Game SheriffOfNottingham = new Game
        {
            Name = "Sheriff Of Nottingham",
            Description = "A game of lies."
        };

        private static readonly Game StarWarsXWing = new Game
        {
            Name = "Star Wars: X-Wing",
            Description = "Table top destruction."
        };

        private static readonly Player BrandonLamkey = new Player {FirstName = "Brandon", LastName = "Lamkey"};
        private static readonly Player CharlieKohlhaas = new Player {FirstName = "Charlie", LastName = "Kohlhaas"};
        private static readonly Player DerekKohlhagen = new Player {FirstName = "Derek", LastName = "Kohlhagen"};
        private static readonly Player JohnDavidson = new Player {FirstName = "John", LastName = "Davidson"};
        private static readonly Player RitchKing = new Player {FirstName = "Ritch", LastName = "King"};
        private static readonly Player WilliamChronowski = new Player {FirstName = "William", LastName = "Chronowski"};

        public static void EnsureSeedData(this ApplicationDbContext context)
        {
            if (!context.AllMigrationsApplied()) return;

            AddCategories(context);
            AddGames(context);
            AddPlayers(context);
            AddPlayerGames(context);

            context.SaveChanges();
        }

        private static void AddCategories(ApplicationDbContext context)
        {
            if (context.Categories.Any())
            {
                context.Categories.RemoveRange(context.Categories);
                context.SaveChanges();
            }

            context.Categories.Add(Competitive);
            context.Categories.Add(Cooperative);
            context.Categories.Add(OneVs);
            context.Categories.Add(Teams);
            context.Categories.Add(Traitor);
        }

        private static void AddGames(ApplicationDbContext context)
        {
            if (context.Games.Any())
            {
                context.Games.RemoveRange(context.Games);
                context.SaveChanges();
            }

            context.Games.Add(BattlestarGalactica);
            context.Games.Add(SettlersOfCatan);
            context.Games.Add(LastNightOnEarth);
            context.Games.Add(SheriffOfNottingham);
            context.Games.Add(StarWarsXWing);
        }

        private static void AddPlayers(ApplicationDbContext context)
        {
            if (context.Players.Any())
            {
                context.Players.RemoveRange(context.Players);
                context.SaveChanges();
            }

            context.Players.Add(BrandonLamkey);
            context.Players.Add(CharlieKohlhaas);
            context.Players.Add(DerekKohlhagen);
            context.Players.Add(JohnDavidson);
            context.Players.Add(RitchKing);
            context.Players.Add(WilliamChronowski);
        }

        private static void AddPlayerGames(ApplicationDbContext context)
        {
            if (context.PlayerGames.Any())
            {
                context.PlayerGames.RemoveRange(context.PlayerGames);
                context.SaveChanges();
            }

            context.PlayerGames.Add(new PlayerGame {GameId = BattlestarGalactica.Id, PlayerId = CharlieKohlhaas.Id});
            context.PlayerGames.Add(new PlayerGame {GameId = BattlestarGalactica.Id, PlayerId = RitchKing.Id});
            context.PlayerGames.Add(new PlayerGame {GameId = SettlersOfCatan.Id, PlayerId = CharlieKohlhaas.Id});
            context.PlayerGames.Add(new PlayerGame {GameId = SettlersOfCatan.Id, PlayerId = DerekKohlhagen.Id});
            context.PlayerGames.Add(new PlayerGame {GameId = SettlersOfCatan.Id, PlayerId = WilliamChronowski.Id});
            context.PlayerGames.Add(new PlayerGame {GameId = LastNightOnEarth.Id, PlayerId = DerekKohlhagen.Id});
            context.PlayerGames.Add(new PlayerGame {GameId = SheriffOfNottingham.Id, PlayerId = DerekKohlhagen.Id});
            context.PlayerGames.Add(new PlayerGame {GameId = SheriffOfNottingham.Id, PlayerId = RitchKing.Id});
            context.PlayerGames.Add(new PlayerGame {GameId = SheriffOfNottingham.Id, PlayerId = WilliamChronowski.Id});
            context.PlayerGames.Add(new PlayerGame {GameId = StarWarsXWing.Id, PlayerId = RitchKing.Id});
        }
    }
}