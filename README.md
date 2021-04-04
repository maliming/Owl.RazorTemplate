# Owl.RazorTemplate

Use razor to build your template!

You can get intellisense in the IDE. Just like writing a razor page!

![image](https://user-images.githubusercontent.com/6908465/113466932-374e6800-9472-11eb-96d4-69a520ea39e9.png)


```cs
[DependsOn(typeof(OwlRazorTemplateModule))]
```

**ForgotPasswordEmailModel**

```cs
public class ForgotPasswordEmailModel
{
    public string Name { get; set; }

    public ForgotPasswordEmailModel()
    {

    }

    public ForgotPasswordEmailModel(string name)
    {
        Name = name;
    }
}
```

**Layout Page**

```cshtml
@inherits Owl.RazorTemplate.RazorTemplatePageBase

<head>
      <title>Im layout page</title>
</head>
<body>

@Body

</body>
```

**ForgotPasswordEmail.cshtml**
```cshtml
@using System.Linq
@using Owl.RazorTemplate.Tests.SampleTemplates.Models;
@inherits Owl.RazorTemplate.RazorTemplatePageBase<ForgotPasswordEmailModel>

@Localizer["HelloText", Model.Name], @Localizer["HowAreYou"]. Please click to the following link to get an email to reset your password!

@foreach (var number in new[] {5, 4, 1, 3, 9, 8, 6, 7, 2, 0}.OrderBy(x => x))
{
    <span>@number</span>
}
```

**RenderAsync**

```cs
var html = await _templateRenderer.RenderAsync(
    TestTemplates.ForgotPasswordEmail,
    new ForgotPasswordEmailModel("John")
);
```

![image](https://user-images.githubusercontent.com/6908465/113466874-b55e3f00-9471-11eb-9cf6-b50ca2af0c45.png)


