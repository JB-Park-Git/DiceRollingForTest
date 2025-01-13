using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ground : MonoBehaviour
{
    public Text Text_Number_ = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //콜라이더가 붙은애가 땅에 부딪힐때 
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("콜라이더에 부딪힌 애 이름:" + collision.collider.name);
        
    }

    //콜라이더가 붙고 IsTrigger가 체크되어있는 애가 땅에 부딪힐때 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리거에 들어온 애 이름:" + other.name);

        Text_Number_.text = other.name;
    }
}
