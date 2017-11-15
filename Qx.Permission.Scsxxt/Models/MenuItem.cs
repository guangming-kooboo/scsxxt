using System.Collections.Generic;
using Qx.Permission.Scsxxt.Entity;

namespace Qx.Permission.Scsxxt.Models
{
    public class MenuItem
    {
        public bool Selected { get; set; }
        public Menu Father { get; set; }
        public List<Menu> Children { get; set; }
       

      
    }
    
}