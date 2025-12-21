using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NTHiep.MiniOdin;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class BoardController : MonoBehaviour
    {
        [Header("Var")]
        [SerializeField] List<DotItem> _dotItems;
        [SerializeField] List<TextMesh> _lsTextMoveStep;
        [SerializeField] CharacterController _characterController;


        [Header("Value")]
        int indexNextMove = 1;


        public static BoardController instance;
        void Awake()
        {
            if (BoardController.instance != null)
            {
                return;
            }
            instance = this;
        }
        void Start()
        {
            SetUpBoard();
        }

        [Button("Set Up Board")]
        public void SetUpBoard()
        {
            for (int i = 0; i < _dotItems.Count; i++)
            {
                _dotItems[i]._idDot = i;
                _dotItems[i].name = $"Dot {i}";
                _dotItems[i].SetBackgroundDot(isRandom: true);
            }
            SetTextMultiMoveStep(12);
        }


        #region Setter
        //// Text Slot Board
        public void SetTextMultiMoveStep(int numberStep)
        {
            int total = _lsTextMoveStep.Count;

            // Tắt hết trước
            for (int i = 0; i < total; i++)
            {
                _lsTextMoveStep[i].gameObject.SetActive(false);
                ClearTextFocusStep(i);
            }

            int step = 1;
            int index = indexNextMove;

            for (int s = 0; s < numberStep; s++)
            {
                int i = (index + s) % total;  // loop vòng

                _lsTextMoveStep[i].gameObject.SetActive(true);
                _lsTextMoveStep[i].text = step.ToString();

                step++;
            }
        }
        public void SetTextMoveStep(int index, int value)
        {
            _lsTextMoveStep[index].gameObject.SetActive(true);
            _lsTextMoveStep[index].text = value.ToString();
        }
        public void SetTextFocusStep(int index)
        {
            _lsTextMoveStep[index].color = Color.red;
        }
        public void ClearTextFocusStep(int index)
        {
            _lsTextMoveStep[index].color = Color.white;
        }

        //// Character
        public void AssignCharacter(int index)
        {
            DotItem dotItem = GetDotItem(index);
            _characterController._dotItemStay = dotItem;
        }


        #endregion

        #region Getter
        public int GetIndexFocusStep(int numberStep)
        {
            int indexFocus = (indexNextMove + numberStep - 1) % _lsTextMoveStep.Count;
            return indexFocus;
        }
        public DotItem GetDotItem(int index)
        {
            return _dotItems[index];
        }
        // public bool 
        #endregion


        #region Animation
        public IEnumerator MoveMultipleStep(int numberStep)
        {
            for (int i = 0; i < numberStep; i++)
            {
                int indexMove = i;
                MoveOneStep(0.25f);
                yield return new WaitForSeconds(0.25f);

                if (indexNextMove - 1 == 0)
                {
                    GameController.instace.UpCurrentFloorPlayerStay();
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
        // [Button("Move One Step")]
        public void MoveOneStep(float duration = 0.4f)
        {
            if (indexNextMove >= _dotItems.Count)
            {
                indexNextMove = 0;
            }
            _characterController.PlayerMove(_dotItems[indexNextMove].transform.position, duration: duration);



            indexNextMove++;



        }
        #endregion
    }

}