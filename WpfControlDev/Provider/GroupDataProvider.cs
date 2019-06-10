using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfControlDev.Model;

namespace WpfControlDev.Provider
{
    public class GroupDataProvider
    {
        public static List<CSData> GetGroupData()
        {
            List<CSData> datas = new List<CSData>();
            int cc = 10;
            List<CCompany> Companys = new List<CCompany>();
            for(int i = 0; i < cc; i++)
            {
                CCompany cmp = new CCompany() { CompanyID = "CID" + (i + 1), CompanyName = "CNAME" + (i + 1) };
                Companys.Add(cmp);
            }
            int dd = 100;
            List<CDepartment> dps = new List<CDepartment>();
            for (int i = 0; i < dd; i++)
            {
                CDepartment dp = new CDepartment() { CompanyID = Companys.ElementAt(i % cc).CompanyID, DepartmentID = "DPID" + (i + 1), DepartmentName = "DPNAME" + (i + 1) };
                dps.Add(dp);
            }
            int pp = 1500;
            List<CPersion> ps = new List<CPersion>();
            for (int i = 0; i < pp; i++)
            {
                CPersion p = new CPersion() { CompanyID = dps.ElementAt(i % dd).CompanyID, DepartmentID = dps.ElementAt(i%dd).DepartmentID, PersionName = "PName" + (i + 1) };
                ps.Add(p);
            }
            int j = 0;
            foreach(var p in ps)
            {
                CSData d1 = new CSData() { CompanyID = p.CompanyID, DepartmentID = p.DepartmentID, PersionID = p.PersionID, Say = "hello"+(j+1) };
                datas.Add(d1);
                j++;
            }
            return datas;
        }


    }
}
