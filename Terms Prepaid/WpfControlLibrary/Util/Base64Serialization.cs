using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace WpfControlLibrary.Util
{
    [Serializable]
    public class McRequest
    {
        public string Message { get; set; }
        public string Segnney { get; set; }
        private string _hash { get; set; }
        public DateTime TimeCreate { get; set; }

        public McRequest(string message, string hash)
        {
            Message = message;
            _hash = hash;
            TimeCreate = DateTime.Now;
            Segnney = GetSegney();
        }

        public McRequest(string message, string hash, DateTime timeCreate)
        {
            Message = message;
            _hash = hash;
            TimeCreate = timeCreate;
            Segnney = GetSegney();
        }

        public string getHash()
        {
            return _hash;
        }

        private string GetSegney()
        {
            StringBuilder sb = new StringBuilder(_hash);
            sb.Append(Message).Append(TimeCreate.ToString());
            return Base64Serialization.GetMd5Hash(sb.ToString());
        }

        public string GetBase64()
        {
            //McRequest r = new McRequest(message, segnney, dt);
            string json = Base64Serialization.ObjectToJson(this);
            string base64 = Base64Serialization.StringToBase64(json);
            return base64;
        }
        /*
        public override bool Equals(object obj)
        {
            var t = obj as McRequest;
            return t != null && Message != null && t.Message != null && Message.Equals(t.Message) &&
                   Segnney != null && t.Segnney != null && Segnney.Equals(t.Segnney) &&
                   TimeCreate.Equals(t.TimeCreate);
        }

        public override int GetHashCode()
        {
            return Message.GetHashCode() ^ Segnney.GetHashCode() ^ TimeCreate.GetHashCode();
        }*/

    }

    public class SignData
    {
        private Dictionary<string, string> _messages;
        public string Messages { get; private set; }
        private DateTime _timeCreate;
        public string TimeCreate { get; private set; }
        public string Signkey { get; private set; }
        private readonly string _hash;

        public SignData(string hash)
        {
            _messages = new Dictionary<string, string>();
            _timeCreate = DateTime.Now;
            TimeCreate = _timeCreate.ToString();
            _hash = hash;
        }

        private string GetSignkey()
        {
            Messages = Base64Serialization.ObjectToJson(_messages);

            StringBuilder sb = new StringBuilder(_hash);
            sb.Append(",")
                .Append(Messages)
                .Append(",")
                .Append(TimeCreate)
                .ToString();

            string str = sb.ToString();
            string md5Str = Base64Serialization.GetMd5Hash(str);

            return md5Str;
        }

        public string GetBase64()
        {
            Signkey = GetSignkey();
            string json = Base64Serialization.ObjectToJson(this);
            string base64 = Base64Serialization.StringToBase64(json);
            return base64;
        }

        public void Add(string key, string value)
        {
            _messages.Add(key, value);
        }

        public void Change(string key, string newValue)
        {
            _messages[key] = newValue;
        }

        public string Get(string key)
        {
            string value;
            return !_messages.TryGetValue(key, out value) ? null : value;
        }

        public void Remove(string key)
        {
            _messages.Remove(key);
        }

        public void Clear()
        {
            _messages.Clear();
        }
    }

    public class Base64Serialization
    {
        public static string SerializeObject(object o)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, o);
                return Convert.ToBase64String(stream.ToArray());
            }
        }

        public static object DeserializeObject(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);

            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return new BinaryFormatter().Deserialize(stream);
            }
        }

        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInput = GetMd5HashSub(md5Hash, input);
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashOfInput, hash) == 0;
        }

        public static string ObjectToJson(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public static string StringToBase64(string str)
        {
            byte[] data = ASCIIEncoding.ASCII.GetBytes(str);
            return Convert.ToBase64String(data);
        }

        public static string GetMd5Hash(string str)
        {
            string hash;
            using (MD5 md5Hash = MD5.Create())
            {
                hash = Base64Serialization.GetMd5HashSub(md5Hash, str);
            }
            return hash;
        }

        private static string GetMd5HashSub(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

    }
}
