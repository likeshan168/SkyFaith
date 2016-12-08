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
using SkyFaith.Module.BusinessObjects.PersistentObjects;
using SkyFaith.Module.BusinessObjects.NonPersistentObjects;

namespace SkyFaith.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ShowDuplicateBooksController : ObjectViewController<ListView, Book>
    {
        public ShowDuplicateBooksController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.

            PopupWindowShowAction showDuplicateAction = new PopupWindowShowAction(this, "ShowDuplicateBooks", "View");
            showDuplicateAction.CustomizePopupWindowParams += ShowDuplicateAction_CustomizePopupWindowParams;
        }

        private void ShowDuplicateAction_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            foreach (Book book in View.CollectionSource.List)
            {
                if (!string.IsNullOrWhiteSpace(book.Title))
                {
                    if (dictionary.ContainsKey(book.Title))
                    {
                        dictionary[book.Title]++;
                    }
                    else
                    {
                        dictionary.Add(book.Title, 1);
                    }
                }
            }

            DuplicateList duplicateList = new DuplicateList();
            int duplicateId = 0;
            foreach (KeyValuePair<string, int> item in dictionary)
            {
                if (item.Value > 1)
                {
                    duplicateList.Duplicates.Add
                        (
                            new Duplicate()
                            {
                                Id = duplicateId,
                                Name = item.Key,
                                Count = item.Value
                            }
                        );

                    duplicateId++;
                }
            }

            DetailView detailView = Application.CreateDetailView(Application.CreateObjectSpace(), duplicateList);
            detailView.ViewEditMode = ViewEditMode.Edit;

            e.View = detailView;
            e.DialogController.SaveOnAccept = false;
            e.DialogController.CancelAction.Active["NothingToCancel"] = false;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
