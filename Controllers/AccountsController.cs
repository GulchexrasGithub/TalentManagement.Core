// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using TalentManagement.Core.Models.Foundations.Users.Exceptions;
using TalentManagement.Core.Models.Orchestrations.UserTokens;
using TalentManagement.Core.Models.Orchestrations.UserTokens.Exceptions;
using TalentManagement.Core.Services.Orchestrations;

namespace TalentManagement.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountsController : RESTFulController
    {
        private readonly IUserSecurityOrchestrationService userSecurityOrchestrationService;

        public AccountsController(IUserSecurityOrchestrationService userSecurityOrchestrationService) =>
            this.userSecurityOrchestrationService = userSecurityOrchestrationService;

        [HttpPost]
        public async ValueTask<ActionResult<User>> SignUpAsync(User user)
        {
            try
            {

                string requestUrl = $"{Request.Scheme}://{Request.Host.Value}";

                User createdUserAccount = await this.userSecurityOrchestrationService
                    .CreateUserAccountAsync(user, requestUrl);

                return Created(createdUserAccount);
            }
            catch (UserOrchestrationDependencyValidationException userOrchestrationDependencyValidationException)
                when (userOrchestrationDependencyValidationException.InnerException is AlreadyExistsUserException)
            {
                return Conflict(userOrchestrationDependencyValidationException.InnerException);
            }
            catch (UserOrchestrationDependencyValidationException userOrchestrationDependencyValidationException)
            {
                return BadGateway(userOrchestrationDependencyValidationException.InnerException);
            }
            catch (UserOrchestrationDependencyException userOrchestrationDependencyException)
            {
                return InternalServerError(userOrchestrationDependencyException.InnerException);
            }
            catch (UserOrchestrationServiceException userOrchestrationServiceException)
            {
                return InternalServerError(userOrchestrationServiceException.InnerException);
            }
        }

        [HttpGet]
        public ActionResult<UserToken> Login(string email, string password)
        {
            try
            {
                return this.userSecurityOrchestrationService.CreateUserToken(email, password);
            }
            catch (UserTokenOrchestrationValidationException userTokenOrchestrationValidationException)
                when (userTokenOrchestrationValidationException.InnerException is InvalidUserException)

            {
                return BadRequest(userTokenOrchestrationValidationException.InnerException);
            }
            catch (UserTokenOrchestrationValidationException userTokenOrchestrationValidationException)
                when (userTokenOrchestrationValidationException.InnerException is NotFoundUserException)
            {
                return NotFound(userTokenOrchestrationValidationException.InnerException);
            }
            catch (UserOrchestrationDependencyException userTokenOrchestrationDependencyException)
            {
                return InternalServerError(userTokenOrchestrationDependencyException.InnerException);
            }
            catch (UserOrchestrationServiceException userTokenOrchestrationServiceException)
            {
                return InternalServerError(userTokenOrchestrationServiceException.InnerException);
            }
        }
    }
}