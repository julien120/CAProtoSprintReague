using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LoginedTitle
{
    public class settingButtonController : MonoBehaviour
    {
        [SerializeField] private GameObject setting;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void HideSetting()
        {
            setting.SetActive(false);
        }
    }


}

