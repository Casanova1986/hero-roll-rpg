using DG.Tweening;
using UnityEngine;

namespace HeroRoll.Battle
{
    public enum StatePlayerStay
    {
        Start = 0,
        Buff = 1,
        Debuff = 2,
        Attack = 3
    }
    public class CharacterController : MonoBehaviour
    {
        [Header("Var")]
        [SerializeField] GameObject _characterIcon;


        [Header("Value")]
        public StatePlayerStay _statePlayerStay;





        #region Animation
        public void PlayerMove(Vector3 targetPosition, float hight = 0.25f, float duration = 0.4f)
        {
            Vector3 start = this.transform.position;

            // Tạo điểm giữa cao lên thành vòng cung
            Vector3 mid = (start + targetPosition) / 2f;
            mid.y += hight; // độ cao của vòng cung

            Vector3[] path = new Vector3[] { start, mid, targetPosition };

            this.transform.DOPath(
                path,
                duration,        // thời gian
                PathType.CatmullRom
            );
        }
        #endregion
    }
}