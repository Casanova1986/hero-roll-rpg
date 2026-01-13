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
        public UIBattleController _UIBattleController;
        public GameObject _hero;
        public List<GameObject> _lsMonster;

        [ColorHeader("<color=red>Value")]
        public int _totalHealthMonster = 0;
        public int _totalMaxHealthMonster = 0;
        public int _totalHealthHero = 0;
        public int _totalMaxHealthHero = 0;
        public bool isTurnHero = true;



        void OnEnable()
        {
            StartCoroutine(StartThread());
        }
        void OnDisable()
        {
            ResetGame();
        }
        void Start()
        {

        }
        void Init()
        {
            int numberMonster = 0;
            numberMonster = BoardController.instance._characterMoveController._dotItemStay._infoDotItem.numberEnemy;
            if (numberMonster == 1)
            {
                SetCharacterBattle(new List<string>
                {
                    "Menghuai",
                });
            }
            else if (numberMonster == 2)
            {
                SetCharacterBattle(new List<string>
                {
                    "Menghuai",
                    "Menghuai",
                });
            }
            else if (numberMonster == 3)
            {
                SetCharacterBattle(new List<string>
                {
                    "Menghuai",
                    "Menghuai",
                    "Menghuai"
                });
            }
            else
            {
                SetCharacterBattle(new List<string>
                {
                    "Menghuai",
                    "Menghuai",
                    "Menghuai",
                    "Menghuai"
                });

            }
        }

        #region ThreadBattle

        IEnumerator StartThread()
        {
            Init();
            yield return new WaitForSeconds(1f);


            //// Thread 1
            yield return StartCoroutine(Thread_1());

            //// Thread 2
            yield return StartCoroutine(Thread_2());

            //// Thread 3
            yield return StartCoroutine(Thread_3());

            //// Thread 4
            yield return StartCoroutine(Thread_4());

            //// Thread end
            yield return StartCoroutine(Thread_end());
        }
        IEnumerator Thread_1()
        {
            yield return new WaitForSeconds(0.5f);
        }
        IEnumerator Thread_2()
        {
            yield break;
        }
        IEnumerator Thread_3()
        {
            yield return StartCoroutine(StartAttack());
            Debug.Log("______");
        }
        IEnumerator Thread_4()
        {
            yield break;
        }

        IEnumerator Thread_end()
        {
            yield break;
        }

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
            yield return StartCoroutine(StartAttack());
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
                _UIBattleController.UpdateDisplayHeath(value, isHero: false);
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
                _UIBattleController.UpdateDisplayHeath(value, isHero: true);
            }, () =>
            {
                completeTurn?.Invoke();
            });

            void Calculate()
            {
                int dameAtk = monster.CalculateDame(monster._infoCharacterBase.atk, hero._infoCharacterBase.def);
                int dameRemain = hero.UpdateHeath(-dameAtk);
                PlayerController.instance.UpdateHp(-dameAtk);
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



        #endregion

        #region Anim

        #endregion

        #region Setter
        public void SetCharacterBattle(List<string> lsIdMonster)
        {
            HeroModelController hero = _hero.GetComponent<HeroModelController>();
            hero._idCharacter = PlayerController.instance._idCharacter;
            hero.SetInfoCharacterBase(new InfoCharacterBase
            {
                hp = PlayerController.instance._infoCharacterBase.hp,
                atk = PlayerController.instance._infoCharacterBase.atk,
                accuracy = PlayerController.instance._infoCharacterBase.accuracy,
                critRate = PlayerController.instance._infoCharacterBase.critRate,
                def = PlayerController.instance._infoCharacterBase.def,
                eva = PlayerController.instance._infoCharacterBase.eva,
                hpRegen = PlayerController.instance._infoCharacterBase.hpRegen,
                trueDamage = PlayerController.instance._infoCharacterBase.trueDamage
            });
            hero.SetSkeletonData();
            hero.IdleAnim();
            _totalHealthHero += hero._infoCharacterBase.hp;
            _totalMaxHealthHero += PlayerController.instance.playerMaxHP;


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


            _UIBattleController.UpdateDisplayHeath(PlayerController.instance._infoCharacterBase.hp / (float)_totalMaxHealthHero, true);
            _UIBattleController.UpdateDisplayHeath(1f, false);
        }

        public void SetHpTotal()
        {

        }

        void ResetGame()
        {
            _totalHealthMonster = 0;
            _totalMaxHealthMonster = 0;
            _totalHealthHero = 0;
            _totalMaxHealthHero = 0;
            isTurnHero = true;
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