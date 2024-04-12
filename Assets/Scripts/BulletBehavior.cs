using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
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
        
    }

    public void MarkToDestroy() {
        if (toDestroy) return;
        toDestroy = true;
        Spawner.Instance.currentBulletCount--;
        Destroy(this.gameObject);
    }
}
