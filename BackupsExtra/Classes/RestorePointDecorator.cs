using Backups.Interfaces;

namespace BackupsExtra.Classes
{
    public abstract class RestorePointDecorator : RestorePointComponent
    {
        public RestorePointDecorator(RestorePointComponent restorePointComponent)
        {
            RestorePointComponent = restorePointComponent;
        }

        public RestorePointComponent RestorePointComponent { get; }
    }
}