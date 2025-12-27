namespace Global.Services.WindowProviderService
{
    public class WindowPresenterBaseWithViewData<TViewData> : WindowPresenterBase
    {
        public TViewData ViewData { get; private set; }

        public void SetViewData(TViewData viewData)
        {
            ViewData = viewData;
        }
    }
}