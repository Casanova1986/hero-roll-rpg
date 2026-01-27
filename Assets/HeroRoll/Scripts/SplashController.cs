using UnityEngine;

namespace HeroRoll
{
    public class SplashController : MonoBehaviour
    {
        void Start()
        {
            Init();
        }
        void Init()
        {
            ConfigScene.Change_HomeScene();
        }
    }
}