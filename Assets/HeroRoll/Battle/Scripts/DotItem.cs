using System;
using System.Collections.Generic;
using UnityEngine;
using NTHiep.MiniOdin;

namespace HeroRoll.Battle
{
    public class DotItem : MonoBehaviour
    {
        [ColorHeader("<color=red>Var")]
        //// Dot Main
        [SerializeField] GameObject _dotMain;
        [SerializeField] GameObject _backgroundDotMain;
        [SerializeField] List<GameObject> _lsItemDotMain;
        [Space(5)]
        //// Dot Sub
        [SerializeField] GameObject _dotSub;
        [SerializeField] GameObject _backgroundDotSub;
        [SerializeField] List<GameObject> _lsItemDotSub;
        [SerializeField] GameObject _itemDotSub;



        [ColorHeader("<color=red>Value")]
        public int _idDot;
        public TypeDot _typeDot;
        public InfoDotItem _infoDotItem;





        #region Setter
        public void UpdateDisplay()
        {
            SetBackgroundDot();
            if (_typeDot == TypeDot.DotMain)
            {
                SetUpItemDotMain();
            }
            else
            {
                SetUpItemDotSub();
            }
        }
        public void SetBackgroundDot()
        {
            switch (_typeDot)
            {
                case TypeDot.DotMain:
                    _backgroundDotMain.GetComponent<SpriteRenderer>().sprite = AssetLoader.instance._dictDotMainInGame.Get(_infoDotItem._typeDotMain);

                    _dotSub.SetActive(false);
                    _dotMain.SetActive(true);
                    break;

                case TypeDot.DotSub:
                    _backgroundDotSub.GetComponent<SpriteRenderer>().sprite = AssetLoader.instance._dictDotSubInGame.Get(_infoDotItem._typeDotSub);

                    _dotSub.SetActive(true);
                    _dotMain.SetActive(false);
                    break;
            }
        }
        public void SetUpItemDotMain()
        {
            switch (_infoDotItem._typeDotMain)
            {
                case TypeDotMain.Buff:

                    break;
            }
        }

        public void SetUpItemDotSub()
        {
            _itemDotSub.SetActive(_infoDotItem._typeDotSub == TypeDotSub.Buff || _infoDotItem._typeDotSub == TypeDotSub.DeBuff);

            switch (_infoDotItem._typeDotSub)
            {
                case TypeDotSub.Empty:

                    for (int i = 0; i < _lsItemDotSub.Count; i++)
                    {
                        _lsItemDotSub[i].SetActive(false);
                    }

                    break;
                case TypeDotSub.Buff:
                    _itemDotSub.GetComponent<SpriteRenderer>().sprite = AssetLoader.instance._dictItemBuffDotSubInGame.Get(_infoDotItem._typeBuffDotSub);
                    break;

                case TypeDotSub.DeBuff:
                    _itemDotSub.GetComponent<SpriteRenderer>().sprite = AssetLoader.instance._dictItemDeBuffDotSubInGame.Get(_infoDotItem._typeDeBuffDotSub);
                    break;

                case TypeDotSub.AttackEnemy:
                    if (_infoDotItem.numberEnemy < 0)
                    {
                        _infoDotItem.numberEnemy = 0;
                    }
                    for (int i = 0; i < _lsItemDotSub.Count; i++)
                    {
                        _lsItemDotSub[i].SetActive(i < _infoDotItem.numberEnemy);
                    }

                    break;

            }
        }
        public void ResetInfo()
        {
            BoardController.instance._characterMoveController._dotItemStay._infoDotItem._typeDotMain = TypeDotMain.Empty;
            BoardController.instance._characterMoveController._dotItemStay._infoDotItem._typeDotSub = TypeDotSub.Empty;
            BoardController.instance._characterMoveController._dotItemStay._infoDotItem.numberEnemy = 0;
        }
        #endregion
    }


    [System.Serializable]
    public class InfoDotItem
    {
        TypeDot _typeDot;

        //// Dot main
        [ShowIf(nameof(_typeDot), (int)TypeDot.DotMain)]
        public TypeDotMain _typeDotMain;

        [ShowIf(nameof(_typeDot), (int)TypeDot.DotMain)]
        public TypeBuffDotMain _typeBuffDotMain;

        [ShowIf(nameof(_typeDot), (int)TypeDot.DotMain)]
        public TypeDeBuffDotMain _typeDeBuffDotMain;

        //// Dot sub
        [ShowIf(nameof(_typeDot), (int)TypeDot.DotSub)]
        public TypeDotSub _typeDotSub;

        [ShowIf(nameof(_typeDot), (int)TypeDot.DotSub)]
        public TypeBuffDotSub _typeBuffDotSub;

        [ShowIf(nameof(_typeDot), (int)TypeDot.DotSub)]
        public TypeDeBuffDotSub _typeDeBuffDotSub;

        [ShowIf(nameof(_typeDot), (int)TypeDot.DotSub)]
        public int numberEnemy;
    }

    public enum TypeDot
    {
        DotMain = 0,
        DotSub = 1
    }

    public enum TypeDotSub
    {
        Empty = 0,
        Buff = 1,
        DeBuff = 2,
        AttackEnemy = 3,
    }
    public enum TypeDotMain
    {
        Empty = 0,
        Buff = 1,
        DeBuff = 2,
        Start = 3
    }

    public enum TypeBuffDotSub
    {
        DestroyRowAllEnemy,
        SubtractAllEnemy
    }
    public enum TypeBuffDotMain
    {
        none
    }
    public enum TypeDeBuffDotSub
    {
        AddAllEnemy,
        AddRowAllEnemy,
    }
    public enum TypeDeBuffDotMain
    {
        none
    }

}