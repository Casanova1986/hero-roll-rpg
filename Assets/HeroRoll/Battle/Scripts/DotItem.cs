using System;
using System.Collections.Generic;
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
        public void SetBackgroundDot(int index = 0, bool isRandom = true)
        {
            switch (_typeDot)
            {
                case TypeDot.DotMain:
                    if (isRandom)
                    {
                        Sprite sprite = NTHiep.Tool.RandomUtil.Pick(AssetLoader.instance._dictDotMainInGame.ToList());
                        int key = AssetLoader.instance._dictDotMainInGame.GetKey(sprite);

                        _backgroundDotMain.GetComponent<SpriteRenderer>().sprite = sprite;
                    }
                    else
                    {
                        Sprite sprite = AssetLoader.instance._dictDotMainInGame.Get(index);
                        int key = index;
                        _backgroundDotMain.GetComponent<SpriteRenderer>().sprite = AssetLoader.instance._dictDotMainInGame.Get(index);
                    }

                    _dotSub.SetActive(false);
                    _dotMain.SetActive(true);
                    break;

                case TypeDot.DotSub:

                    if (isRandom)
                    {
                        Sprite sprite = NTHiep.Tool.RandomUtil.Pick(AssetLoader.instance._dictDotSubInGame.ToList());
                        TypeDotSub key = AssetLoader.instance._dictDotSubInGame.GetKey(sprite);
                        _infoDotItem._typeDotSub = key;


                        _backgroundDotSub.GetComponent<SpriteRenderer>().sprite = sprite;
                    }
                    else
                    {
                        Sprite sprite = AssetLoader.instance._dictDotSubInGame.Get((TypeDotSub)index);
                        TypeDotSub key = (TypeDotSub)index;
                        _infoDotItem._typeDotSub = key;


                        _backgroundDotSub.GetComponent<SpriteRenderer>().sprite = sprite;
                    }


                    _dotSub.SetActive(true);
                    _dotMain.SetActive(false);
                    break;
            }
        }
        public void AddRandom()
        {

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
        public TypeDotMain _typeDotMain;
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