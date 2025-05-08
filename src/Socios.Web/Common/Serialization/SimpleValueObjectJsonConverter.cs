using GSF.Domain.ValueObjects;
using Newtonsoft.Json;
using Socios.Web.Bootstrap;
using System.Collections.Concurrent;
using System.Reflection;

namespace Socios.Web.Common.Serialization;

/// <summary>Custom serialization for value object replacing primitive types.
/// It replaces json serialization with the result of method <see cref="ICustomJsonSerialization.GetValue"/> that the value object must implement.
/// It must be injected in <see cref="Startup"/> with:
/// <code>
/// .AddNewtonsoftJson(opt=> { opt.SerializerSettings.Converters.Add(new SimpleValueObjectJsonConverter()); })
/// </code>
/// </summary>
public class SimpleValueObjectJsonConverter : JsonConverter
{
    private static readonly ConcurrentDictionary<Type, Type> ConstructorArgumenTypes = new();

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is not ICustomJsonSerialization valueObject)
        {
            return;
        }

        serializer.Serialize(writer, valueObject.GetValue());
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var constructorInfo = objectType.GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(string) }, null);
        //var parameterType = ConstructorArgumenTypes.GetOrAdd(
        //    objectType,
        //    t =>
        //    {
        //        var parameterInfo = constructorInfo.GetParameters().Single();
        //        return parameterInfo.ParameterType;
        //    });
        var value = serializer.Deserialize(reader, typeof(string));
        if (value == null) return null;
        var obj = constructorInfo.Invoke(new object[] { value });
        return obj;
        //return Activator.CreateInstance(objectType, new[] { value });
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(ICustomJsonSerialization).IsAssignableFrom(objectType);
    }
}