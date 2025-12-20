using System.Collections.Generic;
using NTHiep.Tool;
using UnityEngine;

namespace HeroRoll
{
    public class AssetLoader : MonoBehaviour
    {
        public static AssetLoader instance { get; private set; }
        [Header("Source")]
        // [SerializeField] List<Sprite> _groundSlotSurrounding;
        // [SerializeField] List<Sprite> _itemSlotSubAround;
        // [SerializeField] List<Sprite> _dotMainInGame;
        // [SerializeField] List<Sprite> _dotSubInGame;

        [Header("Value")]
        public DictShow<bool, Sprite> _dictGroundSlotSurrounding = new DictShow<bool, Sprite>();
        public DictShow<int, Sprite> _dictItemSlotSubAround = new DictShow<int, Sprite>();
        public DictShow<int, Sprite> _dictDotMainInGame = new DictShow<int, Sprite>();
        public DictShow<HeroRoll.Battle.TypeDotSub, Sprite> _dictDotSubInGame = new DictShow<HeroRoll.Battle.TypeDotSub, Sprite>();

        void Awake()
        {
            if (AssetLoader.instance != null)
            {
                return;
            }
            instance = this;
            InitAssignDict();
        }
        void Start()
        {
            // Debug.Log(_dictGoundSlotSurrounding.Count);
            // Debug.Log(_dictItemSlotSubAround.Count);

        }
        void InitAssignDict()
        {
            // for (int i = 0; i < _groundSlotSurrounding.Count; i++)
            // {
            //     _dictGroundSlotSurrounding.Add(i != 0, _groundSlotSurrounding[i]);
            // }
            // for (int i = 0; i < _itemSlotSubAround.Count; i++)
            // {
            //     _dictItemSlotSubAround.Add(i, _itemSlotSubAround[i]);
            // }
            // for (int i = 0; i < _dotMainInGame.Count; i++)
            // {
            //     _dictDotMainInGame.Add(i, _dotMainInGame[i]);
            // }
            // for (int i = 0; i < _dotSubInGame.Count; i++)
            // {
            //     _dictDotSubInGame.Add(i, _dotSubInGame[i]);
            // }
        }


    }

}