using System.Collections.Generic;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class DotItem : MonoBehaviour
    {
        [Header("Var")]
        public int _idDot;
        public TypeDot _typeDot;
        public TypeDotSub _typeDotSub;

        [Header("Value")]
        [SerializeField] GameObject _dotMain;
        [SerializeField] GameObject _dotSub;
        [SerializeField] GameObject _backgroundDotMain;
        [SerializeField] GameObject _backgroundDotSub;


        #region Function
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
                        _typeDotSub = key;


                        _backgroundDotSub.GetComponent<SpriteRenderer>().sprite = sprite;
                    }
                    else
                    {
                        Sprite sprite = AssetLoader.instance._dictDotSubInGame.Get((TypeDotSub)index);
                        TypeDotSub key = (TypeDotSub)index;
                        _typeDotSub = key;


                        _backgroundDotSub.GetComponent<SpriteRenderer>().sprite = sprite;
                    }


                    _dotSub.SetActive(true);
                    _dotMain.SetActive(false);
                    break;
            }
        }

        #endregion
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
}