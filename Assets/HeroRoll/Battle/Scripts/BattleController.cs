using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NTHiep.MiniOdin;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace HeroRoll.Battle
{
    public class BattleController : MonoBehaviour
    {
        [ColorHeader("<color=red>Var")]
        public GameObject _hero;
        public List<GameObject> _lsMonster;
        public GameObject _sliderHpHero;
        public GameObject _sliderHpHeroDelay;
        public GameObject _sliderHpMonster;
        public GameObject _sliderHpMonsterDelay;

        [ColorHeader("<color=red>Value")]
        public int _totalHealthMonster = 0;
        public int _totalMaxHealthMonster = 0;
        public int _totalHealthHero = 0;
        public int _totalMaxHealthHero = 0;
        public bool isTurnHero = true;


        void Start()
        {
            Init();
        }
        void Init()
        {
            SeCharacterBattle("Xihe", new List<string>
            {
                "Menghuai",
                "Menghuai",
                "Menghuai"
            });
        }

        #region ThreadBattle

        #endregion

        #region Funtion

        [Button]
        void OnClickStart()
        {
            StartCoroutine(StartAttack());
        }
        IEnumerator StartAttack()
        {
            yield return new WaitForSeconds(0.25f);
            bool isDoneTurn = false;
            if (isTurnHero)
            {
                TestHeroAttack(() =>
                {
                    isDoneTurn = true;
                });
            }
            else
            {
                TestMonsterAttack(() =>
                {

                    isDoneTurn = true;
                });
            }
            yield return new WaitUntil(() => isDoneTurn);

            if (CheckEndGame())
            {
                ResultBattleController.instance.ShowPopup();
                if (IsHeroWin())
                {
                    ResultBattleController.instance.ShowResult(true);
                }
                else
                {
                    ResultBattleController.instance.ShowResult(false);
                }
                yield break;
            }

            isTurnHero = !isTurnHero;
            StartCoroutine(StartAttack());
        }
        void TestHeroAttack(System.Action completeTurn)
        {
            List<GameObject> lsMonsterRandom = _lsMonster.FindAll(f => f.activeSelf && f.GetComponent<MonsterModelController>()._infoCharacterBase.hp > 0);
            GameObject monsterAttacked = NTHiep.Tool.RandomUtil.Pick(lsMonsterRandom);
            MonsterModelController monster = monsterAttacked.GetComponent<MonsterModelController>();
            HeroModelController hero = _hero.GetComponent<HeroModelController>();


            HeroAttack(monsterAttacked, () =>
            {
                Debug.Log("Done Anim");
                Calculate();

                float value = _totalHealthMonster / (float)_totalMaxHealthMonster;
                UpdateHeath(value, isHero: false);
            }, () =>
            {
                completeTurn?.Invoke();
            });

            void Calculate()
            {
                int dameAtk = hero.CalculateDame(hero._infoCharacterBase.atk, monster._infoCharacterBase.def);
                int dameRemain = monster.UpdateHeath(-dameAtk);

                if (dameRemain > 0)
                {
                    dameAtk -= dameRemain;

                }
                if (monster._infoCharacterBase.hp <= 0)
                {
                    monster.StartDeath(() =>
                    {
                        monsterAttacked.SetActive(false);
                    });
                }
                _totalHealthMonster -= dameAtk;
            }
        }
        void TestMonsterAttack(System.Action completeTurn)
        {
            List<GameObject> lsMonsterRandom = _lsMonster.FindAll(f => f.activeSelf && f.GetComponent<MonsterModelController>()._infoCharacterBase.hp > 0);
            MonsterModelController monster = NTHiep.Tool.RandomUtil.Pick(lsMonsterRandom).GetComponent<MonsterModelController>();
            HeroModelController hero = _hero.GetComponent<HeroModelController>();

            MonsterAttack(monster.gameObject, () =>
            {
                Calculate();

                float value = _totalHealthHero / (float)_totalMaxHealthHero;
                UpdateHeath(value, isHero: true);
            }, () =>
            {
                completeTurn?.Invoke();
            });

            void Calculate()
            {
                int dameAtk = monster.CalculateDame(monster._infoCharacterBase.atk, hero._infoCharacterBase.def);
                int dameRemain = hero.UpdateHeath(-dameAtk);

                if (dameRemain > 0)
                {
                    dameAtk -= dameRemain;

                }
                if (hero._infoCharacterBase.hp <= 0)
                {
                    hero.StartDeath(() =>
                    {
                        // monsterAttacked.SetActive(false);
                    });
                }
                _totalHealthHero -= dameAtk;
            }
        }

        void HeroAttack(GameObject monsterTarget, System.Action onDoneAttack, System.Action onComplete)
        {
            HeroModelController hero = _hero.GetComponent<HeroModelController>();
            hero.transform.DOMoveX(monsterTarget.transform.position.x - 0.5f, 0.15f);

            hero.StartAttack(() =>
            {
                onDoneAttack?.Invoke();
                hero.transform.DOMoveX(hero.transform.parent.position.x, 0.15f).OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
            });
        }
        void MonsterAttack(GameObject monsterAttack, System.Action onDoneAttack, System.Action onComplete)
        {
            MonsterModelController monster = monsterAttack.GetComponent<MonsterModelController>();
            monster.transform.DOMoveX(_hero.transform.position.x + 0.75f, 0.15f);

            monster.StartAttack(() =>
            {
                onDoneAttack?.Invoke();
                monster.transform.DOMoveX(monster.transform.parent.position.x, 0.15f).OnComplete(() =>
                {
                    onComplete?.Invoke();
                });
            });
        }

        void UpdateHeath(float value, bool isHero)
        {
            if (isHero)
            {
                _sliderHpHero.GetComponent<Slider>().DOValue(value, 0.15f);
                _sliderHpHeroDelay.GetComponent<Slider>().DOValue(value, 0.15f).SetDelay(0.15f);
            }
            else
            {
                _sliderHpMonster.GetComponent<Slider>().DOValue(value, 0.15f);
                _sliderHpMonsterDelay.GetComponent<Slider>().DOValue(value, 0.15f).SetDelay(0.15f);
            }
        }

        #endregion

        #region Anim

        #endregion

        #region Setter
        public void SeCharacterBattle(string idHero, List<string> lsIdMonster)
        {
            HeroModelController hero = _hero.GetComponent<HeroModelController>();
            hero._idCharacter = idHero;
            hero.SetInfoData();
            hero.SetSkeletonData();
            hero.IdleAnim();
            _totalHealthHero += hero._infoCharacterBase.hp;
            _totalMaxHealthHero += hero._infoCharacterBase.hp;


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
                    _totalHealthMonster += monsterModelController._infoCharacterBase.hp;
                    _totalMaxHealthMonster += monsterModelController._infoCharacterBase.hp;
                }
            }


            UpdateHeath(1f, true);
            UpdateHeath(1f, false);
        }

        public void SetHpTotal()
        {

        }
        #endregion

        #region Getter
        bool CheckEndGame()
        {
            HeroModelController heroModelController = _hero.GetComponent<HeroModelController>();
            if (heroModelController._infoCharacterBase.hp <= 0)
            {
                return true;
            }
            bool isEndGame = true;
            List<GameObject> lsMonster = _lsMonster.FindAll(f => f.activeSelf && f.GetComponent<MonsterModelController>()._infoCharacterBase.hp > 0);
            lsMonster.ForEach(f =>
            {
                MonsterModelController monsterModelController = f.GetComponent<MonsterModelController>();
                if (monsterModelController._infoCharacterBase.hp > 0)
                {
                    isEndGame = false;
                }
            });
            return isEndGame;
        }
        bool IsHeroWin()
        {
            HeroModelController heroModelController = _hero.GetComponent<HeroModelController>();
            if (heroModelController._infoCharacterBase.hp <= 0)
            {
                return false;
            }
            return true;
        }
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