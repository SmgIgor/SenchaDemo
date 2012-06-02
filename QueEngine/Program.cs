using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using Common.Logging;
using Quartz;
using Quartz.Impl;
using Commuticate.Repository;
using Commuticate;

namespace Commuticate.Que
{

    public class EmailSender : IJob
    {
		private static readonly ILog Log = LogManager.GetLogger(typeof(EmailSender));

        public void Execute(IJobExecutionContext context)
        {
			Log.Info(string.Format("Start! - {0}", System.DateTime.Now.ToString("r")));

            var queDay = DateTime.Now.ToString("ddd").ToUpperInvariant();
            var queLastRun = context.PreviousFireTimeUtc;
            var queTimeEnd = context.NextFireTimeUtc;

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
                    }
                }
            }

			Log.Info(string.Format("End! - {0}", System.DateTime.Now.ToString("r")));

            //Console.CursorLeft = 0;
            Console.WriteLine(String.Format("Scheduled: {0} - Ran: {1} ", context.ScheduledFireTimeUtc, context.FireTimeUtc));
        }
    }

    internal class Program 
    {

        private static void Main(string[] args)
        {
            ILog log = LogManager.GetLogger(typeof(Program));

            log.Info("------- Initializing ----------------------");

            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = sf.GetScheduler();

            log.Info("------- Initialization Complete -----------");


            // computer a time that is on the next round minute
            DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow);

            log.Info("------- Scheduling Job  -------------------");

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<EmailSender>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run on the next round minute
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartAt(runTime)
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            sched.ScheduleJob(job, trigger);
            log.Info(string.Format("{0} will run at: {1}", job.Key, runTime.ToString("r")));

            // Start up the scheduler (nothing can actually run until the 
            // scheduler has been started)
            sched.Start();
            log.Info("------- Started Scheduler -----------------");

            // wait long enough so that the scheduler as an opportunity to 
            // run the job!
            log.Info("------- Waiting 65 seconds... -------------");

            // wait 65 seconds to show jobs
            Console.WriteLine("Running");
            Console.WriteLine("Press any key to shutdown");
            Console.WriteLine("");
            Console.Read();

            // shut down the scheduler
            log.Info("------- Shutting Down ---------------------");
            sched.Shutdown(true);
            log.Info("------- Shutdown Complete -----------------");





            //var queDay = DateTime.Now.ToString("ddd").ToUpperInvariant();
            //var queLastRun = DateTime.Now;
            //var queTimeEnd = queLastRun.AddMinutes(5);


            ////var tScheduler = new System.Threading.Tasks.TaskScheduler();


            ////var thrd = new System.Threading.Tasks.TaskFactory()

            //using (var ent = new Commuticate.Repository.Entities())
            //{
            //    //var comm =
            //    //    ent.Commuters.FirstOrDefault(c => c.Email.Equals("jrosenbaum@smg.com", StringComparison.OrdinalIgnoreCase));

            //    //var rGroup = new RouteGroup() {Commuter = comm, Description = "First Route Group"};
            //    //ent.AddToRouteGroups(rGroup);

            //    //var route = new Route() {Commuter = comm, Description = "First Route"};
            //    //ent.AddToRoutes(route);

            //    //var rGroupR = new RouteGroupRoute() {RouteGroup = rGroup, RouteId = route.Id};
            //    //ent.AddToRouteGroupRoutes(rGroupR);
            //    //ent.SaveChanges();

            //    //var rGroup = ent.RouteGroups.FirstOrDefault(g => g.Description == "First Route Group");
            //    //var que = new Repository.Que() {Description = "John", RouteGroup = rGroup};
            //    //var sch = new Repository.QueSchedule() {AtTime = DateTime.Now.TimeOfDay, NotifyEmail = true, OnDays = queDay, Que = que};

            //    //ent.AddToQues(que);
            //    //ent.AddToQueSchedules(sch);
            //    //ent.SaveChanges();



            //    var queSchedule =
            //        (from q in ent.QueSchedules
            //         where q.OnDays.Contains(queDay)
            //         //&& (q.AtTime >= queLastRun.TimeOfDay && q.AtTime < queTimeEnd.TimeOfDay)
            //         select q);


            //    using (var smtp = new SmtpClient("mail.tecknonerd.com"))
            //    {
            //        smtp.Credentials = new NetworkCredential("hackthemidwest@tecknonerd.com", "fredthefish");

            //        foreach (var schedule in queSchedule)
            //        {
            //            smtp.Send(new MailMessage("hackthemidwest@tecknonerd.com",
            //                                      schedule.Que.RouteGroup.Commuter.Email,
            //                                      "Commuticate Que",
            //                                      "It worked: " + DateTime.Now));
            //        }
            //    }
            //}
        }
    }

}
