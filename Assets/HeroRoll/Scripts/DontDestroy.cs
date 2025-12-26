using UnityEngine;

namespace HeroRoll
{
    public class DontDestroy : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this);
        }

    }
}