
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public static int count;
    public bool toDestroy { get; private set; }

    private Vector3 direction;
    private Vector3 pos;
    private float speed = 10f;

    private void Start() {
        count++;
        // go towards center (0,0,0)
        pos = transform.position;
        direction = -pos.normalized;
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

    private void OnDestroy() {
        count--;
    }
}
