using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porta : MonoBehaviour
{
    public Animator[] anim;
    public bool[] ani = { false };
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim[0].SetBool("Andar1", ani[0]);
        anim[1].SetBool("Andar2", ani[1]);
        anim[2].SetBool("Andar3", ani[2]);
        anim[3].SetBool("Porta", ani[3]);
    }
}
