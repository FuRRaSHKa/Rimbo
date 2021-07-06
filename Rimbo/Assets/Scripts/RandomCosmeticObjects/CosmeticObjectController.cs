using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticObjectController : MonoBehaviour
{
    public void AnimEvent()
    {
        transform.parent.gameObject.SetActive(false); //эвент в анимации, после которого объект исчезает
    }
}
