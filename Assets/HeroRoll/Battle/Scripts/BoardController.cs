using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using MiniOdin;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] List<DotItem> _dotItems;
        [SerializeField] List<TextMesh> _lsTextMoveStep;
        [SerializeField] CharacterController _characterController;

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
                _dotItems[i].SetBackgroundDot(isRandom: true);
            }
            SetTextMultiMoveStep(12);
        }


        #region Setter
        public void SetTextMultiMoveStep(int numberStep)
        {
            int total = _lsTextMoveStep.Count;

            // Tắt hết trước
            for (int i = 0; i < total; i++)
            {
                _lsTextMoveStep[i].gameObject.SetActive(false);
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
        #endregion

        #region Getter
        // public DotItem
        #endregion


        #region Animation
        public IEnumerator MoveMultipleStep(int numberStep)
        {
            for (int i = 0; i < numberStep; i++)
            {
                int indexMove = i;
                MoveOneStep(0.4f);
                yield return new WaitForSeconds(0.4f);
            }
        }
        int indexNextMove = 21;
        // [Button("Move One Step")]
        public void MoveOneStep(float duration = 0.4f)
        {
            if (indexNextMove >= _dotItems.Count)
            {
                indexNextMove = 0;
            }
            PlayerMove(_dotItems[indexNextMove].transform.position, duration);
            indexNextMove++;

        }
        public void PlayerMove(Vector3 targetPosition, float duration = 0.4f)
        {
            Vector3 start = _characterController.transform.position;

            // Tạo điểm giữa cao lên thành vòng cung
            Vector3 mid = (start + targetPosition) / 2f;
            mid.y += 0.5f; // độ cao của vòng cung

            Vector3[] path = new Vector3[] { start, mid, targetPosition };

            _characterController.transform.DOPath(
                path,
                duration,        // thời gian
                PathType.CatmullRom
            );
        }
        #endregion
    }

}