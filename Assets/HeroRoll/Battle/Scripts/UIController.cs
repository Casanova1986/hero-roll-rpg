using NTHiep.MiniOdin;
using TMPro;
using UnityEngine;

namespace HeroRoll.Battle
{
    public class UIController : MonoBehaviour
    {
        public static UIController instace;
        [ColorHeader("<color=red>Var")]
        [SerializeField] GameObject _txtTurnBoss;
        [SerializeField] GameObject _txtCurrentFloor;
        [SerializeField] GameObject _btnRoll;
        [SerializeField] GameObject _hpBar;
        [SerializeField] GameObject _itemBar;
        void Awake()
        {
            if (UIController.instace != null)
            {
                return;
            }
            instace = this;
        }
        void Start()
        {
            Init();
        }
        void Init()
        {
            SetTxtTurnBoss(GameController.instace._turnBossAttackLeft);
            SetTxtCurrentFloor(GameController.instace._currentFloorPlayerStay);
        }
        public void SetTxtTurnBoss(int turn)
        {
            _txtTurnBoss.GetComponent<TextMeshProUGUI>().text = $"<color=red>Turn left: {turn}</color>";
        }
        public void SetTxtCurrentFloor(int value)
        {
            _txtCurrentFloor.GetComponent<TextMeshProUGUI>().text = $"<color=green>Current Floor: {value}</color>";
        }
        public void SetUpHpBar(float value, float maxValue)
        {
            _hpBar.GetComponent<DisplayBar>().SetValueSlider(value, maxValue, duration: 0.2f);
        }

        public void OnEnableButtonRoll(bool isOn)
        {
            _btnRoll.SetActive(isOn);
        }
    }

}