using UnityEngine;
using Zenject;

namespace Platformer
{
    public class LevelManagerPresenter : MonoBehaviour
    {
        [Inject] LevelData data;
        [Inject] LevelManager manager;
        [Inject] GameUI ui;

        void Start() =>
			manager.OnLevelEnded += ShowFinish;

        private void ShowFinish() =>
            ui.ShowFinishUI(data.FruitsCollected);
    }
}
