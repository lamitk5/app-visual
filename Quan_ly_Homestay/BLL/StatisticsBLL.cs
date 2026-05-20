using System;
using System.Data;
using Quan_ly_Homestay.DAL;

namespace Quan_ly_Homestay.BLL
{
    public class StatisticsBLL
    {
        private readonly StatisticsDAL statisticsDAL;

        public StatisticsBLL()
        {
            statisticsDAL = new StatisticsDAL();
        }

        public DataTable GetDailyRevenue(DateTime fromDate, DateTime toDate)
        {
            return statisticsDAL.GetDailyRevenue(fromDate, toDate);
        }

        public DataTable GetRevenueByHomestay(DateTime fromDate, DateTime toDate)
        {
            return statisticsDAL.GetRevenueByHomestay(fromDate, toDate);
        }
    }
}
