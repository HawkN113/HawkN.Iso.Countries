namespace Country.Reference.Iso3166.Models;

/// <summary>
/// Represents the result of a country or code validation operation.
/// </summary>
public sealed class ValidationResult
{
    private ValidationResult() { }

    /// <summary>
    /// Creates a successful validation result.
    /// </summary>
    public static ValidationResult Success() =>
        new()
        {
            Reason = null,
            ValidationType = ValidationType.None,
            IsValid = true
        };

    /// <summary>
    /// Creates a failed validation result with a reason and validation type.
    /// </summary>
    /// <param name="reason">The reason why the validation failed.</param>
    /// <param name="type">The type of validation that failed.</param>
    public static ValidationResult Failure(string reason, ValidationType type = ValidationType.None) =>
        new()
        {
            Reason = reason,
            ValidationType = type,
            IsValid = false
        };

    /// <summary>
    /// Indicates whether the validation succeeded.
    /// </summary>
    public bool IsValid { get; init; }

    /// <summary>
    /// Provides the reason why validation failed, if applicable.
    /// </summary>
    public string? Reason { get; init; }

    /// <summary>
    /// The type of validation performed.
    /// </summary>
    public ValidationType ValidationType { get; init; }
}

/// <summary>
/// Defines the category of the validated data.
/// </summary>
public enum ValidationType
{
    /// <summary>No validation type specified.</summary>
    None = 0,
    /// <summary>Validation performed on a country code (Alpha-2, Alpha-3, or Numeric).</summary>
    Code = 1
}