using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using LibraryCRM.Infrastructure;
using LibraryCRM.Models;
using LibraryCRM.Data.Models;

namespace LibraryCRM.Data
{
    public class SessionTake : Take
    {
        public static Take GetTake(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionTake take = session?.GetJson<SessionTake>("Take")
                ?? new SessionTake();
            take.Session = session;
            return take;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Book book, int quantity)
        {
            base.AddItem(book, quantity);
            Session.SetJson("Take", this);
        }

        public override void RemoveLine(Book book)
        {
            base.RemoveLine(book);
            Session.SetJson("Take", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Take");
        }
    }
}
