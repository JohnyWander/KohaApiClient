using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Diagnostics;
using AllcandoJM.KohaFramework.ApiCore;


namespace AllcandoJM.KohaFramework.JsonDeserialization
{
    public interface IHoldDeserializer
    {
        public List<OrderDeserialized> DeserializeOrderString(string Json);
    }

    public interface IItemDeserializer
    {
        public List<ItemDeserialized> DeserializeItemString(string Json);
        public ItemAfterUpdateDeserialized DeserializeItemAfterUpdateString(string Json);
    }


    public interface IPatronDeserializer
    {
        public List<PatronSafe> DeserializePatronSafeString(string json);
    }

    public interface ITokenDeserializer
    {
        public Token DeserializeToken(string json);
   
    }

    public interface IErrorDeserializer
    {
        public Error DeserializeError(string json);
    }

    public class KohaJsonDeserializer : IHoldDeserializer,IItemDeserializer,IPatronDeserializer, ITokenDeserializer,IErrorDeserializer
    {
        /// <summary>
        /// You can create custom class for deserialization, for example when you need only patron_id or exlude sensitive data you can pass object that will contain only this value
        /// </summary>
        /// <typeparam name="T">Custom class for deserialization</typeparam>
        /// <param name="json">json list of records (json with [records..])</param>
        /// <returns>List of deserialized records in provided class</returns>
        public List<T> DeserializeCustomList<T>(string json)
        {
            return JsonSerializer.Deserialize<List<T>>(json);
        }

        /// <summary>
        /// You can create custom class for deserialization, for example when you need only patron_id or exlude sensitive data, you can pass object that will contain only this value
        /// </summary>
        /// <typeparam name="T">Custom class for deserialization</typeparam>
        /// <param name="json">single record response in json</param>
        /// <returns></returns>
        public T DeserializeCustomRecord<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }

        public Error DeserializeError(string json)
        {
            return JsonSerializer.Deserialize<Error>(json);
        }
       


        public Token DeserializeToken(string Json)
        {
            return JsonSerializer.Deserialize<Token>(Json);
        }


        public List<OrderDeserialized> DeserializeOrderString(string Json)
        {
            return JsonSerializer.Deserialize<List<OrderDeserialized>>(Json);
        }

        public List<ItemDeserialized> DeserializeItemString(string Json)
        {
            Debug.WriteLine(Json);
            return JsonSerializer.Deserialize<List<ItemDeserialized>>(Json);
        }

        public ItemAfterUpdateDeserialized DeserializeItemAfterUpdateString(string Json)
        {
            return JsonSerializer.Deserialize<ItemAfterUpdateDeserialized>(Json);
        }


        public List<PatronSafe> DeserializePatronSafeString(string json)
        {
            return JsonSerializer.Deserialize<List<PatronSafe>>(json);
        }

        public List<PatronFull> DeserializePatronFullString(string json)
        {
            return JsonSerializer.Deserialize<List<PatronFull>>(json);
        }

        public List<PatronDebits> DeserializePatronDebitsString(string json)
        {
            return JsonSerializer.Deserialize<List<PatronDebits>>(json);
        }

    }
}
