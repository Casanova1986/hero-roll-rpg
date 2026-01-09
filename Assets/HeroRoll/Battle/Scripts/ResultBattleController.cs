using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HeroRoll.Battle
{
    public class ResultBattleController : MonoBehaviour
    {
        [SerializeField] GameObject _panel;
        [SerializeField] GameObject _dimmer;
        [SerializeField] TextMeshProUGUI _resultText;

        public static ResultBattleController instance;
        void Awake()
        {
            instance = this;
        }
        public void ShowResult(bool isWin)
        {
            if (isWin)
            {
                _resultText.text = "You Win!";
            }
            else
            {
                _resultText.text = "You Lose!";
            }
            gameObject.SetActive(true);
        }

        public void ShowPopup()
        {
            _dimmer.SetActive(true);
            _panel.SetActive(true);

            _dimmer.GetComponent<Image>().DOFade(0.6f, 0.5f);
            _panel.transform.DOScale(1f, 0.5f);

        }
        public void HidePopup()
        {
            _dimmer.GetComponent<Image>().DOFade(0f, 0.5f).OnComplete(() =>
            {
                _dimmer.SetActive(false);
            });
            _panel.transform.DOScale(0f, 0.5f).OnComplete(() =>
            {
                _panel.SetActive(false);
            });
        }
    }
}