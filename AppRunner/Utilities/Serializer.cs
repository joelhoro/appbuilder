using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace AppRunner.Utilities
{
    class Serializer
    {
        public static void Save<T>(string fileName, T obj)
        {
            var fileStream = File.Create(fileName);

            var ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(fileStream, obj);
            fileStream.Close();
        }

        public static T Load<T>(string fileName)
        {
            var fileStream = File.OpenRead(fileName);

            var formatter = new DataContractJsonSerializer(typeof(T));
            var result = (T)formatter.ReadObject(fileStream);
            fileStream.Close();
            return result;
        }
    }
}
