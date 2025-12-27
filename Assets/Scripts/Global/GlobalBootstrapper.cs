using UnityEngine;

namespace Global
{
    public class GlobalBootstrapper : MonoBehaviour
    {
        private void Start()
        {
            Input.multiTouchEnabled = false;
        }
    }
}