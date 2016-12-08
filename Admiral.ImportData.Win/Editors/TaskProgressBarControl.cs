using DevExpress.XtraEditors;

namespace Admiral.DataImport.Editors.Win
{
    public class TaskProgressBarControl : ProgressBarControl
    {
        static TaskProgressBarControl()
        {
            
            RepositoryItemTaskProgressBarControl.Register();
        }
        public override string EditorTypeName { get { return RepositoryItemTaskProgressBarControl.EditorName; } }
        protected override object ConvertCheckValue(object val)
        {
            return val;
        }

        public TaskProgressBarControl()
        {
            base.Properties.Maximum = 100;
        }
    }
}