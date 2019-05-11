using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient
{
    [Serializable]
    public class UserViewModel
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public List<UserViewModel> OnlineFriends { get; set; }
        public List<UserViewModel> OfflineFriends { get; set; }

        public UserViewModel()
        {
            OnlineFriends = new List<UserViewModel>();
            OfflineFriends = new List<UserViewModel>();
        }

        protected bool Equals(UserViewModel other)
        {
            return UserID == other.UserID;
        }

        public override int GetHashCode()
        {
            return UserID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserViewModel)obj);
        }
    }
}
