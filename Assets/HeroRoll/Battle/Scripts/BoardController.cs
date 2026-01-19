using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NTHiep.MiniOdin;
using NTHiep.Tool;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class BoardController : MonoBehaviour
    {
        [ColorHeader("<color=red>Var")]
        [SerializeField] List<DotItem> _dotItems;
        [SerializeField] List<TextMesh> _lsTextMoveStep;
        public CharacterMoveController _characterMoveController;


        [ColorHeader("<color=red>Value")]
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

        }



        #region Setter
        //// Board
        [Button("Set Up Board")]
        public void SetUpBoard()
        {
            for (int i = 0; i < _dotItems.Count; i++)
            {
                _dotItems[i].name = $"Dot_{i}";
                _dotItems[i]._idDot = i;
            }
            SetTextMultiMoveStep(12);
        }
        public void SetUpDotMainRandom()
        {

        }
        [SerializeField] List<InfoDotItem> _random = new List<InfoDotItem>();
        public void SetUpInfoDotSubRandom()
        {
            List<InfoDotItem> infoDotItemRandoms = GetInfoDotSubRandom();
            _random = infoDotItemRandoms;
            for (int i = 0; i < _dotItems.Count; i++)
            {
                if (_dotItems[i]._typeDot == TypeDot.DotSub)
                {
                    if (infoDotItemRandoms[i]._typeDotSub != TypeDotSub.Empty)
                    {
                        _dotItems[i]._infoDotItem._typeDotSub = infoDotItemRandoms[i]._typeDotSub;
                    }
                    _dotItems[i]._infoDotItem._typeDotMain = infoDotItemRandoms[i]._typeDotMain;
                    switch (_dotItems[i]._infoDotItem._typeDotSub)
                    {
                        case TypeDotSub.AttackEnemy:
                            _dotItems[i]._infoDotItem.numberEnemy += infoDotItemRandoms[i].numberEnemy;
                            break;
                    }
                }
            }

        }


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
            _characterMoveController._dotItemStay = dotItem;
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
        public List<InfoDotItem> GetInfoDotSubRandom()
        {
            List<InfoDotItem> infoDotItemsResult = new List<InfoDotItem>();

            for (int i = 0; i < _dotItems.Count; i++)
            {
                InfoDotItem infoDotItem = new InfoDotItem();

                if (_dotItems[i]._typeDot == TypeDot.DotSub)
                {
                    switch (_dotItems[i]._infoDotItem._typeDotSub)
                    {
                        case TypeDotSub.Empty:
                            infoDotItem._typeDotSub = RandomUtil.RandomEnum<TypeDotSub>();

                            switch (infoDotItem._typeDotSub)
                            {
                                case TypeDotSub.AttackEnemy:
                                    infoDotItem.numberEnemy = 1;
                                    break;
                            }
                            break;

                        case TypeDotSub.AttackEnemy:
                            infoDotItem._typeDotSub = TypeDotSub.AttackEnemy;
                            if (_dotItems[i]._infoDotItem.numberEnemy < 5)
                            {
                                infoDotItem.numberEnemy = 1;
                            }
                            break;
                    }
                }

                infoDotItemsResult.Add(infoDotItem);
            }
            return infoDotItemsResult;
        }
        #endregion


        #region Animation
        public IEnumerator MoveMultipleStep(int numberStep)
        {
            for (int i = 0; i < numberStep; i++)
            {
                int indexMove = i;
                yield return MoveOneStep(0.25f);

                if (indexNextMove - 1 == 0)
                {
                    //// Thread return main dot start
                    yield return new WaitForSeconds(0.5f);


                    GameController.instace.UpCurrentFloorPlayerStay();
                    {
                        //// RandomDotItem
                        yield return new WaitForSeconds(0.1f);
                        BoardController.instance.SetUpInfoDotSubRandom();
                        BoardController.instance.SetUpDotMainRandom();

                        yield return new WaitForSeconds(0.1f);
                        yield return BoardController.instance.AnimDisplayDotItem();
                    }

                    yield return new WaitForSeconds(0.5f);
                }
                // else if (indexNextMove - 1 == 10)
                // {
                //     //// Thread main dot 1
                //     yield return new WaitForSeconds(0.5f);




                //     yield return new WaitForSeconds(0.5f);
                // }
                // else if (indexNextMove - 1 == 20)
                // {
                //     //// Thread main dot 2
                //     yield return new WaitForSeconds(0.5f);




                //     yield return new WaitForSeconds(0.5f);
                // }
                // else if (indexNextMove - 1 == 30)
                // {
                //     //// Thread main dot 3
                //     yield return new WaitForSeconds(0.5f);




                //     yield return new WaitForSeconds(0.5f);
                // }
            }
        }
        // [Button("Move One Step")]
        public IEnumerator MoveOneStep(float duration = 0.4f)
        {
            if (indexNextMove >= _dotItems.Count)
            {
                indexNextMove = 0;
            }

            yield return StartCoroutine(_characterMoveController.PlayerMove(_dotItems[indexNextMove].transform.position, duration: duration));
            indexNextMove++;

        }
        public IEnumerator AnimDisplayDotItem()
        {
            for (int i = 0; i < _dotItems.Count; i++)
            {
                if (i == _dotItems.Count - 1)
                {
                    _dotItems[i].SetBackgroundDot();
                    _dotItems[i].SetUpItemDotMain();
                    _dotItems[i].SetUpItemDotSub();
                    yield return new WaitForSeconds(0);
                }
                else
                {
                    _dotItems[i].SetBackgroundDot();
                    _dotItems[i].SetUpItemDotMain();
                    _dotItems[i].SetUpItemDotSub();
                }

            }
        }
        #endregion
    }

}