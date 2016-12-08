using System;

namespace Admiral.ImportData
{
    /// <summary>
    /// 指示属性、字段是否需要导入
    /// </summary>
    [AttributeUsage(AttributeTargets.Property| AttributeTargets.Field )]
    public class ImportOptionsAttribute : Attribute
    {
        public bool NeedImport { get;private set; } 

        public ImportOptionsAttribute(bool needImport)
        {
            this.NeedImport = needImport;
        }
    }
}