using DG.Tweening;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class GameController : MonoBehaviour
    {
        public static GameController instace;
        [Header("Var")]
        [SerializeField] BossController _bossController;

        [Header("Value")]
        public int _turnBossAttackLeft = 3;
        public int _currentFloorPlayerStay = 0;

        void Awake()
        {
            if (GameController.instace != null)
            {
                return;
            }
            instace = this;
        }


        public void CountDownTurnAttack()
        {
            _turnBossAttackLeft--;
            UIController.instace.SetTxtTurnBoss(_turnBossAttackLeft);
            if (_turnBossAttackLeft <= 0)
            {
                UIController.instace.OnEnableButtonRoll(false);

                _bossController.SetAnimationAttack((duration) =>
                {
                    DOVirtual.DelayedCall(duration, () =>
                    {
                        _turnBossAttackLeft = 3;
                        UIController.instace.SetTxtTurnBoss(_turnBossAttackLeft);
                        UIController.instace.OnEnableButtonRoll(true);
                    });
                });

            }
        }

        public void UpCurrentFloorPlayerStay()
        {
            _currentFloorPlayerStay++;
            UIController.instace.SetTxtCurrentFloor(_currentFloorPlayerStay);
        }
    }
}
