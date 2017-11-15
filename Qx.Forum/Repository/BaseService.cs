using Qx.Community.Entity;

namespace Qx.Community.Repository
{
    public class BaseRepository
    {
        private CommunityDbContext db;

        protected CommunityDbContext Db
        {
            get
            {
                if (db == null)
                {
                    db = new CommunityDbContext();
                }
                return db;
            }
        }
    }
}
