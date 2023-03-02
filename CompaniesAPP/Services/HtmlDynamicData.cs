namespace CompaniesAPP.Services;

public static class HtmlDynamicDisplay
{
    public static IEnumerable<PropertyInfo> GetProperties(object obj)
    {
        return obj.GetType().GetProperties();
    }

    public static string GetValue(object value)
    {
        if (value == null)
        {
            return "N/A";
        }

        var valueType = value.GetType();

        if (valueType.IsPrimitive || valueType == typeof(string))
        {
            return value.ToString()!;
        }

        if (valueType.IsArray || valueType.IsGenericType && typeof(IEnumerable).IsAssignableFrom(valueType))
        {
            var sb = new StringBuilder();
            var items = value as IEnumerable;

            if (items != null)
            {
                foreach (var item in items)
                {
                    sb.Append(GetValue(item));
                }
            }

            return sb.ToString().TrimEnd(' ', ',');
        }

        var properties = GetProperties(value);

        if (!properties.Any())
        {
            return value.ToString()!;
        }

        var sbDetails = new StringBuilder();
        sbDetails.Append("<ul>");

        foreach (var property in properties)
        {
            sbDetails.Append("<li>");
            sbDetails.Append(property.GetDisplayName());
            sbDetails.Append(": ");
            sbDetails.Append(GetValue(property.GetValue(value)!));
            sbDetails.Append("</li>");
        }

        sbDetails.Append("</ul>");

        return sbDetails.ToString();
    }
}