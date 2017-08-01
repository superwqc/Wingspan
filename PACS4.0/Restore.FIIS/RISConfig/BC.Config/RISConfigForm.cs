using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Restore.Dicom.Service;
using Restore.FIIS.BC.Configs.Common;
using Restore.FIIS.BLL;
using Restore.Contracts.Enums;
using Restore.FIIS.Entities;
using Restore.Utilities;
using Restore.Utilities.Configs;
using Restore.FIIS.BLL.CreateIniMgr;

namespace Restore.FIIS.BC.Configs
{
    public class RISConfigForm : RestoreForm
    {
        public event EventHandler<RefreshControlDataArgs> OnRefreshControlData;

        private PACSGateway_CreateIniEntity m_PACSGatewayIniCls = null;
        private HISGateway_CreateIniEntity m_HISGatewayIniCls = null;
        private PatientIDGenerator_CreateIniEntity m_PatientIDGeneratorIniCls = null;
        private AccessionNOGenerator_CreateIniEntity m_AccessionNOGeneratorIniCls = null;
        private DataPrinter_CreateIniEntity m_DataPrinterIniCls = null;

        public RISConfigForm(RestoreEndPoint endPoint, WebBrowser webBrowser)
            : base(endPoint, webBrowser, FormIDs.RISConfigModule)
        {
            this.m_PACSGatewayIniCls = new PACSGateway_CreateIniEntity();
            this.m_HISGatewayIniCls = new HISGateway_CreateIniEntity();
            this.m_PatientIDGeneratorIniCls = new PatientIDGenerator_CreateIniEntity();
            this.m_AccessionNOGeneratorIniCls = new AccessionNOGenerator_CreateIniEntity();
            this.m_DataPrinterIniCls = new DataPrinter_CreateIniEntity();
        }

        #region 定义属性
        public string OperateUserCode
        {
            get { return (string)this.GetFieldValue(RISConfigItemNames.OperateUserCode); }
            set { this.SetFieldValue(RISConfigItemNames.OperateUserCode, value); }
        }

        public bool IsSimpleMode
        {
            get { return (bool)this.GetFieldValue(RISConfigItemNames.IsSimpleMode); }
            set { this.SetFieldValue(RISConfigItemNames.IsSimpleMode, value); }
        }

        public string SimpleModeKey
        {
            get { return (string)this.GetFieldValue(RISConfigItemNames.SimpleModeKey); }
            set { this.SetFieldValue(RISConfigItemNames.SimpleModeKey, value); }
        }

        public string SelectedPKID
        {
            get { return (string)this.GetFieldValue(RISConfigItemNames.SelectedPKID); }
            set { this.SetFieldValue(RISConfigItemNames.SelectedPKID, value); }
        }

        public bool SimpleModeValue
        {
            get { return (bool)this.GetFieldValue(RISConfigItemNames.SimpleModeValue); }
            set { this.SetFieldValue(RISConfigItemNames.SimpleModeValue, value); }
        }

        public IList<ModeUI> SimpleModeRecords
        {
            get { return (IList<ModeUI>)this.GetFieldValue(RISConfigItemNames.SimpleModeRecords); }
            set { this.SetFieldValue(RISConfigItemNames.SimpleModeRecords, value); }
        }
        public double ExamLockValidTimeSpan
        {
            get { return (double)this.GetFieldValue(RISConfigItemNames.ExamLockValidTimeSpan); }
            set { this.SetFieldValue(RISConfigItemNames.ExamLockValidTimeSpan, value); }
        }
        public double FromPrintedToRejectedValidTimeSpan
        {
            get { return (double)this.GetFieldValue(RISConfigItemNames.FromPrintedToRejectedValidTimeSpan); }
            set { this.SetFieldValue(RISConfigItemNames.FromPrintedToRejectedValidTimeSpan, value); }
        }

        #region 值班时间段 + 值班审核医生
        public DateTime? OnDutyTimeBegin
        {
            get { return (DateTime?)this.GetFieldValue(RISConfigItemNames.OnDutyTimeBegin); }
            set { this.SetFieldValue(RISConfigItemNames.OnDutyTimeBegin, value); }
        }
        public DateTime? OnDutyTimeEnd
        {
            get { return (DateTime?)this.GetFieldValue(RISConfigItemNames.OnDutyTimeEnd); }
            set { this.SetFieldValue(RISConfigItemNames.OnDutyTimeEnd, value); }
        }
        public string OnDutyReviewUserCode
        {
            get { return (string)this.GetFieldValue(RISConfigItemNames.OnDutyReviewUserCode); }
            set { this.SetFieldValue(RISConfigItemNames.OnDutyReviewUserCode, value); }
        }
        #endregion

        public string AETitleByWorkStation
        {
            get { return (string)this.GetFieldValue(RISConfigItemNames.AETitleByWorkStation); }
            set { this.SetFieldValue(RISConfigItemNames.AETitleByWorkStation, value); }
        }
        public string PACSGatewayImplement
        {
            get { return (string)this.GetFieldValue(RISConfigItemNames.PACSGatewayImplement); }
            set { this.SetFieldValue(RISConfigItemNames.PACSGatewayImplement, value); }
        }
        public string HISGatewayImplement
        {
            get { return (string)this.GetFieldValue(RISConfigItemNames.HISGatewayImplement); }
            set { this.SetFieldValue(RISConfigItemNames.HISGatewayImplement, value); }
        }
        public string PatientIDGeneratorImplement
        {
            get { return (string)this.GetFieldValue(RISConfigItemNames.PatientIDGeneratorImplement); }
            set { this.SetFieldValue(RISConfigItemNames.PatientIDGeneratorImplement, value); }
        }
        public string AccessionNOGeneratorImplement
        {
            get { return (string)this.GetFieldValue(RISConfigItemNames.AccessionNOGeneratorImplement); }
            set { this.SetFieldValue(RISConfigItemNames.AccessionNOGeneratorImplement, value); }
        }
        public string DataPrinterImplement
        {
            get { return (string)this.GetFieldValue(RISConfigItemNames.DataPrinterImplement); }
            set { this.SetFieldValue(RISConfigItemNames.DataPrinterImplement, value); }
        }
        public string PACSGatewayUrl
        {
            get { return (string)this.GetFieldValue(RISConfigItemNames.PACSGatewayUrl); }
            set { this.SetFieldValue(RISConfigItemNames.PACSGatewayUrl, value); }
        }
        #endregion

        protected override void Request(FormRequest e)
        {
            switch (e.Title)
            {
                case RISConfigItemNames.UserList:
                    {
                        var list = new List<IUser_User_LXUE>(UserService.GetUserList());
                        list.Insert(0, new User_User_LXUE() { UserCode = string.Empty, UserName = string.Empty });
                        this.Response(e.Title, list);
                    }
                    break;

                case RISConfigItemNames.SimpleModeValue:
                    this.SimpleModeValue = "true".Equals(RISConfigService.GetIsSimpleMode(this.SimpleModeKey));
                    this.Response(e.Title, this.SimpleModeValue);
                    break;

                case RISConfigItemNames.SimpleModeRecords:
                    ReloadModeRecords();
                    break;
                case RISConfigItemNames.SelectedPKID:
                    this.Response(e.Title, this.GetFieldValue(e.Title));
                    break;



                //模式保存
                case RISConfigItemNames.SaveSimpleMode:
                    this.SaveSimpleMode();
                    ReloadModeRecords();
                    break;

                //删除
                case RISConfigItemNames.DeleteSimpleMode:
                    this.DeleteMode(this.SelectedPKID);
                    ReloadModeRecords();
                    this.SimpleModeKey = null;
                    break;

                case RISConfigItemNames.ExamLockValidTimeSpan:
                    this.ExamLockValidTimeSpan = RISConfigService.GetExamLockValidTimeSpan();
                    this.Response(e.Title, this.ExamLockValidTimeSpan);
                    break;
                case RISConfigItemNames.FromPrintedToRejectedValidTimeSpan:
                    this.FromPrintedToRejectedValidTimeSpan = RISConfigService.GetFromPrintedToRejectedValidTimeSpan();
                    this.Response(e.Title, this.FromPrintedToRejectedValidTimeSpan);
                    break;

                case RISConfigItemNames.OnDutyTimeBegin:
                    this.OnDutyTimeBegin = RISConfigService.GetOnDutyTimeBegin();
                    this.Response(e.Title, this.OnDutyTimeBegin);
                    break;
                case RISConfigItemNames.OnDutyTimeEnd:
                    this.OnDutyTimeEnd = RISConfigService.GetOnDutyTimeEnd();
                    this.Response(e.Title, this.OnDutyTimeEnd);
                    break;
                case RISConfigItemNames.OnDutyReviewUserCode:
                    this.OnDutyReviewUserCode = RISConfigService.GetOnDutyReviewUserCode();
                    this.Response(e.Title, this.OnDutyReviewUserCode);
                    break;

                case RISConfigItemNames.PACSGatewayImplementList:
                    this.Response(e.Title, this.m_PACSGatewayIniCls.OptionItems);
                    break;
                case RISConfigItemNames.HISGatewayImplementList:
                    this.Response(e.Title, this.m_HISGatewayIniCls.OptionItems);
                    break;
                case RISConfigItemNames.PatientIDGeneratorImplementList:
                    this.Response(e.Title, this.m_PatientIDGeneratorIniCls.OptionItems);
                    break;
                case RISConfigItemNames.AccessionNOGeneratorImplementList:
                    this.Response(e.Title, this.m_AccessionNOGeneratorIniCls.OptionItems);
                    break;
                case RISConfigItemNames.DataPrinterImplementList:
                    this.Response(e.Title, this.m_DataPrinterIniCls.OptionItems);
                    break;

                case RISConfigItemNames.PACSGatewayImplement:
                    this.PACSGatewayImplement = RISConfigService.GetPACSGatewayImplement();
                    this.Response(e.Title, this.PACSGatewayImplement);
                    break;
                case RISConfigItemNames.HISGatewayImplement:
                    this.HISGatewayImplement = RISConfigService.GetHISGatewayImplement();
                    this.Response(e.Title, this.HISGatewayImplement);
                    break;
                case RISConfigItemNames.PatientIDGeneratorImplement:
                    this.PatientIDGeneratorImplement = RISConfigService.GetPatientIDGeneratorImplement();
                    this.Response(e.Title, this.PatientIDGeneratorImplement);
                    break;
                case RISConfigItemNames.AccessionNOGeneratorImplement:
                    this.AccessionNOGeneratorImplement = RISConfigService.GetAccessionNOGeneratorImplement();
                    this.Response(e.Title, this.AccessionNOGeneratorImplement);
                    break;
                case RISConfigItemNames.DataPrinterImplement:
                    this.DataPrinterImplement = RISConfigService.GetDataPrinterImplement();
                    this.Response(e.Title, this.DataPrinterImplement);
                    break;
                case RISConfigItemNames.PACSGatewayUrl:
                    this.PACSGatewayUrl = PACSGatewayConfig.Instance.PACSGatewayUrl;
                    this.Response(e.Title, this.PACSGatewayUrl);
                    break;
                case RISConfigItemNames.SaveRISConfig:
                    this.SaveRISConfig();
                    break;

                case RISConfigItemNames.AETitleListByWorkStation:
                    this.Response(e.Title, DeviceInfoService.GetDeviceInfoList(DeviceType.WorkStation));
                    break;
                case RISConfigItemNames.AETitleByWorkStation:
                    this.AETitleByWorkStation = DcmServiceConfig.Instance.StorageSCUAETitle;
                    this.Response(e.Title, this.AETitleByWorkStation);
                    break;
                case RISConfigItemNames.SaveDcmConfig:
                    this.SaveDcmConfig();
                    break;
               
            }
        }
        protected override object DecodeReportArgs(FERequest e)
        {
            if (e.Arguments == null || e.Arguments.Length == 0) return null;
            switch (e.Title)
            {
                case RISConfigItemNames.IsSimpleMode:
                    return (bool)e.Arguments[0];

                case RISConfigItemNames.ExamLockValidTimeSpan:
                case RISConfigItemNames.FromPrintedToRejectedValidTimeSpan:
                    {
                        double outArg;
                        if (!double.TryParse(e.Arguments[0].ToString(), out outArg))
                            ErrorCodeHelper.Instance.ThrowException_Application(ECS3_Module.ConfigsModule, ECS4_ExceptionCode.InvalidRecord, string.Format("DecodeReportArgs failed. Title = '{0}' , Arguments = '{1}'", e.Title, e.Arguments[0]));
                        return outArg;
                    }
                case RISConfigItemNames.SimpleModeKey:
                    return e.Arguments[0] as string;
                case RISConfigItemNames.SimpleModeValue:
                    return (bool)e.Arguments[0];

                case RISConfigItemNames.OnDutyTimeBegin:
                case RISConfigItemNames.OnDutyTimeEnd:
                    {
                        DateTime outDateTime;
                        if (DateTime.TryParse(e.Arguments[0].ToString(), out outDateTime)) return outDateTime;
                        return null;
                    }
                case RISConfigItemNames.OnDutyReviewUserCode: return e.Arguments[0] as string;

                case RISConfigItemNames.PACSGatewayImplement:
                case RISConfigItemNames.HISGatewayImplement:
                case RISConfigItemNames.PatientIDGeneratorImplement:
                case RISConfigItemNames.AccessionNOGeneratorImplement:
                case RISConfigItemNames.DataPrinterImplement:
                case RISConfigItemNames.AETitleByWorkStation:
                case RISConfigItemNames.PACSGatewayUrl:
                    return e.Arguments[0] as string;
                case RISConfigItemNames.SelectedPKID:
                    return e.Arguments[0] as string;
                default:
                    ErrorCodeHelper.Instance.ThrowException_Application(ECS3_Module.ConfigsModule, ECS4_ExceptionCode.InvalidRecord, string.Format("DecodeReportArgs failed. Title = '{0}' , Arguments = '{1}'", e.Title, e.Arguments[0]));
                    return null;
            }
        }
        protected override void Report(FormRequest e)
        {
            switch (e.Title)
            {
                case RISConfigItemNames.IsSimpleMode:
                    this.IsSimpleMode = (bool)e.Data;
                    break;
                case RISConfigItemNames.ExamLockValidTimeSpan:
                    this.ExamLockValidTimeSpan = (double)e.Data;
                    break;


                case RISConfigItemNames.SimpleModeKey:
                    this.SetFieldValue(e.Title,e.Data);
                    break;
                case RISConfigItemNames.SimpleModeValue:
                    this.SetFieldValue(e.Title, e.Data);
                    break;


                case RISConfigItemNames.FromPrintedToRejectedValidTimeSpan:
                    this.FromPrintedToRejectedValidTimeSpan = (double)e.Data;
                    break;

                case RISConfigItemNames.OnDutyTimeBegin:
                    this.OnDutyTimeBegin = (DateTime?)e.Data;
                    break;
                case RISConfigItemNames.OnDutyTimeEnd:
                    this.OnDutyTimeEnd = (DateTime?)e.Data;
                    break;
                case RISConfigItemNames.OnDutyReviewUserCode:
                    this.OnDutyReviewUserCode = (string)e.Data;
                    break;

                case RISConfigItemNames.PACSGatewayImplement:
                case RISConfigItemNames.HISGatewayImplement:
                case RISConfigItemNames.PatientIDGeneratorImplement:
                case RISConfigItemNames.AccessionNOGeneratorImplement:
                case RISConfigItemNames.DataPrinterImplement:
                case RISConfigItemNames.AETitleByWorkStation:
                case RISConfigItemNames.PACSGatewayUrl:
                    this.SetFieldValue(e.Title, e.Data);
                    break;
                case RISConfigItemNames.SelectedPKID:
                    this.SetFieldValue(e.Title, e.Data);
                    this.SimpleModeKey = this.SelectedPKID;
                    this.Response(RISConfigItemNames.SimpleModeKey, this.SelectedPKID);
                    break;
                
            }
        }

        protected override void CheckFieldValue(string fieldName, object fieldValue)
        {
        }
 
        private void DeleteMode(string keyID) {
            RISConfigService.DeleteSimpleMode(this.OperateUserCode,keyID);
        }
        private void SaveRISConfig()
        {
            RISConfigService.SetExamLockValidTimeSpan(this.OperateUserCode, this.ExamLockValidTimeSpan);
            RISConfigService.SetFromPrintedToRejectedValidTimeSpan(this.OperateUserCode, this.FromPrintedToRejectedValidTimeSpan);

            RISConfigService.SetOnDutyTimeBegin(this.OperateUserCode, this.OnDutyTimeBegin);
            RISConfigService.SetOnDutyTimeEnd(this.OperateUserCode, this.OnDutyTimeEnd);
            RISConfigService.SetOnDutyReviewUserCode(this.OperateUserCode, this.OnDutyReviewUserCode);

            RISConfigService.SetPACSGatewayImplement(this.OperateUserCode, this.PACSGatewayImplement);
            RISConfigService.SetHISGatewayImplement(this.OperateUserCode, this.HISGatewayImplement);
            RISConfigService.SetPatientIDGeneratorImplement(this.OperateUserCode, this.PatientIDGeneratorImplement);
            RISConfigService.SetAccessionNOGeneratorImplement(this.OperateUserCode, this.AccessionNOGeneratorImplement);
            RISConfigService.SetDataPrinterImplement(this.OperateUserCode, this.DataPrinterImplement);

            PACSGatewayConfig.Instance.PACSGatewayUrl = this.PACSGatewayUrl;
        }


        //保存
        private void SaveSimpleMode()
        {
            RISConfigService.SetIsSimpleMode(this.OperateUserCode, this.SimpleModeKey, this.SimpleModeValue);
        }

        private void ReloadModeRecords()
        {
            IList<ISys_Configure_LXUE> list = BLL.SysConfigureService.GetAllCreateIni("objects",Mode_Type.BASE_KEY);
            this.SimpleModeRecords = getModeRecords(list);
            this.Response(RISConfigItemNames.SimpleModeRecords, this.SimpleModeRecords);
        }

        private IList<ModeUI> getModeRecords(IList<ISys_Configure_LXUE> entities)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("True","简单模式");
            dict.Add("False", "审核模式");
            List<ModeUI> list = new List<ModeUI>();
            if (null != entities)
            {
                foreach (ISys_Configure_LXUE item in entities)
                {
                    ModeUI simpleIm = new ModeUI();
                  simpleIm.ID = item.ID;
                  simpleIm.KeyCode = item.Key;
                  simpleIm.Description = dict[item.Value];
                  list.Add(simpleIm);
                }
            }
            return list;
        }      
    }
}
