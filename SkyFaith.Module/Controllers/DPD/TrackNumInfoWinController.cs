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
using SkyFaith.Module.BusinessObjects.DPD;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace SkyFaith.Module.Controllers.DPD
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class TrackNumInfoWinController : WindowController
    {
        public TrackNumInfoWinController()
        {
            InitializeComponent();
            // Target required Windows (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target Window.
            Application.ViewCreated += Application_ViewCreated;
        }

        private void Application_ViewCreated(object sender, ViewCreatedEventArgs e)
        {
            ObjectView objectView = e.View as ObjectView;
            if ((objectView != null) && (objectView.ObjectTypeInfo.Type == typeof(SFITrackNum)))
            {
                objectView.ObjectSpace.Committing += ObjectSpace_Committing;
                if (objectView is ListView)
                {
                    ((NonPersistentObjectSpace)objectView.ObjectSpace).ObjectsGetting += TrackNumInfoWinController_ObjectsGetting;
                    ((ListView)objectView).CollectionSource.ResetCollection();
                }
            }
        }

        private void TrackNumInfoWinController_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            using (IDbConnection conn = new SqlConnection("uid=sa;pwd=skyfaith;Pooling=false;Data Source=47.88.149.87;Initial Catalog=db_SFI"))
            {
                IEnumerable<SFITrackNum> stus = conn.Query<SFITrackNum, Agent, SFITrackNum>("sp_GetAllTrackNumInfo", (Track, agent) => { Track.Agent = agent; return Track; }, null, null, true, "int_AGid", null, CommandType.StoredProcedure);
                BindingList<SFITrackNum> bindingList = new BindingList<SFITrackNum>(stus.ToList());
                bindingList.AllowEdit = true;
                bindingList.AllowNew = true;
                bindingList.AllowRemove = true;

                e.Objects = bindingList;
            }
        }

        private void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
            Application.ViewCreated -= Application_ViewCreated;
        }
    }
}
