using System;
using TMPro;
using UnityEngine;

namespace Challenges._8._DecouplingAndOptimization.Script
{
    [RequireComponent(typeof(TMP_Text))]
    public class TimerText : MonoBehaviour
    {
        public GameTimeMaterialTintSettings settings;
        private TMP_Text _text;
        
    
        // Start is called before the first frame update
        void Start()
        {
            _text = GetComponent<TMP_Text>();
            if (_text == null) throw new Exception("TMP_Text is missing on TimerText");
        }

        private int _gameHour;
        private int _gameSeconds;
        private float _builtUpTime;
        private void Update()
        {
            _builtUpTime += Time.deltaTime;
            bool changeHappened = false;
            while (_builtUpTime >= settings.realSecondsPerGameSecond)
            {
                IncrementTime();
                _builtUpTime -= settings.realSecondsPerGameSecond;
                changeHappened = true;
            }
            if(changeHappened) _text.text = new GameTime(_gameHour,_gameSeconds).ToString();
        }

        private void IncrementTime()
        {
            _gameSeconds++;
            if (_gameSeconds >= 60)
            {
                _gameSeconds = 0;
                _gameHour++;
            }

            if (_gameHour >= 24)
            {
                _gameHour = 0;
            }
        }
    }
}
