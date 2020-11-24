using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace EloBaza.WebApi.ApplicationInsights
{
    class CloudRoleNameInitializer : ITelemetryInitializer
    {
        private readonly string _roleName;

        public CloudRoleNameInitializer()
        {
            _roleName = $"{typeof(CloudRoleNameInitializer).Assembly.GetName().Name}-{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}";
        }

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleName = _roleName;
            telemetry.Context.Cloud.RoleInstance = _roleName;
        }
    }
}
