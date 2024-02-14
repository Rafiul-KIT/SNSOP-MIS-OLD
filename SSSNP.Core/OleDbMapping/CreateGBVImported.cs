using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.OleDbMapping
{
    public class CreateGBVImported
    {
        private List<GBVMain> _main;
        private List<GBVDetail> _detail;
        private int _userID;

        public CreateGBVImported(List<GBVMain> main, List<GBVDetail> detail, int userID)
        {
            this._main = main;
            this._detail = detail;
            this._userID = userID;
        }

        public int CreateGBV()
        {
            int result = 0;
            int newID = 0;

            foreach (GBVMain m in _main)
            {
                newID = 0;
                m.CreationUserID = _userID;
                m.LastModificationUserID = null;
                m.LastModificationTimestamp = null;

                newID = SSSNPContext.SSSNPManager.CreateGBVMain(m);

                if (newID != 0)
                {
                    GBVDetail d = this._detail.Single(x => x.GBVMainID == m.GBVMainID);
                    if (d != null && d.GBVMainID != 0)
                    {
                        d.GBVMainID = newID;
                        d.CreationUserID = _userID;
                        d.LastModificationTimestamp = null;
                        d.LastModificationUserID = null;

                        SSSNPContext.SSSNPManager.CreateGBVDetail(d);
                        result += 1;
                    }

                }
            }

            return result;

        }
    }
}
