using DevExpress.ExpressApp.Model;
using System;
using System.Threading.Tasks;

namespace Admiral.ImportData
{
    //数据导入模块实现方法：
    //1.业务逻辑类中实现了IImportData接口后，即认为将启用数据导入功能

    //2.定义一个数据导入界面业务的逻辑

    public class ImportOption : IImportOption
    {
        public IModelClass MainTypeInfo
        {
            get; set;
        }

        private decimal progress;

        public decimal Progress
        {
            get { return progress; }
            set
            {
                progress = value;
                if (UpdateProgress != null)
                    UpdateProgress(value);
            }
        }

        public Action<decimal> UpdateProgress { get; set; }
    }

    //弹出窗口后，根据元数据信息列举出字段名
    //如果是简单类型，直接列举
    //如果是子列表，则新建一个sheet

    //确认后，根据BO中书写的验证规则验证数据，不满足的数据，给出错误提示，交需要将错误与Excel行做上对应
}
