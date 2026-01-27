using UnityEngine;
using UnityEngine.SceneManagement;

namespace HeroRoll
{
    public class Config
    {

    }
    public class ConfigScene
    {
        public const string Splash_Scene = "Splash";
        public const string Battle_Scene = "BattleScene";
        public const string Home_Scene = "HomeScene";

        public static void Change_SplashScene()
        {
            SceneManager.LoadScene(Splash_Scene);
        }
        public static void Change_HomeScene()
        {
            SceneManager.LoadScene(Home_Scene);
        }
        public static void Change_BattleScene()
        {
            SceneManager.LoadScene(Battle_Scene);
        }
    }
    public class ConfigBattle
    {
        public const int TurnBossAttack = 5;
    }
}