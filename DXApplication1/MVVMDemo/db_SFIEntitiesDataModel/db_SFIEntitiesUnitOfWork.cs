using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.DataModel.EF6;
using MVVMDemo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MVVMDemo.db_SFIEntitiesDataModel
{

    /// <summary>
    /// A db_SFIEntitiesUnitOfWork instance that represents the run-time implementation of the Idb_SFIEntitiesUnitOfWork interface.
    /// </summary>
    public class db_SFIEntitiesUnitOfWork : DbUnitOfWork<db_SFIEntities>, Idb_SFIEntitiesUnitOfWork
    {

        public db_SFIEntitiesUnitOfWork(Func<db_SFIEntities> contextFactory)
            : base(contextFactory)
        {
        }

        IRepository<tb_Agent, int> Idb_SFIEntitiesUnitOfWork.tb_Agent
        {
            get { return GetRepository(x => x.Set<tb_Agent>(), (tb_Agent x) => x.int_AGid); }
        }

        IReadOnlyRepository<tb_SFI_TrackNum> Idb_SFIEntitiesUnitOfWork.tb_SFI_TrackNum
        {
            get { return GetReadOnlyRepository(x => x.Set<tb_SFI_TrackNum>()); }
        }
    }
}
