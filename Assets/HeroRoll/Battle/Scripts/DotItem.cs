using System.Collections.Generic;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class DotItem : MonoBehaviour
    {
        [Header("Var")]
        public TypeDot _typeDot;

        [Header("Source")]
        [SerializeField] List<Sprite> _dotMainSources;
        [SerializeField] List<Sprite> _dotSubSources;

        [Header("Value")]
        [SerializeField] GameObject _dotMain;
        [SerializeField] GameObject _dotSub;
        [SerializeField] GameObject _BackgroundDotMain;
        [SerializeField] GameObject _BackgroundDotSub;


        #region Function
        public void SetBackgroundDot(int index = 0, bool isRandom = true)
        {
            switch (_typeDot)
            {
                case TypeDot.DotMain:
                    if (isRandom)
                    {
                        _BackgroundDotMain.GetComponent<SpriteRenderer>().sprite = Hiep.Tool.RandomUtil.Pick(_dotMainSources);
                    }
                    else
                    {
                        _BackgroundDotMain.GetComponent<SpriteRenderer>().sprite = _dotMainSources[index];
                    }

                    _dotSub.SetActive(false);
                    _dotMain.SetActive(true);
                    break;
                case TypeDot.DotSub:
                    if (isRandom)
                    {
                        _BackgroundDotSub.GetComponent<SpriteRenderer>().sprite = Hiep.Tool.RandomUtil.Pick(_dotSubSources);
                    }
                    else
                    {
                        _BackgroundDotSub.GetComponent<SpriteRenderer>().sprite = _dotSubSources[index];
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
}