﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Restore.FIIS.BC.Configs.Common;
using Restore.FIIS.BLL;
using Restore.FIIS.Entities;
using Restore.Utilities;
using Restore.Utilities.Configs;

namespace Restore.FIIS.BC.Configs
{
    public partial class UserPermissionMappingConfigForm : RestoreForm
    {
        public UserPermissionMappingConfigForm(RestoreEndPoint endPoint, WebBrowser webBrowser)
            : base(endPoint, webBrowser, FormIDs.UserPermissionMappingConfigModule)
        {
            ResetForm();
        }
        public event EventHandler<RefreshControlDataArgs> OnRefreshControlData;

        private void ResetForm()
        {
            IList<IUser_User_LXUE> userlist = UserService.GetUserList();
            if (userlist.Count > 0)
            {
                this.SelectedUserCode = userlist.ToList()[0].UserCode;
                this.UserPermissionMappingRecords = PermissionService.GetPermissionListByUserCode(this.SelectedUserCode);
            }
            else
            {
                this.SelectedUserCode = null;
                this.UserPermissionMappingRecords = null;
            }
        }
        private void SaveUserPermissionMapping(string[] permissionCodes)
        {
            if (this.SelectedUserCode == null)
                ErrorCodeHelper.Instance.ThrowException_Application(ECS3_Module.ConfigsModule, ECS4_ExceptionCode.NullObject, "请先选择一个用户再添加权限！");

            IList<string> listOld = this.UserPermissionMappingRecords.Select(x => x.PermissionCode).ToList<string>();
            IList<string> listNew = new List<string>(permissionCodes);

            //第一种实现：不区分两个集合的大小，一律遍历新的集合（代码少，易维护；缺点：会多循环几次）
            IList<string> listWait2Add = new List<string>();
            IList<string> listWait2Delete = new List<string>(listOld);

            foreach (string item in listNew)
            {
                if (listOld.Contains(item))
                    listWait2Delete.Remove(item);
                else
                    listWait2Add.Add(item);
            }

            //第二种实现：区分两个集合的大小，遍历最小的集合（优点：当数据量特别大时，效率较高；缺点：与一相比较代码较多，不易维护）
            //IList<string> listWait2Add = new List<string>();
            //IList<string> listWait2Delete = new List<string>();
            //if (listNew.Count < listOld.Count)
            //{
            //    listWait2Delete = new List<string>(listOld);
            //    foreach (string item in listNew)
            //    {
            //        if (listOld.Contains(item))
            //            listWait2Delete.Remove(item);
            //        else
            //            listWait2Add.Add(item);
            //    }
            //}
	    // else
            //{
            //    listWait2Add = new List<string>(listNew);
            //    foreach (string item in listOld)
            //    {
            //        if (listNew.Contains(item))
            //            listWait2Add.Remove(item);
            //        else
            //            listWait2Delete.Add(item);
            //    }
            //}

           
        protected override void Request(FormRequest e)
        {
            switch (e.Title)
            {
                case UserPermissionMappingConfigItemNames.UserRecords:
                    this.Response(UserPermissionMappingConfigItemNames.UserRecords, UserService.GetUserList());
                    break;
                case UserPermissionMappingConfigItemNames.PermissionRecords:
                    this.Response(UserPermissionMappingConfigItemNames.PermissionRecords, PermissionService.GetPermissionList());
                    break;
                case UserPermissionMappingConfigItemNames.UserPermissionMappingRecords:
                    this.Response(UserPermissionMappingConfigItemNames.UserPermissionMappingRecords, this.UserPermissionMappingRecords);
                    break;
                case UserPermissionMappingConfigItemNames.Save:
                    {
                        SaveUserPermissionMapping((string[])e.Data);
                        NotifyUserPermissionMappingConfigChanged();
                        break;
                    }
                case UserPermissionMappingConfigItemNames.SelectedUserCode:
                    this.Response(e.Title, this.GetFieldValue(e.Title));
                    break;
                default:
                    break;
            }
        }

        protected override object DecodeReportArgs(FERequest e)
        {
            switch (e.Title)
            {
                case UserPermissionMappingConfigItemNames.SelectedUserCode:
                    return e.Arguments[0] as string;
                default:
                    ErrorCodeHelper.Instance.ThrowException_Application(ECS3_Module.ConfigsModule, ECS4_ExceptionCode.InvalidRecord, "DecodeReportArgs failed.");
                    return null;
            }
        }
        protected override void Report(FormRequest e)
        {
            switch (e.Title)
            {
                case UserPermissionMappingConfigItemNames.SelectedUserCode:
                    this.SetFieldValue(e.Title, e.Data);
                    break;
            }
        }

        protected override object[] EncodeResponseArgs(FormResponse e)
        {
            switch (e.Title)
            {
                case UserPermissionMappingConfigItemNames.PermissionRecords:
                    return this.Array2Json((IList<IUser_Permission_LXUE>)e.Data);
                case UserPermissionMappingConfigItemNames.UserPermissionMappingRecords:
                    return this.Array2Json((IList<IUser_Permission_LXUE>)e.Data);
                case UserPermissionMappingConfigItemNames.UserRecords:
                    return this.Array2Json((IList<IUser_User_LXUE>)e.Data);

                case UserPermissionMappingConfigItemNames.OperateUserCode:
                    return this.Object2Json(e.Data);
                default:
                    return this.Object2Json(e.Data);
            }
        }

