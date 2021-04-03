namespace Owl.RazorTemplate.Tests.SampleTemplates.WelcomeEmail
{
    public class WelcomeEmailModel
    {
        public string Name { get; set; }

        public WelcomeEmailModel()
        {

        }

        public WelcomeEmailModel(string name)
        {
            Name = name;
        }
    }
}
