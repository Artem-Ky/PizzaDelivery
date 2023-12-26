using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IRepositoryReport
    {
        List<SPResult> ReportOrdersByType(int type);
        List<SPResult> ExecuteSP(int type, DateTime date);

        List<DataHistory> orderHistory(int customerId);

    }
}
