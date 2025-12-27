using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Global.Services.WindowProviderService
{
    public interface IWindowProviderService
    {
        UniTask ShowWindow<TPresenter, TViewData>(TViewData viewData) where TPresenter : WindowPresenterBaseWithViewData<TViewData>;
        UniTask ShowWindow<TPresenter>() where TPresenter : WindowPresenterBase;
        void CloseWindow<TPresenter>() where TPresenter : WindowPresenterBase;
        bool IsWindowShowing<TPresenter>() where TPresenter : WindowPresenterBase;
    }
}