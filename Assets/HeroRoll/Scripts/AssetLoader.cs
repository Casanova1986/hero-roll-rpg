using System.Collections.Generic;
using NTHiep.Tool;
using UnityEngine;

namespace HeroRoll
{
    public class AssetLoader : MonoBehaviour
    {
        public static AssetLoader instance { get; private set; }
        [Header("Source")]

        public DictShow<bool, Sprite> _dictGoundSlotSurrounding = new DictShow<bool, Sprite>();
        public DictShow<int, Sprite> _dictItemSlotSubAround = new DictShow<int, Sprite>();
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
            // Debug.Log(_dictGoundSlotSurrounding.Count);
            // Debug.Log(_dictItemSlotSubAround.Count);
        }
        void InitAssignDict()
        {
            // dictItemSlotSubAround.Add(0, listItemSlotSubAround[])
        }


    }

}