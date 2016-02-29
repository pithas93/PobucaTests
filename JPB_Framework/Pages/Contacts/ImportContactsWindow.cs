using JPB_Framework.UI_Utilities;

namespace JPB_Framework.Pages.Contacts
{
    public class ImportContactsWindow
    {
        public static void GoTo()
        {
            Commands.ClickImport();
        }

        public static ImportFileCommand FromPath(string filePath)
        {
            GoTo();
            return new ImportFileCommand(filePath);
        }
    }

}
