using UnityEngine;

namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }    

        [SerializeField] private SpaceShip m_TargetShip;
        public void SetTargetShip(SpaceShip targetShip) => m_TargetShip = targetShip;

        [SerializeField] private VirtualJoystick m_MobileJoystick;

        [SerializeField] private ControlMode m_ControlMode;

        private void Start()
        {          
            if (m_ControlMode == ControlMode.Keyboard)
                m_MobileJoystick.gameObject.SetActive(false);
            else
                m_MobileJoystick.gameObject.SetActive(true);

            // Нужно сделать перед билдом игры
            //if (Application.isMobilePlatform)
            //{
            //    m_ContolMode = ControlMode.Mobile;
            //    m_MobileJoystick.gameObject.SetActive(true);
            //}
            //else
            //{
            //    m_ContolMode = ControlMode.Keyboard;
            //    m_MobileJoystick.gameObject.SetActive(false);
            //}
        }

        private void Update()
        {
            if (m_TargetShip == null) return;

            //база
            if (m_ControlMode == ControlMode.Keyboard)
                ControlKeyboard();
            //база
            if (m_ControlMode == ControlMode.Mobile)
                ControlMoblile();
        }

        private void ControlMoblile()
        {
            /*Vector3*/ var dir = m_MobileJoystick.Value; // Направление стика

            // Скалярное произведение 2-ух векторов
            var dot = Vector2.Dot(dir, m_TargetShip.transform.up); // dir и вектор стика направленный наверх, тоесть если влево (-1), то вектор направлен налево
            var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right); // если мы стик направо, то умножив = 1, если налево, то -1, перпендикулярно = 0.

            m_TargetShip.ThrustControl = /*dir.y;*/ Mathf.Max(0, dot); // Движение вперёд. dot = -1, 0 или +1
            m_TargetShip.TorqueControl = /*dir.x;*/ -dot2; // Разворот
        }

        private void ControlKeyboard()
        {
            float thrust = 0;
            float torque = 0;

            if (Input.GetKey(KeyCode.W))
            {
                thrust = 1.0f;               
            }

            if (Input.GetKey(KeyCode.S))
            {
                thrust = -1.0f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                torque = 1.0f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                torque = -1.0f;
            }

            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }
    }
}
