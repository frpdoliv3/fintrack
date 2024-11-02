using System.Reflection;

namespace FinTrack.Domain.Entities;

public static class UserRole
{
    public const string Admin = "Admin";

    public static List<string> GetUserRoles()
    {
        var rolesUnsafe = typeof(UserRole)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string));
        return rolesUnsafe.Select(f => (f.GetValue(null) as string)!).ToList();
    }
}
