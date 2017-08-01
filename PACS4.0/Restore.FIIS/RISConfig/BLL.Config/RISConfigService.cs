using System;
using System.Collections.Generic;
using LXU.Database;
using Restore.FIIS.Entities;
using Restore.Contracts.Enums;
using Restore.FIIS.BLL.CreateIniMgr;

namespace Restore.FIIS.BLL
{
    /// <summary>
    /// RRIS系统的全局配置服务
    /// </summary>
    public static class RISConfigService
    {
        #region IsSimpleMode
        public static bool GetIsSimpleMode()
        {
            return RISConfig.Instance.IsSimpleMode;
        }
        public static void SetIsSimpleMode(string operateUserCode, bool value)
        {
            CheckoutValid.HasPermission(operateUserCode, "SetIsSimpleMode", PermissionCode.SysConfigure, Utilities.ECS3_Module.ConfigsModule);
            RISConfig.Instance.IsSimpleMode = value;
        }
        #endregion

    }
}
