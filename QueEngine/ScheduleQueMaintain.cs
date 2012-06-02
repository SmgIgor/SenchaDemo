using System.Linq;
using Common.Logging;
using Quartz;

namespace Commuticate.Que
{
    public class ScheduleQueMaintain : IJob
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ScheduleQueMaintain));
        public void Execute(IJobExecutionContext context)
        {
            Log.Info(string.Format("Cleaning Schedule Que [{0}]", context.FireTimeUtc));
            using (var ent = new Commuticate.Repository.Entities())
            {
                var queSchedule =
                    (from q in ent.QueSchedules
                     where q.RunOnce && q.LastRun != null
                     select q);

                foreach (var q in queSchedule)
                    ent.QueSchedules.DeleteObject(q);

                if (queSchedule.Any())
                    Log.Info(string.Format("Removing Single Use Schedules [{0}] - Items: {1}", context.FireTimeUtc, queSchedule.Count()));

                ent.SaveChanges();
            }            
        }        
    }
}