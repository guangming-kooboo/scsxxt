using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wx.book_ticket.Entity;
using wx.book_ticket.Services;
using wx.book_tiket.Interfaces;

namespace wx.book_tiket.Services
{
    public class BookTicketService: BaseRepository,IBookTicketService
    {
        public List<ticket_onsell> AllOnSell()
        {
            var list =  Db.ticket_onsell.ToList();
            return list;
        }
        public ticket_onsell FindTicket(string id)
        {
            return Db.ticket_onsell.Where(a => a.TicketID == id).FirstOrDefault();
        }
        public List<user_ticket> UserTicket(bool flag,string UserId)
        {
            List<user_ticket> list;
            if(flag)
            {
                list = Db.user_ticket.Where(a=>a.State=="未兑换"&&a.UserID==UserId).ToList();
            }
            else
            {
                list = Db.user_ticket.Where(a=>a.State=="已兑换"&&a.UserID==UserId).ToList();
            }
            return list;
        }
    }
}
