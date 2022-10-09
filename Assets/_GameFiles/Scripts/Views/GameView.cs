using DG.Tweening;
using TadPoleFramework.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TadPoleFramework
{
    public class GameView : BaseView
    {
        [SerializeField] private Button shuffleButton;
        
        [SerializeField] private TextMeshProUGUI warningText;
        [SerializeField] private TextMeshProUGUI shufflingText;
        protected override void Initialize()
        {
            shuffleButton.onClick.AddListener((_presenter as GameViewPresenter).OnShufflingButtonClick);
        }
        public void ActivateWarningText(string message)
        {
            warningText.text = message;
        }
        public void ActivateShufflingText()
        {
            var oldPos = shufflingText.transform.position;
            
            shufflingText.gameObject.SetActive(true);
            shufflingText.transform.DOMove(warningText.transform.position, .5f).SetEase(Ease.InOutBounce).OnComplete(
                () =>
                {
                    shufflingText.gameObject.SetActive(false);
                    shufflingText.transform.position = oldPos;
                });
        }
    }
}