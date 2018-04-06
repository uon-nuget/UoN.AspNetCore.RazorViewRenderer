using System.Threading.Tasks;

namespace UoN.AspNetCore.RazorViewRenderer
{
    public interface IRazorViewRenderer
    {
        Task<string> AsString(string view, object model);
    }
}
