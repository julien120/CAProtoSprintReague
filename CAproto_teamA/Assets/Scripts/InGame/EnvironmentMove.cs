using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentMove : MonoBehaviour
{
	[SerializeField] private float _environmantMoveSpeed=0.5f;
	public GameObject[] rocks;
	//private GameObject[] _rocks;
	private GameObject _rock;
	private float _rockInstantiateInterval;
	private float _rockInstantiateProgresTime;
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//_rocks=GameObject.FindGameObjectsWithTag("Rock");

		_rockInstantiateProgresTime += Time.deltaTime;

	   if (_rockInstantiateProgresTime>3)
		{
			_rockInstantiateProgresTime = 0;
			_rock = Instantiate(rocks[Random.Range(0, 7)], new Vector3(Random.Range(-200, 200), Random.Range(-100, 100), 150), Quaternion.identity);
		}
		/*
		 foreach (GameObject _rock in _rocks)
		 {
			 if (Mathf.Abs(_rock.GetComponent<Transform>().position.x) < 10 && Mathf.Abs(_rock.GetComponent<Transform>().position.y) < 10)
			 {
				 Destroy(_rock);
			 }
		 }
		 */
		if (_rock)
		{
			_rock.gameObject.transform.position += new Vector3(0, 0, -_environmantMoveSpeed);
		}
		
	}

	private void Move()
	{
		_rock.gameObject.transform.position += new Vector3(0, 0, -_environmantMoveSpeed);
	}

	private void ReturnPosition()
	{
		if (gameObject.transform.position.z < -10)
		{
			Initialize();
		}

	}

	private void Initialize()
	{
		gameObject.transform.position = new Vector3(10, 10, 150);
	}

}
