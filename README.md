# UoN.AspNetCore.RazorViewRenderer

[![License](https://img.shields.io/badge/licence-MIT-blue.svg)](https://opensource.org/licenses/MIT)

# What is it?

This package provides you with a simple method which renders a Razor view as a string. It can optionally recieve a viewModel object to bind to the view, as per the standard behaviour in ASP.Net Core MVC app.

At UoN we use this to setup templates for emails sent by ASP.Net Core applications. However, it can be used for any application which wants to build up a complex string with variable properties or multiple instances of formatting.

# What are its features?

It provides a single method which you can call in your application:

- `AsString(string view, object model)` which returns a string render of the view name provided. The model parameter is optional, but allows you to populate your string with various values.

# Dependencies

The libary targets `netstandard2.0` and depends upon ASP.Net Core 2.0.

If you can use ASP.Net Core 2, you can use this library.

## Usage

Acquire the library via one of the methods below, and add one of the above extension methods to your ASP.Net Core pipeline.

### NuGet

This library will be hosted on nuget.org from `1.0.0` at the latest.

### Build from source

We recommend building with the `dotnet` cli, but since the package targets `netstandard2.0` and depends only on ASP.Net Core 2.0, you should be able to build it in any tooling that supports those requirements.

- Have the .NET Core SDK 2.0 or newer
- `dotnet build`
- Optionally `dotnet pack`
- Reference the resulting assembly, or NuGet package.

### An example:

#### `ExampleService.cs`

```csharp
public class ExampleService : IExampleService
{  
  private readonly IRazorViewRenderer _razorViewRenderer;
  private readonly IEmailClient _emailClient;
  
  public ExampleService(IRazorViewRenderer razorViewRenderer, IEmailClient _emailClient)
  {
      _razorViewRenderer = razorViewRenderer;
      _emailClient = emailClient;
  }

  public void SendEmail(string title, string surname, DateTime expiry)
  {
      var person = new Person { Title = title, Surname = surname, ExpiryDate = expiry };
      var message = _renderViewService.AsString("ExpiryEmail", person);
      
      _emailClient.Send(message);
  }
}
```
#### 'ExpiryEmail.cshtml'

```cshtml
@model Example.Models.Person

Dear @Model.Title.@Model.Surname,

This is to remind you that you that the record you provided with us is due to expire on the @Model.ExpiryDate.

Please login to your account or contact us to update your details.

Kind Regards,

Example Team
```

## Contributing

Contributions are unlikely to be needed often as this is a library with a very specific purpose.

If there are issues open, please feel free to make pull requests for them, and they will be reviewed.
