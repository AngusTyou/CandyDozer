using UnityEngine;
using System.Collections;

public class ShooterController : MonoBehaviour {

	const int SphereCandyFrequency = 3;

	int sampleCandyCount;
	AudioSource shotSound;

	public GameObject[] candyPrefabs;
	public GameObject[]	candySquarePrefabs;
	public CandyHolder candyHolder;	

	public float shotSpeed;
	public float shotTorque;
	public float baseWidth;


	void Start() {
		shotSound = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
	
		// 入力検知
		if (Input.GetButtonDown ("Fire1"))
			Shot ();
		
	}

	GameObject SampleCandy() {

		GameObject prefab = null;

		if (sampleCandyCount % SphereCandyFrequency == 0) {
			int index = Random.Range (0, candyPrefabs.Length);
			prefab = candyPrefabs [index];
		} else {
			int index = Random.Range (0, candySquarePrefabs.Length);
			prefab = candySquarePrefabs	[index];
		}

		sampleCandyCount++;

		return prefab;

	}

	Vector3 GetInstantiatePosition() {

		float x = baseWidth *
			(Input.mousePosition.x / Screen.width) - (baseWidth / 2);
		return transform.position + new Vector3 (x, 0, 0);
	}


	public void Shot() {

		// キャンディを生成できる条件外ならShotしない
		if (candyHolder.GetCandyAmount () <= 0)
			return;

		// オブジェクト生成
		GameObject candy = 
			(GameObject)Instantiate (              
				SampleCandy(),               
				GetInstantiatePosition(),
			    Quaternion.identity);
	
		candy.transform.parent = candyHolder.transform;	
	
		// 力とトルクの加算
		Rigidbody candyRigidbody = candy.GetComponent<Rigidbody> ();
		candyRigidbody.AddForce (transform.forward * shotSpeed);
		candyRigidbody.AddTorque (new Vector3(0, shotTorque, 0));
	
		candyHolder.ConsumeCandy ();

		shotSound.Play ();	
	
	}
}
