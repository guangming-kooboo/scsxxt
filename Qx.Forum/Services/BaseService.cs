using Qx.Community.Entity;

namespace Qx.Community.Services
{
  public  class BaseService
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
