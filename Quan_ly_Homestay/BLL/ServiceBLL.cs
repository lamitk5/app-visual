using System.Data;
using Quan_ly_Homestay.DAL;

namespace Quan_ly_Homestay.BLL
{
    public class ServiceBLL
    {
        private readonly ServiceDAL serviceDAL;

        public ServiceBLL()
        {
            serviceDAL = new ServiceDAL();
        }

        public DataTable LayDanhSachDichVu()
        {
            return serviceDAL.GetAllAsDataTable();
        }
    }
}
