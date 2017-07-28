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

        #region ExamLockValidTimeSpan
        public static double GetExamLockValidTimeSpan()
        {
            return RISConfig.Instance.ExamLockValidTimeSpan;
        }
        public static void SetExamLockValidTimeSpan(string operateUserCode, double value)
        {
            CheckoutValid.HasPermission(operateUserCode, "SetExamLockValidTimeSpan", PermissionCode.SysConfigure, Utilities.ECS3_Module.ConfigsModule);
            RISConfig.Instance.ExamLockValidTimeSpan = value;
        }
        #endregion

        #region FromPrintedToRejectedValidTimeSpan
        public static double GetFromPrintedToRejectedValidTimeSpan()
        {
            return RISConfig.Instance.FromPrintedToRejectedValidTimeSpan;
        }
        public static void SetFromPrintedToRejectedValidTimeSpan(string operateUserCode, double value)
        {
            CheckoutValid.HasPermission(operateUserCode, "SetFromPrintedToRejectedValidTimeSpan", PermissionCode.SysConfigure, Utilities.ECS3_Module.ConfigsModule);
            RISConfig.Instance.FromPrintedToRejectedValidTimeSpan = value;
        }
        #endregion

        #region 值班时间段 + 值班审核医生
        public static DateTime? GetOnDutyTimeBegin()
        {
            return RISConfig.Instance.OnDutyTimeBegin;
        }
        public static void SetOnDutyTimeBegin(string operateUserCode, DateTime? value)
        {
            CheckoutValid.HasPermission(operateUserCode, "SetOnDutyTimeBegin", PermissionCode.SysConfigure, Utilities.ECS3_Module.ConfigsModule);
            RISConfig.Instance.OnDutyTimeBegin = value;
        }

        public static DateTime? GetOnDutyTimeEnd()
        {
            return RISConfig.Instance.OnDutyTimeEnd;
        }
        public static void SetOnDutyTimeEnd(string operateUserCode, DateTime? value)
        {
            CheckoutValid.HasPermission(operateUserCode, "SetOnDutyTimeEnd", PermissionCode.SysConfigure, Utilities.ECS3_Module.ConfigsModule);
            RISConfig.Instance.OnDutyTimeEnd = value;
        }

        public static string GetOnDutyReviewUserCode()
        {
            return RISConfig.Instance.OnDutyReviewUserCode;
        }
        public static void SetOnDutyReviewUserCode(string operateUserCode, string value)
        {
            CheckoutValid.HasPermission(operateUserCode, "SetOnDutyReviewUserCode", PermissionCode.SysConfigure, Utilities.ECS3_Module.ConfigsModule);
            RISConfig.Instance.OnDutyReviewUserCode = value;
        }
        #endregion

        #region ObjectCreatorBase
        internal static string GetObjectCreatorKeyCode(string keyCode)
        {
            string result = string.Empty;
            ObjectCreatorByConfig_BLLDB.Instance.GetValue<string>(keyCode, ref result);
            return result;
        }
        internal static void SetObjectCreatorKeyCode(string operateUserCode, string keyCode, string value)
        {
            CheckoutValid.HasPermission(operateUserCode, "SetObjectCreatorKeyCode", PermissionCode.SysConfigure, Utilities.ECS3_Module.ConfigsModule);
            ObjectCreatorByConfig_BLLDB.Instance.SetValue(keyCode, value);
        }
        #endregion

        #region PACSGatewayImplement
        public static string GetPACSGatewayImplement()
        {
            return GetObjectCreatorKeyCode(PACSGateway_CreateIniEntity.BASE_KEY);
        }
        public static void SetPACSGatewayImplement(string operateUserCode, string value)
        {
            SetObjectCreatorKeyCode(operateUserCode, PACSGateway_CreateIniEntity.BASE_KEY, value);
        }
        #endregion

        #region HISGatewayImplement
        public static string GetHISGatewayImplement()
        {
            return GetObjectCreatorKeyCode(HISGateway_CreateIniEntity.BASE_KEY);
        }
        public static void SetHISGatewayImplement(string operateUserCode, string value)
        {
            SetObjectCreatorKeyCode(operateUserCode, HISGateway_CreateIniEntity.BASE_KEY, value);
        }
        #endregion

        #region PatientIDGeneratorImplement
        public static string GetPatientIDGeneratorImplement()
        {
            return GetObjectCreatorKeyCode(PatientIDGenerator_CreateIniEntity.BASE_KEY);
        }
        public static void SetPatientIDGeneratorImplement(string operateUserCode, string value)
        {
            SetObjectCreatorKeyCode(operateUserCode, PatientIDGenerator_CreateIniEntity.BASE_KEY, value);
        }
        #endregion

        #region AccessionNOGeneratorImplement
        public static string GetAccessionNOGeneratorImplement()
        {
            return GetObjectCreatorKeyCode(AccessionNOGenerator_CreateIniEntity.BASE_KEY);
        }
        public static void SetAccessionNOGeneratorImplement(string operateUserCode, string value)
        {
            SetObjectCreatorKeyCode(operateUserCode, AccessionNOGenerator_CreateIniEntity.BASE_KEY, value);
        }
        #endregion

        #region DataPrinterImplement
        public static string GetDataPrinterImplement()
        {
            return GetObjectCreatorKeyCode(DataPrinter_CreateIniEntity.BASE_KEY);
        }
        public static void SetDataPrinterImplement(string operateUserCode, string value)
        {
            SetObjectCreatorKeyCode(operateUserCode, DataPrinter_CreateIniEntity.BASE_KEY, value);
        }
        #endregion

        #region CAImplement
        public static string GetCAImplement(string sysTypeCode)
        {
            if (string.IsNullOrEmpty(sysTypeCode))
            {
                return GetObjectCreatorKeyCode(CAImpl_CreateIniEntity.BASE_KEY);
            }
            else
            {
                return GetObjectCreatorKeyCode(CAImpl_CreateIniEntity.BASE_KEY + "_" + sysTypeCode);
            }
        }
        public static void SetCAImplement(string operateUserCode, string sysTypeCode, string value)
        {
            if (string.IsNullOrEmpty(sysTypeCode))
            {
                SetObjectCreatorKeyCode(operateUserCode, CAImpl_CreateIniEntity.BASE_KEY, value);
            }
            else
            {
                SetObjectCreatorKeyCode(operateUserCode, CAImpl_CreateIniEntity.BASE_KEY + "_" + sysTypeCode, value);
            }
        }
        #endregion
    }
}
