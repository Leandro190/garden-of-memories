using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insects : MonoBehaviour
{
    private List<GameObject> insects = new List<GameObject>();

    public int InsectNumber;
    [SerializeField] private GameObject InsectPrefab;
    [SerializeField] private PathCreator pathCreator;

    private float spawnTime = 0;

    private void Start()
    {
        InsectNumber = (int)pathCreator.path.length * 10;
    }

    void Update()
    {
        if (InsectNumber > 0 && Time.time > spawnTime)
        {
            spawnTime += 0.01f;
            insects.Add(Instantiate(InsectPrefab, this.transform));
            insects[insects.Count - 1].transform.GetChild(0).transform.position = new Vector3(insects[insects.Count - 1].transform.GetChild(0).transform.position.x,
                                                                                                Random.Range(-0.27f, 0.27f),
                                                                                                insects[insects.Count - 1].transform.GetChild(0).transform.position.z);
            InsectNumber--;
        }
    }
}
