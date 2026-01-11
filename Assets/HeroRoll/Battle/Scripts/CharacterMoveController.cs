using System;
using System.Collections;
using DG.Tweening;
using NTHiep.MiniOdin;
using UnityEngine;

namespace HeroRoll.Battle
{

    public class CharacterMoveController : MonoBehaviour
    {
        [ColorHeader("<color=red>Var")]
        [SerializeField] GameObject _characterIcon;


        [ColorHeader("<color=red>Value")]
        public DotItem _dotItemStay;




        #region Animation
        public IEnumerator PlayerMove(Vector3 targetPosition, float hight = 0.25f, float duration = 0.4f)
        {
            bool finishAnim = false;
            Vector3 start = this.transform.position;

            // Tạo điểm giữa cao lên thành vòng cung
            Vector3 mid = (start + targetPosition) / 2f;
            mid.y += hight; // độ cao của vòng cung

            Vector3[] path = new Vector3[] { start, mid, targetPosition };

            this.transform.DOPath(
                path,
                duration,        // thời gian
                PathType.CatmullRom
            ).OnComplete(() => { finishAnim = true; });

            yield return new WaitUntil(() => finishAnim);
        }
        #endregion
    }
}