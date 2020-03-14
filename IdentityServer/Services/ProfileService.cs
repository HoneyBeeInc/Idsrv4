using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdentityServer.Services
{
    public interface IExtendedProfileService : IProfileService
    {
        Task<IEnumerable<Claim>> GetClaimsAsync(string subjectId);
    }
    public class ProfileService : IExtendedProfileService
    {
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            context.IssuedClaims = (await GetClaimsAsync(context.Subject.GetSubjectId())).ToList();
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Claim>> GetClaimsAsync(string subjectId)
        {
            var user = TestUsers.Users.Single(u => u.SubjectId == subjectId);
            return Task.FromResult(new List<Claim>(user.Claims).AsEnumerable());
        }
    }
}
