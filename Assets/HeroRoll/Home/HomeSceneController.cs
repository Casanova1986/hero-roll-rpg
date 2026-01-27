using UnityEngine;

namespace HeroRoll.Home
{
    public class HomeSceneController : MonoBehaviour
    {
        public void OnClickPlayBattle()
        {
            ConfigScene.Change_BattleScene();
        }
    }
}