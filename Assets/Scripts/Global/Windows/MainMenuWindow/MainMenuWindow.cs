using Global.Services.WindowProviderService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Windows.MainMenuWindow
{
    public class MainMenuWindow : WindowViewBase<MainMenuWindowPresenter>
    {
        [SerializeField] private Button startGame;
        [SerializeField] private Button loadingGame;
        [SerializeField] private Button settings;
        [SerializeField] private Button exit;
        [SerializeField] private TMP_Text versionText;

        protected override void Show()
        {
            versionText.text = $"v{Application.version}";
            
            startGame.onClick.RemoveAllListeners();
            loadingGame.onClick.RemoveAllListeners();
            settings.onClick.RemoveAllListeners();
            exit.onClick.RemoveAllListeners();

            startGame.onClick.AddListener(OnStartGame);
            loadingGame.onClick.AddListener(OnLoadingGame);
            settings.onClick.AddListener(OnSettings);
            exit.onClick.AddListener(OnExit);
        }

        private void OnStartGame()
        {
            //TODO: Инициализация новой игры
            //TODO: Загрузка стартовой сцены
            //TODO: Закрытие главного меню
        }

        private void OnLoadingGame()
        {
            //TODO: Открытие окна загрузки
            //TODO: Загрузка списка сохранений
            //TODO: Обработка выбора сохранения
        }

        private void OnSettings()
        {
            //TODO: Открытие окна настроек
            //TODO: Инициализация параметров
            //TODO: Настройка контролов
        }

        private void OnExit()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
