using System;
using System.Data;
using Quan_ly_Homestay.DAL;

namespace Quan_ly_Homestay.BLL
{
    public class InvoiceBLL
    {
        private readonly InvoiceDAL invoiceDAL;

        public InvoiceBLL()
        {
            invoiceDAL = new InvoiceDAL();
        }

        public int GetOrCreateTempInvoice(int roomId)
        {
            return invoiceDAL.GetOrCreateTempInvoice(roomId);
        }

        public bool AddServiceToInvoice(int invoiceId, int serviceId, int quantity, decimal unitPrice)
        {
            if (quantity <= 0)
                throw new ArgumentException("Số lượng phải lớn hơn 0!");
            if (unitPrice < 0)
                throw new ArgumentException("Đơn giá không được âm!");

            return invoiceDAL.AddInvoiceDetail(invoiceId, serviceId, quantity, unitPrice);
        }

        public bool HuyDichVu(int detailId)
        {
            return invoiceDAL.DeleteInvoiceDetail(detailId);
        }

        public DataTable GetActiveServicesByRoom(int roomId)
        {
            return invoiceDAL.GetActiveServicesByRoom(roomId);
        }

        public (int days, decimal roomAmount) CalculateRoomAmount(DateTime checkInDate, decimal roomPricePerDay)
        {
            int days = (DateTime.Now - checkInDate).Days;
            if (days < 1) days = 1;

            return (days, days * roomPricePerDay);
        }

        public bool ProcessCheckout(int bookingId, decimal roomAmount, decimal serviceAmount, decimal deposit)
        {
            decimal totalAmount = roomAmount + serviceAmount - deposit;
            if (totalAmount < 0) totalAmount = 0;

            return invoiceDAL.ProcessCheckout(bookingId, totalAmount);
        }

        public DataTable GetActiveBookingByRoomId(int roomId)
        {
            return invoiceDAL.GetActiveBookingByRoomId(roomId);
        }
    }
}
