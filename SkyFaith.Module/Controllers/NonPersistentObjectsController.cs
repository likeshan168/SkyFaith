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
using SkyFaith.Module.BusinessObjects.NonPersistentObjects;
using System.ComponentModel;

namespace SkyFaith.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class NonPersistentObjectsController : WindowController
    {
        public NonPersistentObjectsController()
        {
            InitializeComponent();
            // Target required Windows (via the TargetXXX properties) and create their Actions.
            TargetWindowType = WindowType.Main;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target Window.
            Application.ListViewCreating += Application_ListViewCreating;
            Application.ObjectSpaceCreated += Application_ObjectSpaceCreated;

        }

        private void Application_ObjectSpaceCreated(object sender, ObjectSpaceCreatedEventArgs e)
        {
            if (e.ObjectSpace is NonPersistentObjectSpace)
            {
                ((NonPersistentObjectSpace)e.ObjectSpace).ObjectsGetting += NonPersistentObjectsController_ObjectGetting;
            }
        }

        private void NonPersistentObjectsController_ObjectGetting(object sender, ObjectsGettingEventArgs e)
        {
            BindingList<NonPersistentObject> binding = new BindingList<NonPersistentObject>();
            for (int i = 0; i < 20; i++)
            {
                binding.Add(new NonPersistentObject()
                {
                    Oid = i,
                    Name = string.Format("Name {0}",i)
                });
            }

            e.Objects = binding;
        }

        private void Application_ListViewCreating(object sender, ListViewCreatingEventArgs e)
        {
            if (e.CollectionSource.ObjectTypeInfo.Type == typeof(NonPersistentObject) && (e.CollectionSource.ObjectSpace is NonPersistentObjectSpace))
            {
                ((NonPersistentObjectSpace)e.CollectionSource.ObjectSpace).ObjectsGetting += NonPersistentObjectsController_ObjectsGetting;
            }
        }

        private void NonPersistentObjectsController_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            BindingList<NonPersistentObject> objects = new BindingList<NonPersistentObject>();
            for (int i = 0; i < 10; i++)
            {
                objects.Add(new NonPersistentObject() { Name = string.Format("Object {0}", i) });
            }

            e.Objects = objects;
        }

        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            Application.ListViewCreating -= Application_ListViewCreating;
            Application.ObjectSpaceCreated -= Application_ObjectSpaceCreated;

        }




    }
}
