using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TadPoleFramework.Extends
{
    public class CustomToggle : Toggle, IPointerClickHandler
    {
        public Color Color;
        public Sprite OnSprite;
        public Sprite OffSprite;

        private Image _graphic;
        private Image _targetGraphic;

        protected override void OnEnable()
        {
            _graphic = graphic.GetComponent<Image>();
            _targetGraphic = targetGraphic.GetComponent<Image>();
            base.OnEnable();
            ValueChangeIssues();
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            ValueChangeIssues();
        }
        
        private void ValueChangeIssues()
        {
            if (isOn)
            {
                _graphic.sprite = OnSprite;
                _targetGraphic.sprite = null;

                Color.a = 0;
                _targetGraphic.color = Color;
            }
            else
            {
                _targetGraphic.sprite = OffSprite;
                
                Color.a = 1;
                _targetGraphic.color = Color;
            }
        } 
    }
}