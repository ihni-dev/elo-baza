namespace Microsoft.Extensions.Hosting
{
    public static partial class HostEnvironmentEnvExtensions
    {
        private const string Local = "Local";
        private const string LocalDocker = "LocalDocker";

        public static bool IsLocal(this IHostEnvironment hostEnvironment)
        {
            return hostEnvironment.IsEnvironment(Local) || hostEnvironment.IsEnvironment(LocalDocker);
        }
    }
}
