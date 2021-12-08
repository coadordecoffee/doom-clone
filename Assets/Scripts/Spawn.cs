using System.Collections; 
using UnityEngine; 
public class Spawn : MonoBehaviour{
    public Transform spawnPoint;
    public GameObject Prefab;

    void OnSpawnEnter(){
        Instantiate(Prefab, spawnPoint.position, spawnPoint.rotation);
    }
}