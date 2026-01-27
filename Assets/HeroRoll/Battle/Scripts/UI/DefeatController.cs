using UnityEngine;

namespace HeroRoll.Battle
{
    public class DefeatController : MonoBehaviour
    {
        [SerializeField] GameObject _display;

        public void OnClickHomeScene()
        {
            ConfigScene.Change_HomeScene();
        }
        public void OnClickReplay()
        {
            ConfigScene.Change_BattleScene();
        }


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