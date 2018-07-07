using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

namespace Controllers
{
    public class GameInputController : MonoBehaviour
    {
        [Inject]
        public IGameController GameController { get; set; }

        public Button PauseButton;
        public Button PlayButton;
        public Button ResetButton;
        public Button LeftMoveButton;
        public Button RightMoveButton;

        public void Start()
        {
            PauseButton
                .OnClickAsObservable()
                .Subscribe(_ => GameController.Pause());

            PlayButton
                .OnClickAsObservable()
                .Subscribe(_ => GameController.Resume());

            ResetButton
                .OnClickAsObservable()
                .Subscribe(_ => GameController.Reset());

            LeftMoveButton
                .OnClickAsObservable()
                .Subscribe(_ => GameController.SlideLeft());

            RightMoveButton
                .OnClickAsObservable()
                .Subscribe(_ => GameController.SlideRight());
        }
    }
}