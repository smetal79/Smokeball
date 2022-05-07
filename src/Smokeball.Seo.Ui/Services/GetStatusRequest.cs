using System.Diagnostics.CodeAnalysis;

namespace Smokeball.Seo.Ui.Services
{
    public record GetStatusRequest([NotNull] string Uri, [NotNull] string Keywords);
}
