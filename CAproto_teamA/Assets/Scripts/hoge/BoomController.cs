using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomController : MonoBehaviour
{
    private float boomSpeed = -100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBoom();
        ReturnPosition();
    }


    private void MoveBoom()
	{
        gameObject.transform.position += new Vector3(0, 0, boomSpeed);
    }

    /// <summary>
	/// ごめん、後で絶対直すからハードコーディングさせて...
	/// </summary>
    private void ReturnPosition()
    {
		if (gameObject.transform.position.z < 1000)
		{
            Initialize();
        }
       

    }

	private void Initialize()
	{
        gameObject.transform.position = new Vector3(-874, -292.9f, 4967);
    }

}
