using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDModel;
using TengDa.DB.Base;

namespace TDDB
{
    public class ChuckingDB : RepositoryBase<ChuckingModel>
    {
        public int InsertInfo(ChuckingModel model)
        {
            return this.Insert(model);
        }
    }
}
