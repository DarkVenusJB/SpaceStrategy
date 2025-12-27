using Utils;

namespace Global.Services.EnvironmentChangerService
{
    public class EnvironmentChangerService : IEnvironmentChangerService
    {
        public EEnvironmentType CurrentEnvironment => _currentEnvironment;
        private EEnvironmentType _currentEnvironment;
        public EnvironmentChangerService(EEnvironmentType initialEnvironment)
        {
            _currentEnvironment = initialEnvironment;
        }

        public void SetEnvironment(EEnvironmentType environment)
        {
            _currentEnvironment = environment;
            
            TestUtilsHandler.Instance.DebugMessageShow(TestUtilsHandler.ELogSource.EnvironmentChangerService,"Changing environment", 
                TestUtilsHandler.ELogColor.Pink, $"new environment {_currentEnvironment}");
        }
    }
}