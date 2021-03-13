using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    private float stageSpeed = -100f;
    [SerializeField] private Vector3 position;

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
        gameObject.transform.position += new Vector3(0, 0, stageSpeed);
    }

    /// <summary>
	/// ごめん、後で絶対直すからハードコーディングさせて...
	/// </summary>
    private void ReturnPosition()
    {
        if (gameObject.transform.position.z < -500)
        {
            Initialize();
        }


    }

    private void Initialize()
    {
        gameObject.transform.position = new Vector3(position.x, position.y, position.z);
    }
}
