---
name: WinForms Rendering Expert
description: Comprehensive guide for GDI/GDI+ rendering, typography, and graphics best practices in WinForms.
---

# WinForms Rendering Expert Guide

WinForms supports **GDI+** (modern, object-oriented) and **GDI** (legacy, performance-optimized via TextRenderer).

## Resource Management

### Disposable GDI+ Objects

**MUST dispose:** `Graphics` (only when _you_ create it), `Pen`, `Brush`, `Font`, `Image`, `Region`, `GraphicsPath`, `StringFormat`.

**CRITICAL Graphics Disposal:**
```csharp
// ✅ DISPOSE - You created it
using Graphics g = pictureBox.CreateGraphics();
using Graphics g2 = Graphics.FromImage(bitmap);

// ❌ DO NOT DISPOSE - Framework owns it
protected override void OnPaint(PaintEventArgs e)
{
    e.Graphics.DrawRectangle(Pens.Blue, 10, 10, 80, 80);
    // DO NOT dispose e.Graphics!
}
```

### System Resources (NEVER Dispose)

**NEVER dispose:** `SystemBrushes.*`, `SystemPens.*`, `SystemColors.*` - shared, framework-managed resources.

```csharp
// ✅ SAFE
Brush brush = SystemBrushes.Control;
Pen pen = SystemPens.ControlText;
// ❌ WRONG: SystemBrushes.Control.Dispose();
```

### Caching Pattern

```csharp
public class CustomControl : Control
{
    private readonly Pen _borderPen = new(Color.Blue, 2f);
    private readonly Font _titleFont = new("Segoe UI", 14f, FontStyle.Bold);

    protected override void OnPaint(PaintEventArgs e)
    {
        e.Graphics.DrawRectangle(_borderPen, ClientRectangle);
        e.Graphics.DrawString("Title", _titleFont, Brushes.Black, 10, 10);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _borderPen.Dispose();
            _titleFont.Dispose();
        }
        base.Dispose(disposing);
    }
}
```

## Setup Custom Control with owner content rendering  (CRITICAL)

**ALWAYS set these for custom controls with OnPaint:**

```csharp
public CustomControl()
{
    // Prevent flicker
    DoubleBuffered = true;
    
    // CRITICAL: Prevent paint artifacts during resize
    SetStyle(ControlStyles.ResizeRedraw, true);
}
```

**Without `ResizeRedraw`:** Old paint content remains during resize, causing visual artifacts/smearing.

## Pen Width and Drawing Boundaries

**Fundamental:** Pen width draws AROUND coordinates.

```csharp
// 1-pixel pen: exactly on coordinates
// 3-pixel pen: 1px each side + 1px on coordinate

private void DrawBorderedRectangle(Graphics g, Rectangle bounds, float penWidth)
{
    using Pen pen = new(Color.Black, penWidth);
    float halfPen = penWidth / 2f;
    
    RectangleF drawRect = new(
        bounds.X + halfPen, bounds.Y + halfPen,
        bounds.Width - penWidth, bounds.Height - penWidth);
    
    g.DrawRectangle(pen, drawRect.X, drawRect.Y, drawRect.Width, drawRect.Height);
}
```

**Pixel-Perfect vs Anti-Aliased:**
```csharp
// Crisp 1-pixel lines
e.Graphics.SmoothingMode = SmoothingMode.None;
e.Graphics.DrawLine(pen, 10, 10, 100, 10);  // Integer coords

// Smooth anti-aliased lines
e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
e.Graphics.DrawLine(pen, 10.5f, 10.5f, 100.5f, 10.5f);  // Half-pixel offsets
```

## Double Buffering

**Always enable to prevent flicker:**
```csharp
public CustomControl()
{
    DoubleBuffered = true;
    // OR
    SetStyle(ControlStyles.OptimizedDoubleBuffer 
    | ControlStyles.AllPaintingInWmPaint 
    | ControlStyles.UserPaint, true);
}
```

**Manual (advanced):**
```csharp
private BufferedGraphicsContext? _context;
private BufferedGraphics? _buffer;

protected override void OnPaint(PaintEventArgs e)
{
    _context ??= BufferedGraphicsManager.Current;
    _buffer ??= _context.Allocate(e.Graphics, ClientRectangle);
    
    _buffer.Graphics.Clear(BackColor);
    DrawContent(_buffer.Graphics);
    _buffer.Render(e.Graphics);
}
```

**CRITICAL:** Never call `Invalidate()` from `OnPaint` - causes infinite loop!

## Quality Modes

### SmoothingMode (Shapes/Lines)

```csharp
e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;     // Smooth curves/diagonals
e.Graphics.SmoothingMode = SmoothingMode.None;          // Crisp pixel-perfect
```

**Use:** AntiAlias for curves/diagonals, None for grids/pixel art.

### TextRenderingHint (Text Quality)

```csharp
e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;  // Small text (< 12pt)
e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;         // Large text (> 20pt)
e.Graphics.TextRenderingHint = TextRenderingHint.SystemDefault;     // Match user settings
```

### InterpolationMode (Image Scaling)

```csharp
g.InterpolationMode = InterpolationMode.HighQualityBicubic;  // Downscaling photos
g.InterpolationMode = InterpolationMode.NearestNeighbor;     // Pixel art
```

### CompositingMode/Quality

```csharp
e.Graphics.CompositingMode = CompositingMode.SourceOver;         // Normal alpha blending
e.Graphics.CompositingQuality = CompositingQuality.HighQuality;  // Smooth blending
```

## Offscreen Rendering

**When to use:** Complex compositions, rounded corners with transparency, avoiding flicker, caching.

```csharp
public class RoundedControl : Control
{
    private Bitmap? _offscreenBuffer;

    protected override void OnPaint(PaintEventArgs e)
    {
        // Resize buffer only when growing
        if (_offscreenBuffer is null || _offscreenBuffer.Width < Width || _offscreenBuffer.Height < Height)
        {
            _offscreenBuffer?.Dispose();
            _offscreenBuffer = new Bitmap(Width, Height);
        }

        using (Graphics g = Graphics.FromImage(_offscreenBuffer))
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.Clear(Color.Transparent);
            DrawContent(g);
        }

        e.Graphics.DrawImage(_offscreenBuffer, 0, 0);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) _offscreenBuffer?.Dispose();
        base.Dispose(disposing);
    }
}
```

## Text Measurement

**CRITICAL: For precise measurements (especially monospace fonts), use `StringFormat.GenericTypographic`:**

```csharp
// ✅ PRECISE - Reduces padding, critical for layout calculations
using StringFormat format = StringFormat.GenericTypographic;
SizeF size = g.MeasureString(text, font, PointF.Empty, format);

// ⚠️ INACCURATE - Includes extra padding
SizeF size = g.MeasureString(text, font);  // Causes cumulative errors
```

**Use `GenericTypographic` for:**
- Monospace fonts (Lucida Console, Consolas, Courier New)
- Mixed-style text runs (different fonts/sizes in same line)
- Precise character positioning
- Typography/layout calculations

**GDI alternative (more accurate for UI, but different API):**
```csharp
Size size = TextRenderer.MeasureText("Text", font);
```

**CRITICAL:** Always measure with the SAME Graphics object and settings used for drawing!
**CRITICAL:** Never mix GDI (`TextRenderer`) and GDI+ (`Graphics.DrawString`) for measurement/drawing.

### Line Spacing (Non-Linear Growth)

```csharp
private float CalculateLineSpacing(Font font, Graphics g)
{
    float baseHeight = font.GetHeight(g);
    if (font.Size > 12f)
    {
        float additionalSpacing = (font.Size - 12f) * 0.7f;  // Flattened growth
        return baseHeight + additionalSpacing;
    }
    return baseHeight * 1.2f;
}
```

## GDI vs GDI+ Selection

**Use GDI (TextRenderer):**
- Large amounts of text (grids, lists)
- UI text matching system appearance
- Performance-critical rendering
- Match TextBox/Label rendering

**Use GDI+ (Graphics.DrawString):**
- Rotated/transformed text
- Custom graphics effects (shadows, outlines)
- Mixed with other GDI+ primitives
- Alpha blending/transparency
- Scnearios, where `Run`-Collection approach for dynamic, 
  presize typography and text wrapping is essential!

## DPI and Scaling

```csharp
protected override void OnPaint(PaintEventArgs e)
{
    float dpiX = e.Graphics.DpiX;
    float scaledPenWidth = 2f * (dpiX / 96f);
    
    using Pen pen = new(Color.Black, scaledPenWidth);
    e.Graphics.DrawRectangle(pen, 10, 10, 100, 100);
}

// Physical size calculation
private float InchesToPixels(float inches, float dpi) => inches * dpi;
```

**IMPORTANT:** Font sizes are already DPI-aware - don't manually scale `Font.Size`!

## Dark Mode (.NET 9+)

```csharp
protected override void OnPaint(PaintEventArgs e)
{
    bool isDarkMode = Application.IsDarkModeEnabled;
    
    // SystemColors adapt automatically
    using SolidBrush backBrush = new(SystemColors.Window);
    using SolidBrush textBrush = new(SystemColors.WindowText);

    **Why that matters?**
    The name `SystemColors.ControlDark` e.g. implies an unconditional darker shade.
    In DarkMode, however, this becomes complementary color - thus to be treated like `ControlLight` in DarkMode.
    
    // Custom colors require manual handling
    Color accentColor = isDarkMode 
       ? Color.FromArgb(0, 120, 215) 
       : Color.Blue;
}
```

**Note:** Only `SystemColors` automatically update. 
Absolute colors need manual handling.

## Common Pitfalls

### Memory Leaks
```csharp
// ❌ LEAK - Creating in OnPaint without disposing
protected override void OnPaint(PaintEventArgs e)
{
    Pen pen = new(Color.Black, 2f);  // Never disposed!
    e.Graphics.DrawRectangle(pen, ClientRectangle);
}

// ✅ CORRECT - Using statement
protected override void OnPaint(PaintEventArgs e)
{
    using Pen pen = new(Color.Black, 2f);
    e.Graphics.DrawRectangle(pen, ClientRectangle);
}
```

### Performance Issues
```csharp
// ❌ SLOW - Creating in loop
for (int i = 0; i < 1000; i++)
{
    using Pen pen = new(Color.Red, 1f);
    e.Graphics.DrawLine(pen, 0, i, Width, i);
}

// ✅ FAST - Reuse
using Pen pen = new(Color.Red, 1f);
for (int i = 0; i < 1000; i++)
    e.Graphics.DrawLine(pen, 0, i, Width, i);
```

### Visual Artifacts

When you stroke a rectangle with a non-hairline `Pen`, the stroke is centered on the shape’s edge.
That means part of the stroke can spill outside your intended bounds.

#### ❌ Naive (stroke may extend beyond bounds)
```csharp
using Pen pen = new(Color.Black, 5f);
e.Graphics.DrawRectangle(pen, 0, 0, Width, Height);
```

#### ✅ Reliable: inset manually by half the pen width
```csharp
using Pen pen = new(Color.Black, 5f);
float half = pen.Width / 2f;

// Keep the entire stroke inside the control's client area
e.Graphics.DrawRectangle(
    pen,
    half,
    half,
    Width  - pen.Width,
    Height - pen.Width);
```

#### ✅ Alternative: try `PenAlignment.Inset` (works on _some_ closed shapes)
```csharp
using Pen pen = new(Color.Black, 5f)
{
    Alignment = System.Drawing.Drawing2D.PenAlignment.Inset
};

// Right and bottom edges are "open" in GDI+ rectangles; subtract 1 for pixel-perfect results
e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
```

> ⚠️ Note on `Pen.Alignment`  
> `PenAlignment.Inset` can work on **closed shapes** (rectangles, polygons, etc.), but its effect is **inconsistent** in GDI+.  
> For predictable results - especially with thicker, dashed, or compound pens - prefer the **manual inset** technique above.

---

#### (Optional) Helper: get an inner rectangle for any `RectangleF` + `Pen`
```csharp
static RectangleF InnerRect(RectangleF bounds, Pen pen)
{
    float w = pen?.Width ?? 1f;
    float half = w / 2f;
    return new RectangleF(
        bounds.X + half,
        bounds.Y + half,
        Math.Max(0, bounds.Width  - w),
        Math.Max(0, bounds.Height - w));
}

// Usage:
using Pen pen = new(Color.Black, 5f);
e.Graphics.DrawRectangle(pen, InnerRect(new RectangleF(0, 0, Width, Height), pen));
```

## Best Practices

### DO:
✅ Enable double buffering for custom controls  
✅ Set `ResizeRedraw` style for custom paint controls  
✅ Use `StringFormat.GenericTypographic` for precise text measurement  
✅ Initialize fullscreen state variables before first use  
✅ Dispose GDI+ objects you create  
✅ Use `SystemBrushes`/`SystemPens` for themes  
✅ Cache frequently-used objects as fields  
✅ Set quality modes at start of `OnPaint`  
✅ Account for pen width in calculations  
✅ Use `TextRenderer` for performance  
✅ Scale for DPI awareness  
✅ Test Light and Dark mode  

### DON'T:
❌ Dispose `Graphics` from `PaintEventArgs`  
❌ Dispose `SystemBrushes.*` or `SystemPens.*`  
❌ Create GDI+ objects in tight loops  
❌ Call `Invalidate()` from `OnPaint`  
❌ Manually scale `Font.Size` for DPI  
❌ Assume Light mode colors in Dark mode  
❌ Skip `ResizeRedraw` for custom controls with OnPaint  
❌ Use default `MeasureString` for precise typography  

## Pre-Implementation Checklist

**Before implementing custom rendering:**

| Requirement | Code | Purpose |
|-------------|------|---------|
| Double buffering | `DoubleBuffered = true` | Prevent flicker |
| Resize redraw | `SetStyle(ControlStyles.ResizeRedraw, true)` | Prevent artifacts |
| Text precision | `StringFormat.GenericTypographic` | Accurate measurements |
| Quality modes | Set once at OnPaint start | Consistent rendering |
| Fullscreen state | Initialize before first call | Correct positioning |

## Performance Checklist

1. ✅ Double buffering enabled?
2. ✅ ResizeRedraw style set?
3. ✅ Objects cached as fields?
4. ✅ Using statements for temps?
5. ✅ Minimal quality settings where acceptable?
6. ✅ Offscreen buffers reused?
7. ✅ TextRenderer for large text amounts?
8. ✅ Targeted invalidation (regions not entire control)?

---

Effective rendering requires resource discipline, performance awareness, visual quality, DPI awareness, and theme support.
