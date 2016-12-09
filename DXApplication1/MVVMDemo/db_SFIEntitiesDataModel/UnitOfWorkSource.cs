using DevExpress.Mvvm;
using DevExpress.Mvvm.DataModel;
using DevExpress.Mvvm.DataModel.DesignTime;
using DevExpress.Mvvm.DataModel.EF6;
using MVVMDemo;
using System;
using System.Collections;
using System.Linq;

namespace MVVMDemo.db_SFIEntitiesDataModel
{

    /// <summary>
    /// Provides methods to obtain the relevant IUnitOfWorkFactory.
    /// </summary>
    public static class UnitOfWorkSource
    {

        /// <summary>
        /// Returns the IUnitOfWorkFactory implementation.
        /// </summary>
        public static IUnitOfWorkFactory<Idb_SFIEntitiesUnitOfWork> GetUnitOfWorkFactory()
        {
            return new DbUnitOfWorkFactory<Idb_SFIEntitiesUnitOfWork>(() => new db_SFIEntitiesUnitOfWork(() => new db_SFIEntities()));
        }
    }
}