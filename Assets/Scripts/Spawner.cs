using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;

    public Vector2 spawnAreaSize;
    public float spawnAreaRadius;

    public int maxEnemies = 1000;
    public int maxBullets = 1000;
    public int defaultBulletSpawnCount = 30;
    public int defaultEnemySpawnCount = 30;

    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public int currentEnemyCount;
    public int currentBulletCount;

    private float debugLogInterval = 1f;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        BulletBehavior.count = 0;
        EnemyBehavior.count = 0;
        //StartCoroutine(CallMethodRepeatedly());
    }

    IEnumerator CallMethodRepeatedly() {
        while (true) {
            Debug.Log($"bullets:{BulletBehavior.count}, enemies:{EnemyBehavior.count}");
            yield return new WaitForSeconds(debugLogInterval);
        }
    }
    
    private void Update() {

        if (currentEnemyCount < maxEnemies) {
            //int count = Mathf.Min(defaultEnemySpawnCount, maxEnemies - currentEnemyCount);
            AddEnemies(defaultEnemySpawnCount);
        }
        if (currentBulletCount < maxBullets) {
            //int count = Mathf.Min(defaultBulletSpawnCount, maxBullets - currentBulletCount);
            AddBullets(defaultBulletSpawnCount);
        }
    }

    private void AddEnemies(int count) {
        for (int i = 0; i < count; i++) {
            currentEnemyCount++;
            var pos = Random.insideUnitCircle.normalized * spawnAreaRadius;
            var instance = Instantiate(enemyPrefab, pos, Quaternion.identity);
        }
    }

    private void AddBullets(int count) {
        for (int i = 0; i < count; i++) {
            currentBulletCount++;
            var instance = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
