using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler // Интерфейсы
    {
        /// <summary>
        /// Фон Джойстика
        /// </summary>
        [SerializeField] private Image m_VisualJoystick;

        /// <summary>
        /// Джойстик
        /// </summary>
        [SerializeField] private Image m_Stick;

        /// <summary>
        /// Значение Джойстика
        /// </summary>
        public Vector3 Value { get; private set; }

        public void OnDrag(PointerEventData eventData) // Срабатывает, когда мы перемещаем элемент UI, если на нём этот скрипт
        {
            Vector2 position = Vector2.zero;
            
            //Переводим координаты экрана на задний фон Джойстика (из 1920х1080 в 400х400)
            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (m_VisualJoystick.rectTransform,
                eventData.position, // Текущая позиция 
                eventData.pressEventCamera, // Камера, связанная с последним событием OnPointerPress.
                out position); // 0, 0 - левый нижний, 400, 400 - правый верхний угол

            // Нормализуем значение в фоне Джойстика
            position.x = (position.x / m_VisualJoystick.rectTransform.sizeDelta.x); // sizeDelta.x = значение Width в Rect Transform
            position.y = (position.y / m_VisualJoystick.rectTransform.sizeDelta.y); // 400/400 = 1, 200/400 = 0.5, 0/400 = 0

            /* Было моё
            // Смещаем координаты в центр, чтобы был положительный и отрицательный шаг от -1 до +1
            position.x = position.x * 2 - 1;
            position.y = position.y * 2 - 1;

            Value = new Vector3(position.x, position.y, 0);

            // Окончательная нормализация, если мы мышкой за пределы фона, то всё равно значение от -1 до +1
            if (Value.magnitude > 1) // Длина вектора
                Value = Value.normalized;

            // Pivot point у стика в центре(0.5, 0.5). Что нужно что бы сместить стик до конца фона не вылязя за пределы?
            // Мы передвигаем стик вправо на 200(половина фона) и отнимаем 75(половину стика)
            // Это мы его смещаем на 1/2 фона минус 1/2 самого стика
            float offsetX = m_VisualJoystick.rectTransform.sizeDelta.x / 2 - m_Stick.rectTransform.sizeDelta.x / 2; 
            float offsetY = m_VisualJoystick.rectTransform.sizeDelta.y / 2 - m_Stick.rectTransform.sizeDelta.y / 2;

            //  anchoredPosition - Положение центра этого RectTransform относительно опорной точки привязки.
            m_Stick.rectTransform.anchoredPosition = new Vector2(Value.x * offsetX, Value.y * offsetY);
            */

            float x = (m_VisualJoystick.rectTransform.pivot.x == 1f) ? position.x * 2 + 1 : position.x * 2 - 1;
            float y = (m_VisualJoystick.rectTransform.pivot.y == 1f) ? position.y * 2 + 1 : position.y * 2 - 1;

            Value = new Vector3(x, y, 0);
            Value = (Value.magnitude > 1) ? Value.normalized : Value;

            //to define the area in which joystick can move around
            m_Stick.rectTransform.anchoredPosition = new Vector3(Value.x * (m_VisualJoystick.rectTransform.sizeDelta.x / 3)
                                                                   , Value.y * (m_VisualJoystick.rectTransform.sizeDelta.y) / 3);

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Value = Vector3.zero;
            m_Stick.rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}

