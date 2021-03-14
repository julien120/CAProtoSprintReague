using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeneitivityManager : MonoBehaviour
{
	
	public GameObject sliderFlick;	
	public  static float sensitivityValue;
	public GameObject sensitivityTextGameObject;
	private Text _sensitivityText;
	// Start is called before the first frame update
	void Start()
    {
		_sensitivityText = sensitivityTextGameObject.GetComponent<Text>();

	}

    // Update is called once per frame
    void Update()
    {
		sensitivityValue = sliderFlick.GetComponent<Scrollbar>().value;
		_sensitivityText.text = sensitivityValue.ToString("N2");

	}

	public static float GetTurnSpeed()
	{
		return sensitivityValue;
	}
}
