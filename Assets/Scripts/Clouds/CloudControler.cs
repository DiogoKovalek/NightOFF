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

    //Controle
    private List<GameObject> listClouds = new List<GameObject>();
    private Vector2 position = Vector2.zero;
    private float speedSpawn = 0;
    private float speedfPref = 0;
    private float scalePref = 1;
    private float alphaPref = 1;

    void Start() {
        // Spawnar em pontos aleatorios para comecar
        int aux = Random.Range(minQuantPrefab, maxQuantPrefab + 1);
        Debug.Log(aux);
        for(int i = 0; i < aux; i++) {
            randomlyAtribute(true);
            spawnPrefab<Cloud>(position, Vector2.left, speedfPref, scalePref, alphaPref);
        }
    }

    private IEnumerator loopSpawn() {
        yield return null;
    }

    private void randomlyAtribute(bool isStart) {
        if (isStart) { // Qualquer posicao do quadrado
            position.x = Random.Range(spawnAreaDownLeft.x, spawnAreaUpRight.x);
            position.y = Random.Range(spawnAreaDownLeft.y, spawnAreaUpRight.y);
        }
        else { // Apenas nas laterais
            position.x = spawnAreaUpRight.x;
            position.y = Random.Range(spawnAreaDownLeft.y, spawnAreaUpRight.y);
        }
        speedfPref = Random.Range(minSpeedPrefab, maxSpeedPrefab);
        scalePref = Random.Range(minScalePrefab, maxScalePrefab);
        alphaPref = Random.Range(minAlphaPrefab, maxAlphaPrefab + 1) / 255f;
    }

    private void spawnPrefab<T>(Vector2 position, Vector2 direction, float speed, float scale, float alpha)
    where T : IMovable {
        GameObject obj = getPrefDisable();
        if (obj == null && listClouds.Count < maxQuantPrefab) {
            obj = Instantiate(prefabCloud, position, prefabCloud.transform.rotation, this.transform);
            listClouds.Add(obj);
        }
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
    private GameObject getPrefDisable() {
        foreach (GameObject obj in listClouds) {
            if (!obj.activeSelf) {
                return obj;
            }
        }
        return null;
    }
}
