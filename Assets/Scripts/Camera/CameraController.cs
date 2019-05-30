using UnityEngine;
using Zenject;

namespace TestDemo
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _target;

        [Inject] private readonly Player _player;

        protected void Update()
        {
            _target.position = new Vector3(_player.Position.x, _target.position.y, _player.Position.z);
        }
    }
}