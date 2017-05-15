namespace SkyTimer.Service
{
    public interface IRenameDialogService
    {
        bool Rename();
        string NewName { get; }
    }
}
