using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Global.Services.WindowProviderService
{
    public class WindowProviderService :  IWindowProviderService
    {
        [Inject] private DiContainer _diContainer;
        
        private Dictionary<string, WindowPresenterBase> _pesenters = new();

        public async UniTask ShowWindow<TPresenter, TViewData>(TViewData viewData) where TPresenter : WindowPresenterBaseWithViewData<TViewData>
        {
            WindowPresenterBase windowPresenter = TryGetOrCreatePresenter<TPresenter>();

            WindowPresenterBaseWithViewData<TViewData> presenter = (WindowPresenterBaseWithViewData<TViewData>)windowPresenter;
                
            presenter.SetViewData(viewData);
            
            await windowPresenter.ShowWindow();
        }
        
        public async UniTask ShowWindow<TPresenter>() where TPresenter : WindowPresenterBase
        {
            WindowPresenterBase windowPresenter = TryGetOrCreatePresenter<TPresenter>();

            await windowPresenter.ShowWindow();
        }

        private WindowPresenterBase TryGetOrCreatePresenter<TPresenter>() where TPresenter : WindowPresenterBase
        {
            string windowPresenterTypeName = typeof(TPresenter).Name;
            string windowName = windowPresenterTypeName.Replace("Presenter", "");
            
            Type windowPresenterType = typeof(TPresenter);

            if (!_pesenters.ContainsKey(windowPresenterTypeName))
            {
                var presenter = (TPresenter)Activator.CreateInstance(windowPresenterType);

                GameObject windowViewPrefab = Resources.Load<GameObject>("Windows/" + windowName);
                
                var windowView = _diContainer
                    .InstantiatePrefab(windowViewPrefab, GetWindowHandler(windowViewPrefab))
                    .GetComponent<WindowViewBase<TPresenter>>();
                
                windowView.SetPresenter(presenter);
                windowView.SubscribeOnEvents();
                windowView.Init();

                _pesenters.Add(windowPresenterTypeName, presenter);
            }
            
            return _pesenters[windowPresenterTypeName];
        }

        private Transform GetWindowHandler(GameObject windowViewPrefab)
        {
            switch (LayerMask.LayerToName(windowViewPrefab.layer))
            {
                case "GameSceneWindowsHandler":
                    return GameSceneWindowsHandler.Instance.transform;
                case "MetaScenesWindowsHandler":
                    return MetaSceneWindowsHandler.Instance.transform;
            }
            
            return CoreSceneWindowsHandler.Instance.transform;
        }

        public void CloseWindow<TPresenter>() where TPresenter : WindowPresenterBase
        {
            try
            {
                string windowPresenterTypeName = typeof(TPresenter).Name;
                _pesenters[windowPresenterTypeName].CloseWindow();
            }
            catch (Exception e)
            {
                
            }
        }

        public bool IsWindowShowing<TPresenter>() where TPresenter : WindowPresenterBase
        {
            try
            {
                string windowPresenterTypeName = typeof(TPresenter).Name;
                return _pesenters[windowPresenterTypeName].IsShowing;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public bool IsShowAnimationNow<TPresenter>() where TPresenter : WindowPresenterBase
        {
            try
            {
                string windowPresenterTypeName = typeof(TPresenter).Name;
                return _pesenters[windowPresenterTypeName].IsShowAnimationNow;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
