using UnityEngine.Events;

namespace TestDemo
{
    public enum EMoveDirection
    {
        None,
        Forward,
        Right
    }

    public class PlayerInputState
    {
        public UnityAction OnChangeDirection;

        public EMoveDirection MoveDirection
        {
            get => _moveDirection;
            set
            {
                var val = _moveDirection;
                _moveDirection = value;

                if (val != _moveDirection)
                    OnChangeDirection?.Invoke();
            }
        }
        private EMoveDirection _moveDirection;
    }
}
