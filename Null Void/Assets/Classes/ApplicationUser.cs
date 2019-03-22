using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scenes.Default.Classes
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser
    {
        #region Newly-added properties for a user/player.                    
        public int Wins { get; set; }
        public int Losses { get; set; }

        public string UserName { get; set; }

        //public ICollection<PlayerMessage> SentMessages { get; set; }
        #endregion
    }
}