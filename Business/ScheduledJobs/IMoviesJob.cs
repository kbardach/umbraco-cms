using Hangfire.Server;

namespace kim_umbraco.Business.ScheduledJobs
{
    // Definierar ett gränssnitt för ett bakgrundsjobb som hanterar filmrelaterade uppgifter.
    public interface IMoviesJob
    {
        // En metoddeklaration för att ta bort filmer.
        // PerformContext är en Hangfire-klass som används för att tillhandahålla kontextuell information om det aktuella jobbet.
        // Den kan ge tillgång till information som jobbs id, loggning, eller hur det hanteras.
        void RemoveMovies(PerformContext context);
    }
}
