using UnityEngine;
using UnityEngine.UI;

namespace Global.Services.WindowProviderService
{
    public class CoreSceneWindowsHandler : MonoBehaviour
    {
        [SerializeField] private CanvasScaler _canvasScaler;
        
        public static CoreSceneWindowsHandler Instance => GetInstance();
        private static CoreSceneWindowsHandler _instance;

        private static CoreSceneWindowsHandler GetInstance()
        {
            if (_instance == null)
                _instance = FindObjectOfType<CoreSceneWindowsHandler>();
            
            return _instance;
        }
    }
}