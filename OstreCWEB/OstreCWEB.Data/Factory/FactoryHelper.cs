using System.Text.Json;
namespace OstreCWEB.Services.Factory
{
    public static class FactoryHelper
    {
        public static T GenerateNewObjectInstance<T>(T objectInstance)
        {

            var objectAsText = JsonSerializer.Serialize(objectInstance);

            var newObjectInstance = JsonSerializer.Deserialize<T>(objectAsText);
            return newObjectInstance;
        }
    }
}
