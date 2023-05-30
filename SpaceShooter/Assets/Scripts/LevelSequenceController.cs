using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;
using System.Collections;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "Main_Menu";

        public Episode CurrentEpisode { get; private set; }
        public int CurrentLevel { get; private set; }
        public bool LastLevelResult { get; private set; }
        public PlayerStatistics LevelStatistics { get; private set; }
        public static SpaceShip PlayerShip { get; set; }

        protected override void Awake()
        {
            base.Awake();
            //Load();
        }

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
            LevelStatistics.m_StatScore = Player.Instance.m_PlayerScore;
            LevelStatistics.m_StatNumKills = Player.Instance.m_PlayerNumKills;
            LevelStatistics.m_StatKillsBonusScore = Player.Instance.m_PlayerNumKills * 10;
            LevelStatistics.m_StatTime = (int) LevelController.Instance.LevelTime;


            if (Player.Instance.m_PlayerScore >= LevelStatistics.m_StatRecordScore)
            {
                LevelStatistics.m_StatRecordScore = Player.Instance.m_PlayerScore;
            }

            if (Player.Instance.m_PlayerNumKills > LevelStatistics.m_StatRecordKills)
            {
                LevelStatistics.m_StatRecordKills = Player.Instance.m_PlayerNumKills;
            }

            if ((int)LevelController.Instance.LevelTime > LevelStatistics.m_StatRecordTime)
            {
                LevelStatistics.m_StatRecordTime = (int)LevelController.Instance.LevelTime;
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
