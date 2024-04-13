using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float raycastDistance = 1.0f;
    public float collisionRadius = 0.5f;
    public bool toDestroy;

    private Vector3 direction;
    private float speed = 30f;
    private Vector3 pos;

    void Start() {
        direction = Random.insideUnitCircle.normalized;
        pos = transform.position;
    }

    void Update() {
        if (toDestroy) return;

        pos += speed * Time.deltaTime * direction;
        if(pos.magnitude > Spawner.Instance.spawnAreaRadius) {
            pos = direction * Spawner.Instance.spawnAreaRadius;
            direction = -direction;
        }
        transform.position = pos;

        if (Physics.SphereCast(pos, collisionRadius, direction, out RaycastHit hitinfo, 1f, Spawner.Instance.enemiesLayerMask)) {
            if (hitinfo.collider.TryGetComponent(out EnemyBehavior enemy)) {
                if (enemy.toDestroy == false) {
                    enemy.MarkToDestroy();
                    MarkToDestroy();
                }
            }
        }
    }

    public void MarkToDestroy() {
        if (toDestroy) return;
        toDestroy = true;
        Spawner.Instance.currentBulletCount--;
        Destroy(this.gameObject);
    }
}
