using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient
{
    [Serializable]
    public class MessageViewModel
    {
        public int MessageID { get; set; }
        public string SourceID { get; set; }
        public string DestinationID { get; set; }
        public string Content { get; set; }

        public bool Equals(MessageViewModel other)
        {
            return other.MessageID == MessageID;
        }

        public override int GetHashCode()
        {
            return MessageID.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MessageViewModel)obj);
        }
    }

}
