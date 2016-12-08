// Developer Express Code Central Example:
// XAF Project Management Application - Stage 8
// 
// This example contains the source code for the XAF Project Management Application
// - Stage 8. The complete description can be found in the XAF – Project Management
// Application #10
// (http://community.devexpress.com/blogs/garyshort/archive/2009/09/30/xaf-project-management-application-10.aspx)
// blog post.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E1750

using System;
using System.Collections.Generic;
using System.Text;
using Admiral.ImportData;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;

namespace Admiral.DataImport.Editors.Win
{
    public class RepositoryItemTaskProgressBarControl : RepositoryItemProgressBar
    {
        protected internal const string EditorName = "TaskProgressBarControl";
        protected internal static void Register()
        {
            if (!EditorRegistrationInfo.Default.Editors.Contains(EditorName))
            {
                EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName, typeof(TaskProgressBarControl),
                    typeof(RepositoryItemTaskProgressBarControl), typeof(ProgressBarViewInfo),
                    new ProgressBarPainter(), true, EditImageIndexes.ProgressBarControl, typeof(DevExpress.Accessibility.ProgressBarAccessible)));
            }
        }
        static RepositoryItemTaskProgressBarControl()
        {
            Register();
        }
        public RepositoryItemTaskProgressBarControl()
        {
            Maximum = 100;
            Minimum = 0;
            ShowTitle = true;
            Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        }
        protected override int ConvertValue(object val)
        {
            try
            {
                float number = Convert.ToSingle(val);
                return (int)(Minimum + number * Maximum);
            }
            catch { }
            return Minimum;
        }

        public override string EditorTypeName { get { return EditorName; } }
    }

}
