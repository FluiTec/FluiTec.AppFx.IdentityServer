﻿using System.Collections.Generic;
using System.Linq;
using Dapper;
using FluiTec.AppFx.Data;
using FluiTec.AppFx.Data.Sql;
using FluiTec.AppFx.IdentityServer.Dapper.Repositories;
using FluiTec.AppFx.IdentityServer.Entities;

namespace FluiTec.AppFx.IdentityServer.Dapper.Mysql.Repositories
{
    /// <summary>	A Mysql API resource scope repository. </summary>
    public class MysqlApiResourceScopeRepository : ApiResourceScopeRepository
    {
        #region Constructors

        /// <summary>	Constructor. </summary>
        /// <param name="unitOfWork">	The unit of work. </param>
        public MysqlApiResourceScopeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #endregion

        #region ApiResourceScopeRepository

        /// <summary>	Gets the scope identifiers in this collection. </summary>
        /// <param name="ids">	The identifiers. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the scope identifiers in this
        ///     collection.
        /// </returns>
        public override IEnumerable<ApiResourceScopeEntity> GetByScopeIds(int[] ids)
        {
            var command =
                $"SELECT {SqlBuilder.Adapter.RenderPropertyList(SqlCache.TypePropertiesChache(typeof(ApiResourceScopeEntity)).ToArray())} FROM {TableName} WHERE {nameof(ApiResourceScopeEntity.ScopeId)} IN @Ids";
            return UnitOfWork.Connection.Query<ApiResourceScopeEntity>(command, new {Ids = ids},
                UnitOfWork.Transaction);
        }

        /// <summary>	Gets the API identifiers in this collection. </summary>
        /// <param name="ids">	The identifiers. </param>
        /// <returns>
        ///     An enumerator that allows foreach to be used to process the API identifiers in this
        ///     collection.
        /// </returns>
        public override IEnumerable<ApiResourceScopeEntity> GetByApiIds(int[] ids)
        {
            var command =
                $"SELECT {SqlBuilder.Adapter.RenderPropertyList(SqlCache.TypePropertiesChache(typeof(ApiResourceScopeEntity)).ToArray())} FROM {TableName} WHERE {nameof(ApiResourceScopeEntity.ApiResourceId)} IN @Ids";
            return UnitOfWork.Connection.Query<ApiResourceScopeEntity>(command, new {Ids = ids},
                UnitOfWork.Transaction);
        }

        #endregion
    }
}