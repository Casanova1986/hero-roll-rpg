using System.Collections;
using NTHiep.MiniOdin;
using Spine.Unity;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class DiceController : MonoBehaviour
    {
        [Header("Value")]
        public int _valueDice1, _valueDice2;
        public bool _isAnimDiceAndMove = false;

        [Header("Var")]
        [SerializeField] SkeletonAnimation diceSke1;
        [SerializeField] SkeletonAnimation diceSke2;

        Coroutine _coroutineDice;

        [Button("Roll Dice")]
        public void RollDice()
        {
            if (_isAnimDiceAndMove)
            {
                return;
            }
            _valueDice1 = NTHiep.Tool.RandomUtil.Range(1, 7);
            _valueDice2 = NTHiep.Tool.RandomUtil.Range(1, 7);

            _coroutineDice = StartCoroutine(WaitAnimRollAndMove());


            IEnumerator WaitAnimRollAndMove()
            {
                _isAnimDiceAndMove = true;
                //// Animation Roll Dice Here
                {
                    diceSke1.gameObject.SetActive(true);
                    diceSke1.Skeleton.SetSkin(_valueDice1.ToString());
                    diceSke1.Skeleton.SetSlotsToSetupPose();
                    diceSke1.AnimationState.ClearTracks();
                    diceSke1.AnimationState.SetAnimation(0, "animation", false);



                    diceSke2.gameObject.SetActive(true);
                    diceSke2.Skeleton.SetSkin(_valueDice2.ToString());
                    diceSke2.Skeleton.SetSlotsToSetupPose();
                    diceSke2.AnimationState.ClearTracks();
                    diceSke2.AnimationState.SetAnimation(0, "animation", false);
                }



                yield return new WaitForSeconds(0.5f);
                int indexFocusStep = BoardController.instance.GetIndexFocusStep(_valueDice1 + _valueDice2);
                BoardController.instance.SetTextFocusStep(indexFocusStep);

                yield return new WaitForSeconds(0.5f);

                diceSke1.gameObject.SetActive(false);
                diceSke2.gameObject.SetActive(false);

                //// Affter Animation Done
                yield return BoardController.instance.MoveMultipleStep(_valueDice1 + _valueDice2);

                SurroundingController.instance.UpdateSurrounding(indexFocusStep);
                BoardController.instance.SetTextMultiMoveStep(12);
                BoardController.instance.AssignCharacter(indexFocusStep);
                GameController.instace.CountDownTurnAttack();
                _isAnimDiceAndMove = false;
            }
        }


    }
}