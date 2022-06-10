using OrchardCore.Modules.Manifest;
using static Lombiq.AuditTrailExtensions.Constants.FeatureIds;

[assembly: Module(
    Name = "Lombiq Audit Trail Extensions",
    Author = "Lombiq Technologies",
    Version = "0.0.1",
    Description = "Module with additional functionality to the stock Audit Trail.",
    Website = "https://github.com/Lombiq/Audit-Trail-Extensions"
)]

[assembly: Feature(
    Id = Default,
    Name = "Lombiq Audit Trail Extensions",
    Category = "Content",
    Description = "Module with additional functionality to the stock Audit Trail.",
    Dependencies = new[]
    {
        "OrchardCore.Contents",
        "OrchardCore.AuditTrail",
    }
)]
