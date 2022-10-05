using TadPoleFramework.Core;
using UnityEngine;

namespace TadPoleFramework.Game
{
    public abstract class BaseGameModel : BaseModel
    {
        #region IsMusicOn //////////////////////////////////////////////////////
        protected string prefKey_IsMusicOn = "IsMusicOn";
        public bool IsMusicOn
        {
            get { return (PlayerPrefs.GetInt(prefKey_IsMusicOn) == 1) ? true : false; }
            set
            {
                PlayerPrefs.SetInt(prefKey_IsMusicOn, (value) ? 1 : 0);
                OnPropertyChanged(nameof(IsMusicOn));
            }
        }
        #endregion

        #region IsSoundOn //////////////////////////////////////////////////////
        protected string prefKey_IsSoundOn = "IsSoundOn";
        public bool IsSoundOn
        {
            get { return (PlayerPrefs.GetInt(prefKey_IsSoundOn) == 1) ? true : false; }
            set
            {
                PlayerPrefs.SetInt(prefKey_IsSoundOn, (value) ? 1 : 0);
                OnPropertyChanged(nameof(IsSoundOn));
            }
        }
        #endregion

        #region IsHepticOn //////////////////////////////////////////////////////
        protected string prefKey_IsHapticOn = "IsHapticOn";
        public bool IsHapticOn
        {
            get { return (PlayerPrefs.GetInt(prefKey_IsHapticOn) == 1) ? true : false; }
            set
            {
                PlayerPrefs.SetInt(prefKey_IsHapticOn, (value) ? 1 : 0);
                OnPropertyChanged(nameof(IsHapticOn));
            }
        }
        #endregion
        
        #region IsJoystickOn //////////////////////////////////////////////////////
        protected string prefKey_IsJoystickOn = "IsJoystickOn";
        public bool IsJoystickOn
        {
            get { return (PlayerPrefs.GetInt(prefKey_IsJoystickOn) == 1) ? true : false; }
            set
            {
                PlayerPrefs.SetInt(prefKey_IsJoystickOn, (value) ? 1 : 0);
                OnPropertyChanged(nameof(IsJoystickOn));
            }
        }
        #endregion

        #region IsAdminOn //////////////////////////////////////////////////////
        protected string prefKey_IsAdminOn = "IsAdminOn";
        public bool IsAdminOn
        {
            get { return (PlayerPrefs.GetInt(prefKey_IsAdminOn) == 1) ? true : false; }
            set
            {
                PlayerPrefs.SetInt(prefKey_IsAdminOn, (value) ? 1 : 0);
                OnPropertyChanged(nameof(IsAdminOn));
            }
        }
        #endregion

        protected BaseGameModel()
        {
            #region IsMusicOn //////////////////////////////////////////////////
            if (!PlayerPrefs.HasKey(prefKey_IsMusicOn))
                IsMusicOn = true;
            else
                IsMusicOn = (PlayerPrefs.GetInt(prefKey_IsMusicOn) == 1) ? true : false;
            #endregion

            #region IsSoundOn //////////////////////////////////////////////////
            if (!PlayerPrefs.HasKey(prefKey_IsSoundOn))
                IsSoundOn = true;
            else
                IsSoundOn = (PlayerPrefs.GetInt(prefKey_IsSoundOn) == 1) ? true : false;
            #endregion

            #region IsHepticOn //////////////////////////////////////////////////
            if (!PlayerPrefs.HasKey(prefKey_IsHapticOn))
                IsHapticOn = true;
            else
                IsHapticOn = (PlayerPrefs.GetInt(prefKey_IsHapticOn) == 1) ? true : false;
            #endregion
            
            #region IsHepticOn //////////////////////////////////////////////////
            if (!PlayerPrefs.HasKey(prefKey_IsJoystickOn))
                IsJoystickOn = true;
            else
                IsJoystickOn = (PlayerPrefs.GetInt(prefKey_IsJoystickOn) == 1) ? true : false;
            #endregion

            #region IsAdminOn //////////////////////////////////////////////////
            if (!PlayerPrefs.HasKey(prefKey_IsAdminOn))
                IsAdminOn = false;
            else
                IsAdminOn = (PlayerPrefs.GetInt(prefKey_IsAdminOn) == 1) ? true : false;
            #endregion
        }
    }
}