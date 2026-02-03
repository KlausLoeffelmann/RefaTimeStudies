using System.ComponentModel;

namespace TimeStudiesApp.Models;

/// <summary>
/// Represents a time study definition template (Zeitaufnahme-Definition).
/// Defines what process steps to measure.
/// </summary>
public class TimeStudyDefinition : INotifyPropertyChanged
{
    private Guid _id = Guid.NewGuid();
    private string _name = string.Empty;
    private string _description = string.Empty;
    private DateTime _createdAt = DateTime.Now;
    private DateTime _modifiedAt = DateTime.Now;
    private bool _isLocked;
    private List<ProcessStepDefinition> _processSteps = new();
    private int _defaultProcessStepOrderNumber;

    /// <summary>
    /// Unique identifier for this definition.
    /// </summary>
    public Guid Id
    {
        get => _id;
        set
        {
            if (_id != value)
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
    }

    /// <summary>
    /// Name of the time study definition.
    /// </summary>
    public string Name
    {
        get => _name;
        set
        {
            if (_name != value)
            {
                _name = value ?? string.Empty;
                OnPropertyChanged(nameof(Name));
            }
        }
    }

    /// <summary>
    /// Description of the time study definition.
    /// </summary>
    public string Description
    {
        get => _description;
        set
        {
            if (_description != value)
            {
                _description = value ?? string.Empty;
                OnPropertyChanged(nameof(Description));
            }
        }
    }

    /// <summary>
    /// Date and time when this definition was created.
    /// </summary>
    public DateTime CreatedAt
    {
        get => _createdAt;
        set
        {
            if (_createdAt != value)
            {
                _createdAt = value;
                OnPropertyChanged(nameof(CreatedAt));
            }
        }
    }

    /// <summary>
    /// Date and time when this definition was last modified.
    /// </summary>
    public DateTime ModifiedAt
    {
        get => _modifiedAt;
        set
        {
            if (_modifiedAt != value)
            {
                _modifiedAt = value;
                OnPropertyChanged(nameof(ModifiedAt));
            }
        }
    }

    /// <summary>
    /// True if recordings exist for this definition. Locked definitions cannot be edited.
    /// </summary>
    public bool IsLocked
    {
        get => _isLocked;
        set
        {
            if (_isLocked != value)
            {
                _isLocked = value;
                OnPropertyChanged(nameof(IsLocked));
            }
        }
    }

    /// <summary>
    /// List of process steps in this definition.
    /// </summary>
    public List<ProcessStepDefinition> ProcessSteps
    {
        get => _processSteps;
        set
        {
            if (_processSteps != value)
            {
                _processSteps = value ?? new List<ProcessStepDefinition>();
                OnPropertyChanged(nameof(ProcessSteps));
            }
        }
    }

    /// <summary>
    /// Order number of the default process step (catch-all step).
    /// </summary>
    public int DefaultProcessStepOrderNumber
    {
        get => _defaultProcessStepOrderNumber;
        set
        {
            if (_defaultProcessStepOrderNumber != value)
            {
                _defaultProcessStepOrderNumber = value;
                OnPropertyChanged(nameof(DefaultProcessStepOrderNumber));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Gets the default process step, or null if none is designated.
    /// </summary>
    public ProcessStepDefinition? GetDefaultProcessStep()
    {
        return ProcessSteps.FirstOrDefault(s => s.OrderNumber == DefaultProcessStepOrderNumber);
    }

    /// <summary>
    /// Gets a process step by its order number.
    /// </summary>
    public ProcessStepDefinition? GetProcessStep(int orderNumber)
    {
        return ProcessSteps.FirstOrDefault(s => s.OrderNumber == orderNumber);
    }

    /// <summary>
    /// Creates a deep copy of this definition with a new ID and unlocked state.
    /// </summary>
    public TimeStudyDefinition CreateCopy()
    {
        var copy = new TimeStudyDefinition
        {
            Id = Guid.NewGuid(),
            Name = Name + " (Copy)",
            Description = Description,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            IsLocked = false,
            DefaultProcessStepOrderNumber = DefaultProcessStepOrderNumber,
            ProcessSteps = ProcessSteps.Select(s => s.Clone()).ToList()
        };

        return copy;
    }
}
