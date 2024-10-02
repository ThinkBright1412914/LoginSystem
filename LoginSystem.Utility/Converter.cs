using System;
using System.Data;
using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace LoginSystem.Utility
{
    public sealed class Converter
    {
        public DataTable ConvertToDataTable<T>(List<T> items)
        {
            DataTable dt = new DataTable(typeof(T).Name);
            PropertyInfo[] propInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in propInfo)
            {
                dt.Columns.Add(prop.Name);
            }

            foreach (T item in items)
            {
                var values = new object[propInfo.Length];
                for (int i = 0; i < propInfo.Length; i++)
                {
                    values[i] = propInfo[i].GetValue(item, null);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }

        public static byte[] ImageConverter(IFormFile file)
        {
            using (Stream s = file.OpenReadStream())
            {
                using(BinaryReader br = new BinaryReader(s))
                {
                    byte[] data = br.ReadBytes((Int32)s.Length);
                    return data;
                }
            }
        }

        public byte[] ConvertBase64ToByteArray(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                throw new ArgumentException("Base64 string cannot be null or empty", nameof(base64String));
            }

            return Convert.FromBase64String(base64String);
        }


        public async Task<string> ConvertToBase64(IFormFile file) 
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        public String DateFormatter(String date)
        {
			DateTime dateTime = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
			var result = dateTime.ToString("MM/dd/yyyy");
            return result;
        }


    }
}
