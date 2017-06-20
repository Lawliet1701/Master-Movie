using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class User
    {
        public string UserID { get; set; }

        public string UserName { get; set; }

        public Dictionary<int, byte> Ratings { get; set; }

    }
}