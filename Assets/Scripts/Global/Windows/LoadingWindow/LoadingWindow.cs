using Global.Services.WindowProviderService;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Global.Windows.LoadingWindow
{
    public class LoadingWindow : WindowViewBase<LoadingWindowPresenter>
    {
        [SerializeField] private Transform loadingIcon;
        [SerializeField] private float duration = 1f;
        [SerializeField] private Ease ease = Ease.Linear;

        protected override void Show()
        {
            LMotion.Create(0f, 360f, duration)
                .WithEase(ease)
                .WithLoops(-1)
                .BindToLocalEulerAnglesZ(loadingIcon);
        }
    }
}