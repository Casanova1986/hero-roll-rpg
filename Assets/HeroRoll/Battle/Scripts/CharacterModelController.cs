using System;
using NTHiep.MiniOdin;
using Spine.Unity;
using UnityEngine;

namespace HeroRoll.Battle
{
    [Serializable]
    public class InfoCharacterBase
    {
        public int atk;
        public int hp;
        public int def;
    }

    public class CharacterModelController : MonoBehaviour
    {
        [Header("Var")]
        [SerializeField] SkeletonAnimation _skeletonAnimation;

        [Header("Value")]
        public string _idCharacter;
        public CharacterType _characterType;
        public InfoCharacterBase _infoCharacterBase = new InfoCharacterBase
        {
            atk = 10,
            hp = 20,
            def = 5,
        };


        #region SkeletonAnimation

        #endregion
    }

}