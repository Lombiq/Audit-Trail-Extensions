using static System.Environment;

namespace Finitive.AuditTrail.Services.Models
{
    public static class AuditTrailCreateContextExtensions
    {
        public static void AppendComment(this AuditTrailCreateContextFork context, string newComment, string separator = null)
        {
            separator ??= NewLine;
            context.Comment = !string.IsNullOrEmpty(context.Comment)
                ? context.Comment + separator + newComment
                : newComment;
        }
    }
}
