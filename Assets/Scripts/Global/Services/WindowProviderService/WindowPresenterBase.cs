using System;
using Cysharp.Threading.Tasks;

namespace Global.Services.WindowProviderService
{
    public abstract class WindowPresenterBase
    {
        public event Action OnShow;
        public event Action OnClose;

        public bool IsShowing { get; protected set; }
        public bool IsShowAnimationNow;

        public void SetInNotShowing()
        {
            IsShowing = false;
        }
        
        public async UniTask ShowWindow()
        {
            if (IsShowing)
                return;
            
            IsShowing = true;
            OnShow?.Invoke();

            IsShowAnimationNow = true;
            
            await UniTask.WaitWhile(() => IsShowing);
        }
        
        public void CloseWindow()
        {
            if (!IsShowing)
                return;
            
            OnClose?.Invoke();
        }
    }
}