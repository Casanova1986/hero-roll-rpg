using System.Collections.Generic;
using MiniOdin;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] List<DotItem> _dotItems;


        public static BoardController Instance { get { return _instance; } }
        private static BoardController _instance;
        void Awake()
        {
            if (BoardController.Instance != null)
            {
                return;
            }
            _instance = this;
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
        }

        #region Setter

        #endregion

        #region Getter
        // public DotItem
        #endregion
    }

}