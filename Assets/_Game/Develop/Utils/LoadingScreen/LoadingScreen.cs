using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace _MiniGame
{
    public class LoadingScreen : MonoBehaviour, ILoadingScreen
    {
        [SerializeField] private Image _loadingImage;

        private Coroutine _scaleProcess;

        private void Awake()
        {
            //Hide();
            DontDestroyOnLoad(gameObject);
        }

        public bool IsShowing => gameObject.activeSelf;

        public void Show()
        {
            gameObject.SetActive(true);
            _scaleProcess = StartCoroutine(ScaleProcess());
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            StopCoroutine(_scaleProcess);
        }

        private IEnumerator ScaleProcess()
        {
            Vector3 startScale = _loadingImage.rectTransform.localScale;
            float process = 0f;
            float duration = 1f;
            int multiplier = 1;

            while (true)
            {
                _loadingImage.rectTransform.localScale = Vector3.Lerp(startScale, Vector3.zero, process / duration);
                process += Time.deltaTime * multiplier;

                yield return null;

                if (process >= duration) multiplier = -1;

                if (process <= 0) multiplier = 1;
            }
        }
    }
}