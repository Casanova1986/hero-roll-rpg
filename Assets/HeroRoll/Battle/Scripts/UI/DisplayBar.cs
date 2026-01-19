using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HeroRoll.Battle
{
    public class DisplayBar : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _txt;
        [SerializeField] Slider _slider;
        [SerializeField] Image _icon;


        public void SetValueSlider(float value, float maxValue, float duration = 0.15f)
        {
            _txt.text = $"{(int)value}/{(int)maxValue}";
            _slider.DOValue(value / maxValue, duration);
        }
        public void SetValueIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
    }
}
