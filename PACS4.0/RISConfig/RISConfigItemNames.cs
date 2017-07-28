using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Restore.Utilities;

namespace Restore.FIIS.BC.Configs.Common
{
    internal class RISConfigItemNames
    {
        public const string OperateUserCode = "OperateUserCode";
        public const string UserList = "UserList";

        public const string IsSimpleMode = "IsSimpleMode";


        public const string ExamLockValidTimeSpan = "ExamLockValidTimeSpan";
        public const string FromPrintedToRejectedValidTimeSpan = "FromPrintedToRejectedValidTimeSpan";

        public const string OnDutyTimeBegin = "OnDutyTimeBegin";
        public const string OnDutyTimeEnd = "OnDutyTimeEnd";
        public const string OnDutyReviewUserCode = "OnDutyReviewUserCode";

        public const string PACSGatewayImplement = "PACSGatewayImplement";
        public const string PACSGatewayImplementList = "PACSGatewayImplementList";

        public const string HISGatewayImplement = "HISGatewayImplement";
        public const string HISGatewayImplementList = "HISGatewayImplementList";

        public const string PatientIDGeneratorImplement = "PatientIDGeneratorImplement";
        public const string PatientIDGeneratorImplementList = "PatientIDGeneratorImplementList";

        public const string AccessionNOGeneratorImplement = "AccessionNOGeneratorImplement";
        public const string AccessionNOGeneratorImplementList = "AccessionNOGeneratorImplementList";

        public const string DataPrinterImplement = "DataPrinterImplement";
        public const string DataPrinterImplementList = "DataPrinterImplementList";

        public const string SaveRISConfig = "SaveRISConfig";

        public const string AETitleByWorkStation = "AETitleByWorkStation";
        public const string AETitleListByWorkStation = "AETitleListByWorkStation";

        public const string SaveDcmConfig = "SaveDcmConfig";

        public const string PACSGatewayUrl = "PACSGatewayUrl";
    }
}
