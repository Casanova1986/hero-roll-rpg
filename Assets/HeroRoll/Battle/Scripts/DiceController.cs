using System.Collections;
using NTHiep.MiniOdin;
using Spine.Unity;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class DiceController : MonoBehaviour
    {
        [Header("<color=red>Value")]
        public int _valueDice1;
        public int _valueDice2;
        [SerializeField] int indexFocusStep;

        [Header("<color=red>Var")]
        [SerializeField] SkeletonAnimation diceSke1;
        [SerializeField] SkeletonAnimation diceSke2;

        Coroutine _coroutineDice;

        public void StartRollDice()
        {
            StartCoroutine(ThreadDice());
        }

        public IEnumerator ThreadDice()
        {
            UIController.instace.OnEnableButtonRoll(false);
            
            //// Thread 1
            yield return StartCoroutine(Thread_1());

            //// Thread 2
            yield return StartCoroutine(Thread_2());

            //// Thread 3
            yield return StartCoroutine(Thread_3());


            //// Thread end
            yield return StartCoroutine(Thread_end());

            UIController.instace.OnEnableButtonRoll(true);
        }

        IEnumerator Thread_1()
        {
            RandomValueDice();
            SetPlayDice();
            indexFocusStep = BoardController.instance.GetIndexFocusStep(_valueDice1 + _valueDice2);

            yield return new WaitForSeconds(0.5f);
            OffDice();
            BoardController.instance.SetTextFocusStep(indexFocusStep);

            yield return new WaitForSeconds(0.5f);
        }
        IEnumerator Thread_2()
        {


            yield break;
        }
        IEnumerator Thread_3()
        {
            //// Player move
            yield return BoardController.instance.MoveMultipleStep(_valueDice1 + _valueDice2);
            SurroundingController.instance.UpdateSurrounding(indexFocusStep);

        }
        IEnumerator Thread_end()
        {
            BoardController.instance.SetTextMultiMoveStep(12);
            BoardController.instance.AssignCharacter(indexFocusStep);
            yield return StartCoroutine(GameController.instace.CountDownTurnAttack());
        }

        #region Setter
        void SetPlayDice()
        {
            OnDice();

            //// Dice 1
            diceSke1.Skeleton.SetSkin(_valueDice1.ToString());
            diceSke1.Skeleton.SetSlotsToSetupPose();
            diceSke1.AnimationState.ClearTracks();
            diceSke1.AnimationState.SetAnimation(0, "animation", false);

            //// Dice 2
            diceSke2.Skeleton.SetSkin(_valueDice2.ToString());
            diceSke2.Skeleton.SetSlotsToSetupPose();
            diceSke2.AnimationState.ClearTracks();
            diceSke2.AnimationState.SetAnimation(0, "animation", false);
        }
        void OnDice()
        {
            diceSke1.gameObject.SetActive(true);
            diceSke2.gameObject.SetActive(true);
        }
        void OffDice()
        {
            diceSke1.gameObject.SetActive(false);
            diceSke2.gameObject.SetActive(false);
        }
        void RandomValueDice()
        {
            _valueDice1 = NTHiep.Tool.RandomUtil.Range(1, 7);
            _valueDice2 = NTHiep.Tool.RandomUtil.Range(1, 7);
        }
        #endregion

        #region Getter

        #endregion

    }
}