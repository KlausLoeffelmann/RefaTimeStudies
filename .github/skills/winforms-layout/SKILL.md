---
name: winforms-layout
description: Comprehensive guide for WinForms layout strategies, DPI-aware design, TableLayoutPanel patterns, and form design best practices. Use this when designing forms, implementing responsive layouts, or working with high-DPI displays.
---

# WinForms Layout and Design Guide

Modern WinForms layout strategies for scalable, DPI-aware, and maintainable form designs.

## When to Use This Skill

Use this skill when:
- Designing new forms or UserControls
- Implementing responsive layouts
- Working with TableLayoutPanel or FlowLayoutPanel
- Ensuring DPI compatibility
- Creating complex nested layouts
- Implementing fullscreen or presentation modes
- Designing modal dialogs

## Core Layout Principles

### Scaling and DPI Awareness

**DPI Settings:**
- Use 96 DPI / 100% for new Forms/UserControls
- Set `AutoScaleMode` appropriately
- For existing forms: Leave `AutoScaleMode` as-is, account for scaling
- .NET 9+: Query DarkMode status via `Application.IsDarkModeEnabled`

**Important:** Only `SystemColors` change automatically in DarkMode. Absolute colors require manual handling.

### Layout Strategy Overview

**Divide and Conquer:**
- Use multiple or nested TableLayoutPanels for logical sections
- Don't cram everything into one mega-grid
- Main form uses SplitContainer or outer TableLayoutPanel with percentage/AutoSize rows/cols
- Each UI section gets its own nested TableLayoutPanel or UserControl

**Keep It Simple:**
- Individual TableLayoutPanels: 2-4 columns maximum
- Use GroupBoxes with nested TableLayoutPanels for visual grouping
- RadioButton clusters: single-column, auto-size-cells TableLayoutPanel inside AutoSize GroupBox
- Large content scrolling: Nested panel controls with `AutoScroll` enabled

## TableLayoutPanel Fundamentals

### Cell Sizing Priority

**For Rows:**
```
AutoSize > Percent > Absolute
```

**For Columns:**
```
AutoSize > Percent > Absolute
```

### Column Sizing Rules

| Size Mode | Use Case | Anchor | Notes |
|-----------|----------|--------|-------|
| **AutoSize** | Caption columns | `Left \| Right` | For labels and prompts |
| **Percent** | Content columns | `Top \| Bottom \| Left \| Right` | Distribute by reasoning |
| **Absolute** | Fixed content only | Varies | Icons, buttons (avoid when possible) |

**CRITICAL:** Never dock cells in TableLayoutPanel, always use Anchor!

### Row Sizing Rules

| Size Mode | Use Case | Notes |
|-----------|----------|-------|
| **AutoSize** | Single-line content | Entry fields, captions, checkboxes |
| **Percent** | Multi-line content | TextBoxes, rendering areas, fill space |
| **Absolute** | Avoid | Only for truly fixed-size content |

### Margin and Padding

**Margins:**
- Set `Margin` on controls (minimum 3px default)
- Margins create spacing between controls

**Padding:**
- `Padding` does NOT affect TableLayoutPanel cells
- Use for container controls only

## Example: Basic Form Layout

```csharp
private void InitializeComponent()
{
    _tableLayoutPanel = new TableLayoutPanel();
    _lblName = new Label();
    _txtName = new TextBox();
    _lblEmail = new Label();
    _txtEmail = new TextBox();
    _btnOK = new Button();
    _btnCancel = new Button();
    
    SuspendLayout();
    _tableLayoutPanel.SuspendLayout();
    
    // TableLayoutPanel setup
    _tableLayoutPanel.ColumnCount = 2;
    _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));      // Labels
    _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); // Inputs
    _tableLayoutPanel.Dock = DockStyle.Fill;
    _tableLayoutPanel.Location = new Point(0, 0);
    _tableLayoutPanel.RowCount = 3;
    _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));    // Name row
    _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));    // Email row
    _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Buttons + fill
    
    // Add controls to cells
    _tableLayoutPanel.Controls.Add(_lblName, 0, 0);
    _tableLayoutPanel.Controls.Add(_txtName, 1, 0);
    _tableLayoutPanel.Controls.Add(_lblEmail, 0, 1);
    _tableLayoutPanel.Controls.Add(_txtEmail, 1, 1);
    
    // Label configuration
    _lblName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
    _lblName.AutoSize = true;
    _lblName.Text = "Name:";
    _lblName.Margin = new Padding(3);
    
    _lblEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
    _lblEmail.AutoSize = true;
    _lblEmail.Text = "Email:";
    _lblEmail.Margin = new Padding(3);
    
    // TextBox configuration
    _txtName.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    _txtName.Margin = new Padding(3);
    
    _txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    _txtEmail.Margin = new Padding(3);
    
    // Button configuration (in separate container)
    FlowLayoutPanel buttonPanel = new FlowLayoutPanel();
    buttonPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    buttonPanel.AutoSize = true;
    buttonPanel.FlowDirection = FlowDirection.LeftToRight;
    buttonPanel.Controls.Add(_btnOK);
    buttonPanel.Controls.Add(_btnCancel);
    
    _tableLayoutPanel.Controls.Add(buttonPanel, 1, 2);
    
    // Form configuration
    Controls.Add(_tableLayoutPanel);
    ClientSize = new Size(400, 200);
    
    _tableLayoutPanel.ResumeLayout(false);
    _tableLayoutPanel.PerformLayout();
    ResumeLayout(false);
}
```

## Complex Nested Layout Pattern

```csharp
// Main form with sections
private void SetupMainLayout()
{
    // Outer TableLayoutPanel: 3 rows
    // Row 0: Toolbar (AutoSize)
    // Row 1: Content (Percent 100%)
    // Row 2: Status bar (AutoSize)
    
    TableLayoutPanel outerLayout = new TableLayoutPanel();
    outerLayout.Dock = DockStyle.Fill;
    outerLayout.RowCount = 3;
    outerLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
    outerLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
    outerLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
    
    // Content area: SplitContainer
    SplitContainer splitContainer = new SplitContainer();
    splitContainer.Dock = DockStyle.Fill;
    splitContainer.Orientation = Orientation.Vertical;
    
    // Left panel: Navigation tree
    splitContainer.Panel1.Controls.Add(CreateNavigationPanel());
    
    // Right panel: Details with nested TableLayoutPanel
    splitContainer.Panel2.Controls.Add(CreateDetailsPanel());
    
    outerLayout.Controls.Add(CreateToolbar(), 0, 0);
    outerLayout.Controls.Add(splitContainer, 0, 1);
    outerLayout.Controls.Add(CreateStatusBar(), 0, 2);
    
    Controls.Add(outerLayout);
}

private TableLayoutPanel CreateDetailsPanel()
{
    TableLayoutPanel detailsPanel = new TableLayoutPanel();
    detailsPanel.Dock = DockStyle.Fill;
    detailsPanel.ColumnCount = 2;
    detailsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
    detailsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
    
    // Add detail fields...
    
    return detailsPanel;
}
```

## RadioButton Cluster Pattern

```csharp
private GroupBox CreateRadioButtonGroup()
{
    GroupBox groupBox = new GroupBox();
    groupBox.Text = "Select Option";
    groupBox.AutoSize = true;
    groupBox.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    
    TableLayoutPanel radioPanel = new TableLayoutPanel();
    radioPanel.Dock = DockStyle.Fill;
    radioPanel.ColumnCount = 1;
    radioPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
    radioPanel.AutoSize = true;
    
    // Add radio buttons
    RadioButton rb1 = new RadioButton { Text = "Option 1", AutoSize = true };
    RadioButton rb2 = new RadioButton { Text = "Option 2", AutoSize = true };
    RadioButton rb3 = new RadioButton { Text = "Option 3", AutoSize = true };
    
    radioPanel.RowCount = 3;
    radioPanel.Controls.Add(rb1, 0, 0);
    radioPanel.Controls.Add(rb2, 0, 1);
    radioPanel.Controls.Add(rb3, 0, 2);
    
    groupBox.Controls.Add(radioPanel);
    return groupBox;
}
```

## Form/Control Initialization Order

### Constructor Execution Chain

**CRITICAL:** WinForms uses OOP inheritance and virtual methods heavily. For inherited controls, the inherited constructor does NOT run first!

**Order of Execution:**
1. Base class field initializers
2. Base class constructor
3. (If overridden) `DefaultSize` of youngest descendant
4. (If overridden) `SetStyle` of youngest descendant
5. Likely all `SetStyle` calls in inheritance chain up to base
6. (If overridden) `CreateParams` of youngest descendant
7. Likely all `CreateParams` calls in inheritance chain up to base
8. All constructor bodies, oldest to youngest descendant
9. Field initializers of youngest descendant
10. Constructor body of youngest descendant

### Why This Matters

**When overriding `CreateParams` or `SetStyle`:**
- Class constructor has NOT run yet
- Accessing backing fields may get `null` or uninitialized values
- Return const values for `DefaultSize` - don't depend on constructor state

**Example:**

```csharp
public class CustomControl : Control
{
    private int _borderWidth;  // Not yet initialized in CreateParams!
    
    public CustomControl()
    {
        _borderWidth = 2;  // This runs AFTER CreateParams
    }
    
    protected override CreateParams CreateParams
    {
        get
        {
            CreateParams cp = base.CreateParams;
            // ❌ WRONG - _borderWidth is still 0 here!
            // cp.ExStyle |= _borderWidth;
            
            // ✅ CORRECT - Use const or literal
            cp.ExStyle |= 2;
            return cp;
        }
    }
    
    protected override Size DefaultSize
    {
        get
        {
            // ✅ CORRECT - Return const values
            return new Size(200, 100);
            
            // ❌ WRONG - Don't depend on fields
            // return new Size(_width, _height);
        }
    }
}
```

## Fullscreen Pattern (Presentations, Kiosk Mode)

### CRITICAL: State Initialization

**Initialize state variables BEFORE entering fullscreen!**

```csharp
public class PresentationForm : Form
{
    private bool _isFullscreen = false;
    private FormWindowState _previousWindowState;
    private FormBorderStyle _previousBorderStyle;
    
    public PresentationForm()
    {
        InitializeComponent();
        
        // CRITICAL: Store state BEFORE first fullscreen call
        _previousWindowState = WindowState;
        _previousBorderStyle = FormBorderStyle;
        
        // Now safe to enter fullscreen
        EnterFullscreen();
    }
    
    private void EnterFullscreen()
    {
        if (_isFullscreen) return;
        
        // Store current state
        _previousWindowState = WindowState;
        _previousBorderStyle = FormBorderStyle;
        
        // Enter fullscreen
        FormBorderStyle = FormBorderStyle.None;
        WindowState = FormWindowState.Maximized;
        _isFullscreen = true;
    }
    
    private void ExitFullscreen()
    {
        if (!_isFullscreen) return;
        
        // Restore previous state
        FormBorderStyle = _previousBorderStyle;
        WindowState = _previousWindowState;
        _isFullscreen = false;
    }
    
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        // Toggle fullscreen with F11
        if (keyData == Keys.F11)
        {
            if (_isFullscreen)
                ExitFullscreen();
            else
                EnterFullscreen();
            return true;
        }
        
        // Exit fullscreen with Escape
        if (keyData == Keys.Escape && _isFullscreen)
        {
            ExitFullscreen();
            return true;
        }
        
        return base.ProcessCmdKey(ref msg, keyData);
    }
}
```

**Without pre-initialization:** Form appears offset (approximately 1/8th screen) on first fullscreen.

## Modal Dialog Patterns

### Dialog Button Configuration

| Aspect | Rule |
|--------|------|
| Primary button (OK) | `AcceptButton` property, `DialogResult = OK` |
| Secondary button (Cancel) | `CancelButton` property, `DialogResult = Cancel` |
| Close strategy | `DialogResult` applied automatically, no additional code needed |
| Validation | Perform at Form level, never block focus change with `Cancel = true` |

### Example: Modal Dialog

```csharp
public class InputDialog : Form
{
    private TableLayoutPanel _layoutPanel;
    private Label _lblPrompt;
    private TextBox _txtInput;
    private FlowLayoutPanel _buttonPanel;
    private Button _btnOK;
    private Button _btnCancel;
    
    public string InputValue => _txtInput.Text;
    
    public InputDialog(string prompt)
    {
        InitializeComponent();
        _lblPrompt.Text = prompt;
        
        // Dialog configuration
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        
        // Button assignment
        AcceptButton = _btnOK;
        CancelButton = _btnCancel;
    }
    
    private void InitializeComponent()
    {
        // Setup controls...
        
        // Button configuration
        _btnOK.DialogResult = DialogResult.OK;
        _btnOK.Text = "OK";
        
        _btnCancel.DialogResult = DialogResult.Cancel;
        _btnCancel.Text = "Cancel";
        
        // No Click handlers needed - DialogResult handles closing!
    }
}

// Usage:
using (InputDialog dialog = new InputDialog("Enter name:"))
{
    if (dialog.ShowDialog() == DialogResult.OK)
    {
        string value = dialog.InputValue;
        // Process value...
    }
}
```

### .NET 8+ DataContext Pattern

```csharp
public class PersonEditDialog : Form
{
    private PersonViewModel _viewModel;
    
    public PersonEditDialog(PersonViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);
        _viewModel = viewModel;
        
        InitializeComponent();
        
        // Set DataContext for binding (.NET 8+)
        DataContext = _viewModel;
    }
    
    // DataContext flows to child controls automatically
}
```

## Button Layout Pattern (High-DPI Aware)

### Goal

Ensure buttons in a logical group (OK/Cancel or Yes/No/Cancel) have:
- Same size
- Never clip text
- Scale properly with DPI

### Case 1: Inside TableLayoutPanel

**Same Row:**
```csharp
// Buttons in same row
_btnOK.AutoSize = true;
_btnOK.AutoSizeMode = AutoSizeMode.GrowAndShrink;
_btnOK.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

_btnCancel.AutoSize = true;
_btnCancel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
_btnCancel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

// Row must be AutoSize
tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
```

**Same Column:**
```csharp
// Buttons in same column
_btnYes.AutoSize = true;
_btnYes.AutoSizeMode = AutoSizeMode.GrowAndShrink;
_btnYes.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;

// Column must be AutoSize
tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
```

### Case 2: Reusable ButtonContainer

For all other cases, implement a reusable `ButtonContainer`:

```csharp
public class ButtonContainer : FlowLayoutPanel
{
    private bool _isInLayout = false;
    
    public ButtonContainer()
    {
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        FlowDirection = FlowDirection.LeftToRight;
        MinimumSize = new Size(300, 200);
    }
    
    protected override void OnControlAdded(ControlEventArgs e)
    {
        base.OnControlAdded(e);
        
        // Accept ONLY Button controls
        if (e.Control is not Button button)
        {
            Controls.Remove(e.Control);
            throw new InvalidOperationException(
                "ButtonContainer can only contain Button controls.");
        }
        
        // Configure button
        button.AutoSize = true;
        button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        
        // Reset MinimumSize when controls added
        MinimumSize = Size.Empty;
    }
    
    protected override void OnLayout(LayoutEventArgs levent)
    {
        // Guard against re-entrancy
        if (_isInLayout)
        {
            base.OnLayout(levent);
            return;
        }
        
        try
        {
            _isInLayout = true;
            
            // First pass: Clear button MinimumSize, measure all
            int maxWidth = 0;
            int maxHeight = 0;
            
            foreach (Control control in Controls)
            {
                if (control is Button button)
                {
                    button.MinimumSize = Size.Empty;  // MUST use Size.Empty!
                    button.PerformLayout();
                    
                    Size preferred = button.PreferredSize;
                    maxWidth = Math.Max(maxWidth, preferred.Width);
                    maxHeight = Math.Max(maxHeight, preferred.Height);
                }
            }
            
            // Second pass: Apply uniform MinimumSize
            Size uniformSize = new Size(maxWidth, maxHeight);
            foreach (Control control in Controls)
            {
                if (control is Button button)
                {
                    // Only update if changed (avoid layout thrashing)
                    if (button.MinimumSize != uniformSize)
                    {
                        button.MinimumSize = uniformSize;
                    }
                }
            }
            
            base.OnLayout(levent);
        }
        finally
        {
            _isInLayout = false;
        }
    }
    
    protected override void OnControlRemoved(ControlEventArgs e)
    {
        base.OnControlRemoved(e);
        
        // Restore MinimumSize when last control removed
        if (Controls.Count == 0)
        {
            MinimumSize = new Size(300, 200);
        }
    }
}

// Usage:
ButtonContainer buttonContainer = new ButtonContainer();
buttonContainer.Controls.Add(new Button { Text = "OK" });
buttonContainer.Controls.Add(new Button { Text = "Cancel" });
form.Controls.Add(buttonContainer);
```

**Critical Implementation Details:**
- Use `_isInLayout` flag to prevent recursion
- Use `Size.Empty` when clearing `MinimumSize` (not `new Size(0,0)`)
- Only update `MinimumSize` if value changed
- Call base methods FIRST in override methods

## Window Position Management

### Saving and Restoring

**CRITICAL:** Use `Form.RestoreBounds`, not `Location` or `Size`!

```csharp
public class MainForm : Form
{
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        
        // ✅ CORRECT - Use RestoreBounds
        Properties.Settings.Default.WindowX = RestoreBounds.X;
        Properties.Settings.Default.WindowY = RestoreBounds.Y;
        Properties.Settings.Default.WindowWidth = RestoreBounds.Width;
        Properties.Settings.Default.WindowHeight = RestoreBounds.Height;
        Properties.Settings.Default.WindowState = WindowState;
        Properties.Settings.Default.Save();
    }
    
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        
        // Restore position
        if (Properties.Settings.Default.WindowWidth > 0)
        {
            StartPosition = FormStartPosition.Manual;
            Location = new Point(
                Properties.Settings.Default.WindowX,
                Properties.Settings.Default.WindowY);
            Size = new Size(
                Properties.Settings.Default.WindowWidth,
                Properties.Settings.Default.WindowHeight);
            WindowState = Properties.Settings.Default.WindowState;
        }
    }
}
```

**Why RestoreBounds:**
- `Location` and `Size` return incorrect values when maximized/minimized
- `RestoreBounds` always contains last normal (non-maximized/minimized) position
- Read `RestoreBounds` no earlier than `Form.Closing` event

## Accessibility

### Essential Accessibility Features

```csharp
// Set AccessibleName and AccessibleDescription
_txtName.AccessibleName = "Name";
_txtName.AccessibleDescription = "Enter the person's full name";

// Maintain logical TabIndex
_txtName.TabIndex = 0;
_txtEmail.TabIndex = 1;
_btnOK.TabIndex = 2;
_btnCancel.TabIndex = 3;

// Verify keyboard-only navigation works
// Test with Tab, Shift+Tab, Enter, Escape
```

## TreeView and ListView Patterns

### TreeView

```csharp
private void SetupTreeView()
{
    treeView.Nodes.Clear();
    
    // Visible, expanded root
    TreeNode root = new TreeNode("Root");
    root.Nodes.Add("Child 1");
    root.Nodes.Add("Child 2");
    root.Expand();
    
    treeView.Nodes.Add(root);
}
```

### ListView

**Prefer ListView over DataGridView for small lists:**

```csharp
private void SetupListView()
{
    listView.View = View.Details;
    listView.FullRowSelect = true;
    listView.GridLines = true;
    
    // Add columns
    listView.Columns.Add("Name", -1);    // -1 = size to content
    listView.Columns.Add("Email", -2);   // -2 = size to header
    listView.Columns.Add("Phone", 100);  // Fixed width
    
    // Add items in code (NOT designer)
    ListViewItem item = new ListViewItem("John Doe");
    item.SubItems.Add("john@example.com");
    item.SubItems.Add("555-1234");
    listView.Items.Add(item);
}
```

**Column Width Options:**
- `-1` - Size to widest content
- `-2` - Size to column header
- `100` - Fixed pixel width

## Resources and Localization

### Resource Files for UI Strings

```csharp
// ❌ WRONG - Hardcoded strings
_lblName.Text = "Name:";
_btnOK.Text = "OK";

// ✅ CORRECT - Resource strings
_lblName.Text = Resources.Label_Name;
_btnOK.Text = Resources.Button_OK;
```

### Layout Considerations for Localization

- Account for longer translations (German, Finnish typically longest)
- Use AutoSize where possible
- Test with longest expected translations
- Use TableLayoutPanel percentage columns for flexibility
- Avoid fixed-width columns for text content

## Best Practices Summary

### DO

✅ Use TableLayoutPanel for structured layouts
✅ Prefer AutoSize > Percent > Absolute sizing
✅ Set Anchor on controls in TableLayoutPanel cells
✅ Use nested TableLayoutPanels for complex layouts
✅ Set Margin on controls (min 3px)
✅ Use RestoreBounds for window position
✅ Initialize fullscreen state before first call
✅ Use GroupBoxes for visual grouping
✅ Implement proper accessibility features
✅ Use resource files for all UI strings

### DON'T

❌ Dock controls in TableLayoutPanel cells
❌ Create one mega-grid for entire form
❌ Use Absolute sizing unless necessary
❌ Forget Padding doesn't work in TableLayoutPanel
❌ Save Location/Size when maximized
❌ Skip accessibility properties
❌ Hardcode UI strings
❌ Design layouts that don't scale

## Troubleshooting

**Controls not sizing correctly:**
- Check AutoSize and AutoSizeMode settings
- Verify Anchor properties
- Ensure row/column SizeType is appropriate
- Check Margin values

**Layout not responsive:**
- Use Percent sizing instead of Absolute
- Verify Anchor includes all needed sides
- Check that parent containers allow growth

**DPI scaling issues:**
- Verify AutoScaleMode is set correctly
- Use percentage-based layouts
- Test on different DPI settings
- Avoid hard-coded pixel values

**Fullscreen appears offset:**
- Initialize state variables in constructor
- Store WindowState and FormBorderStyle before first fullscreen

## Summary

Effective WinForms layout requires:
- Strategic use of TableLayoutPanel and nesting
- Proper AutoSize/Percent/Absolute sizing decisions
- Correct Anchor settings on controls
- DPI awareness and testing
- Accessibility consideration
- Localization planning

Follow these patterns for layouts that scale properly across DPI settings, support accessibility, and remain maintainable.
