namespace Admission.UI.ViewModels
{
    public class Category
    {
        public string Title { get; set; }
        public List<string> SubItems { get; set; }

        public Category()
        {
            SubItems = new List<string>();
        }
    }
}
