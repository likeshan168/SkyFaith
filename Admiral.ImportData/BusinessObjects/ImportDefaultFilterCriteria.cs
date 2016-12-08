using System;

namespace Admiral.ImportData
{
    /// <summary>
    /// 在导入数据时，指定引用型属性如何去查找值
    /// 如何定的是：“Name=?”则使用Excel中单元格的值去替换？号的值。
    /// 有结构：客户（姓名、年龄、联系人｛联系人姓名，手机｝），在导入客户信息时，联系人是引用型属性，指定了 “联系人姓名”后则可以导入。
    /// 指定的条件为 在联系人属性上设置[ImportDefaultFilterCriteria("联系人姓名=?")]即可。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public class ImportDefaultFilterCriteria : Attribute
    {
        // Methods
        public ImportDefaultFilterCriteria(string criteria)
        {
            this.Criteria = criteria;
        }

        // Properties
        public string Criteria { get; set; }
    }

    
}