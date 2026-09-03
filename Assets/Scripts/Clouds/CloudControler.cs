using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudControler : MonoBehaviour {
    [SerializeField] private GameObject prefabCloud;

    [Header("Area Spawn")]
    [SerializeField] private Vector2 spawnAreaDownLeft = Vector2.zero;
    [SerializeField] private Vector2 spawnAreaUpRight = Vector2.zero;

    [Header("Atributes Spawn")]
    // Len
    [SerializeField] private int minQuantPrefab = 2;
    [SerializeField] private int maxQuantPrefab = 6;
    // speed spawn
    [SerializeField] private float minSpeedSpawn = 1;
    [SerializeField] private float maxSpeedSpawn = 1.5f;

    [Header("Atributes Prefab")]
    // speed prefab
    [SerializeField] private float minSpeedPrefab = 3;
    [SerializeField] private float maxSpeedPrefab = 10;
    // size prefab
    [SerializeField] private float minScalePrefab = 0.8f;
    [SerializeField] private float maxScalePrefab = 1.3f;
    // alpha prefab
    [SerializeField] private byte minAlphaPrefab = 230;
    [SerializeField] private byte maxAlphaPrefab = 255;

    void Start() {
        spawnPrefab<Cloud>(Vector2.zero, Vector2.left, 3, 2, 200/255f);
    }

    private void spawnPrefab<T>(Vector2 position, Vector2 direction, float speed, float scale, float alpha)
    where T : IMovable{
        GameObject obj = Instantiate(prefabCloud, position, prefabCloud.transform.rotation, this.transform);
        obj.transform.localScale = obj.transform.localScale * scale;
        SpriteRenderer sprRen = obj.GetComponent<SpriteRenderer>();
        if (sprRen != null) {
            Color c = sprRen.color;
            c.a = alpha;
            sprRen.color = c;
        }
        T scr = obj.GetComponent<T>();
        scr?.SetSpeed(speed);
        scr?.SetDirection(direction);
        scr?.Movement(true);
    }
}
