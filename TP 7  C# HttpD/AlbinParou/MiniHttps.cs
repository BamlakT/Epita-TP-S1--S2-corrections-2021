using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace HttpD
{
    public class MiniHttps
    {
        private byte[] _key;
        private HttpClientStream _client;
        
        public MiniHttps(string url, string key)
        {
            _client = new HttpClientStream(HttpMethod.Post, url);
            _key = Encoding.ASCII.GetBytes(key);
        }

        public byte[] EncryptFullMessage(string message)
        {
            var encoded = Encoding.ASCII.GetBytes(message);
            var encrypted = new List<byte>();
            var i = 0;
            while (i <= encoded.Length - 16)
            {
                var tmp = encoded.Skip(i).Take(16).ToArray();
                i += 16;
                encrypted.AddRange(Encryption.Encrypt(tmp, _key));
            }

            return encrypted.ToArray();
        }
        
        public string DecryptFullCipher(byte[] cipher)
        {
            var decrypted = new List<byte>();
            var i = 0;
            while (i <= cipher.Length - 16)
            {
                var tmp = cipher.Skip(i).Take(16).ToArray();
                i += 16;
                decrypted.AddRange(Encryption.Decrypt(tmp, _key));
            }

            return Encoding.ASCII.GetString(decrypted.ToArray());
        }
        
        private string BytesToString(byte[] bytes)
        {
            var res = "";
            foreach (var hex in bytes)
                res += (char) hex;
            return res;
        }

        public string EncryptAndSend(string message)
        {
            var encrypted = BytesToString(EncryptFullMessage(message));

            var response = _client.SendMessage(encrypted);
            var response_enc = _client.ResponseFromStream(response);
            return DecryptFullCipher(Encryption.ToBytes(response_enc));
        }
    }
}