using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Title
{
    public class HowToPlayAnimation : MonoBehaviour
    {
        [SerializeField] private CanvasGroup howToPlayCanvasGroup;
        [SerializeField] private RectTransform frameRectTransform;
        private Vector2 frameSizeDelta;

        [SerializeField] private CanvasGroup buttonCloseCanvasGroup;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button HowtoPlayButton;

        // Start is called before the first frame update
        void Start()
        {
            closeButton.onClick.AddListener(CloseButton);
            HowtoPlayButton.onClick.AddListener(Animate);
            Initialize();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Animate()
		{
            var sequence = DOTween.Sequence()
                .Append(howToPlayCanvasGroup.DOFade(1f, 0.5f))
                .Join(frameRectTransform.DOSizeDelta(new Vector2(frameSizeDelta.x, 2), 0.2f))
                .Append(frameRectTransform.DOSizeDelta(new Vector2(frameSizeDelta.x, frameSizeDelta.y), 0.2f));

            //閉じるボタンの表示・実行
            sequence.Append(buttonCloseCanvasGroup.DOFade(1.0f, 0.5f))
                .OnComplete(() => howToPlayCanvasGroup.blocksRaycasts = true)
                .Play();


        }

		public void Initialize()
		{
            howToPlayCanvasGroup.alpha = 0;
            buttonCloseCanvasGroup.alpha = 0;
            frameSizeDelta = frameRectTransform.sizeDelta;
            frameRectTransform.sizeDelta = Vector2.zero;
        }

        public void CloseButton()
        {
            Initialize();
        }
    }
}