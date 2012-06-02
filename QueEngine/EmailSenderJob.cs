using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Common.Logging;
using Quartz;

namespace Commuticate.Que
{
    public class EmailSenderJob : IJob
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(EmailSenderJob));

        public void Execute(IJobExecutionContext context)
        {
            Log.Info(string.Format("Start! - {0}", System.DateTime.Now.ToString("r")));

            var queDay = DateTime.Now.ToString("ddd").ToUpperInvariant();
            var queLastRun = context.PreviousFireTimeUtc;
            var queTimeEnd = context.FireTimeUtc;

            using (var ent = new Commuticate.Repository.Entities())
            {
                //var comm =
                //    ent.Commuters.FirstOrDefault(c => c.Email.Equals("jrosenbaum@smg.com", StringComparison.OrdinalIgnoreCase));

                //var rGroup = new RouteGroup() {Commuter = comm, Description = "First Route Group"};
                //ent.AddToRouteGroups(rGroup);

                //var route = new Route() {Commuter = comm, Description = "First Route"};
                //ent.AddToRoutes(route);

                //var rGroupR = new RouteGroupRoute() {RouteGroup = rGroup, RouteId = route.Id};
                //ent.AddToRouteGroupRoutes(rGroupR);
                //ent.SaveChanges();

                //var rGroup = ent.RouteGroups.FirstOrDefault(g => g.Description == "First Route Group");
                //var que = new Repository.Que() {Description = "John", RouteGroup = rGroup};
                //var sch = new Repository.QueSchedule() {AtTime = DateTime.Now.TimeOfDay, NotifyEmail = true, OnDays = queDay, Que = que};

                //ent.AddToQues(que);
                //ent.AddToQueSchedules(sch);
                //ent.SaveChanges();

                

                var queSchedule =
                    (from q in ent.QueSchedules
                     where q.OnDays.Contains(queDay)
                     //&& (q.AtTime >= queLastRun.TimeOfDay && q.AtTime < queTimeEnd.TimeOfDay)
                     select q);


                using (var smtp = new SmtpClient("mail.tecknonerd.com"))
                {
                    smtp.Credentials = new NetworkCredential("hackthemidwest@tecknonerd.com", "fredthefish");

                    foreach (var schedule in queSchedule)
                    {
                        smtp.Send(new MailMessage("hackthemidwest@tecknonerd.com",
                                                  schedule.Que.RouteGroup.Commuter.Email,
                                                  "Commuticate Que",
                                                  "It worked: " + DateTime.Now));

                        schedule.LastRun = DateTime.Now;
                    }
                }

                ent.SaveChanges();
            }

            Log.Info(string.Format("End! - {0}", System.DateTime.Now.ToString("r")));

            //Console.CursorLeft = 0;
            Console.WriteLine(String.Format("Scheduled: {0} - Ran: {1} ", context.ScheduledFireTimeUtc, context.FireTimeUtc));
        }
    }
}