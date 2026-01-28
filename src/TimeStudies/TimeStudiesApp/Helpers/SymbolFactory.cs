using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace TimeStudiesApp.Helpers;

/// <summary>
/// Factory class for rendering symbols from Segoe icon fonts into images.
/// Uses Segoe Fluent Icons, Segoe MDL2 Assets, or Segoe UI Symbol fonts.
/// </summary>
public static class SymbolFactory
{
    // Common Segoe MDL2 Assets / Fluent Icons symbols
    public static class Symbols
    {
        // File operations
        public const string NewDocument = "\uE8A5";
        public const string OpenFile = "\uE8E5";
        public const string Save = "\uE74E";
        public const string SaveAs = "\uE792";
        public const string Export = "\uEDE1";
        public const string Delete = "\uE74D";

        // Recording controls
        public const string Play = "\uE768";
        public const string Stop = "\uE71A";
        public const string Pause = "\uE769";
        public const string Record = "\uE7C8";

        // Navigation and UI
        public const string Settings = "\uE713";
        public const string Info = "\uE946";
        public const string Help = "\uE897";
        public const string Add = "\uE710";
        public const string Remove = "\uE738";
        public const string Edit = "\uE70F";
        public const string Copy = "\uE8C8";
        public const string Refresh = "\uE72C";
        public const string CheckMark = "\uE73E";
        public const string Cancel = "\uE711";
        public const string Clock = "\uE823";
        public const string Timer = "\uE916";
        public const string Stopwatch = "\uE916";

        // Arrows
        public const string Up = "\uE74A";
        public const string Down = "\uE74B";
        public const string Left = "\uE76B";
        public const string Right = "\uE76C";

        // Status
        public const string Warning = "\uE7BA";
        public const string Error = "\uE783";
        public const string Completed = "\uE930";

        // Misc
        public const string Folder = "\uE8B7";
        public const string Document = "\uE8A5";
        public const string List = "\uE8FD";
        public const string Grid = "\uE80A";
    }

    private static readonly string[] s_preferredFonts =
    [
        "Segoe Fluent Icons",
        "Segoe MDL2 Assets",
        "Segoe UI Symbol"
    ];

    private static string? s_cachedFontName;

    /// <summary>
    /// Gets the best available Segoe icon font on this system.
    /// </summary>
    public static string GetAvailableIconFont()
    {
        if (s_cachedFontName is not null)
        {
            return s_cachedFontName;
        }

        using InstalledFontCollection installedFonts = new();
        HashSet<string> availableFonts = new(
            installedFonts.Families.Select(f => f.Name),
            StringComparer.OrdinalIgnoreCase);

        foreach (string fontName in s_preferredFonts)
        {
            if (availableFonts.Contains(fontName))
            {
                s_cachedFontName = fontName;
                return fontName;
            }
        }

        // Fallback to Segoe UI if no icon font found
        s_cachedFontName = "Segoe UI";
        return s_cachedFontName;
    }

    /// <summary>
    /// Renders a symbol character into an image with the specified size.
    /// The symbol is centered in the target size.
    /// </summary>
    /// <param name="symbol">The symbol character to render.</param>
    /// <param name="size">Target image size (width and height).</param>
    /// <param name="foreColor">Foreground color for the symbol.</param>
    /// <param name="backColor">Background color (use Color.Transparent for transparent background).</param>
    /// <returns>A bitmap containing the rendered symbol.</returns>
    public static Bitmap RenderSymbol(string symbol, int size, Color foreColor, Color? backColor = null)
    {
        return RenderSymbol(symbol, new Size(size, size), foreColor, backColor);
    }

    /// <summary>
    /// Renders a symbol character into an image with the specified size.
    /// The symbol is centered in the target size.
    /// </summary>
    /// <param name="symbol">The symbol character to render.</param>
    /// <param name="targetSize">Target image size.</param>
    /// <param name="foreColor">Foreground color for the symbol.</param>
    /// <param name="backColor">Background color (use Color.Transparent for transparent background).</param>
    /// <returns>A bitmap containing the rendered symbol.</returns>
    public static Bitmap RenderSymbol(string symbol, Size targetSize, Color foreColor, Color? backColor = null)
    {
        Bitmap bitmap = new(targetSize.Width, targetSize.Height);
        bitmap.SetResolution(96f, 96f);

        using (Graphics g = Graphics.FromImage(bitmap))
        {
            // High quality rendering settings
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Fill background
            if (backColor.HasValue && backColor.Value != Color.Transparent)
            {
                g.Clear(backColor.Value);
            }
            else
            {
                g.Clear(Color.Transparent);
            }

            // Calculate optimal font size
            // Start with a size slightly smaller than target to allow for centering
            float fontSize = targetSize.Height * 0.7f;
            string fontName = GetAvailableIconFont();

            using Font font = new(fontName, fontSize, FontStyle.Regular, GraphicsUnit.Pixel);
            using SolidBrush brush = new(foreColor);

            // Measure the symbol to center it properly
            SizeF measuredSize = g.MeasureString(symbol, font, PointF.Empty, StringFormat.GenericTypographic);

            // Calculate position to center the symbol
            float x = (targetSize.Width - measuredSize.Width) / 2f;
            float y = (targetSize.Height - measuredSize.Height) / 2f;

            // Draw the symbol
            g.DrawString(symbol, font, brush, x, y, StringFormat.GenericTypographic);
        }

        return bitmap;
    }

    /// <summary>
    /// Renders a symbol that adapts to the current system theme (light/dark mode).
    /// </summary>
    /// <param name="symbol">The symbol character to render.</param>
    /// <param name="size">Target image size (width and height).</param>
    /// <returns>A bitmap containing the rendered symbol in appropriate colors.</returns>
    public static Bitmap RenderThemedSymbol(string symbol, int size)
    {
        // Use SystemColors for theme-aware rendering
        Color foreColor = SystemColors.ControlText;
        return RenderSymbol(symbol, size, foreColor);
    }

    /// <summary>
    /// Creates an ImageList populated with commonly used symbols.
    /// </summary>
    /// <param name="size">Size for each icon in the ImageList.</param>
    /// <returns>An ImageList with keyed symbol images.</returns>
    public static ImageList CreateToolbarImageList(int size = 24)
    {
        ImageList imageList = new()
        {
            ImageSize = new Size(size, size),
            ColorDepth = ColorDepth.Depth32Bit
        };

        Color foreColor = SystemColors.ControlText;

        // Add commonly used toolbar symbols
        imageList.Images.Add("NewDocument", RenderSymbol(Symbols.NewDocument, size, foreColor));
        imageList.Images.Add("OpenFile", RenderSymbol(Symbols.OpenFile, size, foreColor));
        imageList.Images.Add("Save", RenderSymbol(Symbols.Save, size, foreColor));
        imageList.Images.Add("SaveAs", RenderSymbol(Symbols.SaveAs, size, foreColor));
        imageList.Images.Add("Export", RenderSymbol(Symbols.Export, size, foreColor));
        imageList.Images.Add("Play", RenderSymbol(Symbols.Play, size, foreColor));
        imageList.Images.Add("Stop", RenderSymbol(Symbols.Stop, size, foreColor));
        imageList.Images.Add("Pause", RenderSymbol(Symbols.Pause, size, foreColor));
        imageList.Images.Add("Record", RenderSymbol(Symbols.Record, size, foreColor));
        imageList.Images.Add("Settings", RenderSymbol(Symbols.Settings, size, foreColor));
        imageList.Images.Add("Help", RenderSymbol(Symbols.Help, size, foreColor));
        imageList.Images.Add("Info", RenderSymbol(Symbols.Info, size, foreColor));
        imageList.Images.Add("Add", RenderSymbol(Symbols.Add, size, foreColor));
        imageList.Images.Add("Remove", RenderSymbol(Symbols.Remove, size, foreColor));
        imageList.Images.Add("Edit", RenderSymbol(Symbols.Edit, size, foreColor));
        imageList.Images.Add("Copy", RenderSymbol(Symbols.Copy, size, foreColor));
        imageList.Images.Add("Delete", RenderSymbol(Symbols.Delete, size, foreColor));
        imageList.Images.Add("Clock", RenderSymbol(Symbols.Clock, size, foreColor));
        imageList.Images.Add("Timer", RenderSymbol(Symbols.Timer, size, foreColor));
        imageList.Images.Add("Completed", RenderSymbol(Symbols.Completed, size, foreColor));

        return imageList;
    }

    /// <summary>
    /// Creates a button-style image with the symbol and optional text.
    /// </summary>
    /// <param name="symbol">The symbol character.</param>
    /// <param name="text">Optional text below the symbol.</param>
    /// <param name="size">Total button size.</param>
    /// <param name="foreColor">Foreground color.</param>
    /// <param name="backColor">Background color.</param>
    /// <returns>A bitmap for use as a button image.</returns>
    public static Bitmap RenderButtonImage(
        string symbol,
        string? text,
        Size size,
        Color foreColor,
        Color backColor)
    {
        Bitmap bitmap = new(size.Width, size.Height);
        bitmap.SetResolution(96f, 96f);

        using Graphics g = Graphics.FromImage(bitmap);

        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        g.Clear(backColor);

        string fontName = GetAvailableIconFont();

        if (string.IsNullOrEmpty(text))
        {
            // Just the symbol, centered
            float symbolSize = size.Height * 0.6f;
            using Font symbolFont = new(fontName, symbolSize, FontStyle.Regular, GraphicsUnit.Pixel);
            using SolidBrush brush = new(foreColor);

            SizeF measured = g.MeasureString(symbol, symbolFont, PointF.Empty, StringFormat.GenericTypographic);
            float x = (size.Width - measured.Width) / 2f;
            float y = (size.Height - measured.Height) / 2f;

            g.DrawString(symbol, symbolFont, brush, x, y, StringFormat.GenericTypographic);
        }
        else
        {
            // Symbol on top, text below
            float symbolSize = size.Height * 0.45f;
            float textSize = size.Height * 0.18f;

            using Font symbolFont = new(fontName, symbolSize, FontStyle.Regular, GraphicsUnit.Pixel);
            using Font textFont = new("Segoe UI", textSize, FontStyle.Regular, GraphicsUnit.Pixel);
            using SolidBrush brush = new(foreColor);

            // Measure both
            SizeF symbolMeasured = g.MeasureString(symbol, symbolFont, PointF.Empty, StringFormat.GenericTypographic);
            SizeF textMeasured = g.MeasureString(text, textFont);

            float totalHeight = symbolMeasured.Height + textMeasured.Height + 4;
            float startY = (size.Height - totalHeight) / 2f;

            // Draw symbol
            float symbolX = (size.Width - symbolMeasured.Width) / 2f;
            g.DrawString(symbol, symbolFont, brush, symbolX, startY, StringFormat.GenericTypographic);

            // Draw text
            float textX = (size.Width - textMeasured.Width) / 2f;
            float textY = startY + symbolMeasured.Height + 4;
            g.DrawString(text, textFont, brush, textX, textY);
        }

        return bitmap;
    }
}
