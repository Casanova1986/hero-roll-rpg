using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NTHiep.MiniOdin;
using NTHiep.Tool;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class GameController : MonoBehaviour
    {
        public static GameController instace;
        [ColorHeader("<color=red>Var")]
        [SerializeField] BossController _bossController;
        [SerializeField] GameObject _displayBattleUI;
        [SerializeField] GameObject _displayBattle;

        [ColorHeader("<color=red>Value")]
        public int _turnBossAttackLeft = 3;
        public int _currentFloorPlayerStay = 0;
        public bool _isBattle = false;
        void Awake()
        {
            if (GameController.instace != null)
            {
                return;
            }
            instace = this;
        }

        void Start()
        {
            StartCoroutine(GameStart());
        }

        IEnumerator GameStart()
        {
            yield return new WaitForSeconds(0);
            BoardController.instance.SetUpBoard();
            yield return new WaitForSeconds(0.1f);
            BoardController.instance.SetUpInfoDotSubRandom();
            BoardController.instance.SetUpDotMainRandom();

            yield return new WaitForSeconds(0.1f);
            yield return BoardController.instance.AnimDisplayDotItem();


        }

        public IEnumerator CountDownTurnAttack()
        {
            bool finishAnim = false;
            _turnBossAttackLeft--;
            UIController.instace.SetTxtTurnBoss(_turnBossAttackLeft);
            if (_turnBossAttackLeft <= 0)
            {
                _bossController.SetAnimationAttack((duration) =>
                {
                    DOVirtual.DelayedCall(duration, () =>
                    {
                        finishAnim = true;
                    });
                });

                yield return new WaitUntil(() => finishAnim);
                _turnBossAttackLeft = 3;
                UIController.instace.SetTxtTurnBoss(_turnBossAttackLeft);
            }
            else
            {
                finishAnim = true;
            }


        }

        public void UpCurrentFloorPlayerStay()
        {
            _currentFloorPlayerStay++;
            UIController.instace.SetTxtCurrentFloor(_currentFloorPlayerStay);
        }

        public void EnableDisplayBattle(bool isOn)
        {
            _displayBattle.SetActive(isOn);
            _displayBattleUI.SetActive(isOn);
        }
        #region Getter

        #endregion
    }
}
