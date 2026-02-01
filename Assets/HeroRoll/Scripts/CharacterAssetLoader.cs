using System.Collections.Generic;
using NTHiep.Tool;
using Spine.Unity;
using UnityEngine;

namespace HeroRoll
{
    public class CharacterAssetLoader : MonoBehaviour
    {
        public GameObject _heroPrefab;
        public GameObject _monsterPrefab;
        public DictShow<string, CharacterDataAsset> _characterDataAssets;


        public static CharacterAssetLoader instance;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            if (CharacterAssetLoader.instance != null)
            {
                return;
            }
            instance = this;
        }
        public void SetSkeletonData(SkeletonAnimation skeletonAnimation, string idCharacter)
        {
            skeletonAnimation.skeletonDataAsset = _characterDataAssets.Get(idCharacter)._skeletonDataAsset;
            skeletonAnimation.ClearState();
            skeletonAnimation.Initialize(true);
        }

      
    }
}