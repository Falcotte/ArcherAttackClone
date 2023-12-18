using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ArcherAttack.Inputs
{
    public class InputController : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerUpHandler
    {
        #region Attributes

        private Vector2 _initialTouchPosition;
        private Vector2 _touchPosition;

        private Vector2 _dragDelta;
        private Vector2 _dragDeltaScreenPercent { get { return new Vector2(_dragDelta.x / Screen.width, _dragDelta.y / Screen.height); } }

        private bool _isTouching;
        #endregion

        #region Events

        public static UnityAction OnTouchDown;
        public static UnityAction OnTouchUp;
        public static UnityAction OnTouch;

        public static UnityAction<Vector2> OnDragDelta;
        public static UnityAction<Vector2> OnDragDeltaScreenPercent;

        #endregion

        private void Update()
        {
            if(_isTouching)
            {
                _dragDelta = CurrentTouchPosition() - _touchPosition;
                _touchPosition = CurrentTouchPosition();

                OnTouch?.Invoke();
            }
        }

        #region Touch

        public void OnPointerDown(PointerEventData eventData)
        {
            _isTouching = true;

            _initialTouchPosition = eventData.position;
            _touchPosition = _initialTouchPosition;

            OnTouchDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isTouching = false;

            OnTouchUp?.Invoke();

            _initialTouchPosition = Vector2.zero;
            _touchPosition = Vector2.zero;
        }

#if UNITY_EDITOR
        private Vector2 CurrentTouchPosition()
        {
            if(_isTouching)
            {
                return Input.mousePosition;
            }
            return Vector2.zero;
        }
#else
        private Vector2 CurrentTouchPosition()
        {
            if(Input.touchCount > 0)
            {
                return Input.touches[0].position;
            }
            return Vector2.zero;
        }
#endif

        #endregion

        #region Drag

        public virtual void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnDrag(PointerEventData eventData)
        {
            _dragDelta = (eventData.position - _touchPosition);

            OnDragDelta?.Invoke(_dragDelta);
            OnDragDeltaScreenPercent?.Invoke(_dragDeltaScreenPercent);
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {
            _dragDelta = Vector2.zero;
        }

        #endregion
    }
}