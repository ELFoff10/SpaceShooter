namespace SpaceShooter
{
    public class Timer
    {
        private float m_CurrentTime;

        public bool IsFinished => m_CurrentTime <= 0; // ���������� �� ������

        public Timer(float startTime)
        {
            StartTime(startTime);
        }

        public void StartTime(float startTime)
        {
            m_CurrentTime = startTime;
        }

        public void RemoveTime(float deltaTime)
        {
            if (m_CurrentTime <= 0)
                return;

            m_CurrentTime -= deltaTime;
        }
    }
}
