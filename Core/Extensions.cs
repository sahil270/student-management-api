using System.Linq.Expressions;

namespace Core
{
    public static class Extensions
    {
        public static string ToFilterPattern(this string str)
        {
            if (!string.IsNullOrEmpty(str?.Trim()))
                str = str.Replace("[", "[[]")
                         .Replace("_", "[_]")
                         .Replace("%", "[%]")
                         .Replace("-", "[-]");

            return $"%{str}%" ;
        }

        public static Expression<Func<T, object>> BuildSortExpression<T>(this string fieldName)
        {
            var property = typeof(T).GetProperties().FirstOrDefault(x => x.Name.Equals(fieldName, StringComparison.OrdinalIgnoreCase));

            if (property == null)
                throw new Exception($"No field Found named {fieldName}");

            var param = Expression.Parameter(typeof(T), "x");
            var body = Expression.Convert(Expression.Property(param, property), typeof(object));

            return Expression.Lambda<Func<T, object>>(body, param);
        }
    }
}
