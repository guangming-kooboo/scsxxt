using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wx.book_ticket.Entity;

namespace wx.book_tiket.Interfaces
{
    public interface IBookTicketService
    {
        List<ticket_onsell> AllOnSell();
        ticket_onsell FindTicket(string id);
        List<user_ticket> UserTicket(bool flag, string UserId);
    }
}
