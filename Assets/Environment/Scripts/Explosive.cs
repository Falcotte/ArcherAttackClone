using ArcherAttack.Enemy;
using UnityEngine;

namespace ArcherAttack.Environment
{
    public class Explosive : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemy;

        [SerializeField] private Outline _outline;
        [SerializeField] private ParticleSystem _explosionEffect;

        [SerializeField] private GameObject _visual;
        [SerializeField] private CapsuleCollider _collider;

        public void Explode()
        {
            _enemy.HealthController.Explode();
            _enemy.Ragdoll.PushRagdollUniform(5f,transform.position - Vector3.up);

            _outline.enabled = false;

            _explosionEffect.Play();

            _visual.SetActive(false);
            _collider.enabled = false;
        }
    }
}

