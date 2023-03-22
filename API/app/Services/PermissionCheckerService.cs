using app.Data.Interfaces;
using app.Interfaces;
using app.Shared;

namespace app.Services
{
    public class PermissionCheckerService : IPermissionCheckerService
    {
        private IRepositoryManager _repository;
        public PermissionCheckerService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public async Task CheckUserPermisison(int senderId, int targetId, string forbiddenMessage, params string[] privilegedRoles)
        {
            var sender = await _repository.Account.GetWithRoleById(senderId, false);

            if(sender == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError, "Sender account not found");

            if(sender.Id != targetId && !privilegedRoles.Contains(sender.Role.Name))
                throw new HttpResponseException(System.Net.HttpStatusCode.Forbidden, forbiddenMessage);
        }
    }
}