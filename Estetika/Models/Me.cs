using Estetika.Models.Entities;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Estetika
{
    public static class Me
    {
        public static Polzovatel Get()
        {
            using (SalonEntities entities = new SalonEntities())
            {
                return entities.Polzovatel
                    .Include(u => u.Tip_Polzovatel)
                    .First(p => p.Login == HttpContext.Current.User.Identity.Name);
            }
        }
    }
}