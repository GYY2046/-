using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TOTP
{
    public class TOTPHelper
    {
        /// <summary>
        /// HMAC-SHA1 
        /// </summary>
        /// <param name="counter"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string ToHMACSHA1(byte[] counter, byte[] Key)
        {
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Key;//System.Text.Encoding.UTF8.GetBytes(encryptKey);
            byte[] dataBuffer = counter;//System.Text.Encoding.UTF8.GetBytes(encryptText);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
            return Convert.ToBase64String(hashBytes);
        }
        /// <summary>
        ///  HOTP基于消息认证码的一次性密码
        /// </summary>
        /// <param name="counter"></param>
        /// <param name="Key"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string  HOTP(byte[] counter,byte[] Key,int length = 6)
        {
            var hmac = ToHMACSHA1(counter, Key);
            var offset = hmac[hmac.Length - 1] & 0xF;
            var b1 = (hmac[offset] & 0x7F) << 24;
            var b2 = (hmac[offset + 1] & 0xFF) << 16;
            var b3 = (hmac[offset + 2] & 0xFF) << 8;
            var b4 = (hmac[offset] & 0xFF);

            var code = b1 | b2 | b3 | b4;
            var value = code % (int)Math.Pow(10, length);

            return value.ToString().PadLeft(length, '0');
        }
        /// <summary>
        /// TOTP基于时间的一次性密码
        /// </summary>
        /// <param name="key"></param>
        /// <param name="step"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string TOTP(string key, int step = 60, int length = 6)
        {
            var unixTime = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            var counter = ((int)unixTime) / step;
            var counterBytes = BitConverter.GetBytes(counter);
            byte[] keyBytes = System.Text.Encoding.UTF8.GetBytes(key);
                //BitConverter.GetBytes(key);
            return HOTP(counterBytes, keyBytes, length);
        }
    }
}
