using UnityEngine;
using System.Collections;

public class CandyHolder : MonoBehaviour {

	const int DefaultCandyAmount = 30;
	const int RecoverySecondes = 10;

	// 現在のキャンディのストック数
	int candy = DefaultCandyAmount;
	// ストック回復までの残り数
	int counter;


	public void ConsumeCandy () {
	
		if (candy > 0)
			candy--; 
	
	}

	public int GetCandyAmount() {

		return candy;

	}

	public void AddCandy(int amount) {

		candy += amount;

	}

	void OnGUI() {

		GUI.color = Color.black;

		string label = "Candy : " + candy;

		if (counter > 0) label = label + " (" + counter + "s)";

		GUI.Label (new Rect(0, 0, 100, 200), label);

	}


	void Update() {

		if (candy < DefaultCandyAmount && counter <= 0) {
			StartCoroutine (RecoverCandy());
		}

	}

	IEnumerator RecoverCandy() {

		counter = RecoverySecondes;

		while (counter > 0) {
			yield return new WaitForSeconds (1.0f);
			counter--;
		}

		candy++;
	}

}
