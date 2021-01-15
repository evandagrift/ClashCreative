using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RoyaleTrackerAPI
{
    public class EmployeeWithMoreYearsHandler : AuthorizationHandler<EmployeeWithMoreYearsRequirement>
    {
        private IEmployeeNumberOfYears employeeNumberOfYears;

        public EmployeeWithMoreYearsHandler(IEmployeeNumberOfYears employeeNumberOfYears)
        {
            this.employeeNumberOfYears = employeeNumberOfYears;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EmployeeWithMoreYearsRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == ClaimTypes.Name))
            {
                return Task.CompletedTask;
            }
            var name = context.User.FindFirst(c => c.Type == ClaimTypes.Name);

            var yearsOfExperience = employeeNumberOfYears.Get(name.Value);

            if(yearsOfExperience >= requirement.Years)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;

        }
    }
}
