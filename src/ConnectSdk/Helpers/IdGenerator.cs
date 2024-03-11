using ConnectSdk.Utils;
using System;
using System.Text.RegularExpressions;

namespace ConnectSdk.Helpers;

/// <summary>
/// Id generator
/// </summary>
public class IdGenerator
{
    /// <summary>
    /// Validate ID (only alphanumeric + 26 char lenght)
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static bool IsValidId(string id)
    {
        return Regex.IsMatch(id, @"^[a-z0-9]{26}$");
    }

    /// <summary>
    /// Validate if Id starts with section Id
    /// </summary>
    /// <param name="sectionId"></param>
    /// <returns></returns>
    public static bool IsValidSectionId(string sectionId)
    {
        return sectionId.StartsWith(Constants.IdPrefixes.SECTION) && IsValidId(sectionId.Split("_")[1]);
    }

    /// <summary>
    /// Generate a new Section Id
    /// </summary>
    /// <returns>Section Id</returns>
    public static string GenerateSectionId() => GenerateId(Constants.IdPrefixes.SECTION);


    private static string GenerateId(string prefix = "")
    {
        const int defaultLength = 26;
        var uuid = Guid.NewGuid().ToString("N");
        return $"{prefix}{uuid.Substring(0, defaultLength)}";

    }
}