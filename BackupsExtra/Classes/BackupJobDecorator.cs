using Backups.Classes;
using Backups.Interfaces;
using Backups.Services;

namespace BackupsExtra.Classes
{
    public abstract class BackupJobDecorator : BackupJobComponent
    {
        public BackupJobDecorator(BackupJobComponent backupJob)
        {
            BackupJob = backupJob;
        }

        public BackupJobComponent BackupJob { get; }
        /*Check nullPointerException!!!*/
        public override string CreateRestorePoint()
        {
            return BackupJob.CreateRestorePoint();
        }

        public override JobObject AddJobObject(JobObject jobObject)
        {
            return BackupJob.AddJobObject(jobObject);
        }

        public override void DeleteJobObject(JobObject jobObject)
        {
            BackupJob.DeleteJobObject(jobObject);
        }
    }
}