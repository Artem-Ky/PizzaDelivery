using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Model;
using DAL.Entities;

namespace Interfaces.Services
{
    public interface IServicesReport
    {
        List<SPResult> ExecuteSP(int type, DateTime date);
        List<SPResult> ReportOrdersByType(int type);
        List<DataHistory> orderHistory(int customerId);
        Dictionary<string, int> ReportBestWorse(ReportBestWorseData data);
        Dictionary<int, int> ReportTime(ReportTimeData data);

    }
}
