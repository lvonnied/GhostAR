﻿using UnityEngine;
using UnityEngine.UI;

namespace HSR.GhostAR.GameTime
{
    public class Countdown : MonoBehaviour
    {

        private float _totalTime;
        private float _elapsedTime;
        private float _lastCaughtTime;
        private bool _hasWaited;

        public bool gameHasEnded { get; set; }
        public Text countDownText;
        public static Countdown s_instance;

        public Countdown(float _totalTime, float _elapsedTime, float _lastCaughtTime, bool gameHasEnded, bool _hasWaited, Text countDownText)
        {
            this._totalTime = _totalTime;
            this._elapsedTime = _elapsedTime;
            this._lastCaughtTime = _lastCaughtTime;
            this.gameHasEnded = gameHasEnded;
            this._hasWaited = _hasWaited;
            this.countDownText = countDownText;
        }

        private void Awake()
        {
            if (s_instance)
            {
                Debug.Log("Warning: Overriding instance reference");
            }
            s_instance = this;
        }

        private void Start()
        {
            gameHasEnded = false;
            _totalTime = 5f;
            _lastCaughtTime = 0;
            _hasWaited = false;
            _elapsedTime = 0;
        }

        private void Update()
        {
            if (!gameHasEnded)
            {
                UpdateCountDownTextAndTime();
                if (_elapsedTime >= _totalTime - 1)
                {
                    gameHasEnded = true;
                }
            }
            else
            {
                EndScreen.s_instance.SetEndScreenInfo();
                if (!EndScreen.s_instance.hasBeenBuilt)
                {
                    countDownText.enabled = false;
                    EndScreen.s_instance.baseScore = Score.s_instance.score;
                    EndScreen.s_instance.ActivateEndScreen();
                }
            }
        }

        /// <summary>
        /// Calculates the remaining time and updates the UI-Element displaying the time by flooring and parsing the float number into a String
        /// </summary>
        public void UpdateCountDownTextAndTime()
        {
            _elapsedTime += Time.deltaTime;
            countDownText.text = Mathf.FloorToInt(_totalTime - _elapsedTime).ToString();
        }

        public int getTimeBonus()
        {
            int timeBonus = Mathf.FloorToInt(_totalTime - _lastCaughtTime);
            return timeBonus;
        }

        public float getCentisecondOfLastCaughtGhost()
        {
            return Mathf.Ceil(_lastCaughtTime * 100);
        }
    }
}
