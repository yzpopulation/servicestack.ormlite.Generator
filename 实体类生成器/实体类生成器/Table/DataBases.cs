using System.Collections.Generic;
using ServiceStack.DataAnnotations;

namespace 实体类生成器.Table
{
    public class Rdbms
    {
        public List<DataBases> Db { get; set; }
    }
    public class DataBases
    {
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DataBaseName { get; set; }
        public List<Schemas> Schemases { get; set; }
        public List<Tables> Tableses { get; set; }
    }

    public class Schemas
    {
        /// <summary>
        /// 模式名称
        /// </summary>
        public string SchemaName { get; set; }
        public List<Tables> Tableses { get; set; }
    }
    
    public class Tables
    {
        /// <summary>
        ///     Table
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        ///     [CompositeKey(nameof(Username), nameof(Region))]
        /// </summary>
        public string CompositeKey { get; set; }

        /// <summary>
        ///     [CompositeIndex(nameof(Username), nameof(Region))]
        /// </summary>
        public string CompositeIndex { get; set; }

        /// <summary>
        ///     [Alias("PlayerProfile")]  Maps to [PlayerProfile] RDBMS Table
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///     唯一键约束[UniqueConstraint(nameof(PartialUnique1), nameof(PartialUnique2), nameof(PartialUnique3))]
        /// </summary>
        public string UniqueConstraint { get; set; }

        public List<Columns> Columnses { get; set; }
    }

    public class Columns
    {
        /// <summary>
        ///     Column
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        ///     自增长
        /// </summary>
        public bool AutoIncrement { get; set; }

        /// <summary>
        ///     SQL Server 2012 [Sequence("Seq_SequenceTest_Id"), ReturnOnInsert]
        ///     var user = new SequenceTest { Name = "me", Email = "me@mydomain.com" };
        ///     db.Insert(user);ReturnOnInsert 将返回user.Id
        ///     [Sequence("Seq_Counter")]
        /// </summary>
        public string Sequence { get; set; }
        /// <summary>
        /// 格式
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        ///     主键
        /// </summary>
        public bool PrimaryKey { get; set; }

        /// <summary>
        ///     NOT NULL Column
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        ///     别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///     索引
        /// </summary>
        public bool Index { get; set; }

        /// <summary>
        ///     索引唯一
        /// </summary>
        public bool IndexUnique { get; set; }

        /// <summary>
        ///     聚簇索引
        /// </summary>
        public bool IndexClustered { get; set; }

        /// <summary>
        ///     非聚簇索引
        /// </summary>
        public bool IndexNonClustered { get; set; }

        /// <summary>
        ///     默认值 [Default(OrmLiteVariables.SystemUtc)]  [Default(0)]
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        ///     字符串长度[StringLength(50)]   [StringLength(StringLengthAttribute.MaxText)]
        /// </summary>
        public string StringLength { get; set; }

        /// <summary>
        ///     [CustomField("CHAR(20)")]
        /// </summary>
        public string CustomField { get; set; }

        /// <summary>
        ///     外键[ForeignKey(typeof(ForeignKeyTable2), OnDelete = "CASCADE", OnUpdate = "CASCADE")]
        /// </summary>
        public string ForeignKey { get; set; }

        /// <summary>
        ///     [CheckConstraint("Name IS NOT NULL")]  [CheckConstraint("Age > 1")]
        /// </summary>
        public string CheckConstraint { get; set; }
    }
}