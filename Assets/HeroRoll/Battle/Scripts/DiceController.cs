using System.Collections;
using MiniOdin;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class DiceController : MonoBehaviour
    {
        [Header("Var")]
        public int _valueDice1, _valueDice2;
        public bool isAnimDiceAndMove = false;

        Coroutine _coroutineDice;

        [Button("Roll Dice")]
        public void RollDice()
        {
            if (isAnimDiceAndMove)
            {
                return;
            }
            _valueDice1 = Hiep.Tool.RandomUtil.Range(1, 7);
            _valueDice2 = Hiep.Tool.RandomUtil.Range(1, 7);

            _coroutineDice = StartCoroutine(WaitAnimRollAndMove(_valueDice1 + _valueDice2));


            IEnumerator WaitAnimRollAndMove(int steps)
            {
                isAnimDiceAndMove = true;
                //// Animation Roll Dice Here
                yield return new WaitForSeconds(1f);

                yield return BoardController.instance.MoveMultipleStep(steps);
                isAnimDiceAndMove = false;
                BoardController.instance.SetTextMultiMoveStep(12);
            }
        }


    }
}