using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commuticate.Repository;

namespace Commuticate
{
    public class Assistant
    {
        public void Snooze(Commuticate.Repository.QueSchedule queSchudle, TimeSpan delay)
        {
            using (var rep = new Entities())
            {
                var snoozeQue = new QueSchedule()
                                    {
                                        AtTime = DateTime.Now.Add(delay).TimeOfDay,
                                        Que = queSchudle.Que,
                                        OnDays = queSchudle.OnDays,
                                        NotifyEmail = queSchudle.NotifyEmail,
                                        NotifySMS = queSchudle.NotifySMS,
                                        NotifyTwitter = queSchudle.NotifyTwitter,
                                        RunOnce = true
                                    };

                rep.AddToQueSchedules(snoozeQue);
                rep.SaveChanges();
            }
        }
    }
}
