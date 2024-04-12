using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public bool toDestroy;

    private Vector3 direction;
    private Vector3 pos;
    private float speed = 10f;

    private void Start() {
        pos = transform.position;
        direction = -pos.normalized; // go towards center (0,0,0)
    }

    void Update() {
        if (toDestroy) return;

        pos += speed * Time.deltaTime * direction;
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other) {
        if (toDestroy) return;

        if (other.gameObject.TryGetComponent(out BulletBehavior bullet)) {
            if (bullet.toDestroy) return;
            MarkToDestroy();
            bullet.MarkToDestroy();
        }
    }

    public void MarkToDestroy() {
        if (toDestroy) return;
        toDestroy = true;
        Spawner.Instance.currentEnemyCount--;
        Destroy(this.gameObject);
    }
}
