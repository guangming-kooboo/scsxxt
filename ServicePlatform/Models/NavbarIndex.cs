using System.Collections.Generic;

namespace ServicePlatform.Models
{
    public class NavbarIndex
    {
        public static NavbarIndex Init(IEnumerable<Qx.Permission.Scsxxt.Models.Navbar> navbars,
             IEnumerable<Qx.Permission.Scsxxt.Entity.Button> buttons)
        {
            return new NavbarIndex()
            {
                Navbars = navbars,
                Buttons = buttons
            };
        }
        public IEnumerable<Qx.Permission.Scsxxt.Models.Navbar> Navbars;
        public IEnumerable<Qx.Permission.Scsxxt.Entity.Button> Buttons;
    }
}