using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICMServer.Models
{
    class IPAddress
    {
        private uint m_Address = 0;

        private IPAddress(uint newAddress)
        {
            m_Address = newAddress;
        }

        public IPAddress NextIPAddress()
        {
            return new IPAddress(m_Address + 1);
        }
        
        public static IPAddress Parse(string ipString)
        {
            if (ipString != null)
            {
                string[] segments = ipString.Split('.');
                if (segments.Count() == 4)
                {
                    byte[] bytes = new byte[4];
                    for (int i = 0; i < 4; ++i)
                        bytes[i] = Byte.Parse(segments[i]);
                    return new IPAddress(
                        ((uint)bytes[0]) << 24
                      | ((uint)bytes[1]) << 16
                      | ((uint)bytes[2]) <<  8
                      | ((uint)bytes[3]) <<  0);
                }
            }
            return null;
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}.{2}.{3}", 
                (m_Address >> 24) & 0xFF, 
                (m_Address >> 16) & 0xFF,
                (m_Address >>  8) & 0xFF,
                (m_Address >>  0) & 0xFF);
        }
    }
}
