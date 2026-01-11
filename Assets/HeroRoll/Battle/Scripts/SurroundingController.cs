using System.Collections.Generic;
using NTHiep.MiniOdin;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class SurroundingController : MonoBehaviour
    {
        [Header("<color=red>Var")]
        [SerializeField] List<SlotSurrounding> _slotSurroundingLevel1;
        [SerializeField] List<SlotSurrounding> _slotSurroundingLevel2;
        [SerializeField] List<SlotSurrounding> _slotSurroundingLevel3;

        public static SurroundingController instance;
        void Awake()
        {
            if (SurroundingController.instance != null)
            {
                return;
            }
            instance = this;
            AssignSlot();
        }

        void AssignSlot()
        {
            int maxIndexSlot = _slotSurroundingLevel1.Count + 4;
            List<int> idMainSlot = new List<int> { 0, 10, 20, 30 };
            List<int> lsIdSurrounding = new List<int>();

            for (int i = 0; i < maxIndexSlot; i++)
            {
                if (!idMainSlot.Contains(i))
                {
                    lsIdSurrounding.Add(i);
                }
            }

            for (int i = 0; i < lsIdSurrounding.Count; i++)
            {
                _slotSurroundingLevel1[i]._idSlot = lsIdSurrounding[i];
                _slotSurroundingLevel2[i]._idSlot = lsIdSurrounding[i];
                _slotSurroundingLevel3[i]._idSlot = lsIdSurrounding[i];
            }
        }

        public void UpdateSurrounding(int idSlot)
        {
            Debug.Log(idSlot);
            if (idSlot == 0 || idSlot == 10 || idSlot == 20 || idSlot == 30)
            {
                return;
            }
            SlotSurrounding slotSurroundingLevel1 = GetSlotSurrounding(idSlot, 1);
            SlotSurrounding slotSurroundingLevel2 = GetSlotSurrounding(idSlot, 2);
            SlotSurrounding slotSurroundingLevel3 = GetSlotSurrounding(idSlot, 3);

            slotSurroundingLevel1.UpdateItemSubSlot((fullActive) =>
            {
                if (fullActive)
                {
                    slotSurroundingLevel2.UpdateItemSubSlot((fullActive) =>
                    {
                        if (fullActive)
                        {
                            slotSurroundingLevel3.UpdateItemSubSlot((fullActive) =>
                            {
                                if (fullActive)
                                {

                                }
                            });
                        }
                    });
                }
            });


        }

        public SlotSurrounding GetSlotSurrounding(int idSlot, int level)
        {
            switch (level)
            {
                default:
                    return _slotSurroundingLevel1.Find(f => f._idSlot == idSlot);

                case 2:
                    return _slotSurroundingLevel2.Find(f => f._idSlot == idSlot);

                case 3:
                    return _slotSurroundingLevel3.Find(f => f._idSlot == idSlot);
            }
        }
    }
}