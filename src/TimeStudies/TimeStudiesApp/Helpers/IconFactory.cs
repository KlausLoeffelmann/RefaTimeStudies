using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace TimeStudiesApp.Helpers;

/// <summary>
/// Factory class for rendering Segoe font icons (Segoe Fluent Icons, Segoe MDL2 Assets, Segoe UI Symbol)
/// into images with proper anti-aliasing and centering.
/// </summary>
public static class IconFactory
{
    /// <summary>
    /// Available icon fonts in order of preference.
    /// </summary>
    private static readonly string[] IconFontNames =
    [
        "Segoe Fluent Icons",
        "Segoe MDL2 Assets",
        "Segoe UI Symbol"
    ];

    /// <summary>
    /// Common icon codes for the application.
    /// These work with Segoe MDL2 Assets and Segoe Fluent Icons.
    /// </summary>
    public static class Icons
    {
        public const char NewDocument = '\uE8A5';      // New file
        public const char OpenFile = '\uE8E5';         // Open folder/file
        public const char Save = '\uE74E';             // Save
        public const char SaveAs = '\uE792';           // Save as
        public const char Export = '\uEDE1';           // Export
        public const char Settings = '\uE713';         // Settings gear
        public const char Help = '\uE897';             // Help/Question mark
        public const char Info = '\uE946';             // Info
        public const char Play = '\uE768';             // Play/Start
        public const char Stop = '\uE71A';             // Stop
        public const char Pause = '\uE769';            // Pause
        public const char Timer = '\uE916';            // Timer/Stopwatch
        public const char Add = '\uE710';              // Add/Plus
        public const char Delete = '\uE74D';           // Delete/Trash
        public const char Edit = '\uE70F';             // Edit/Pencil
        public const char Copy = '\uE8C8';             // Copy
        public const char Checkmark = '\uE73E';        // Checkmark
        public const char Cancel = '\uE711';           // Cancel/X
        public const char Up = '\uE74A';               // Move up
        public const char Down = '\uE74B';             // Move down
        public const char Folder = '\uE8B7';           // Folder
        public const char Document = '\uE8A5';         // Document
        public const char Clock = '\uE823';            // Clock
        public const char Recording = '\uE7C8';        // Recording indicator
        public const char Exit = '\uE7E8';             // Exit/Door
    }

    /// <summary>
    /// Gets the best available icon font installed on the system.
    /// </summary>
    private static string GetAvailableIconFont()
    {
        using var installedFonts = new InstalledFontCollection();
        var availableFontNames = installedFonts.Families.Select(f => f.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var fontName in IconFontNames)
        {
            if (availableFontNames.Contains(fontName))
            {
                return fontName;
            }
        }

        // Fallback to Segoe UI Symbol which is always available on Windows
        return "Segoe UI Symbol";
    }

    /// <summary>
    /// Creates an image from a Segoe icon character.
    /// </summary>
    /// <param name="iconChar">The Unicode character representing the icon.</param>
    /// <param name="targetSize">The target size of the image in pixels.</param>
    /// <param name="foreColor">The foreground color for the icon.</param>
    /// <param name="backColor">The background color (use Color.Transparent for transparent background).</param>
    /// <returns>A bitmap containing the rendered icon.</returns>
    public static Bitmap CreateIcon(char iconChar, int targetSize, Color foreColor, Color? backColor = null)
    {
        return CreateIcon(iconChar.ToString(), targetSize, foreColor, backColor);
    }

    /// <summary>
    /// Creates an image from a Segoe icon string (can be multiple characters).
    /// </summary>
    /// <param name="iconText">The Unicode string representing the icon(s).</param>
    /// <param name="targetSize">The target size of the image in pixels.</param>
    /// <param name="foreColor">The foreground color for the icon.</param>
    /// <param name="backColor">The background color (use Color.Transparent for transparent background).</param>
    /// <returns>A bitmap containing the rendered icon.</returns>
    public static Bitmap CreateIcon(string iconText, int targetSize, Color foreColor, Color? backColor = null)
    {
        var bitmap = new Bitmap(targetSize, targetSize);
        bitmap.SetResolution(96, 96);

        using var graphics = Graphics.FromImage(bitmap);
        
        // Set up high-quality anti-aliased rendering
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

        // Fill background
        if (backColor.HasValue && backColor.Value != Color.Transparent)
        {
            graphics.Clear(backColor.Value);
        }
        else
        {
            graphics.Clear(Color.Transparent);
        }

        var fontName = GetAvailableIconFont();
        
        // Calculate the optimal font size to fit within the target size
        // We use about 75% of target size for the icon to leave some padding
        float fontSize = targetSize * 0.70f;
        
        using var font = new Font(fontName, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
        using var brush = new SolidBrush(foreColor);

        // Measure the actual size of the rendered text
        var textSize = graphics.MeasureString(iconText, font, new PointF(0, 0), StringFormat.GenericTypographic);

        // Calculate position to center the icon
        float x = (targetSize - textSize.Width) / 2f;
        float y = (targetSize - textSize.Height) / 2f;

        // Use StringFormat for precise positioning
        var format = new StringFormat(StringFormat.GenericTypographic)
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.NoClip
        };

        // Draw the icon centered in the bitmap
        var rect = new RectangleF(0, 0, targetSize, targetSize);
        graphics.DrawString(iconText, font, brush, rect, format);

        return bitmap;
    }

    /// <summary>
    /// Creates an icon with automatic color based on system colors.
    /// </summary>
    /// <param name="iconChar">The Unicode character representing the icon.</param>
    /// <param name="targetSize">The target size of the image in pixels.</param>
    /// <returns>A bitmap containing the rendered icon using system control text color.</returns>
    public static Bitmap CreateIcon(char iconChar, int targetSize)
    {
        return CreateIcon(iconChar, targetSize, SystemColors.ControlText);
    }

    /// <summary>
    /// Creates a toolbar-sized icon (default 32x32).
    /// </summary>
    public static Bitmap CreateToolbarIcon(char iconChar, int size = 32)
    {
        return CreateIcon(iconChar, size, SystemColors.ControlText);
    }

    /// <summary>
    /// Creates a menu-sized icon (default 16x16).
    /// </summary>
    public static Bitmap CreateMenuIcon(char iconChar, int size = 16)
    {
        return CreateIcon(iconChar, size, SystemColors.MenuText);
    }

    /// <summary>
    /// Creates an icon suitable for a large touch-friendly button.
    /// </summary>
    /// <param name="iconChar">The Unicode character representing the icon.</param>
    /// <param name="buttonSize">The button size in pixels.</param>
    /// <param name="foreColor">The foreground color.</param>
    /// <returns>A bitmap sized appropriately for the button.</returns>
    public static Bitmap CreateButtonIcon(char iconChar, int buttonSize, Color foreColor)
    {
        // Icon should be about 60% of the button size
        int iconSize = (int)(buttonSize * 0.6);
        return CreateIcon(iconChar, iconSize, foreColor);
    }

    /// <summary>
    /// Creates an icon with a highlighted/active state appearance.
    /// </summary>
    public static Bitmap CreateActiveIcon(char iconChar, int targetSize)
    {
        return CreateIcon(iconChar, targetSize, SystemColors.HighlightText, SystemColors.Highlight);
    }

    /// <summary>
    /// Creates a disabled/grayed-out icon.
    /// </summary>
    public static Bitmap CreateDisabledIcon(char iconChar, int targetSize)
    {
        return CreateIcon(iconChar, targetSize, SystemColors.GrayText);
    }
}
