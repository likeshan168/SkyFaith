using System;
using System.Linq;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.ViewModel;
using MVVMDemo.db_SFIEntitiesDataModel;
using MVVMDemo.Common;
using MVVMDemo;

namespace MVVMDemo.ViewModels
{

    /// <summary>
    /// Represents the tb_SFI_TrackNum collection view model.
    /// </summary>
    public partial class tb_SFI_TrackNumCollectionViewModel : ReadOnlyCollectionViewModel<tb_SFI_TrackNum, Idb_SFIEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of tb_SFI_TrackNumCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static tb_SFI_TrackNumCollectionViewModel Create(IUnitOfWorkFactory<Idb_SFIEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new tb_SFI_TrackNumCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the tb_SFI_TrackNumCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the tb_SFI_TrackNumCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected tb_SFI_TrackNumCollectionViewModel(IUnitOfWorkFactory<Idb_SFIEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.tb_SFI_TrackNum)
        {
        }
    }
}