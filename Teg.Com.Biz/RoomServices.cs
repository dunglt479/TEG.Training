using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teg.Com.Data;
using Teg.Com.IBiz;
using Teg.Com.Model;

namespace Teg.Com.Biz
{
    public class RoomServices : Repository<RoomDao,Room> ,IRoomServices
    {
    }
}
