using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.ViewModel;
using MVVMDemo.db_SFIEntitiesDataModel;

namespace MVVMDemo.ViewModels
{
    /// <summary>
    /// Represents the root POCO view model for the db_SFIEntities data model.
    /// </summary>
    public partial class db_SFIEntitiesViewModel : DocumentsViewModel<db_SFIEntitiesModuleDescription, Idb_SFIEntitiesUnitOfWork>
    {

        const string TablesGroup = "Tables";

        const string ViewsGroup = "Views";
        INavigationService NavigationService { get { return this.GetService<INavigationService>(); } }

        /// <summary>
        /// Creates a new instance of db_SFIEntitiesViewModel as a POCO view model.
        /// </summary>
        public static db_SFIEntitiesViewModel Create()
        {
            return ViewModelSource.Create(() => new db_SFIEntitiesViewModel());
        }

        /// <summary>
        /// Initializes a new instance of the db_SFIEntitiesViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the db_SFIEntitiesViewModel type without the POCO proxy factory.
        /// </summary>
        protected db_SFIEntitiesViewModel()
            : base(UnitOfWorkSource.GetUnitOfWorkFactory())
        {
        }

        protected override db_SFIEntitiesModuleDescription[] CreateModules()
        {
            return new db_SFIEntitiesModuleDescription[] {
                new db_SFIEntitiesModuleDescription("tb Agent", "tb_AgentCollectionView", TablesGroup, GetPeekCollectionViewModelFactory(x => x.tb_Agent)),
                new db_SFIEntitiesModuleDescription("tb SFI Track Num", "tb_SFI_TrackNumCollectionView", ViewsGroup),
            };
        }



        protected override void OnActiveModuleChanged(db_SFIEntitiesModuleDescription oldModule)
        {
            if (ActiveModule != null && NavigationService != null)
            {
                NavigationService.ClearNavigationHistory();
            }
            base.OnActiveModuleChanged(oldModule);
        }
    }

    public partial class db_SFIEntitiesModuleDescription : ModuleDescription<db_SFIEntitiesModuleDescription>
    {
        public db_SFIEntitiesModuleDescription(string title, string documentType, string group, Func<db_SFIEntitiesModuleDescription, object> peekCollectionViewModelFactory = null)
            : base(title, documentType, group, peekCollectionViewModelFactory)
        {
        }
    }
}