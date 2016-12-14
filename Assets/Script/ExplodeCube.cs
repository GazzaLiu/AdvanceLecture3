using UnityEngine;
using System.Collections;

public class ExplodeCube : MonoBehaviour {

    public GameObject Explosion;

    float expPeriod;

    void Start () {
        expPeriod = Random.Range(0.6f, 1.2f);
    }

    public IEnumerator explode () {
        yield return new WaitForSeconds(expPeriod);
        GameObject exp = (GameObject)Instantiate(Explosion);
        exp.transform.position = transform.position;
        Destroy(gameObject);
    }
}
