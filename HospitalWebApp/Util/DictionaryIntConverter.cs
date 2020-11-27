using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// https://www.thecodebuzz.com/system-text-json-create-dictionary-converter-json-serialization/

namespace HospitalWebApp.Util
{
    
    public class DictionaryIntConverter:JsonConverter<Dictionary<int,int>>
    {
        public override Dictionary<int, int> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
 
            var value = new Dictionary<int, int>();
 
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return value;
                }
 
                var keyString = reader.GetString();
 
                if (!int.TryParse(keyString, out var keyAsInt32))
                {
                    throw new JsonException($"Unable to convert \"{keyString}\" to System.Int32.");
                }
 
                reader.Read();
 
                var itemValue = reader.GetInt32();
 
                value.Add(keyAsInt32, itemValue);
            }
 
            throw new JsonException("Error Occured");
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<int, int> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
 
            foreach (var (key, i) in value)
            {
                writer.WriteString(key.ToString(), i.ToString());
            }
 
            writer.WriteEndObject();
        }
    }
}