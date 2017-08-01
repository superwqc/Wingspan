using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Restore.FIIS.Entities;
using Restore.FIIS.BLL.CreateIniMgr;
using Restore.Utilities.CreateIniMgr;
using Restore.FIIS.BLL;

namespace Restore.FIIS.BC.Configs
{
    [Serializable]
    public class UICAImplement
    {
        public string KeyCode { get; set; }
        public string Description { get; set; }

        public static IList<UICAImplement> FromEntities(IList<ISys_Configure_LXUE> entities)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var item in new CAImpl_CreateIniEntity().OptionItems)
            {
                dict.Add(item.KeyCode, item.KeyName);
            }

            List<UICAImplement> list = new List<UICAImplement>();
            if (null != entities)
            {
                foreach (ISys_Configure_LXUE item in entities)
                {
                    if (dict.ContainsKey(item.Value))
                    {
                        list.Add(new UICAImplement()
                        {
                            KeyCode = item.Key,
                            Description = dict[item.Value],
                        });
                    }
                }
            }
            return list.AsReadOnly();
        }
    }

    public class ModeUI
    {
        public int ID { get; set; }
        public string KeyCode { get; set; }
        public string Description { get; set; }
    }
}
