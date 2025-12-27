using System;
using UnityEngine;

namespace Utils
{
    public class TestUtilsHandler : MonoBehaviour
    {
        public static TestUtilsHandler Instance => _instance ??= FindObjectOfType<TestUtilsHandler>();
        private static TestUtilsHandler _instance;
        
        [Header("Debug Logs enable")] 
        public bool EnvironmentChangerServiceEnabled;
        
        public void DebugMessageShow(
            ELogSource logSource, 
            string title, 
            ELogColor eLogColor = null, 
            string message = "")
        {
            try
            {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
                if ((bool)GetType().GetField(logSource + "Log").GetValue(this) == false)
                    return;
#endif
            }
            catch (Exception e)
            {
                throw new Exception("You didn't add a variable '" + logSource + "Log' for " + logSource +
                                    " in TestUtilsHandler");
            }

#if UNITY_EDITOR ||DEVELOPMENT_BUILD
            if (eLogColor == null)
                eLogColor = ELogColor.Green;

            string offset = "\n \n";

#if UNITY_EDITOR
            if (message != "")
                message = ": </color>" + message;
            else
                message = "</color>";

            Debug.Log("<color=" + ELogColor.White + ">" + "[" + logSource + "] " + "</color>" + "<color=" + eLogColor +
                      ">" + title + message + offset);
#else
            if (message != "")
                message = ": " + message;

            Debug.Log(title + message + offset);
#endif
#endif
        }
        
        public enum ELogSource
        {
           EnvironmentChangerService,
           TickService,
        }

        public class ELogColor
        {
            private ELogColor(string value)
            {
                Value = value;
            }

            public string Value { get; private set; }

            public static ELogColor Red
            {
                get { return new ELogColor("red"); }
            }

            public static ELogColor Green
            {
                get { return new ELogColor("#00ff00"); }
            }

            public static ELogColor Blue
            {
                get { return new ELogColor("#0081ff"); }
            }
            
            public static ELogColor Magenta
            {
                get { return new ELogColor("#fc0fc0"); }
            }

            public static ELogColor Swamp
            {
                get { return new ELogColor("#7c8a58"); }
            }
            
            public static ELogColor Yellow
            {
                get { return new ELogColor("#ddd602"); }
            }

            public static ELogColor Pink
            {
                get { return new ELogColor("#ff8fa2"); }
            }

            public static ELogColor White
            {
                get { return new ELogColor("#ffffff"); }
            }

            public static ELogColor Cyan
            {
                get { return new ELogColor("#21cfcc"); }
            }

            public static ELogColor Orange
            {
                get { return new ELogColor("#ff6a00"); }
            }
        }
        
    }
    
    
}