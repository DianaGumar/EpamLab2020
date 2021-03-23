using System.Collections.Generic;
using TicketManagement.Domain.DTO;

namespace TicketManagement.BusinessLogic.Standart.IServices
{
    public interface IPurchaceService
    {
        List<PurchaseHistoryDto> GetPurchaseHistory(string userId);

        PurchaseStatus BuyTicket(string userId, int tmeventSeatId);

        PurchaseStatus BuyTicket(string userId, params int[] tmeventSeatId);

        PurchaseStatus ReturnTicket(string userId, int tmeventSeatId);
    }
}
