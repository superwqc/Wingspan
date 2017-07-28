using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Restore.Utilities;
using System.Windows.Forms;
using Restore.FIIS.BLL;
using Restore.FIIS.BC.Statistics;
using Microsoft.Office.Interop.Excel;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.Text.RegularExpressions;


namespace Restore.FIIS.BC.Statistics
{
    
    public class StatisticsForm : RestoreForm
    {

        public StatisticsForm(RestoreEndPoint endPoint, WebBrowser webBrowser)
            : base(endPoint, webBrowser, FormIDs.StatisticsModule)
        {
            this.InitForm();
        }
        private void InitForm()
        {
            this.CurrModalityCode = LocalConfiguration.Instance.DefaultModalityCode;
            this.SexList = UISex.FromEntities(SexService.GetSexList());
            this.ExamTypeList = UIExamType.FromEntities(ExamTypeService.GetExamTypeList());
            this.ModalityList = UIModality.FromEntities(ModalityService.GetModalityList());
            this.DepartmentList = UIDept.FromEntities(DeptService.GetDeptList());
            this.ReportUserList = UIUser.FromEntities(UserService.GetUserList());
            this.ReviewUserList = UIUser.FromEntities(UserService.GetUserList());
            this.PatientSourceList = UIPatientList.FromEntities(PatientSourceService.GetPatientSourceList());
        }
        public IList<UIPatientList> PatientSourceList
        {
            get { return (IList<UIPatientList>)this.GetFieldValue(StatisticsTitles.PatientSourceList); }
            set { this.SetFieldValue(StatisticsTitles.PatientSourceList, value); }
        }

        public string CurrModalityCode
        {
            get { return (string)this.GetFieldValue(StatisticsTitles.CurrModalityCode); }
            set { this.SetFieldValue(StatisticsTitles.CurrModalityCode, value); }
        }
        public IList<UIBodyPart> BodyPartList
        {
            get { return (IList<UIBodyPart>)this.GetFieldValue(StatisticsTitles.BodyPartList); }
            set { this.SetFieldValue(StatisticsTitles.BodyPartList, value); }
        }
        public IList<UISex> SexList
        {
            get { return (IList<UISex>)this.GetFieldValue(StatisticsTitles.SexList); }
            set { this.SetFieldValue(StatisticsTitles.SexList, value); }
        }
        public IList<UIExamType> ExamTypeList
        {
            get { return (IList<UIExamType>)this.GetFieldValue(StatisticsTitles.ExamTypeList); }
            set { this.SetFieldValue(StatisticsTitles.ExamTypeList, value); }
        }
        public IList<UIModality> ModalityList
        {
            get { return (IList<UIModality>)this.GetFieldValue(StatisticsTitles.ModalityList); }
            set { this.SetFieldValue(StatisticsTitles.ModalityList, value); }
        }
        

        public IList<UIDept> DepartmentList
        {
            get { return (IList<UIDept>)this.GetFieldValue(StatisticsTitles.DepartmentList); }
            set { this.SetFieldValue(StatisticsTitles.DepartmentList, value); }
        }
        public IList<UIUser> ReportUserList
        {
            get { return (IList<UIUser>)this.GetFieldValue(StatisticsTitles.ReportUserList); }
            set { this.SetFieldValue(StatisticsTitles.ReportUserList, value); }
        }
        public IList<UIUser> ReviewUserList
        {
            get { return (IList<UIUser>)this.GetFieldValue(StatisticsTitles.ReviewUserList); }
            set { this.SetFieldValue(StatisticsTitles.ReviewUserList, value); }
        }
        public string OperateUserCode
        {
            get
            {
                return (string)this.GetFieldValue(StatisticsTitles.OperateUserCode);
            }
            set
            {
                this.SetFieldValue(StatisticsTitles.OperateUserCode, value);
            }
        }
        Dictionary<string, object> result;

        
        
        protected override void Request(FormRequest e)
        {
            switch (e.Title)
            {
                case StatisticsTitles.PatientSourceList:
                    this.Response(e.Title, this.PatientSourceList);
                    break;
                case StatisticsTitles.ModalityList:
                    this.Response(e.Title, this.ModalityList);
                    break;
                case StatisticsTitles.BodyPartList:
                    this.Response(e.Title, this.BodyPartList);
                    break;
                case StatisticsTitles.SexList:
                    this.Response(e.Title, this.SexList);
                    break;
                case StatisticsTitles.ExamTypeList:
                    this.Response(e.Title, this.ExamTypeList);
                    break;


                case StatisticsTitles.DepartmentList:
                    this.Response(e.Title, this.DepartmentList);
                    break;
                case StatisticsTitles.ReportUserList:
                    this.Response(e.Title, this.ReportUserList);
                    break;
                case StatisticsTitles.ReviewUserList:
                    this.Response(e.Title, this.ReviewUserList);
                    break;
                case StatisticsTitles.CurrModalityCode:
                    this.Response(e.Title, this.CurrModalityCode);
                    break;

             //导出
                case StatisticsTitles.ExportStatisticReport:
                    DownloadExcel(e.Data as UIRows);
                    break;

                case StatisticsTitles.Statistics:
                    UIStatisticsParam param = e.Data as UIStatisticsParam;
                    result = Restore.FIIS.BLL.Statistic.StatisticService.GetNormalStaticData(this.OperateUserCode, param.StartTime, param.EndTime, param.Select, param.Group, param.Where, param.Order, param.TableName, param.TimeField);
                    this.Response(param.Name, result);

                    break;
                case StatisticsTitles.OperateUserCode:
                    this.Response(e.Title, this.OperateUserCode);
                    break;
                default:
                    ErrorCodeHelper.Instance.ThrowException_Application(ECS3_Module.StatisticsModule, ECS4_ExceptionCode.Unsupported, string.Format("'{0}' unsupported", e.Title));
                    break;
            }

        }
       

        }
        
        protected override void CheckFieldValue(string fieldName, object fieldValue)
        { }
        
    }
}
