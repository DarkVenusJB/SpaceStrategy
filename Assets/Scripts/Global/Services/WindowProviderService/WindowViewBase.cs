using UnityEngine;

namespace Global.Services.WindowProviderService
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class WindowViewBase<TWindowPresenter> : MonoBehaviour where TWindowPresenter : WindowPresenterBase
    {
        [SerializeField] protected Animator _baseWindowAnimator;
        
        protected TWindowPresenter _presenter;
        
        private static readonly int ShowTrigger = Animator.StringToHash("Show");
        private static readonly int CloseTrigger = Animator.StringToHash("Close");
        
        public void SetPresenter(TWindowPresenter presenter)
        {
            _presenter = presenter;
        }
        
        public void SubscribeOnEvents()
        {
            _presenter.OnShow -= Show;
            _presenter.OnShow += Show;
            
            _presenter.OnClose -= Close;
            _presenter.OnClose += Close;
        }

        public virtual void Init()
        {
        }

        protected virtual void Show()
        {
            gameObject.SetActive(true);
            
            transform.SetAsLastSibling();

            if (_baseWindowAnimator)
                _baseWindowAnimator.SetTrigger(ShowTrigger);
            else
                _presenter.IsShowAnimationNow = false;
        }

        protected virtual void Close()
        {
            if (_baseWindowAnimator)
            {
                _baseWindowAnimator.SetTrigger(CloseTrigger);
            }
            else
            {
                gameObject.SetActive(false);
                _presenter.SetInNotShowing();
            }
        }

        public virtual void OnShowAnimationEnd()
        {
            _presenter.IsShowAnimationNow = false;
        }
        
        public virtual void OnCloseAnimationEnd()
        {
            gameObject.SetActive(false);
            _presenter.SetInNotShowing();
        }
    }
}