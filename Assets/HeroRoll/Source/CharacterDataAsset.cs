using Spine.Unity;
using Unity.VisualScripting;
using UnityEngine;

namespace HeroRoll
{
    public enum CharacterType
    {
        Hero,
        Monster,
        Boss
    }
    [CreateAssetMenu(fileName = "New Character Data", menuName = "Character Data/Create Character Data")]
    public class CharacterDataAsset : ScriptableObject
    {
        [Header("Value")]
        public SkeletonDataAsset _skeletonDataAsset;



        [Header("Value")]
        public string _idCharacter;
        public CharacterType _characterType;

        [Header("Info Base")]
        public int _atk;
        public int _hp;
        public int _def;
        public int _accuracy;
        public int _trueDamage;
        public int _critRate;
        public int _eva;
        public int _hpRegen;
        public float _cooldown;





    }
}
