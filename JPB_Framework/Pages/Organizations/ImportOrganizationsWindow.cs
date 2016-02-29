using JPB_Framework.UI_Utilities;

namespace JPB_Framework.Pages.Organizations
{
    public class ImportOrganizationsWindow
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

