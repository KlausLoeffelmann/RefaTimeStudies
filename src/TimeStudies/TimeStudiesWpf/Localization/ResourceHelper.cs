using System.Resources;
using System.Reflection;

namespace TimeStudiesWpf.Localization;

/// <summary>
/// Helper class for accessing localized resources.
/// Hilfsklasse für den Zugriff auf lokalisierte Ressourcen.
/// </summary>
public static class ResourceHelper
{
    private static readonly ResourceManager ResourceManager = new ResourceManager(
        "TimeStudiesWpf.Properties.Resources",
        Assembly.GetExecutingAssembly());

    /// <summary>
    /// Gets the localized string for the specified resource key.
    /// Ruft den lokalisierten String für den angegebenen Ressourcenschlüssel ab.
    /// </summary>
    /// <param name="key">The resource key.</param>
    /// <returns>The localized string, or the key itself if not found.</returns>
    public static string GetString(string key)
    {
        try
        {
            string value = ResourceManager.GetString(key) ?? key;
            return value;
        }
        catch
        {
            return key;
        }
    }

    /// <summary>
    /// Gets the localized string for the specified resource key with formatting.
    /// Ruft den lokalisierten String für den angegebenen Ressourcenschlüssel mit Formatierung ab.
    /// </summary>
    /// <param name="key">The resource key.</param>
    /// <param name="args">The arguments to format into the string.</param>
    /// <returns>The formatted localized string.</returns>
    public static string GetStringFormatted(string key, params object[] args)
    {
        string value = GetString(key);
        try
        {
            return string.Format(value, args);
        }
        catch
        {
            return value;
        }
    }

    /// <summary>
    /// Sets the current UI culture for resource lookups.
    /// Legt die aktuelle UI-Kultur für Ressourcensuchvorgänge fest.
    /// </summary>
    /// <param name="cultureName">The culture name (e.g., "de-DE" or "en-US").</param>
    public static void SetCulture(string cultureName)
    {
        try
        {
            var culture = new System.Globalization.CultureInfo(cultureName);
            System.Globalization.CultureInfo.CurrentUICulture = culture;
            System.Globalization.CultureInfo.CurrentCulture = culture;
        }
        catch
        {
            // If the culture is invalid, fall back to the system default
        }
    }
}

/// <summary>
/// Extension methods for resource string access.
/// Erweiterungsmethoden für den Zugriff auf Ressourcenstrings.
/// </summary>
public static class ResourceExtensions
{
    /// <summary>
    /// Gets the localized string for the specified resource key.
    /// Ruft den lokalisierten String für den angegebenen Ressourcenschlüssel ab.
    /// </summary>
    /// <param name="key">The resource key.</param>
    /// <returns>The localized string.</returns>
    public static string Localize(this string key)
    {
        return ResourceHelper.GetString(key);
    }

    /// <summary>
    /// Gets the localized string for the specified resource key with formatting.
    /// Ruft den lokalisierten String für den angegebenen Ressourcenschlüssel mit Formatierung ab.
    /// </summary>
    /// <param name="key">The resource key.</param>
    /// <param name="args">The arguments to format into the string.</param>
    /// <returns>The formatted localized string.</returns>
    public static string LocalizeFormat(this string key, params object[] args)
    {
        return ResourceHelper.GetStringFormatted(key, args);
    }
}