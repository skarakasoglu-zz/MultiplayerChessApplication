using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ChessClient
{
    [Serializable]
    public class ClientServerCommunication
    {

        public object Content { get; set; }
        public CommunicationType Type {
            get
            {
                return _type;
            }
        }

        private readonly CommunicationType _type;

        public ClientServerCommunication(CommunicationType type)
        {
            _type = type;
        }

        public static byte[] SerializeToByteArray(object obj)
        {
            if (obj == null) return null;

            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] byteArray) where T: class
        {
            if (byteArray == null) return null;

            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                ms.Write(byteArray, 0, byteArray.Length);
                ms.Seek(0, SeekOrigin.Begin);
                var obj = (T)formatter.Deserialize(ms);
                return obj;
            }
        }

        public enum CommunicationType
        {
            Login,
            Logout,
            FriendGotOnline,
            FriendGotOffline,
            SendMessage,
            InSuspence,
            CreateARoom,
            GetRooms,
            JoinRoom,
            Move,
            GameStart,
            CloseSession
        }
    }
}
