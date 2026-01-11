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
            base.ShowPopup(() =>
            {

            });

        }
        public void HidePopup()
        {
            base.HidePopup(() =>
            {
                GameController.instace.EnableDisplayBattle(false);
                GameController.instace._isBattle = false;
            });
        }
    }
}