using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Route.Filters
{
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        private MvcOptions mvcOptions;
        private AuthorizationPolicy effectivePolicy;

        public AuthorizationFilter(AuthorizationPolicy policy)
        {
            Policy = policy ?? throw new ArgumentNullException(nameof(policy));
        }

        public AuthorizationFilter(IAuthorizationPolicyProvider policyProvider, IEnumerable<IAuthorizeData> authorizeData)
        {
            PolicyProvider = policyProvider ?? throw new ArgumentNullException(nameof(policyProvider));
            AuthorizeData = authorizeData ?? throw new ArgumentNullException(nameof(authorizeData));
        }

        public AuthorizationPolicy Policy { get; }
        public IAuthorizationPolicyProvider PolicyProvider { get; }
        public IEnumerable<IAuthorizeData> AuthorizeData { get; }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            var effectivePolicy = await GetEffectivePolicyAsync(context);
            if (effectivePolicy == null) return;

            var policyEvaluator = context.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();

            var authenticateResult = await policyEvaluator.AuthenticateAsync(effectivePolicy, context.HttpContext);

            var authorizeResult = await policyEvaluator.AuthorizeAsync(effectivePolicy, authenticateResult, context.HttpContext, context);

            if (authorizeResult.Challenged)
                context.Result = new ChallengeResult(effectivePolicy.AuthenticationSchemes.ToArray());
            else if (authorizeResult.Forbidden)
                context.Result = new ForbidResult(effectivePolicy.AuthenticationSchemes.ToArray());
        }

        private async Task<AuthorizationPolicy> GetEffectivePolicyAsync(AuthorizationFilterContext context)
        {
            if (effectivePolicy != null) return effectivePolicy;
            var currentEffectivePolicy = await ComputePolicyAsync();
            var canCache = PolicyProvider == null;

            if (mvcOptions == null)
                mvcOptions = context.HttpContext.RequestServices.GetRequiredService<IOptions<MvcOptions>>().Value;

            if (mvcOptions.AllowCombiningAuthorizeFilters)
            {
                if (!context.IsEffectivePolicy(this)) return null;
                var builder = new AuthorizationPolicyBuilder(currentEffectivePolicy);
                for (var i = 0; i < context.Filters.Count; i++)
                {
                    if (ReferenceEquals(this, context.Filters[i])) continue;

                    if (context.Filters[i] is AuthorizationFilter authorizationFilter)
                    {
                        builder.Combine(await authorizationFilter.ComputePolicyAsync());
                        canCache = canCache && authorizationFilter.PolicyProvider == null;
                    }
                }
                currentEffectivePolicy = builder?.Build() ?? currentEffectivePolicy;
            }

            if (canCache) effectivePolicy = currentEffectivePolicy;
            return effectivePolicy;
        }

        private Task<AuthorizationPolicy> ComputePolicyAsync()
        {
            if (Policy != null) return Task.FromResult(Policy);
            if (PolicyProvider == null) throw new InvalidOperationException();
            return AuthorizationPolicy.CombineAsync(PolicyProvider, AuthorizeData);
        }
    }
}