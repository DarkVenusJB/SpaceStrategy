using Cysharp.Threading.Tasks;

namespace Global.Services.EnvironmentChangerService
{
    public interface IEnvironmentChangerService
    {
        UniTask SetEnvironment(EEnvironmentType environment);
    }
}