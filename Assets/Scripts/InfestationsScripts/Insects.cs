using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insects : MonoBehaviour
{
    private List<GameObject> insects = new List<GameObject>();

    public int InsectNumber = 200;
    [SerializeField] private GameObject InsectPrefab;
    [SerializeField] private PathCreator pathCreator;

    private float spawnTime = 0;

    private void Start()
    {
        //InsectNumber = (int)(pathCreator.path.length * 15f);
    }

    void Update()
    {
        if (InsectNumber > 0 && Time.time > spawnTime)
        {
            spawnTime += 0.01f;
            insects.Add(Instantiate(InsectPrefab, this.transform));
            insects[insects.Count - 1].GetComponent<PathFollower2D>().pathCreator = pathCreator;
            insects[insects.Count - 1].transform.GetChild(0).transform.position = new Vector3(insects[insects.Count - 1].transform.GetChild(0).transform.position.x,
                                                                                                Random.Range(0.05f, 0.50f),
                                                                                                insects[insects.Count - 1].transform.GetChild(0).transform.position.z);
            
            InsectNumber--;
        }
    }
}
