using System.Collections.Generic;
using NTHiep.MiniOdin;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class SlotSurrounding : MonoBehaviour
    {
        [Header("Value")]
        public int _idSlot = 0;
        public int _itemSubInSlotActive = 0;

        [Header("Var")]
        [SerializeField] SpriteRenderer _iconGround;
        [SerializeField] List<GameObject> _itemSubInSlots;


        void Awake()
        {

        }
        void Start()
        {

        }
        bool isOn = false;
        [Button]
        public void Test()
        {
            Debug.Log(AssetLoader.instance._dictItemSlotSubAround.Count);
            _iconGround.sprite = AssetLoader.instance._dictItemSlotSubAround.Get(isOn);
            isOn = !isOn;
        }
        public void UpdateItemSubSlot()
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

            _itemSubInSlotActive++;
            _iconGround.sprite = AssetLoader.instance._dictItemSlotSubAround.Get(true);
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

        }

    }

}