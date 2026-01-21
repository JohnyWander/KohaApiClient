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
        public List<Patron> DeserializePatronString(string json);
    }

    public interface ITokenDeserializer
    {
        public Token DeserializeToken(string json);
   
    }

    public class KohaJsonDeserializer : IHoldDeserializer,IItemDeserializer,IPatronDeserializer, ITokenDeserializer
    {
        
       


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


        public List<Patron> DeserializePatronString(string json)
        {
            return JsonSerializer.Deserialize<List<Patron>>(json);
        }



    }
}
