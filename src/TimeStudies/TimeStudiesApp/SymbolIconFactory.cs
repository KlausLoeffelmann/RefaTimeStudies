using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace TimeStudiesApp;

/// <summary>
/// Factory class for rendering symbol icons from Segoe icon fonts into images.
/// Supports Segoe Fluent Icons, Segoe MDL2 Assets, and Segoe UI Symbol fonts.
/// </summary>
public static class SymbolIconFactory
{
    private static readonly string[] IconFontPreference =
    [
        "Segoe Fluent Icons",
        "Segoe MDL2 Assets",
        "Segoe UI Symbol"
    ];

    /// <summary>
    /// Common icon symbols available in Segoe icon fonts.
    /// </summary>
    public static class Symbols
    {
        // File operations
        public const char NewDocument = '\uE8A5';      // New file
        public const char OpenFile = '\uE8E5';          // Open folder
        public const char Save = '\uE74E';              // Save
        public const char SaveAs = '\uE792';            // Save as
        public const char Export = '\uEDE1';            // Export/Share
        public const char Close = '\uE8BB';             // Close/Exit

        // Recording controls
        public const char Play = '\uE768';              // Play/Start
        public const char Stop = '\uE71A';              // Stop
        public const char Pause = '\uE769';             // Pause

        // Navigation and actions
        public const char Settings = '\uE713';          // Settings gear
        public const char Help = '\uE897';              // Help/Question
        public const char Info = '\uE946';              // Information
        public const char Add = '\uE710';               // Add/Plus
        public const char Remove = '\uE738';            // Remove/Delete
        public const char Edit = '\uE70F';              // Edit/Pencil
        public const char Copy = '\uE8C8';              // Copy
        public const char Clock = '\uE823';             // Clock/Time
        public const char Timer = '\uE916';             // Timer
        public const char CheckMark = '\uE73E';         // Checkmark
        public const char Cancel = '\uE711';            // Cancel/X

        // Arrows
        public const char Up = '\uE74A';                // Up arrow
        public const char Down = '\uE74B';              // Down arrow

        // List and grid
        public const char List = '\uE8FD';              // List view
        public const char Grid = '\uE8A9';              // Grid view
    }

    /// <summary>
    /// Gets the first available icon font from the system.
    /// </summary>
    private static string GetAvailableIconFont()
    {
        using InstalledFontCollection installedFonts = new();
        HashSet<string> availableFonts = new(
            installedFonts.Families.Select(f => f.Name),
            StringComparer.OrdinalIgnoreCase);

        foreach (string fontName in IconFontPreference)
        {
            if (availableFonts.Contains(fontName))
            {
                return fontName;
            }
        }

        // Fallback to Segoe UI Symbol which is available on all Windows versions
        return "Segoe UI Symbol";
    }

    /// <summary>
    /// Renders a symbol character into a bitmap image.
    /// </summary>
    /// <param name="symbol">The Unicode symbol character to render.</param>
    /// <param name="targetSize">The target size of the output image.</param>
    /// <param name="foreColor">The foreground color for the symbol.</param>
    /// <param name="backColor">The background color (use Color.Transparent for transparent background).</param>
    /// <returns>A bitmap containing the rendered symbol.</returns>
    public static Bitmap RenderSymbol(
        char symbol,
        Size targetSize,
        Color foreColor,
        Color? backColor = null)
    {
        Color effectiveBackColor = backColor ?? Color.Transparent;
        Bitmap bitmap = new(targetSize.Width, targetSize.Height);
        bitmap.SetResolution(96, 96);

        using Graphics graphics = Graphics.FromImage(bitmap);

        // Set up high-quality anti-aliased rendering
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        // Clear background
        graphics.Clear(effectiveBackColor);

        string fontName = GetAvailableIconFont();
        string symbolText = symbol.ToString();

        // Calculate optimal font size to fit the target area with some padding
        float padding = targetSize.Width * 0.1f;
        float availableSize = Math.Min(targetSize.Width, targetSize.Height) - (padding * 2);
        float fontSize = CalculateOptimalFontSize(graphics, symbolText, fontName, availableSize);

        using Font symbolFont = new(fontName, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
        using SolidBrush brush = new(foreColor);

        // Measure the actual string size
        SizeF textSize = graphics.MeasureString(symbolText, symbolFont, PointF.Empty, StringFormat.GenericTypographic);

        // Calculate centered position
        float x = (targetSize.Width - textSize.Width) / 2;
        float y = (targetSize.Height - textSize.Height) / 2;

        // Draw the symbol centered
        graphics.DrawString(
            symbolText,
            symbolFont,
            brush,
            new PointF(x, y),
            StringFormat.GenericTypographic);

        return bitmap;
    }

    /// <summary>
    /// Renders a symbol character into a bitmap image using system colors.
    /// Automatically adapts to dark mode when enabled.
    /// </summary>
    public static Bitmap RenderSymbol(char symbol, Size targetSize)
    {
        Color foreColor = Application.IsDarkModeEnabled
            ? SystemColors.ControlLightLight
            : SystemColors.ControlText;

        return RenderSymbol(symbol, targetSize, foreColor, Color.Transparent);
    }

    /// <summary>
    /// Renders a symbol for use in a ToolStrip button.
    /// </summary>
    public static Bitmap RenderToolStripSymbol(char symbol, int size = 20)
    {
        return RenderSymbol(symbol, new Size(size, size));
    }

    /// <summary>
    /// Renders a symbol for use in a menu item.
    /// </summary>
    public static Bitmap RenderMenuSymbol(char symbol, int size = 16)
    {
        return RenderSymbol(symbol, new Size(size, size));
    }

    /// <summary>
    /// Renders a large symbol for touch-friendly buttons.
    /// </summary>
    public static Bitmap RenderTouchButtonSymbol(char symbol, int size = 32)
    {
        return RenderSymbol(symbol, new Size(size, size));
    }

    /// <summary>
    /// Calculates the optimal font size to fit text within a given area.
    /// </summary>
    private static float CalculateOptimalFontSize(
        Graphics graphics,
        string text,
        string fontName,
        float targetSize)
    {
        // Start with target size as initial font size and adjust
        float fontSize = targetSize;
        const float minFontSize = 8f;
        const float maxIterations = 20;

        for (int i = 0; i < maxIterations && fontSize > minFontSize; i++)
        {
            using Font testFont = new(fontName, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
            SizeF measured = graphics.MeasureString(text, testFont, PointF.Empty, StringFormat.GenericTypographic);

            float maxDimension = Math.Max(measured.Width, measured.Height);

            if (maxDimension <= targetSize)
            {
                return fontSize;
            }

            // Reduce font size proportionally
            fontSize = fontSize * (targetSize / maxDimension) * 0.95f;
        }

        return Math.Max(fontSize, minFontSize);
    }

    /// <summary>
    /// Creates an ImageList populated with common toolbar icons.
    /// </summary>
    public static ImageList CreateToolbarImageList(int iconSize = 20)
    {
        ImageList imageList = new()
        {
            ImageSize = new Size(iconSize, iconSize),
            ColorDepth = ColorDepth.Depth32Bit
        };

        imageList.Images.Add("New", RenderToolStripSymbol(Symbols.NewDocument, iconSize));
        imageList.Images.Add("Open", RenderToolStripSymbol(Symbols.OpenFile, iconSize));
        imageList.Images.Add("Save", RenderToolStripSymbol(Symbols.Save, iconSize));
        imageList.Images.Add("Export", RenderToolStripSymbol(Symbols.Export, iconSize));
        imageList.Images.Add("Play", RenderToolStripSymbol(Symbols.Play, iconSize));
        imageList.Images.Add("Stop", RenderToolStripSymbol(Symbols.Stop, iconSize));
        imageList.Images.Add("Pause", RenderToolStripSymbol(Symbols.Pause, iconSize));
        imageList.Images.Add("Settings", RenderToolStripSymbol(Symbols.Settings, iconSize));

        return imageList;
    }
}
