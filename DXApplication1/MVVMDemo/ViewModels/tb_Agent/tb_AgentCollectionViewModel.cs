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
    /// Represents the tb_Agent collection view model.
    /// </summary>
    public partial class tb_AgentCollectionViewModel : CollectionViewModel<tb_Agent, int, Idb_SFIEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of tb_AgentCollectionViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static tb_AgentCollectionViewModel Create(IUnitOfWorkFactory<Idb_SFIEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new tb_AgentCollectionViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the tb_AgentCollectionViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the tb_AgentCollectionViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected tb_AgentCollectionViewModel(IUnitOfWorkFactory<Idb_SFIEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.tb_Agent)
        {
        }
    }
}