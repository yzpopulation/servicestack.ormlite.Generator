using ServiceStack.DataAnnotations;

namespace 实体类生成器.Common
{
    
    public interface Itable_info
    {
        string PrimaryKey { get;  }
        string Unique { get;  }
        string AutoIncrement { get;  }
        string Type { get;  }
        string Name { get;  }

    }
}