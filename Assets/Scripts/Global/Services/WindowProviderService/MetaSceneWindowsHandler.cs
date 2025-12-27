using UnityEngine;
using UnityEngine.UI;

namespace Global.Services.WindowProviderService
{
    public class MetaSceneWindowsHandler : MonoBehaviour
    {
        [SerializeField] private CanvasScaler _canvasScaler;
        
        public static MetaSceneWindowsHandler Instance => GetInstance();
        private static MetaSceneWindowsHandler _instance;

        private static MetaSceneWindowsHandler GetInstance()
        {
            if (_instance == null)
                _instance = FindObjectOfType<MetaSceneWindowsHandler>();
            
            return _instance;
        }
    }
}