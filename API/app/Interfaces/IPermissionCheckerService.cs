namespace app.Interfaces
{
    public interface IPermissionCheckerService
    {
        Task CheckUserPermisison(int senderId, int targetId, string forbiddenMessage, params string[] privilegedRoles);
    }
}