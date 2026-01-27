using DG.Tweening;
using NTHiep.MiniOdin;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HeroRoll.Battle
{
    public class ResultBattleController : NTHiep.Tool.PopupUI
    {
        [ColorHeader("<Color=red>Var")]
        // [SerializeField] TextMeshProUGUI _resultText;
        [SerializeField] VictoryController _victoryController;
        [SerializeField] DefeatController _defeatController;
        [SerializeField] GameObject _btnClose;

        public static ResultBattleController instance;
        void Awake()
        {
            instance = this;
        }
        public void ShowResult(bool isWin)
        {
            if (isWin)
            {
                // _resultText.text = "You Win!";
                _victoryController.ShowDisplay();
            }
            else
            {
                // _resultText.text = "You Lose!";
                _defeatController.ShowDisplay();
                _btnClose.SetActive(false);
            }
        }

        public void ShowPopup()
        {
            base.ShowPopup(() =>
            {

            });

        }
        public void HidePopup()
        {
            base.HidePopup(() =>
            {
                _victoryController.HideDisplay();
                _defeatController.HideDisplay();
                _btnClose.SetActive(true);

                GameController.instace.EnableDisplayBattle(false);
                GameController.instace._isBattle = false;
            });

        }
    }
}