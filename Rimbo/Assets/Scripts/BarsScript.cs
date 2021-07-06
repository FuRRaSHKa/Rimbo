using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsScript : MonoBehaviour {

    public static BarsScript current;

    public SpriteRenderer HealthBar;
    public SpriteRenderer MindBar;

    private float maxSizeH;
    private float maxSizeM;

    public void Awake() {

        if (current != null)
            Destroy(this);

        current = this;

    }

    public void Start() {

        maxSizeH = HealthBar.size.x;
        maxSizeM = MindBar.size.x;

    }

    public void SetHealthPoint(float hp) {
       
        HealthBar.size = new Vector2(hp* maxSizeH, HealthBar.size.y);

    }

    public void SetMindPoint(float mp) {
        MindBar.size = new Vector2(mp * maxSizeM, MindBar.size.y);

    }

}
