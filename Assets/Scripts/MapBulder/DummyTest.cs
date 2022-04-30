using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
namespace Game.Environment.Map
{
    public class DummyTest : MonoBehaviour
    {
        public void OnEnable()
        {
            MapData mapData = new MapData();
            string output = JsonConvert.SerializeObject(mapData);
            /* Product product = new Product();
             product.ExpiryDate = new DateTime(2008, 12, 28);

             JsonSerializer serializer = new JsonSerializer();
             serializer.Converters.Add(new JavaScriptDateTimeConverter());
             serializer.NullValueHandling = NullValueHandling.Ignore;

             using (StreamWriter sw = new StreamWriter(@"c:\json.txt"))
             using (JsonWriter writer = new JsonTextWriter(sw))
             {
                 serializer.Serialize(writer, product);
                 // {"ExpiryDate":new Date(1230375600000),"Price":0}
             }*/
        }
    }
}