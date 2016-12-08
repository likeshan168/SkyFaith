using System;
using DevExpress.ExpressApp.Model;

namespace Admiral.ImportData
{
    public interface IImportOption
    {
        IModelClass MainTypeInfo { get; set; }
        decimal Progress { get; set; }
        Action<decimal> UpdateProgress { get; set; }
    }
}