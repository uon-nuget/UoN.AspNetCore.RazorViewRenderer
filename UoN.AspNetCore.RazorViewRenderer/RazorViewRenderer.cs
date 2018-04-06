using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

namespace UoN.AspNetCore.RazorViewRenderer
{
    public class RazorViewRenderer :  IRazorViewRenderer
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;

        public RazorViewRenderer(IServiceProvider serviceProvider, ITempDataProvider tempDataProvider, IRazorViewEngine razorViewEngine)
        {
            _serviceProvider = serviceProvider;
            _tempDataProvider = tempDataProvider;
            _razorViewEngine = razorViewEngine;
        }

        public async Task<string> AsString(string view, object model = null)
        {
            var actionContext = new ActionContext(new DefaultHttpContext {RequestServices = _serviceProvider}, new RouteData(), new ActionDescriptor());

            var viewEngineResult = _razorViewEngine.FindView(actionContext, view, false) ?? throw new ArgumentNullException($"A view does not exist: {view}");

            var viewDataDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };

            using (var stringWriter = new StringWriter())
            {
                var viewContext = new ViewContext(
                    actionContext,
                    viewEngineResult.View,
                    viewDataDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    stringWriter,
                    new HtmlHelperOptions()
                );

                await viewEngineResult.View.RenderAsync(viewContext);
                return stringWriter.ToString();
            }
        }
    }

}
