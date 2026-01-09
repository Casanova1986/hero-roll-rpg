using System;
using System.Collections.Generic;
using NTHiep.MiniOdin;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class DotItem : MonoBehaviour
    {
        [Header("Value")]
        public int _idDot;
        public TypeDot _typeDot;
        public InfoDotItem _infoDotItem;



        [Header("Var")]
        //// Dot Main
        [SerializeField] GameObject _dotMain;
        [SerializeField] GameObject _backgroundDotMain;
        [SerializeField] List<GameObject> _lsItemDotMain;

        //// Dot Sub
        [SerializeField] GameObject _dotSub;
        [SerializeField] GameObject _backgroundDotSub;
        [SerializeField] List<GameObject> _lsItemDotSub;


        #region Setter
        public void SetBackgroundDot(int index = 0)
        {
            int key;
            switch (_typeDot)
            {
                case TypeDot.DotMain:
                    key = index;
                    _infoDotItem._typeDotMain = (TypeDotMain)key;
                    _backgroundDotMain.GetComponent<SpriteRenderer>().sprite = AssetLoader.instance._dictDotMainInGame.Get((TypeDotMain)index);

                    _dotSub.SetActive(false);
                    _dotMain.SetActive(true);
                    break;

                case TypeDot.DotSub:
                    key = index;

                    _infoDotItem._typeDotSub = (TypeDotSub)key;
                    _backgroundDotSub.GetComponent<SpriteRenderer>().sprite = AssetLoader.instance._dictDotSubInGame.Get((TypeDotSub)index);

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
            switch (_infoDotItem._typeDotSub)
            {
                case TypeDotSub.Buff:

                    break;

                case TypeDotSub.DeBuff:

                    break;

                case TypeDotSub.AttackEnemy:
                    for (int i = 0; i < _infoDotItem.numberEnemy; i++)
                    {
                        _lsItemDotSub[i].SetActive(true);
                    }

                    break;

                default:

                    break;
            }
        }

        #endregion
    }


    [Serializable]
    public class InfoDotItem
    {
        [ShowIf(nameof(TypeDot), (int)TypeDot.DotMain)]
        public TypeDotMain _typeDotMain;
        [ShowIf(nameof(TypeDot), (int)TypeDot.DotSub)]
        public TypeDotSub _typeDotSub;
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
        DeBuff = 2
    }
}