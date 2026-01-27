using UnityEngine;

namespace HeroRoll.Battle
{
    public class VictoryController : MonoBehaviour
    {
        [SerializeField] GameObject _display;


        public void ShowDisplay()
        {
            _display.SetActive(true);
        }
        public void HideDisplay()
        {
            _display.SetActive(false);
        }
    }
}