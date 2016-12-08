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
using System.ComponentModel;
using SkyFaith.Module.BusinessObjects.NonPersistentObjects;
using DevExpress.Persistent.BaseImpl;

namespace SkyFaith.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class MyNonPersistentClassWindowController : WindowController
    {
        private IObjectSpace additionalObjectSpace;
        public MyNonPersistentClassWindowController()
        {
            InitializeComponent();
            TargetWindowType = WindowType.Main;
            // Target required Windows (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            Application.ObjectSpaceCreated += Application_ObjectSpaceCreated;

            additionalObjectSpace = Application.CreateObjectSpace(typeof(DevExpress.Persistent.BaseImpl.Person));
            
        }
        private void Application_ObjectSpaceCreated(object sender, ObjectSpaceCreatedEventArgs e)
        {
            if (e.ObjectSpace is NonPersistentObjectSpace)
            {
                ((NonPersistentObjectSpace)e.ObjectSpace).ObjectsGetting += ObjectSpace_ObjectsGetting;
                ((NonPersistentObjectSpace)e.ObjectSpace).AdditionalObjectSpaces.Add(additionalObjectSpace);

            }
        }
        private void ObjectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            BindingList<MyNonPersistentObject> objects = new BindingList<MyNonPersistentObject>();
            for (int i = 1; i < 10; i++)
            {
                objects.Add(new MyNonPersistentObject() { Name = string.Format("Object {0}", i) });
            }
            e.Objects = objects;
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            Application.ObjectSpaceCreated -= Application_ObjectSpaceCreated;
            Application.ObjectSpaceCreated -= Application_ObjectSpaceCreated;
            if (additionalObjectSpace != null)
            {
                additionalObjectSpace.Dispose();
                additionalObjectSpace = null;
            }

        }
    }
}
