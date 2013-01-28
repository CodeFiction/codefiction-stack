using System;
using System.Text;
using System.Web.Script.Serialization;
using Pandora.CommonTypes;

namespace Pandora.Utilities
{
    public static class SessionWrapperExtensions
    {
        public static ApiSessionWrapper _DecodeApiSessionWrapper(this string signedRequest)
        {
            var encoding = new UTF8Encoding();
            string decodedJson = signedRequest; //.Replace("=", string.Empty).Replace('-', '+').Replace('_', '/');
            byte[] base64JsonArray = Convert.FromBase64String(decodedJson);
            //(decodedJson.PadRight(decodedJson.Length + (4 - decodedJson.Length % 4) % 4, '='));
            string json = encoding.GetString(base64JsonArray);
            var ser = new JavaScriptSerializer();
            return ser.Deserialize<ApiSessionWrapper>(json);
            //JObject.Parse(json);
        }

        public static string _EncodeApiSessionWrapper(this ApiSessionWrapper session)
        {
            var ser = new JavaScriptSerializer();
            string json = ser.Serialize(session);
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(json);

            string encodedText = Convert.ToBase64String(bytesToEncode);
            return encodedText;
        }
    }
}