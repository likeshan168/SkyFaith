using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;

namespace SkyFaith.Module.Controllers.Student
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class StudentWinController : WindowController
    {
        public StudentWinController()
        {
            InitializeComponent();
            // Target required Windows (via the TargetXXX properties) and create their Actions.
            TargetWindowType = WindowType.Main;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target Window.
            Application.ViewCreated += Application_ViewCreated;
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            Application.ViewCreated -= Application_ViewCreated;
        }

        private void Application_ViewCreated(object sender,ViewCreatedEventArgs e)
        {
            ObjectView objectView = e.View  as ObjectView;
            if((objectView != null)&&(objectView.ObjectTypeInfo.Type==typeof(BusinessObjects.NonPersistentObjects.Student)))
            {
                objectView.ObjectSpace.Committing += ObjectSpace_Committing;
                if(objectView is ListView)
                {
                    ((NonPersistentObjectSpace)objectView.ObjectSpace).ObjectsGetting += StudentWinController_ObjectsGetting;
                    ((ListView)objectView).CollectionSource.ResetCollection();
                }
            }
        }

        private void StudentWinController_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            using (IDbConnection conn = new SqlConnection("uid=sa;pwd=sa;Pooling=false;Data Source=.;Initial Catalog=SkyFaith"))
            {
               IEnumerable<BusinessObjects.NonPersistentObjects.Student> stus = conn.Query<BusinessObjects.NonPersistentObjects.Student>("select * from student;");
                BindingList<BusinessObjects.NonPersistentObjects.Student> bindingList = new BindingList<BusinessObjects.NonPersistentObjects.Student>(stus.ToList());
                bindingList.AllowEdit = true;
                bindingList.AllowNew = true;
                bindingList.AllowRemove = true;

                e.Objects = bindingList;

            }
        }

        private void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)sender;
            using (IDbConnection conn = new SqlConnection("uid=sa;pwd=sa;Pooling=false;Data Source=.;Initial Catalog=SkyFaith"))
            {
                foreach (object obj in objectSpace.ModifiedObjects)
                {
                    if (obj is BusinessObjects.NonPersistentObjects.Student)
                    {
                        if (objectSpace.IsNewObject(obj))
                        {
                            conn.Execute("insert into student(Id,name,gender,phone) values(@Id,@name,@gender,@phone)", obj);
                        }
                        else if (objectSpace.IsDeletedObject(obj))
                        {
                            conn.Execute("delete from student where Id = @Id", obj);
                        }
                        else
                        {
                            conn.Execute("update student set name=@name,gender=@gender,phone=@phone where Id=@Id", obj);
                        }
                    }
                } 
            }
        }
    }
}
