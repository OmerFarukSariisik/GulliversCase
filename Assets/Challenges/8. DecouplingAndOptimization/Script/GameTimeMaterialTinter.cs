using UnityEngine;

namespace Challenges._8._DecouplingAndOptimization.Script
{
    public struct GameTime
    {
        public readonly int Hour;
        public readonly int Seconds;

        public GameTime(int hour, int seconds)
        {
            Hour = hour;
            Seconds = seconds;
        }

        public override string ToString()
        {
            return Hour.ToString("D2") + ":" + Seconds.ToString("D2");
        }
    }
    public class GameTimeMaterialTinter : MonoBehaviour
    {
        [SerializeField]
        private Renderer targetRenderer;
        [SerializeField]
        private GameTimeMaterialTintSettings settings;

        private bool _isInside = false;

        private Color _baseColor;
        void Start()
        {
            _gameHour = settings.startHour;
            _gameSeconds = settings.startSeconds;
            if (targetRenderer == null) targetRenderer = GetComponent<Renderer>();
            _baseColor = targetRenderer.material.GetColor("_Color");
        }
        private void OnTimerUpdate(GameTime time)
        {
            if (_isInside) return;
            Color color;
            if (time.Hour < 6)
            {
                color = Color.Lerp(settings.colorAtMidnight, settings.colorAt6AM, (60*time.Hour + time.Seconds) / (360f));
            }else if (time.Hour < 12)
            {
                color = Color.Lerp(settings.colorAt6AM, settings.colorAt12PM, (60*(time.Hour-6) + time.Seconds) / (360f));
            }else if (time.Hour < 18)
            {
                color = Color.Lerp(settings.colorAt12PM, settings.colorAt6PM, (60*(time.Hour-12) + time.Seconds) / (360f));
            }
            else
            {
                color = Color.Lerp(settings.colorAt6PM, settings.colorAtMidnight, (60*(time.Hour-18) + time.Seconds) / (360f));
            }
            
            targetRenderer.material.SetColor("_Color",_baseColor*color);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == IndoorArea.IndoorAreaLayer)
            {
                _isInside = true;
                targetRenderer.material.SetColor("_Color",_baseColor);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == IndoorArea.IndoorAreaLayer)
            {
                _isInside = false;
            }
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
            if(changeHappened) OnTimerUpdate(new GameTime(_gameHour,_gameSeconds));
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
