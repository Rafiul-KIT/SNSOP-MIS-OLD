using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.OleDbMapping
{
    public class CreateNonGBVImported
    {
        private List<NonGBVMain> _main;
        private List<NonGBVDetail> _detail;
        private int _userID;
        public CreateNonGBVImported(List<NonGBVMain> main, List<NonGBVDetail> detail, int userID)
        {
            this._main = main;
            this._detail = detail;
            this._userID = userID;
        }

        public int CreateNonGBV()
        {
            int result = 0;
            int newID = 0;

            foreach (NonGBVMain m in _main)
            {
                newID = 0;
                m.CreationUserID = _userID;
                m.LastModificationUserID = null;
                m.LastModificationTimestamp = null;

                newID = SSSNPContext.SSSNPManager.CreateNonGBVMain(m);

                if (newID != 0)
                {
                    NonGBVDetail d = this._detail.Single(x => x.NonGBVMainID == m.NonGBVMainID);
                    if (d != null && d.NonGBVMainID != 0)
                    {
                        d.NonGBVMainID = newID;
                        d.CreationUserID = _userID;
                        d.LastModificationTimestamp = null;
                        d.LastModificationUserID = null;

                        SSSNPContext.SSSNPManager.CreateNonGBVDetail(d);
                        result += 1;
                    }
                    
                }
            }

            return result;

        }

    }
}
