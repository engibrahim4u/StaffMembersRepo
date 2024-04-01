using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Repository
{
   public interface ISettingRepository
    {
        Setting Find(int id);
        Task<Setting> Get(int id);

        void Update(Setting newSetting);
    }
}
