# Owl.RazorTemplate

Use razor to build your template!

You can get intellisense in the IDE. Just like writing a razor page!

```cs
[DependsOn(typeof(OwlRazorTemplateModule))]
```

**ForgotPasswordEmail.cshtml**

```cshtml
@using Owl.RazorTemplate.Tests.SampleTemplates.Models;
@inherits Owl.RazorTemplate.RazorTemplatePageBase<ForgotPasswordEmailModel>
@Localizer["HelloText", Model.Name], @Localizer["HowAreYou"]. Please click to the following link to get an email to reset your password!
```
