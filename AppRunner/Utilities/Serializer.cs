using JsonPrettyPrinterPlus;
using JsonPrettyPrinterPlus.JsonPrettyPrinterInternals;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace AppRunner.Utilities
{
    class Serializer
    {
        public static void Save<T>(string fileName, T obj)
        {
            var resultsSerializer = new DataContractJsonSerializer(typeof(T));
            var stream = new MemoryStream();
            resultsSerializer.WriteObject(stream, obj);
            string resultString = Encoding.Default.GetString(stream.ToArray());

            stream.Close();
            stream.Dispose();

            var resultsFile = new StreamWriter(fileName);
            var jPrinter = new JsonPrettyPrinter(new JsonPPStrategyContext());
            var beautifiedJson = jPrinter.PrettyPrint(resultString);

            resultsFile.Write(beautifiedJson);
            resultsFile.Flush();
            resultsFile.Close();
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
