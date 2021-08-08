using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Handlers;

namespace Microsoft.AspNetCore.Http
{
    public static class AuditTrailHttpContextExtensions
    {
        public static string GetCurrentUserName(this IHttpContextAccessor hca) => GetCurrentUserName(hca?.HttpContext);

        public static string GetCurrentUserName(this HttpContext httpContext) =>
            httpContext?.User?.Identity?.Name;

        public static ContentItem GetRestoredContentItem(this IHttpContextAccessor hca) =>
            GetRestoredContentItem(hca?.HttpContext);

        public static ContentItem GetRestoredContentItem(this HttpContext httpContext) =>
            httpContext != null &&
            httpContext.Items.TryGetValue("OrchardCore.AuditTrail.Restored", out var contentItemObject)
                ? contentItemObject as ContentItem
                : null;

        public static bool IsContextContentItemBeingRestored(
            this IHttpContextAccessor hca,
            ContentContextBase context) =>
            IsContextContentItemBeingRestored(hca?.HttpContext, context);

        public static bool IsContextContentItemBeingRestored(
            this HttpContext httpContext,
            ContentContextBase context) =>
            IsContentItemBeingRestored(httpContext, context.ContentItem);

        public static bool IsContentItemBeingRestored(this IHttpContextAccessor hca, IContent content) =>
            IsContentItemBeingRestored(hca?.HttpContext, content);

        public static bool IsContentItemBeingRestored(
            this HttpContext httpContext,
            IContent content) =>
            GetRestoredContentItem(httpContext)?.ContentItemId == content.ContentItem.ContentItemId;
    }
}
