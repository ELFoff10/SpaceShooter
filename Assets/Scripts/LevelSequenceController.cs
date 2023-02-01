using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "main_menu";

        public Episode CurrentEpisode { get; private set; }
        public int CurrentLevel { get; private set; }
        public bool LastLevelResult { get; private set; }
        public PlayerStatistics LevelStatistics { get; private set; }
        public static SpaceShip PlayerShip { get; set; }

        //protected override void Awake()
        //{
        //    //Load();
        //}

        public void StartEpisode(Episode episode)
        {
            CurrentEpisode = episode;
            CurrentLevel = 0;

            // сбрасываем статы перед началом эпизода
            LevelStatistics = new PlayerStatistics();
            LevelStatistics.Reset();

            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;

            CalculateLevelStatistics();

            ResultPanelController.Instance.ShowResults(LevelStatistics, success);
        }

        public void AdvanceLevel()
        {
            LevelStatistics.Reset();

            CurrentLevel++;

            if (CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }

        private void CalculateLevelStatistics()
        {
            LevelStatistics.m_Score = Player.Instance.Score;
            LevelStatistics.m_NumKills = Player.Instance.NumKills;
            LevelStatistics.m_KillsBonusScore = Player.Instance.NumKills * 10;
            LevelStatistics.m_Time = (int) LevelController.Instance.LevelTime;

            if (LevelStatistics.m_Score > LevelStatistics.m_RecordScore)
            {
                LevelStatistics.m_RecordScore = LevelStatistics.m_Score;
            }

            if (LevelStatistics.m_NumKills > LevelStatistics.m_RecordKills)
            {
                LevelStatistics.m_RecordKills = LevelStatistics.m_NumKills;
            }

            if (LevelStatistics.m_Time > LevelStatistics.m_RecordTime)
            {
                LevelStatistics.m_RecordTime = LevelStatistics.m_Time;
            }

            //Save();
        }

        //private void Save()
        //{
        //    PlayerPrefs.SetInt("RecordScore", LevelStatistics.m_RecordScore);
        //    PlayerPrefs.SetInt("RecordKills", LevelStatistics.m_RecordKills);
        //    PlayerPrefs.SetInt("RecordTime", LevelStatistics.m_RecordTime);
        //}

        //private void Load() 
        //{
        //    if (PlayerPrefs.GetInt("RecordScore", LevelStatistics.m_RecordScore) != 0)
        //    {
        //        LevelStatistics.m_RecordScore = PlayerPrefs.GetInt("RecordScore", LevelStatistics.m_RecordScore);
        //        LevelStatistics.m_RecordKills = PlayerPrefs.GetInt("RecordKills", LevelStatistics.m_RecordKills);
        //        LevelStatistics.m_RecordTime = PlayerPrefs.GetInt("RecordTime", LevelStatistics.m_RecordTime);
        //    }
        //}
    }
}
