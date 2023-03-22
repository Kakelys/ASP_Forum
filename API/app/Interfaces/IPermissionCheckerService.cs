namespace app.Interfaces
{
    public interface IPermissionCheckerService
    {
        Task CheckUserPermission(int senderId, int targetId, string? forbiddenMessage, params string[] privilegedRoles);
    }
}