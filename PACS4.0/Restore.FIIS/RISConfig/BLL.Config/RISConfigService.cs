using System;
using System.Collections.Generic;
using LXU.Database;
using Restore.FIIS.Entities;
using Restore.Contracts.Enums;
using Restore.FIIS.BLL.CreateIniMgr;
using Restore.FIIS.BLL;
using Restore.FIIS;


namespace Restore.FIIS.BLL
{
   //
    public static class RISConfigService
    {
        #region IsSimpleMode

        public static void DeleteSimpleMode(string operateUserCode, string key)
        { 
            CheckoutValid.HasPermission(operateUserCode, "DeleteSimpleMode", PermissionCode.SysConfigure, Utilities.ECS3_Module.ConfigsModule);
            
            using (ISys_Configure_LXUO table = Global.CreateTable<ISys_Configure_LXUO>())
            {
                DBCondition condition = table.Key_LXUF.EqualTo(key);
                table.Delete(condition);
            }
        }

        
        public static bool isSimpleModeByKey(string keyCode)
        {
            IList<ISys_Configure_LXUE> entities = BLL.SysConfigureService.GetAllCreateIni("objects",Mode_Type.BASE_KEY);
            if (null != entities)
            {
                foreach (ISys_Configure_LXUE item in entities)
                {
                    if (item.Key.Equals(keyCode))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static string GetIsSimpleMode(string keyCode)
        {
            if (string.IsNullOrEmpty(keyCode))
            {
                keyCode = Mode_Type.BASE_KEY;
            }
            else
            {
                keyCode = Mode_Type.BASE_KEY + "_" + keyCode;
            }
            
            string result = string.Empty;
            ObjectCreatorByConfig_BLLDB.Instance.GetValue<string>(keyCode, ref result);
            return result;
        }
        
        public static void SetIsSimpleMode(string operateUserCode, string simpleModeKey, bool SimpleModeValue)
        {

            if (string.IsNullOrEmpty(simpleModeKey))
            {
                simpleModeKey = Mode_Type.BASE_KEY;
            }
            else
            {
                string[] sims = simpleModeKey.Split('_');
                if (!Mode_Type.BASE_KEY.Equals(sims[0]))
                {
                    simpleModeKey = Mode_Type.BASE_KEY + "_" + simpleModeKey;
                }
               
                CheckoutValid.HasPermission(operateUserCode, "SetIsSimpleMode", PermissionCode.SysConfigure, Utilities.ECS3_Module.ConfigsModule);
                ObjectCreatorByConfig_BLLDB.Instance.SetValue(simpleModeKey, SimpleModeValue);               
            }
           
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
