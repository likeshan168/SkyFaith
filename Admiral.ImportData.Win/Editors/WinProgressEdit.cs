using System;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors.Repository;

namespace Admiral.DataImport.Editors.Win
{
    [PropertyEditor(typeof(decimal), false)]
    public class WinProgressEdit : DXPropertyEditor
    {
        public WinProgressEdit(Type objectType, IModelMemberViewItem model) : base(objectType, model) { }
        protected override object CreateControlCore()
        {
            return new TaskProgressBarControl();
        }
        protected override RepositoryItem CreateRepositoryItem()
        {
            return new RepositoryItemTaskProgressBarControl();
        }
        

        protected override void SetupRepositoryItem(RepositoryItem item)
        {
            RepositoryItemTaskProgressBarControl repositoryItem = (RepositoryItemTaskProgressBarControl)item;
            repositoryItem.Maximum = 100;
            repositoryItem.Minimum = 0;
            base.SetupRepositoryItem(item);
        }
    }
}