using TadPoleFramework.Game;
using UnityEngine;

namespace TadPoleFramework
{
    public class GameModel : BaseGameModel
    {
        #region Score //////////////////////////////////////////////////////
        protected string prefKey_Score = "Score";
        public int Score
        {
            get { return PlayerPrefs.GetInt(prefKey_Score); }
            set
            {
                PlayerPrefs.SetInt(prefKey_Score, value);
                OnPropertyChanged(nameof(Score));
            }
        }
        #endregion
        
        #region InstantScore //////////////////////////////////////////////////////
        protected string prefKey_InstantScore = "InstantScore";
        public int InstantScore
        {
            get { return PlayerPrefs.GetInt(prefKey_InstantScore); }
            set
            {
                PlayerPrefs.SetInt(prefKey_InstantScore, value);
                OnPropertyChanged(nameof(InstantScore));
            }
        }
        #endregion
        

        #region Level //////////////////////////////////////////////////////
        protected string prefKey_Level = "Level";
        public int Level
        {
            get { return PlayerPrefs.GetInt(prefKey_Level); }
            set
            {
                PlayerPrefs.SetInt(prefKey_Level, value);
                OnPropertyChanged(nameof(Level));
            }
        }
        #endregion
        
        public GameModel()
        {
            #region Score //////////////////////////////////////////////////
            if (!PlayerPrefs.HasKey(prefKey_Score))
                Score = 0;
            else
                Score = PlayerPrefs.GetInt(prefKey_Score);
            #endregion
            
            #region InstantScore //////////////////////////////////////////////////
            if (!PlayerPrefs.HasKey(prefKey_InstantScore))
                InstantScore = 0;
            else
                InstantScore = PlayerPrefs.GetInt(prefKey_InstantScore);
            #endregion
            
            #region Level //////////////////////////////////////////////////
            if (!PlayerPrefs.HasKey(prefKey_Level))
                Level = 1;
            else
                Level = PlayerPrefs.GetInt(prefKey_Level);
            #endregion

        }
    }
}