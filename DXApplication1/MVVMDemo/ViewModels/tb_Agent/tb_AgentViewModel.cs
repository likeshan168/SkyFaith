using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.ViewModel;
using MVVMDemo.db_SFIEntitiesDataModel;
using MVVMDemo.Common;
using MVVMDemo;

namespace MVVMDemo.ViewModels
{

    /// <summary>
    /// Represents the single tb_Agent object view model.
    /// </summary>
    public partial class tb_AgentViewModel : SingleObjectViewModel<tb_Agent, int, Idb_SFIEntitiesUnitOfWork>
    {

        /// <summary>
        /// Creates a new instance of tb_AgentViewModel as a POCO view model.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        public static tb_AgentViewModel Create(IUnitOfWorkFactory<Idb_SFIEntitiesUnitOfWork> unitOfWorkFactory = null)
        {
            return ViewModelSource.Create(() => new tb_AgentViewModel(unitOfWorkFactory));
        }

        /// <summary>
        /// Initializes a new instance of the tb_AgentViewModel class.
        /// This constructor is declared protected to avoid undesired instantiation of the tb_AgentViewModel type without the POCO proxy factory.
        /// </summary>
        /// <param name="unitOfWorkFactory">A factory used to create a unit of work instance.</param>
        protected tb_AgentViewModel(IUnitOfWorkFactory<Idb_SFIEntitiesUnitOfWork> unitOfWorkFactory = null)
            : base(unitOfWorkFactory ?? UnitOfWorkSource.GetUnitOfWorkFactory(), x => x.tb_Agent, x => x.vchar_AGname)
        {
        }



    }
}
