using CoinRand;
using System.Collections;
using System.Linq;
using UnityEngine;
using TMPro;


public class CoinCollection : MonoBehaviour
{
    private CoinInst randomizer;
    [SerializeField] private GameObject gameobj;
    [SerializeField] private TMP_Text textmesh;
    private int count=0;
    private AudioSource click;
    private int totalSpawnPoints;
    int y;

    private void Start()
    {
        click = GetComponent<AudioSource>();
        randomizer = gameobj.GetComponent(typeof(CoinInst)) as CoinInst;
        totalSpawnPoints = randomizer.spawnpoints.Count();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            // Destroy(other.gameObject);
            count++;
            textmesh.text = count.ToString();
            other.gameObject.SetActive(false);
            click.Play();
            StartCoroutine(Spawn(other.gameObject));
        }
    }

    IEnumerator Spawn(GameObject gameObject)
    {

        int x = randomizer.spawnpoints.IndexOf(gameObject.transform.parent.transform);

        // 3 saniye sonra active state don
        randomizer.randomValues.Remove(x);

        while (randomizer.randomValues.Count < Mathf.Ceil(totalSpawnPoints / 2.0f))
        {
            y = randomizer.r.Next(0, randomizer.spawnpoints.Count() - 1);
            randomizer.randomValues.Add(y);
        }
        yield return new WaitForSeconds(3);
        gameObject.transform.position = randomizer.spawnpoints[y].transform.position;
        gameObject.SetActive(true);
    }
}



