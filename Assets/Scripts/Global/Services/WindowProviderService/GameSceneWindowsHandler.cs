using UnityEngine;
using UnityEngine.UI;

namespace Global.Services.WindowProviderService
{
    public class GameSceneWindowsHandler : MonoBehaviour
    {
        [SerializeField] private CanvasScaler _canvasScaler;
        
        public static GameSceneWindowsHandler Instance => GetInstance();
        private static GameSceneWindowsHandler _instance;

        private static GameSceneWindowsHandler GetInstance()
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameSceneWindowsHandler>();
            
            return _instance;
        }
    }
}