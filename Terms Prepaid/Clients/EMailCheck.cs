using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Net.Mail;
using System.IO;

namespace lanta.Clients
{
    class EMailCheck
    {
        private string GetAnswer(Socket s, string request)
        {
            byte[] msg = Encoding.UTF8.GetBytes(request);
            byte[] bytes = new byte[512];
            int i = s.Send(msg);
            i = s.Receive(bytes);
            string answer = Encoding.UTF8.GetString(bytes);
            return answer;
        }
        public  bool Check(string mail)
        {
            //nslookup -type=mx mail.ru
            try
            {
                string[] ml = mail.Split('@');
                if (ml.Length != 2)
                    return false;
                ArrayList answers = QueryServer(102, ml[1], QTYPE.MX, 1);
                if (answers == null || answers.Count < 1)
                    return false;
                MX_Record rec = (MX_Record)answers[0];
                string Host = rec.Host;

                //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Host + " 25");
                //Stream s = request.GetRequestStream();
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // Connect to the host using its IPEndPoint.
                s.Connect(Host, 25);
                if (!s.Connected)
                {
                    s = null;
                    return false;
                }
                string answer = GetAnswer(s, "HELO IT\n");
                answer = GetAnswer(s, "HELO IT\n");
                answer = GetAnswer(s, "MAIL FROM: <1@mail.ru>\n");
                answer = GetAnswer(s, "RCPT TO: <" + mail + ">\n");
                answer = GetAnswer(s, "quit\n");
                s.Disconnect(true);

                /*           Список почтовых серверов домена mail.ru: (MX записи)
                mxs.mail.ru

                SMTP сессия:
                Проверка сервера mxs.mail.ru...
                Открытие соединения с mxs.mail.ru... Успешно!
                HELO DEKODA.net
                250 mx70.mail.ru ready to serve
                (53.17 мс)
                MAIL FROM: <dekoda@DEKODA.net>
                250 OK
                (375.49 мс)
                RCPT TO: <dsdsd@mail.ru>
                250 OK или 550 Must be local Recipient
                (53.33 мс)
                QUIT
                221 mx70.mail.ru закрыл соединение
                (54.32 мс)

                Успешное соединение с mxs.mail.ru. Серьезных ошибок необнаружено. 

                Cтатус e-mail адреса dsdsd@mail.ru - OK
          
                 */
                if (!answer.StartsWith("250"))
                    return false;
            }
            catch (Exception cex)
            {
                cex.ToString();
                return false;
            }
            return true;
        }
        internal enum QTYPE
        {
            /// <summary>
            /// a host address
            /// </summary>
            A = 1,

            /// <summary>
            /// an authoritative name server
            /// </summary>
            NS = 2,
            //	MD    = 3,  Obsolete
            //	MF    = 5,  Obsolete

            /// <summary>
            /// the canonical name for an alias
            /// </summary>
            CNAME = 5,

            /// <summary>
            /// marks the start of a zone of authority
            /// </summary>
            SOA = 6,
            //	MB    = 7,  EXPERIMENTAL
            //	MG    = 8,  EXPERIMENTAL
            //  MR    = 9,  EXPERIMENTAL
            //	NULL  = 10, EXPERIMENTAL
            /// <summary>
            /// a well known service description
            /// </summary>
            WKS = 11,

            /// <summary>
            /// a domain name pointer
            /// </summary>
            PTR = 12,

            /// <summary>
            /// host information
            /// </summary>
            HINFO = 13,

            /// <summary>
            /// mailbox or mail list information
            /// </summary>
            MINFO = 14,

            /// <summary>
            /// mail exchange
            /// </summary>
            MX = 15,

            /// <summary>
            /// text strings
            /// </summary>
            TXT = 16,

            /// <summary>
            /// UnKnown
            /// </summary>
            UnKnown = 9999,
        }
        internal enum OPCODE
        {
            /// <summary>
            ///  a standard query.
            /// </summary>
            QUERY = 0,

            /// <summary>
            /// an inverse query.
            /// </summary>
            IQUERY = 1,

            /// <summary>
            /// a server status request.
            /// </summary>
            STATUS = 2,
        }
        internal enum RCODE
        {
            /// <summary>
            /// No error condition.
            /// </summary>
            NO_ERROR = 0,

            /// <summary>
            /// Format error - The name server was unable to interpret the query.
            /// </summary>
            FORMAT_ERRROR = 1,

            /// <summary>
            /// Server failure - The name server was unable to process this query due to a problem with the name server.
            /// </summary>
            SERVER_FAILURE = 2,

            /// <summary>
            /// Name Error - Meaningful only for responses from an authoritative name server, this code signifies that the
            /// domain name referenced in the query does not exist.
            /// </summary>
            NAME_ERROR = 3,

            /// <summary>
            /// Not Implemented - The name server does not support the requested kind of query.
            /// </summary>
            NOT_IMPLEMENTED = 4,

            /// <summary>
            /// Refused - The name server refuses to perform the specified operation for policy reasons.
            /// </summary>
            REFUSED = 5,
        }
        private byte[] CreateQuery(int ID, string qname, QTYPE qtype, int qclass)
        {
            byte[] query = new byte[521];

            //---- Create header --------------------------------------------//
            // Header is first 12 bytes of query

            /* 4.1.1. Header section format
                                          1  1  1  1  1  1
            0  1  2  3  4  5  6  7  8  9  0  1  2  3  4  5
            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
            |                      ID                       |
            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
            |QR|   Opcode  |AA|TC|RD|RA|   Z    |   RCODE   |
            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
            |                    QDCOUNT                    |
            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
            |                    ANCOUNT                    |
            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
            |                    NSCOUNT                    |
            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
            |                    ARCOUNT                    |
            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
            */

            //--------- Header part -----------------------------------//
            query[0] = (byte)(ID >> 8); query[1] = (byte)ID;
            query[2] = (byte)0; query[3] = (byte)0;
            query[4] = (byte)0; query[5] = (byte)1;
            query[6] = (byte)0; query[7] = (byte)0;
            query[8] = (byte)0; query[9] = (byte)0;
            query[10] = (byte)0; query[11] = (byte)0;
            //---------------------------------------------------------//

            //---- End of header --------------------------------------------//


            //----Create query ------------------------------------//

            /* 	Rfc 1035 4.1.2. Question section format
                                              1  1  1  1  1  1
            0  1  2  3  4  5  6  7  8  9  0  1  2  3  4  5
            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
            |                                               |
            /                     QNAME                     /
            /                                               /
            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
            |                     QTYPE                     |
            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
            |                     QCLASS                    |
            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
			
            QNAME
                a domain name represented as a sequence of labels, where
                each label consists of a length octet followed by that
                number of octets.  The domain name terminates with the
                zero length octet for the null label of the root.  Note
                that this field may be an odd number of octets; no
                padding is used.
            */
            string[] labels = qname.Split(new char[] { '.' });
            int position = 12;

            // Copy all domain parts(labels) to query
            // eg. lumisoft.ee = 2 labels, lumisoft and ee.
            // format = label.length + label(bytes)
            foreach (string label in labels)
            {
                // add label lenght to query
                query[position++] = (byte)(label.Length);

                // convert label string to byte array
                byte[] b = Encoding.ASCII.GetBytes(label.ToCharArray());
                b.CopyTo(query, position);

                // Move position by label length
                position += b.Length;
            }

            // Terminate domain (see note above)
            query[position++] = (byte)0;

            // Set QTYPE 
            query[position++] = (byte)0;
            query[position++] = (byte)qtype;

            // Set QCLASS
            query[position++] = (byte)0;
            query[position++] = (byte)qclass;
            //-------------------------------------------------------//

            return query;
        }
        private bool GetQName(byte[] reply, ref int offset, ref string name)
        {
            try
            {
                // Do while not terminator
                while (reply[offset] != 0)
                {

                    // Check if it's pointer(In pointer first two bits always 1)
                    bool isPointer = ((reply[offset] & 0xC0) == 0xC0);

                    // If pointer
                    if (isPointer)
                    {
                        int pStart = ((reply[offset] & 0x3F) << 8) | (reply[++offset]);
                        offset++;
                        return GetQName(reply, ref pStart, ref name);
                    }
                    else
                    {
                        // label lenght (length = 8Bit and first 2 bits always 0)
                        int labelLenght = (reply[offset] & 0x3F);
                        offset++;

                        // Copy label into name 
                        name += Encoding.ASCII.GetString(reply, offset, labelLenght);
                        offset += labelLenght;
                    }

                    // If the next char isn't terminator,
                    // label continues - add dot between two labels
                    if (reply[offset] != 0)
                    {
                        name += ".";
                    }
                }

                // Move offset by terminator lenght
                offset++;

                return true;
            }
            catch//(Exception x)
            {
                //		System.Windows.Forms.MessageBox.Show(x.Message);
                return false;
            }
        }
        private A_Record ParseARecord(byte[] reply, ref int offset, int rdLength)
        {
            // IPv4 = byte byte byte byte

            byte[] ip = new byte[rdLength];
            Array.Copy(reply, offset, ip, 0, rdLength);

            return new A_Record(ip[0] + "." + ip[1] + "." + ip[2] + "." + ip[3]);
        }
        private PTR_Record ParsePTRRecord(byte[] reply, ref int offset, int rdLength)
        {
            string name = "";
            GetQName(reply, ref offset, ref name);

            return new PTR_Record(name);
        }
        private MX_Record ParseMxRecord(byte[] reply, ref int offset)
        {
            /* RFC 1035	3.3.9. MX RDATA format

            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
            |                  PREFERENCE                   |
            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
            /                   EXCHANGE                    /
            /                                               /
            +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+

            where:

            PREFERENCE      
                A 16 bit integer which specifies the preference given to
                this RR among others at the same owner.  Lower values
                are preferred.

            EXCHANGE 
                A <domain-name> which specifies a host willing to act as
                a mail exchange for the owner name. 
            */

            try
            {
                int pref = reply[offset++] << 8 | reply[offset++];

                string name = "";
                if (GetQName(reply, ref offset, ref name))
                {
                    return new MX_Record(pref, name);
                }
            }
            catch
            {
            }

            return null;
        }
        private ArrayList ParseAnswers(byte[] reply, int queryID)
        {
            try
            {
                //--- Parse headers ------------------------------------//

                /*
                                                1  1  1  1  1  1
                  0  1  2  3  4  5  6  7  8  9  0  1  2  3  4  5
                 +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
                 |                      ID                       |
                 +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
                 |QR|   Opcode  |AA|TC|RD|RA|   Z    |   RCODE   |
                 +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
                 |                    QDCOUNT                    |
                 +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
                 |                    ANCOUNT                    |
                 +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
                 |                    NSCOUNT                    |
                 +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
                 |                    ARCOUNT                    |
                 +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
			 
                QDCOUNT
                    an unsigned 16 bit integer specifying the number of
                    entries in the question section.

                ANCOUNT
                    an unsigned 16 bit integer specifying the number of
                    resource records in the answer section.
                */

                // Get reply code
                int id = (reply[0] << 8 | reply[1]);
                OPCODE opcode = (OPCODE)((reply[2] >> 3) & 15);
                RCODE replyCode = (RCODE)(reply[3] & 15);
                int queryCount = (reply[4] << 8 | reply[5]);
                int answerCount = (reply[6] << 8 | reply[7]);
                int nsCount = (reply[8] << 8 | reply[9]);
                int arCount = (reply[10] << 8 | reply[11]);
                //---- End of headers ---------------------------------//

                // Check that it's query what we want
                if (queryID != id)
                {
                    return null;
                }

                int pos = 12;

                //----- Parse question part ------------//
                for (int q = 0; q < queryCount; q++)
                {
                    string dummy = "";
                    GetQName(reply, ref pos, ref dummy);
                    //qtype + qclass
                    pos += 4;
                }
                //--------------------------------------//


                /*
                                               1  1  1  1  1  1
                 0  1  2  3  4  5  6  7  8  9  0  1  2  3  4  5
                +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
                |                                               |
                /                                               /
                /                      NAME                     /
                |                                               |
                +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
                |                      TYPE                     |
                +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
                |                     CLASS                     |
                +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
                |                      TTL                      |
                |                                               |
                +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
                |                   RDLENGTH                    |
                +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--|
                /                     RDATA                     /
                /                                               /
                +--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
                */

                ArrayList answers = new ArrayList();
                //---- Start parsing aswers ------------------------------------------------------------------//
                for (int i = 0; i < answerCount; i++)
                {
                    string name = "";
                    if (!GetQName(reply, ref pos, ref name))
                    {
                        return null;
                    }

                    int type = reply[pos++] << 8 | reply[pos++];
                    int rdClass = reply[pos++] << 8 | reply[pos++];
                    int ttl = reply[pos++] << 24 | reply[pos++] << 16 | reply[pos++] << 8 | reply[pos++];
                    int rdLength = reply[pos++] << 8 | reply[pos++];

                    object answerObj = null;
                    switch ((QTYPE)type)
                    {
                        case QTYPE.A:
                            answerObj = ParseARecord(reply, ref pos, rdLength);
                            pos += rdLength;
                            break;

                        case QTYPE.PTR:
                            answerObj = ParsePTRRecord(reply, ref pos, rdLength);
                            //	pos += rdLength;		
                            break;

                        case QTYPE.MX:
                            answerObj = ParseMxRecord(reply, ref pos);
                            break;

                        default:
                            answerObj = "dummy"; // Dummy place holder for now
                            pos += rdLength;
                            break;
                    }

                    // Add answer to answer list
                    if (answerObj != null)
                    {
                        answers.Add(answerObj);
                    }
                }
                //-------------------------------------------------------------------------------------------//

                return answers;
            }
            catch
            {
                return null;
            }
        }
        private ArrayList QueryServer(int queryID, string qname, QTYPE qtype, int qclass)
        {
            // See if query is in cache
            /*
             if (m_UseDnsCache)
             {
                 ArrayList entries = DnsCache.GetFromCache(qname, (int)qtype);
                 if (entries != null)
                 {
                     return entries;
                 }
             }
             */
            byte[] query = CreateQuery(queryID, qname, qtype, qclass);

            int helper = 0;
            string[] m_DnsServers = new string[] { "192.168.10.8","194.126.115.18"};
            for (int i = 0; i < 10; i++)
            {

                if (helper > m_DnsServers.Length)
                {
                    helper = 0;
                }

                try
                {
                    IPEndPoint ipRemoteEndPoint = new IPEndPoint(IPAddress.Parse(m_DnsServers[helper]), 53);
                    Socket udpClient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    IPEndPoint ipLocalEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    EndPoint localEndPoint = (EndPoint)ipLocalEndPoint;
                    udpClient.Bind(localEndPoint);

                    udpClient.Connect(ipRemoteEndPoint);

                    //send query
                    udpClient.Send(query);

                    // Wait until we have a reply
                    byte[] retVal = null;
                    if (udpClient.Poll(5 * 1000000, SelectMode.SelectRead))
                    {
                        retVal = new byte[512];
                        udpClient.Receive(retVal);
                    }

                    udpClient.Close();

                    // If reply is ok, return it
                    ArrayList answers = ParseAnswers(retVal, queryID);

                    if (answers != null)
                    {
                        // Cache query
                        /*
                        if (m_UseDnsCache && answers.Count > 0)
                        {
                            DnsCache.AddToCache(qname, (int)qtype, answers);
                        }
                        */
                        return answers;
                    }
                }
                catch (Exception cex)
                {
                    cex.ToString();
                }

                helper++;
            }

            return null;
        }
    }
    
    public class A_Record
    {
        private string m_IP = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="IP">IP address.</param>
        public A_Record(string IP)
        {
            m_IP = IP;
        }

        #region Properties Implementation

        /// <summary>
        /// Gets mail host dns name.
        /// </summary>
        public string IP
        {
            get { return m_IP; }
        }

        #endregion

    }
    public class PTR_Record
    {
        private string m_DomainName = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="domainName">DomainName.</param>
        public PTR_Record(string domainName)
        {
            m_DomainName = domainName;
        }

        #region Properties Implementation

        /// <summary>
        /// Gets domain name.
        /// </summary>
        public string DomainName
        {
            get { return m_DomainName; }
        }

        #endregion

    }
    public class MX_Record
    {
        private int m_Preference = 0;
        private string m_Host = "";

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="preference">MX record preference.</param>
        /// <param name="host">Mail host dns name.</param>
        public MX_Record(int preference, string host)
        {
            m_Preference = preference;
            m_Host = host;
        }

        #region Properties Implementation

        /// <summary>
        /// Gets MX record preference.
        /// </summary>
        public int Preference
        {
            get { return m_Preference; }
        }

        /// <summary>
        /// Gets mail host dns name.
        /// </summary>
        public string Host
        {
            get { return m_Host; }
        }

        #endregion

    }
}
