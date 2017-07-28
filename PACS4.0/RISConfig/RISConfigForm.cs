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
       
        public bool IsSimpleMode
        {
            get { return (bool)this.GetFieldValue(RISConfigItemNames.IsSimpleMode); }
            set { this.SetFieldValue(RISConfigItemNames.IsSimpleMode, value); }
        }

        protected override void Request(FormRequest e)
        {
            switch (e.Title)
            {    
                case RISConfigItemNames.IsSimpleMode:
                    this.IsSimpleMode = RISConfigService.GetIsSimpleMode();
                    this.Response(e.Title, this.IsSimpleMode);
                    break;
 
                case RISConfigItemNames.SaveRISConfig:
                    this.SaveRISConfig();
                    break;
            }
        }
  
        private void SaveRISConfig()
        {
            RISConfigService.SetIsSimpleMode(this.OperateUserCode, this.IsSimpleMode);      
        }
        private void SaveDcmConfig()
        {
          
        }
    }
}
