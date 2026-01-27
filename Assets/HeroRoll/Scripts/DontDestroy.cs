using UnityEngine;

namespace HeroRoll
{
    public class DontDestroy : MonoBehaviour
    {
        static DontDestroy instance;
        void Awake()
        {
            if (DontDestroy.instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            instance = this;
            DontDestroyOnLoad(this);
        }

    }
}