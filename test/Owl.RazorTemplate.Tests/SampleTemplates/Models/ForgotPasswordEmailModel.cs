namespace Owl.RazorTemplate.Tests.SampleTemplates.Models
{
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
}
