using System.Collections.Generic;
using DG.Tweening;
using NTHiep.MiniOdin;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class BattleController : MonoBehaviour
    {
        [Header("Var")]
        // [SerializeField] Transform _posHero;
        // [SerializeField] List<Transform> _posMonster;
        public GameObject _hero;
        public List<GameObject> _lsMonster;
        public GameObject _sliderHpHero;
        public GameObject _sliderHpMonster;

        [Header("Value")]
        public bool isTurnHero = true;


        void Start()
        {
            Init();
        }
        #region Funtion
        void Init()
        {
            SeCharacterBattle("Xihe", new List<string>
            {
                "Menghuai",
                "Menghuai",
                "Menghuai"
            });
        }
        [Button]
        void TestAttack()
        {
            List<GameObject> lsMonsterRandom = _lsMonster.FindAll(f => f.activeSelf);
            HeroAttack(NTHiep.Tool.RandomUtil.Pick(lsMonsterRandom), () =>
            {
                Debug.Log("Done Anim");
            });
        }
        void HeroAttack(GameObject monsterTarget, System.Action onComplete)
        {
            HeroModelController hero = _hero.GetComponent<HeroModelController>();
            hero.transform.DOMoveX(monsterTarget.transform.position.x - 0.5f, 0.15f);

            hero.StartAttack(() =>
            {


                hero.transform.DOMoveX(hero.transform.parent.position.x, 0.15f).OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
            });
        }
        #endregion

        #region Setter
        public void SeCharacterBattle(string idHero, List<string> lsIdMonster)
        {
            HeroModelController hero = _hero.GetComponent<HeroModelController>();
            hero._idCharacter = idHero;
            hero.SetInfoData();
            hero.SetSkeletonData();
            hero.IdleAnim();


            for (int i = 0; i < _lsMonster.Count; i++)
            {
                _lsMonster[i].SetActive(i < lsIdMonster.Count);
                if (i < lsIdMonster.Count)
                {
                    MonsterModelController monsterModelController = _lsMonster[i].GetComponent<MonsterModelController>();
                    monsterModelController._idCharacter = lsIdMonster[i];
                    monsterModelController.SetInfoData();
                    monsterModelController.SetSkeletonData();
                    monsterModelController.IdleAnim();
                }
            }
        }


        #endregion

        #region Getter
        // public GameObject SpawnCharacter(bool isSpawnHero = true)
        // {
        //     CharacterAssetLoader characterAssetLoader = CharacterAssetLoader.instance;
        //     GameObject gbCharacter = null;
        //     if (isSpawnHero)
        //     {
        //         gbCharacter = Instantiate(characterAssetLoader._heroPrefab, _posHero);
        //         gbCharacter.transform.position = _posHero.transform.position;
        //         return gbCharacter;
        //     }
        //     else
        //     {
        //         if (_lsMonster.Count == 4)
        //         {
        //             return null;
        //         }
        //         if (_lsMonster.Count == 0)
        //         {
        //             gbCharacter = Instantiate(characterAssetLoader._monsterPrefab, _posMonster[0]);
        //         }
        //         else
        //         {
        //             gbCharacter = Instantiate(characterAssetLoader._monsterPrefab, _posMonster[_lsMonster.Count]);
        //         }
        //         gbCharacter.transform.position = _posHero.transform.position;
        //         return gbCharacter;
        //     }
        // }
        #endregion
    }
}