using Hangfire;
using kim_umbraco.Business.ScheduledJobs;  // Inkluderar namnområdet där dina schemalagda jobb (IMoviesJob) finns.
using Umbraco.Cms.Core.Composing;  // Inkluderar Umbraco's IComposer för att skapa och registrera kompositioner.

namespace kim_umbraco.Business.Composers
{
    // Definierar en klass för att komponera schemalagda jobb (Composer-mönster).
    public class ScheduledJobsComposer : IComposer
    {
        // Detta är den metod som körs när kompositören registreras i Umbraco.
        public void Compose(IUmbracoBuilder builder)
        {
            // Lägger till eller uppdaterar ett återkommande jobb via Hangfire. 
            // Det schemalagda jobbet kommer att köras med en identifierare "Remove movies".
            // "x => x.RemoveMovies(null)" är metoden som körs när jobbet exekveras.
            // Cron.Never innebär att jobbet aldrig kommer att köras automatiskt, det är schemalagt men pausat.
            RecurringJob.AddOrUpdate<IMoviesJob>(
                "Remove movies",             // Identifierare för jobbet
                x => x.RemoveMovies(null),   // Anropet till IMoviesJob-metoden 'RemoveMovies'
                Cron.Never                   // Definierar att jobbet aldrig körs automatiskt
            );
        }
    }
}

