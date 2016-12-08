using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Spreadsheet;
using DevExpress.Xpo;

namespace Admiral.ImportData
{

    public interface IModelImportData
    {
        bool AllowImportData { get; set; }
    }


    public class ExcelImporter
    {
        XafApplication _application;
        IImportOption option;
        public void Setup(XafApplication app)
        {
            _application = app;

        }

        public ExcelImporter()
        {

        }
        public void ProcessImportAction(IWorkbook document)
        {
            var os = _application.CreateObjectSpace() as XPObjectSpace;
            os.Session.BeginTransaction();
            bool rst = true;

            foreach (var item in document.Worksheets)
            {
                var typeName = item.Cells[0, 1].DisplayText;
                var t = option.MainTypeInfo.Application.BOModel.GetClass(ReflectionHelper.FindType(typeName));
                var success = StartImport(item, t, os);

                item.Cells[0, 3].SetValue(success ? "导入成功" : "导入失败");


                rst &= success;

            }

            if (rst)
            {
                try
                {
                    os.Session.CommitTransaction();
                    //os.CommitChanges();
                }
                catch
                {
                    os.Session.RollbackTransaction();
                    //os.Rollback();
                }
            }
            else {
                //os.Rollback();
                os.Session.RollbackTransaction();
            }
        }

        public static Action DoApplicationEvent { get; set; }

        private bool StartImport(Worksheet ws, IModelClass bo, IObjectSpace os)
        {
            //开始导入:
            //1.先使用表头的标题找到属性名称
            Dictionary<int, IModelMember> fields = new Dictionary<int, IModelMember>();
            List<SheetRowObject> objs = new List<SheetRowObject>();
            //var ws = _spreadsheet.Document.Worksheets[0];
            var columnCount = ws.Columns.LastUsedIndex;

            var updateImport = bo.TypeInfo.FindAttribute<UpdateImportAttribute>();
            var findObjectProviderAttribute = bo.TypeInfo.FindAttribute<FindObjectProviderAttribute>();

            if (findObjectProviderAttribute != null)
                findObjectProviderAttribute.Reset();

            var isUpdateImport = updateImport != null;

            var keyColumn = 0;
            var headerError = false;
            IModelMember keyField = null;
            for (int c = 1; c <= columnCount; c++)
            {
                var fieldCaption = ws.Cells[1, c].DisplayText;
                var fieldName = bo.AllMembers.SingleOrDefault(x => x.Caption == fieldCaption);
                if (fieldName != null)
                {
                    fields.Add(c, fieldName);
                    if (isUpdateImport && fieldName.Name == updateImport.KeyMember)
                    {
                        keyColumn = c;
                        keyField = fieldName;
                    }
                }
                else
                {
                    ws.Cells[1, c].FillColor = Color.Red;
                    headerError = true;
                }
            }

            var sheetContext = new SheetContext(ws, fields.ToDictionary(x => x.Value.Name, x => x.Key));

            var rowCount = ws.Rows.LastUsedIndex;
            ws.Workbook.BeginUpdate();
            //清理上次的结果.
            for (int r = 2; r <= rowCount; r++)
            {
                //ws.Cells[r, 0].ClearContents();

                for (int c = 1; c <= columnCount; c++)
                {
                    var cel = ws.Cells[r, c];
                    if (cel.FillColor != Color.Empty)
                        cel.FillColor = Color.Empty;

                    if (cel.Font.Color != Color.Empty)
                        cel.Font.Color = Color.Empty;
                }

                var errorCell = ws.Cells[r, 0];
                if (!errorCell.Value.IsEmpty)
                {
                    errorCell.Clear();
                }
            }

            ws.Workbook.EndUpdate();
            if (headerError)
            {
                ws.Cells[0, 4].SetValue("表头有错误，请查看被标红色的表头，确认行中没有对应的数据。");
                return false;
            }




            var updateStep = rowCount/100;
            if (updateStep == 0)
                updateStep = 1;

            var numberTypes = new[]
            {
                typeof(Int16),typeof(Int32),typeof(Int64),typeof(UInt16),typeof(UInt32),typeof(UInt64),typeof(decimal),typeof(float),typeof(double),
                typeof(byte),typeof(sbyte)
            };

            ws.Workbook.BeginUpdate();
            for (int r = 2; r <= rowCount; r++)
            {
                
                
                XPBaseObject obj = null;
                if (isUpdateImport)
                {
                    var cdvalue = Convert.ChangeType(ws.Cells[r, keyColumn].Value.ToObject(), keyField.Type);
                    var cri = new BinaryOperator(updateImport.KeyMember, cdvalue);
                    if (findObjectProviderAttribute != null)
                    {
                        var t = findObjectProviderAttribute.FindObject(os, bo.TypeInfo.Type, cri, true);
                        if (t.Count > 0)
                        {
                            obj = t[0] as XPBaseObject;
                        }
                        else
                        {
                            t = null;
                        }
                    }
                    else
                    {
                        obj = os.FindObject(bo.TypeInfo.Type, cri) as XPBaseObject;
                    }

                    if (obj == null)
                    {
                        obj = os.CreateObject(bo.TypeInfo.Type) as XPBaseObject;
                    }
                }
                else
                {
                    obj = os.CreateObject(bo.TypeInfo.Type) as XPBaseObject;
                }

                var result = new SheetRowObject(sheetContext) {Object = obj, Row = r, RowObject = ws.Rows[r]};
               
                //var vle = ws.Cells[r, c];
                for (int c = 1; c <= columnCount; c++)
                {
                    
                    var field = fields[c];
                    var cell = ws.Cells[r, c];

                    if (!cell.Value.IsEmpty)
                    {
                        object value = null;
                        //引用类型
                        //兼容DC类型
                        var memberType = field.MemberInfo.MemberType;
                        if (memberType.IsValueType && memberType.IsGenericType)
                        {
                            if (memberType.GetGenericTypeDefinition() == typeof (Nullable<>))
                            {
                                memberType = memberType.GetGenericArguments()[0];
                            }
                        }

                        if (typeof (XPBaseObject).IsAssignableFrom(memberType) || field.MemberInfo.MemberTypeInfo.IsDomainComponent)
                        {
                            #region 引用类型
                            var conditionValue = cell.Value.ToObject();
                            //如果指定了查找条件，就直接使用
                            var idf = field.MemberInfo.FindAttribute<ImportDefaultFilterCriteria>();
                            var condition = idf == null ? "" : idf.Criteria;

                            #region 查找条件

                            if (string.IsNullOrEmpty(condition))
                            {
                                //没指定查找条件，主键不是自动生成的，必定为手工输入
                                if (!field.MemberInfo.MemberTypeInfo.KeyMember.IsAutoGenerate)
                                {
                                    condition = field.MemberInfo.MemberTypeInfo.KeyMember.Name + " = ?";
                                }
                            }

                            if (string.IsNullOrEmpty(condition))
                            {
                                //还是没有，找设置了唯一规则的
                                var ufield =
                                    field.MemberInfo.MemberTypeInfo.Members.FirstOrDefault(
                                        x => x.FindAttribute<RuleUniqueValueAttribute>() != null
                                        );
                                if (ufield != null)
                                    condition = ufield.Name + " = ? ";
                            }

                            if (string.IsNullOrEmpty(condition))
                            {
                                //还是没有，用defaultproperty指定的
                                var ufield = field.MemberInfo.MemberTypeInfo.DefaultMember;
                                if (ufield != null)
                                {
                                    condition = ufield.Name + " = ? ";
                                }
                            }

                            #endregion

                            #region p

                            if (string.IsNullOrEmpty(condition))
                            {
                                result.AddErrorMessage(
                                    string.Format(
                                        "错误，没有为引用属性{0}设置查找条件，查询过程中出现了错误，请修改查询询条!",
                                        field.MemberInfo.Name), cell);
                            }
                            else
                            {
                                try
                                {
                                    var @operator = CriteriaOperator.Parse(condition, new object[] {conditionValue});


                                    IList list = null;
                                    if (findObjectProviderAttribute != null)
                                    {
                                        list = findObjectProviderAttribute.FindObject(os, field.MemberInfo.MemberType,
                                            @operator, true);
                                    }
                                    else
                                    {
                                        list = os.GetObjects(field.MemberInfo.MemberType, @operator, true);

                                    }
                                    if (field.Caption == "办事处")
                                        Debug.WriteLine(list.Count + "," + field.Caption, @operator.ToString());
                                    if (list.Count != 1)
                                    {
                                        result.AddErrorMessage(
                                            string.Format(
                                                "错误，在查找“{0}”时，使用查找条件“{1}”，输入值是：“{3}”，查询过程中出现了错误，请修改查询询条!错误详情:{2}",
                                                field.MemberInfo.MemberType.FullName, condition,
                                                "找到了" + list.Count + "条记录", conditionValue), cell);
                                    }
                                    else
                                    {
                                        value = list[0];
                                    }
                                }
                                catch (Exception exception1)
                                {
                                    result.AddErrorMessage(
                                        string.Format("错误，在查找“{0}”时，使用查找条件“{1}”，查询过程中出现了错误，请修改查询询条!错误详情:{2}",
                                            field.MemberInfo.MemberType.FullName, condition, exception1.Message),
                                        cell);
                                }
                            }

                            #endregion

                            #endregion

                        }
                        else if (memberType == typeof (DateTime))
                        {
                            if (!cell.Value.IsDateTime)
                            {
                                result.AddErrorMessage(string.Format("字段:{0},要求输入日期!", field.Name), cell);
                            }
                            else
                            {
                                value = cell.Value.DateTimeValue;
                            }
                        }
                        else if (numberTypes.Contains(memberType))
                        {
                            if (!cell.Value.IsNumeric)
                            {
                                result.AddErrorMessage(string.Format("字段:{0},要求输入数字!", field.Name), cell);
                            }
                            else
                            {
                                value = Convert.ChangeType(cell.Value.NumericValue, field.MemberInfo.MemberType);
                            }
                        }
                        else if (memberType == typeof (bool))
                        {
                            if (!cell.Value.IsBoolean)
                            {
                                result.AddErrorMessage(string.Format("字段:{0},要求输入布尔值!", field.Name), cell);
                            }
                            else
                            {
                                value = cell.Value.BooleanValue;
                            }
                        }
                        else if (memberType == typeof (string))
                        {
                            var v = cell.Value.ToObject();
                            if (v != null)
                                value = v.ToString();
                        }
                        else if (memberType.IsEnum)
                        {
                            #region 枚举
                            if (cell.Value.IsNumeric)
                            {
                                #region 填写的是数字
                                var vle = Convert.ToInt64(cell.Value.NumericValue);
                                var any =
                                    Enum.GetValues(field.MemberInfo.MemberType)
                                        .OfType<object>()
                                        .Any(
                                            x =>
                                            {
                                                return object.Equals(Convert.ToInt64(x), vle);
                                            }
                                        );


                                if (any)
                                {
                                    value = Enum.ToObject(field.MemberInfo.MemberType, vle);
                                    // cell.Value.NumericValue;    
                                }
                                else
                                {
                                    result.AddErrorMessage(string.Format("字段:{0},所填写的枚举值，没在定义中出现!", field.Name), cell);
                                }
                                #endregion

                            }
                            else
                            {
                                #region 填写的是字符
                                var names = field.MemberInfo.MemberType.GetEnumNames();
                                if (names.Contains(cell.Value.TextValue))
                                {
                                    value = Enum.Parse(field.MemberInfo.MemberType, cell.Value.TextValue);
                                }
                                else
                                {
                                    result.AddErrorMessage(string.Format("字段:{0},所填写的枚举值，没在定义中出现!", field.Name), cell);
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            value = cell.Value.ToObject();
                        }
                        obj.SetMemberValue(field.Name, value);
                    }
                }
                
                objs.Add(result);
                if ((r-2)%updateStep == 0)
                {
                    Debug.WriteLine("Process:" + r);
                    if (DoApplicationEvent != null)
                    {
                        DoApplicationEvent();

                        this.option.Progress = ((r/(decimal) rowCount)+0.01m );
                        //Debug.WriteLine(this.option.Progress);
                        //var progress = ws.Cells[r, 0];
                        //progress.SetValue("完成");
                    }
                }
            }
            ws.Workbook.EndUpdate();
            if (objs.All(x => !x.HasError)){
                try
                {
                    Validator.RuleSet.ValidateAll(os, objs.Select(x => x.Object), "Save");
                    return true;
                }
                catch (ValidationException msgs)
                {
                    var rst = true;
                    ws.Workbook.BeginUpdate();
                    foreach (var item in msgs.Result.Results)
                    {
                        if (item.Rule.Properties.ResultType == ValidationResultType.Error && item.State == ValidationState.Invalid)
                        {
                            var r = objs.FirstOrDefault(x => x.Object == item.Target);
                            if (r != null)
                            {
                                r.AddErrorMessage(item.ErrorMessage, item.Rule.UsedProperties);
                            }
                            rst &= false;
                        }
                    }
                    ws.Workbook.EndUpdate();
                    return rst;
                }
            }


            return false;
        }

        IModelMember[] GetMembers(IModelClass cls)
        {
            return cls.AllMembers.Where(x =>
                !x.MemberInfo.IsAutoGenerate &&
                !x.IsCalculated &&
                !x.MemberInfo.IsReadOnly &&
                !x.MemberInfo.IsList
                ).ToArray().Except(cls.AllMembers.Where((x) =>
                {
                    var ia = x.MemberInfo.FindAttribute<ImportOptionsAttribute>();
                    return ia != null && !ia.NeedImport;
                }
                    )
                ).ToArray();
        }

        public void InitializeExcelSheet(IWorkbook book, IImportOption option)
        {
            this.option = option;
            CreateSheet(book.Worksheets[0], option.MainTypeInfo);
            if (book.Worksheets.Count == 1)
            {
                var listProperties = option.MainTypeInfo.AllMembers.Where(x => x.MemberInfo.IsList && x.MemberInfo.ListElementTypeInfo.IsPersistent);
                foreach (var item in listProperties)
                {
                    var cls = option.MainTypeInfo.Application.BOModel.GetClass(item.MemberInfo.ListElementTypeInfo.Type);
                    var b = book.Worksheets.Add(cls.Caption);
                    CreateSheet(b, cls);
                }
            }
            if (book.Worksheets.Count > 0)
                book.Worksheets.ActiveWorksheet = book.Worksheets[0];
        }

        private void CreateSheet(Worksheet book, IModelClass boInfo)
        {
            book.Name = boInfo.Caption;
            book.Cells[0, 0].Value = "系统类型";
            book.Cells[0, 1].Value = boInfo.TypeInfo.FullName;
            book.Cells[0, 2].Value = "本行信息为导入时对应系统业务信息，请勿删除!";
            //1.第一行，用于显示消息.
            //2.第一列，用于显示录入错误信息。
            var i = 1;
            #region main
            var cells = book.Cells;
            var members = GetMembers(boInfo);
            foreach (var item in members)
            {
                var c = cells[1, i];
                c.Value = item.Caption;
                c.FillColor = Color.FromArgb(255, 153, 0);
                c.Font.Color = Color.White;
                var isRequiredField = IsRequiredField(item);

                var range = book.Range.FromLTRB(i, 2, i, 20000);

                //DataValidation dv = null;

                if (isRequiredField)
                {
                    c.Font.Bold = true;
                    c.Font.Color = Color.Red;
                }
                i++;
            }
            #endregion
        }

        IRule[] _rules;
        IRule[] Rules
        {
            get
            {
                if (_rules == null)
                {
                    _rules = Validator.RuleSet.GetRules().ToArray();
                }
                return _rules;
            }
        }

        public bool IsRequiredField(IModelMember member)
        {
            //Rules.Where(x=>x.t)
            return Rules.Any(x => x.Properties is IRuleRequiredFieldProperties && x.Properties.TargetType == member.ModelClass.TypeInfo.Type && x.UsedProperties.IndexOf(member.Name) > -1);

        }
    }
}