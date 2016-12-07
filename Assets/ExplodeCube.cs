using UnityEngine;
using System.Collections;

public class ExplodeCube : MonoBehaviour {
    public GameObject Explosion;
    float expPeriod;

	// Use this for initialization
	void Start () {
	    expPeriod = Random.RandomRange(0.6f, 1.2f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public IEnumerator explode() {
        yield return new WaitForSeconds(expPeriod);
        GameObject exp = (GameObject)Instantiate(Explosion);
        exp.transform.position = transform.position;
        Destroy(gameObject);
    }
}
