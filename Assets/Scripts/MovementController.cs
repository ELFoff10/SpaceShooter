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

        [SerializeField] private PointerClickHold m_MobileFirePrimary;
        [SerializeField] private PointerClickHold m_MobileFireSecondary;

        private void Start()
        {          
            if (m_ControlMode == ControlMode.Keyboard)
            {
                m_MobileJoystick.gameObject.SetActive(false);

                m_MobileFirePrimary.gameObject.SetActive(false);
                m_MobileFireSecondary.gameObject.SetActive(false);
            }
            else
            {
                m_MobileJoystick.gameObject.SetActive(true);

                m_MobileFirePrimary.gameObject.SetActive(true);
                m_MobileFireSecondary.gameObject.SetActive(true);
            }

            // ����� ������� ����� ������ ����
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

            //����
            if (m_ControlMode == ControlMode.Keyboard)
                ControlKeyboard();
            //����
            if (m_ControlMode == ControlMode.Mobile)
                ControlMoblile();
        }

        private void ControlMoblile()
        {
            /*Vector3*/ var dir = m_MobileJoystick.Value; // ����������� �����

            // ��������� ������������ 2-�� ��������
            var dot = Vector2.Dot(dir, m_TargetShip.transform.up); // dir � ������ ����� ������������ ������, ������ ���� ����� (-1), �� ������ ��������� ������
            var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right); // ���� �� ���� �������, �� ������� = 1, ���� ������, �� -1, ��������������� = 0.

            if (m_MobileFirePrimary.Hold == true)
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }

            if (m_MobileFireSecondary.Hold == true)
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }

            m_TargetShip.ThrustControl = /*dir.y;*/ Mathf.Max(0, dot); // �������� �����. dot = -1, 0 ��� +1
            m_TargetShip.TorqueControl = /*dir.x;*/ -dot2; // ��������
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

            if (Input.GetKey(KeyCode.Space))
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }

            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }
    }
}
