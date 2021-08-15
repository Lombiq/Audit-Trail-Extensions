using OrchardCore.Modules.Manifest;
using static Lombiq.AuditTrailExtensions.Constants.FeatureIds;
using static Finitive.AuditTrail.Constants.FeatureIds;

[assembly: Module(
    Name = "Lombiq Audit Trail Extensions",
    Author = "Lombiq Technologies",
    Version = "1.0",
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
        OrchardCore_AuditTrail,
    }
)]
