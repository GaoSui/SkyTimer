using SkyTimer.View;

namespace SkyTimer.Service
{
    public class RenameDialogService : IRenameDialogService
    {
        public bool Rename()
        {
            var dialog = new RenameDialog();
            var res = dialog.ShowDialog();
            NewName = dialog.NewName;

            return (bool)res;
        }

        public string NewName { get; private set; }
    }
}
