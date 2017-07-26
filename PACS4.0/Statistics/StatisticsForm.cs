using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Restore.FIIS.BLL;
using Restore.Utilities;

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
            get { return (string)this.GetFieldValue(StatisticsTitles.OperateUserCode); }
            set { this.SetFieldValue(StatisticsTitles.OperateUserCode, value); }
        }
        private Dictionary<string, object> m_Result;

        #region 重写父类方法，个性化消息处理
        protected override void OnFieldValueChanged(DataDepositoryEventArgs e)
        {
            base.OnFieldValueChanged(e);
            switch (e.FieldName)
            {
                case StatisticsTitles.CurrModalityCode:
                    if (string.IsNullOrEmpty(this.CurrModalityCode))
                        this.BodyPartList = UIBodyPart.FromEntities(BodyPartService.GetBodyPartList());
                    else
                        this.BodyPartList = UIBodyPart.FromEntities(BodyPartService.GetBodyPartList(this.CurrModalityCode));
                    break;
                default:
                    break;
            }
        }
        protected override object DecodeRequestArgs(FERequest e)
        {
            switch (e.Title)
            {
                case StatisticsTitles.ExportStatisticReport:
                    return (UIRows)this.Json2Object(e.Arguments[0] as string, typeof(UIRows));
                case StatisticsTitles.PatientSourceList:
                    return null;
                case StatisticsTitles.ModalityList:
                    return null;
                case StatisticsTitles.BodyPartList:
                    return null;
                case StatisticsTitles.SexList:
                    return null;
                case StatisticsTitles.ExamTypeList:
                    return null;
                case StatisticsTitles.DepartmentList:
                    return null;
                case StatisticsTitles.ReportUserList:
                    return null;
                case StatisticsTitles.ReviewUserList:
                    return null;
                case StatisticsTitles.CurrModalityCode:
                    return null;
                case StatisticsTitles.Statistics:
                    return (UIStatisticsParam)this.Json2Object(e.Arguments[0] as string, typeof(UIStatisticsParam));
                case StatisticsTitles.OperateUserCode:
                    return null;
                default:
                    ErrorCodeHelper.Instance.ThrowException_Application(ECS3_Module.StatisticsModule, ECS4_ExceptionCode.Unsupported, string.Format("'{0}' unsupported", e.Title));
                    return null;
            }
        }
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
                case StatisticsTitles.ExportStatisticReport:
                    this.DownloadExcel(e.Data as UIRows);
                    break;
                case StatisticsTitles.Statistics:
                    {
                        UIStatisticsParam param = e.Data as UIStatisticsParam;
                        m_Result = Restore.FIIS.BLL.Statistic.StatisticService.GetNormalStaticData(this.OperateUserCode, param.StartTime, param.EndTime, param.Select, param.Group, param.Where, param.Order, param.TableName, param.TimeField);
                        this.Response(param.Name, m_Result);
                    }
                    break;
                case StatisticsTitles.OperateUserCode:
                    this.Response(e.Title, this.OperateUserCode);
                    break;
                default:
                    ErrorCodeHelper.Instance.ThrowException_Application(ECS3_Module.StatisticsModule, ECS4_ExceptionCode.Unsupported, string.Format("'{0}' unsupported", e.Title));
                    break;
            }
        }
        public class UIRows
        {
            public string ExportType { get; set; }
            public ArrayList Columns { get; set; }
            public object Rows { get; set; }
        }
        private string ClearHtml(string Content)
        {
            Content = Zxj_ReplaceHtml("<option>.*</option>", "", Content);
            Content = Zxj_ReplaceHtml("</?option[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?select[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("<li.*>.*</li>", "", Content);
            Content = Zxj_ReplaceHtml("&#[^>]*;", "", Content);
            Content = Zxj_ReplaceHtml("</?marquee[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?object[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?param[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?embed[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?table[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?tr[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?th[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("<p[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</p>", "\r\n", Content);
            Content = Zxj_ReplaceHtml("<br>", "\r\n", Content);
            Content = Zxj_ReplaceHtml("</?a[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?img[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?tbody[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?li[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?span[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?div[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?th[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?td[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?script[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("(javascript|jscript|vbscript|vbs):", "", Content);
            Content = Zxj_ReplaceHtml("on(mouse|exit|error|click|key)", "", Content);
            Content = Zxj_ReplaceHtml("<\\?xml[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("<\\/?[a-z]+:[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?font[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?b[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?u[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?i[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("</?strong[^>]*>", "", Content);
            Content = Zxj_ReplaceHtml("&nbsp;", " ", Content);

            string clearHtml = Content;
            return clearHtml;
        }
        private string Zxj_ReplaceHtml(string patrn, string strRep, string content)
        {
            if (string.IsNullOrEmpty(content)) content = string.Empty;
            Regex rgEx = new Regex(patrn, RegexOptions.IgnoreCase);
            string strTxt = rgEx.Replace(content, strRep);
            return strTxt;
        }
        private void DownloadExcel(UIRows Title)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                IList list = new ArrayList();
                object[] str1 = Title.Rows as object[];
                object[] str2 = null;
                foreach (object o in str1)
                {
                    IList listText = new ArrayList();
                    str2 = o as object[];
                    foreach (object i in str2)
                    {
                        listText.Add((string)i);
                    }
                    list.Add(listText);
                }

                ExcelManager em = null;
                if (list.Count > 0)
                {
                    System.Data.DataTable table = new System.Data.DataTable();

                    for (int j = 0; j < Title.Columns.Count; j++)
                    {
                        table.Columns.Add(Title.Columns[j].ToString(), Type.GetType("System.String"));
                    }
                    DataRow newrowtitle = table.NewRow();
                    for (int j = 0; j < Title.Columns.Count; j++)
                    {
                        newrowtitle[Title.Columns[j].ToString()] = Title.Columns[j].ToString();
                    }
                    table.Rows.Add(newrowtitle);
                    for (int i = 0; i < list.Count; i++)
                    {
                        IList lt = (IList)list[i];
                        DataRow newrow = table.NewRow();
                        for (int j = 0; j < lt.Count; j++)
                        {
                            newrow[Title.Columns[j].ToString()] = ClearHtml(lt[j].ToString());
                        }
                        table.Rows.Add(newrow);
                    }
                    em = new ExcelManager(table);
                    em.SaveNewExcel(fbd.SelectedPath + "\\", Title.ExportType);
                }
            }
        }

        protected override object DecodeReportArgs(FERequest e)
        {
            switch (e.Title)
            {
                case StatisticsTitles.CurrModalityCode:
                    return e.Arguments[0] as string;
                default:
                    ErrorCodeHelper.Instance.ThrowException_Application(ECS3_Module.StatisticsModule, ECS4_ExceptionCode.Unsupported, string.Format("'{0}' unsupported", e.Title));
                    return null;
            }
        }
        protected override void Report(FormRequest e)
        {
            switch (e.Title)
            {
                case StatisticsTitles.CurrModalityCode:
                    this.CurrModalityCode = e.Data as string;
                    break;
                default:
                    ErrorCodeHelper.Instance.ThrowException_Application(ECS3_Module.StatisticsModule, ECS4_ExceptionCode.Unsupported, string.Format("'{0}' unsupported", e.Title));
                    break;
            }
        }
        protected override object[] EncodeResponseArgs(FormResponse e)
        {
            switch (e.Title)
            {
                case StatisticsTitles.PatientSourceList:
                    return this.Array2Json((IList<UIPatientList>)e.Data);
                case StatisticsTitles.ModalityList:
                    return this.Array2Json((IList<UIModality>)e.Data);
                case StatisticsTitles.BodyPartList:
                    return this.Array2Json((IList<UIBodyPart>)e.Data);
                case StatisticsTitles.SexList:
                    return this.Array2Json((IList<UISex>)e.Data);
                case StatisticsTitles.ExamTypeList:
                    return this.Array2Json((IList<UIExamType>)e.Data);
                case StatisticsTitles.DepartmentList:
                    return this.Array2Json((IList<UIDept>)e.Data);
                case StatisticsTitles.ReportUserList:
                    return this.Array2Json((IList<UIUser>)e.Data);
                case StatisticsTitles.ReviewUserList:
                    return this.Array2Json((IList<UIUser>)e.Data);
                case StatisticsTitles.CurrModalityCode:
                    return this.Object2Json(e.Data);
                case StatisticsTitles.OperateUserCode:
                    return this.Object2Json(e.Data);
                default:
                    return this.Object2Json(e.Data);
            }
        }
        protected override void CheckFieldValue(string fieldName, object fieldValue)
        { }
        #endregion
    }
}
