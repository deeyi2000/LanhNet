using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.LanhNet.Iot.WebApi.Infrastructure.common
{
    public class Base64Encoder
    {
        protected const string CIPHER_CODE = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        public static string Encode(string data, string cipherCode = Base64Encoder.CIPHER_CODE)
        {
            StringBuilder result = new StringBuilder();

            int length = data.Length / 3;
            int tail = data.Length % 3;
            int i = 0;

            for (; i < length; i++)
            {
                int temp = (data[i * 3] & 0xFC) >> 2;
                result.Append(cipherCode[temp]);
                temp = ((data[i * 3] & 0x03) << 4) | ((data[i * 3 + 1] & 0xF0) >> 4);
                result.Append(cipherCode[temp]);
                temp = ((data[i * 3 + 1] & 0x0F) << 2) | ((data[i * 3 + 2] & 0xC0) >> 6);
                result.Append(cipherCode[temp]);
                temp = (data[i * 3 + 2] & 0x3F);
                result.Append(cipherCode[temp]);
            }

            switch (tail)
            {
                case 1:
                    {
                        int temp = (data[i * 3] & 0xFC) >> 2;
                        result.Append(cipherCode[temp]);
                        temp = ((data[i * 3] & 0x03) << 4);
                        result.Append(cipherCode[temp]);
                    }
                    break;
                case 2:
                    {
                        int temp = (data[i * 3] & 0xFC) >> 2;
                        result.Append(cipherCode[temp]);
                        temp = ((data[i * 3] & 0x03) << 4) | ((data[i * 3 + 1] & 0xF0) >> 4);
                        result.Append(cipherCode[temp]);
                        temp = ((data[i * 3 + 1] & 0x0F) << 2);
                        result.Append(cipherCode[temp]);
                    }
                    break;
                default: break;
            }

            return result.ToString();
        }

        public static string Decode(string data, string cipherCode = Base64Encoder.CIPHER_CODE)
        {
            StringBuilder result = new StringBuilder();

            int length = data.Length / 4;
            int tail = data.Length % 4;
            int i = 0;

            for (; i < length; i++)
            {
                byte[] code = new byte[]
                {
                    (byte)cipherCode.IndexOf(data[i * 4]),
                    (byte)cipherCode.IndexOf(data[i * 4 + 1]),
                    (byte)cipherCode.IndexOf(data[i * 4 + 2]),
                    (byte)cipherCode.IndexOf(data[i * 4 + 3])
                };
                result.Append((char)(((code[0] << 2) & 0xFC) | ((code[1] >> 4) & 0x03)));
                result.Append((char)(((code[1] << 4) & 0xF0) | ((code[2] >> 2) & 0x0F)));
                result.Append((char)(((code[2] << 6) & 0xC0) | (code[3] & 0x3F)));
            }
            switch (tail)
            {
                case 1:
                    {
                        byte[] code = new byte[]
                        {
                            (byte)cipherCode.IndexOf(data[i * 4])
                        };
                        result.Append((char)((code[0] << 2) & 0xFC));
                    } break;
                case 2:
                    {
                        byte[] code = new byte[]
                        {
                            (byte)cipherCode.IndexOf(data[i * 4]),
                            (byte)cipherCode.IndexOf(data[i * 4 + 1])
                        };
                        result.Append((char)(((code[0] << 2) & 0xFC) | ((code[1] >> 4) & 0x03)));
                        byte temp = (byte)((code[1] << 4) & 0xF0);
                        if (0 != temp)
                            result.Append((char)temp);
                    } break;
                case 3:
                    {
                        byte[] code = new byte[]
                        {
                            (byte)cipherCode.IndexOf(data[i * 4]),
                            (byte)cipherCode.IndexOf(data[i * 4 + 1]),
                            (byte)cipherCode.IndexOf(data[i * 4 + 2])
                        };
                        result.Append((char)(((code[0] << 2) & 0xFC) | ((code[1] >> 4) & 0x03)));
                        result.Append((char)(((code[1] << 4) & 0xF0) | ((code[2] >> 2) & 0x0F)));
                        byte temp = (byte)((code[2] << 6) & 0xC0);
                        if (0 != temp)
                            result.Append((char)temp);
                    }
                    break;
                default: break;
            }
            
            return result.ToString();
        }
    }
}
