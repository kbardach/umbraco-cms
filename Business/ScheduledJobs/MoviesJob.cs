using Hangfire.Console;   // Inkluderar Hangfire Console, vilket gör det möjligt att visa framsteg, loggar och andra meddelanden i Hangfire Dashboard.
using Hangfire.Server;    // Inkluderar Hangfire Server som ger kontext för bakgrundsjobbet (PerformContext).

namespace kim_umbraco.Business.ScheduledJobs
{
    // Implementering av IMoviesJob-gränssnittet, där logiken för RemoveMovies-jobbet ligger.
    public class MoviesJob : IMoviesJob
    {
        // Metoden RemoveMovies är ansvarig för att ta bort filmer.
        // PerformContext används för att skriva loggar och visa progressbar för jobbet.
        public void RemoveMovies(PerformContext context)
        {
            // Skapar en progress bar i Hangfire dashboard för att visualisera hur mycket av jobbet som har utförts.
            var progressBar = context.WriteProgressBar();

            // Skapar en lista av MyMovie (en lokal klass definierad längre ner i koden).
            var movies = new List<MyMovie>();

            // Användning av WithProgress gör att vi kan rapportera framstegen för varje film som bearbetas.
            // Här används Hangfire Console för att skriva en logg för varje film (item) som bearbetas.
            foreach (var item in movies.WithProgress(progressBar, movies.Count()))
            {
                // Skriver ut ett meddelande i Hangfire Console med namnet på filmen.
                context.WriteLine($"Movie {item.Name} added");
            }
        }

        // En privat klass som representerar en film med bara ett fält: Name.
        // Den används lokalt i RemoveMovies-metoden.
        private class MyMovie
        {
            public string Name { get; set; }   // En egenskap som representerar namnet på filmen.
        }
    }
}




//for (int i = 0; i < 10000; i++)
//{
//    var movie = new MyMovie
//    {
//        Name = i.ToString()
//    };

//    movies.Add(movie);
//}