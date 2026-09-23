namespace Backend.Tests;

public abstract class PropertyReflection<T>
{
    public static void Set(T type, object value, string field)
    {
        var property = typeof(T).GetProperty(field, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        property?.SetValue(type, value);
    }

}
