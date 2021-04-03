using System.Threading.Tasks;
using Owl.RazorTemplate.Tests.SampleTemplates.Models;
using Owl.RazorTemplate.Tests.SampleTemplates.WelcomeEmail;
using Shouldly;
using Volo.Abp.TextTemplating;
using Xunit;

namespace Owl.RazorTemplate.Tests
{
    public class TemplateRenderer_Tests : OwlRazorTemplateTestBase
    {
        private readonly ITemplateRenderer _templateRenderer;

        public TemplateRenderer_Tests()
        {
            _templateRenderer = GetRequiredService<ITemplateRenderer>();
        }

        [Fact]
        public async Task Should_Get_Rendered_Localized_Template_Content_With_Different_Cultures()
        {
            // (await _templateRenderer.RenderAsync(
            //     TestTemplates.WelcomeEmail,
            //     model: new WelcomeEmailModel()
            //     {
            //         Name = "John"
            //     },
            //     cultureName: "en"
            // )).ShouldContain("Welcome John to the abp.io!");

            (await _templateRenderer.RenderAsync(
                TestTemplates.WelcomeEmail,
                model: new WelcomeEmailModel
                {
                    Name = "John"
                },
                cultureName: "tr"
            )).ShouldContain("Merhaba John, abp.io'ya hoşgeldiniz!");

            //"en-US" fallbacks to "en" since "en-US" doesn't exists and "en" is the fallback culture
            (await _templateRenderer.RenderAsync(
                TestTemplates.WelcomeEmail,
                model: new WelcomeEmailModel
                {
                    Name = "John"
                },
                cultureName: "en-US"
            )).ShouldContain("Welcome John to the abp.io!");

            //"fr" fallbacks to "en" since "fr" doesn't exists and "en" is the default culture
            (await _templateRenderer.RenderAsync(
                TestTemplates.WelcomeEmail,
                model: new WelcomeEmailModel
                {
                    Name = "John" //Intentionally written as PascalCase since Scriban supports it
                },
                cultureName: "fr"
            )).ShouldContain("Welcome John to the abp.io!");
        }

        [Fact]
        public async Task Should_Get_Rendered_Localized_Template_Content_With_Stronly_Typed_Model()
        {
            (await _templateRenderer.RenderAsync(
                TestTemplates.WelcomeEmail,
                model: new WelcomeEmailModel("John"),
                cultureName: "en"
            )).ShouldContain("Welcome John to the abp.io!");
        }

        [Fact]
        public async Task Should_Get_Rendered_Inline_Localized_Template()
        {
            (await _templateRenderer.RenderAsync(
                TestTemplates.ForgotPasswordEmail,
                new ForgotPasswordEmailModel("John"),
                cultureName: "en"
            )).ShouldContain("*BEGIN*Hello John, how are you?. Please click to the following link to get an email to reset your password!*END*");
        }

        [Fact]
        public async Task Should_Get_Localized_Numbers()
        {
            (await _templateRenderer.RenderAsync(
                TestTemplates.ShowDecimalNumber,
                new ShowDecimalNumberModel
                {
                    Amount =  123.45M
                },
                cultureName: "en"
            )).ShouldContain("*BEGIN*123.45*END*");

            (await _templateRenderer.RenderAsync(
                TestTemplates.ShowDecimalNumber,
                new ShowDecimalNumberModel
                {
                    Amount =  123.45M
                },
                cultureName: "de"
            )).ShouldContain("*BEGIN*123,45*END*");
        }
    }
}
