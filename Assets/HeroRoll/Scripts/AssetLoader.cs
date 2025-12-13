using System.Collections.Generic;
using NTHiep.Tool;
using UnityEngine;

namespace HeroRoll
{
    public class AssetLoader : MonoBehaviour
    {
        public static AssetLoader instance { get; private set; }
        [Header("Source")]
        public DictShow<bool, Sprite> _dictItemSlotSubAround = new DictShow<bool, Sprite>();
        void Awake()
        {
            if (AssetLoader.instance != null)
            {
                return;
            }
            instance = this;
        }
        void Start()
        {

        }
        void InitAssignDict()
        {
            // dictItemSlotSubAround.Add(0, listItemSlotSubAround[])
        }


    }

}