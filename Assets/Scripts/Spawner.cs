using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public static Spawner Instance;
    public Text infoText;
    public float spawnAreaRadius;

    public int maxEnemies = 1000;
    public int maxBullets = 1000;
    public int defaultBulletSpawnCount = 30;
    public int defaultEnemySpawnCount = 30;

    public GameObject enemyPrefab;
    public GameObject bulletPrefab;
    public int currentEnemyCount;
    public int currentBulletCount;

    public int enemiesLayerMask;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        enemiesLayerMask = LayerMask.GetMask("Enemies");
    }

    private void Update() {

        if (currentEnemyCount < maxEnemies) {
            AddEnemies(defaultEnemySpawnCount);
        }
        if (currentBulletCount < maxBullets) {
            AddBullets(defaultBulletSpawnCount);
        }

        infoText.text = $"Enemy count =  {currentEnemyCount} \nBullet count = {currentBulletCount}";
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
