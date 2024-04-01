using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;

namespace WebApp.Models.Repository
{
    public class SettingDbRepository:ISettingRepository
    {
        ApplicationDbContext db;
        public SettingDbRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public Setting Find(int id)
        {
            return db.Settings.SingleOrDefault(x => x.Id == id);
        }
        public async Task<Setting> Get(int id)
        {
            return await db.Settings.FindAsync(id);
        }

        public void Update( Setting newSetting)
        {
            db.Settings.Update(newSetting);
            db.SaveChanges();
        }
    }
}
