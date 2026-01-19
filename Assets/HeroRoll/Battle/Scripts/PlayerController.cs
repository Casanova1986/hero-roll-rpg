using System;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;




        [Header("Value")]
        public string _idCharacter;
        public int playerMaxHP = 0;
        public InfoCharacterBase _infoCharacterBase = new InfoCharacterBase
        {
            atk = 10,
            hp = 20,
            def = 5,
        };


        void Awake()
        {
            if (PlayerController.instance)
            {
                return;
            }
            instance = this;
        }
        void Start()
        {
            Init();
        }
        void Init()
        {
            _idCharacter = "Xihe";
            SetInitInfoData();
        }
        #region InfoBase
        void SetInitInfoData()
        {
            CharacterDataAsset characterDataAsset = CharacterAssetLoader.instance._characterDataAssets.Get(_idCharacter);
            _infoCharacterBase = new InfoCharacterBase
            {
                atk = characterDataAsset._atk,
                hp = characterDataAsset._hp,
                def = characterDataAsset._def,
                accuracy = characterDataAsset._accuracy,
                trueDamage = characterDataAsset._trueDamage,
                critRate = characterDataAsset._critRate,
                eva = characterDataAsset._eva,
                hpRegen = characterDataAsset._hpRegen,
            };
            playerMaxHP = characterDataAsset._hp;
            UIController.instace.SetUpHpBar(_infoCharacterBase.hp, playerMaxHP);
        }
        public void UpdateAtk(int value)
        {
            _infoCharacterBase.atk += value;
            if (_infoCharacterBase.atk <= 0)
            {
                _infoCharacterBase.atk = 0;
            }
        }
        public void UpdateHp(int value)
        {
            _infoCharacterBase.hp += value;
            if (_infoCharacterBase.hp <= 0)
            {
                _infoCharacterBase.hp = 0;
            }
            UIController.instace.SetUpHpBar(_infoCharacterBase.hp, playerMaxHP);
        }
        public void UpdateDef(int value)
        {
            _infoCharacterBase.def += value;
            if (_infoCharacterBase.def <= 0)
            {
                _infoCharacterBase.def = 0;
            }
        }
        public void UpdateAccuracy(int value)
        {
            _infoCharacterBase.accuracy += value;
            if (_infoCharacterBase.accuracy <= 0)
            {
                _infoCharacterBase.accuracy = 0;
            }
        }
        public void UpdateTrueDamage(int value)
        {
            _infoCharacterBase.trueDamage += value;
            if (_infoCharacterBase.trueDamage <= 0)
            {
                _infoCharacterBase.trueDamage = 0;
            }
        }
        public void UpdateCritRate(int value)
        {
            _infoCharacterBase.critRate += value;
            if (_infoCharacterBase.critRate <= 0)
            {
                _infoCharacterBase.critRate = 0;
            }
        }
        public void UpdateEva(int value)
        {
            _infoCharacterBase.eva += value;
            if (_infoCharacterBase.eva <= 0)
            {
                _infoCharacterBase.eva = 0;
            }
        }
        public void UpdateHpRegen(int value)
        {
            _infoCharacterBase.hpRegen += value;
            if (_infoCharacterBase.hpRegen <= 0)
            {
                _infoCharacterBase.hpRegen = 0;
            }
        }

        #endregion

    }
    [Serializable]
    public class InfoCharacterBase
    {
        public int atk;
        public int hp;
        public int def;
        public int accuracy;
        public int trueDamage;
        public int critRate;
        public int eva;
        public int hpRegen;
    }
}