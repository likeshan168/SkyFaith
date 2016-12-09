using DevExpress.Mvvm.DataModel;
using MVVMDemo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVMDemo.db_SFIEntitiesDataModel
{

    /// <summary>
    /// Idb_SFIEntitiesUnitOfWork extends the IUnitOfWork interface with repositories representing specific entities.
    /// </summary>
    public interface Idb_SFIEntitiesUnitOfWork : IUnitOfWork
    {

        /// <summary>
        /// The tb_Agent entities repository.
        /// </summary>
        IRepository<tb_Agent, int> tb_Agent { get; }

        /// <summary>
        /// The tb_SFI_TrackNum entities repository.
        /// </summary>
        IReadOnlyRepository<tb_SFI_TrackNum> tb_SFI_TrackNum { get; }
    }
}
