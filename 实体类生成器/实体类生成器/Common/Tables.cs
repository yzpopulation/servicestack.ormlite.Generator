using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 实体类生成器.Sqlite;

namespace 实体类生成器.Common
{
    public interface ITables
    {
        string objname { get; set; }
        List<object> infos { get; set; }
        string ToClassString();
    }
}
