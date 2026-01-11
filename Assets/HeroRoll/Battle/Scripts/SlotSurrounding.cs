using System.Collections.Generic;
using NTHiep.MiniOdin;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class SlotSurrounding : MonoBehaviour
    {
        [Header("<color=red>Var")]
        [SerializeField] SpriteRenderer _iconGround;
        [SerializeField] List<GameObject> _itemSubInSlots;

        
        [Header("<color=red>Value")]
        public int _idSlot = 0;
        public int _itemSubInSlotActive = 0;




        void Awake()
        {

        }
        void Start()
        {
            ActiveSlotSubAround(false);
            SetUpItemIconSubInSlot();
        }
        // bool isOn = false;
        // [Button]
        // public void Test()
        // {
        //     Debug.Log(AssetLoader.instance._dictItemSlotSubAround.Count);
        //     _iconGround.sprite = AssetLoader.instance._dictItemSlotSubAround.Get(isOn);
        //     isOn = !isOn;
        // }

        public void ActiveSlotSubAround(bool isOn)
        {
            _iconGround.sprite = AssetLoader.instance._dictGroundSlotSurrounding.Get(isOn);
        }

        public void UpdateItemSubSlot(System.Action<bool> fullActive)
        {
            // (List<GameObject> listSlotPick, List<GameObject> listSlotRest) = NTHiep.Tool.RandomUtil.SplitTwoListRandom(itemSubInSlot, itemSubInSlotActive);

            // listSlotPick.ForEach(itemSlotPick =>
            // {
            //     itemSlotPick.SetActive(true);
            // });

            // listSlotRest.ForEach(itemSlotRest =>
            // {
            //     itemSlotRest.SetActive(false);
            // });
            if (_itemSubInSlotActive == _itemSubInSlots.Count)
            {
                fullActive?.Invoke(true);
                return;
            }
            if (_itemSubInSlotActive == 0)
            {
                _itemSubInSlots.ForEach(f => f.SetActive(false));
                SetUpItemIconSubInSlot(true);
            }


            _itemSubInSlotActive++;
            List<GameObject> listSlotRest = new List<GameObject>();
            foreach (var item in _itemSubInSlots)
            {
                if (!item.activeSelf)
                {
                    listSlotRest.Add(item);
                }
            }

            NTHiep.Tool.RandomUtil.PickAndRemove(listSlotRest).gameObject.SetActive(true);

            foreach (var item in listSlotRest)
            {
                item.gameObject.SetActive(false);
            }
            fullActive?.Invoke(false);

            ActiveSlotSubAround(true);
        }
        public void SetUpItemIconSubInSlot(bool isHouse = false)
        {
            if (isHouse)
            {
                _itemSubInSlots.ForEach(f =>
                {
                    f.GetComponent<SpriteRenderer>().sprite = AssetLoader.instance._dictItemSlotSubAround.Get(0);
                });
            }
            else
            {
                _itemSubInSlots.ForEach(f =>
                {
                    f.GetComponent<SpriteRenderer>().sprite = NTHiep.Tool.RandomUtil.PickExclude(AssetLoader.instance._dictItemSlotSubAround.ToList(), new List<Sprite> { AssetLoader.instance._dictItemSlotSubAround.Get(0) });
                });
            }
        }
    }

}