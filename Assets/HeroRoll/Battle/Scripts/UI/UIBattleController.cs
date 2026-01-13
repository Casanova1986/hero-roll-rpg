using DG.Tweening;
using NTHiep.MiniOdin;
using UnityEngine;
using UnityEngine.UI;

namespace HeroRoll.Battle
{
    public class UIBattleController : MonoBehaviour
    {
        [ColorHeader("<color=red>Var")]
        [SerializeField] GameObject _sliderHpHero;
        [SerializeField] GameObject _sliderHpHeroDelay;
        [SerializeField] GameObject _sliderHpMonster;
        [SerializeField] GameObject _sliderHpMonsterDelay;


        public void UpdateDisplayHeath(float value, bool isHero)
        {
            if (isHero)
            {
                _sliderHpHero.GetComponent<Slider>().DOValue(value, 0.15f);
                _sliderHpHeroDelay.GetComponent<Slider>().DOValue(value, 0.15f).SetDelay(0.15f);
            }
            else
            {
                _sliderHpMonster.GetComponent<Slider>().DOValue(value, 0.15f);
                _sliderHpMonsterDelay.GetComponent<Slider>().DOValue(value, 0.15f).SetDelay(0.15f);
            }
        }
    }
}