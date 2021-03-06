﻿using FluiTec.AppFx.IdentityServer.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluiTec.AppFx.IdentityServer.Dapper.Mysql.Test
{
    [TestClass]
    public class MysqlApiResourceClaimTest : ApiResourceClaimTest
    {
        public MysqlApiResourceClaimTest() : base(Helper.GetDataService())
        {
        }

        [TestMethod]
        public override void CanAddAndGetApiResourceClaim()
        {
            base.CanAddAndGetApiResourceClaim();
        }

        [TestMethod]
        public override void CanAddAndGetApiResourceClaims()
        {
            base.CanAddAndGetApiResourceClaims();
        }

        [TestMethod]
        public override void CanUpdateApiResource()
        {
            base.CanUpdateApiResource();
        }

        [TestMethod]
        public override void CanDeleteApiResource()
        {
            base.CanDeleteApiResource();
        }

        [TestMethod]
        public override void CanAddAndGetApiResourceClaimByApiResourceId()
        {
            base.CanAddAndGetApiResourceClaimByApiResourceId();
        }

        [TestMethod]
        public override void CanGetByApiIds()
        {
            base.CanGetByApiIds();
        }
    }
}