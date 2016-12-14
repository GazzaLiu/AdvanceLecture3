using UnityEngine;
using System.Collections;

public class UnityChan2P : UnityChan {

	// Use this for initialization
	protected override void Start () {
        base.Start();
        keyJump = KeyCode.Alpha1;
        keyFlame = KeyCode.Alpha2;
        keyPunch = KeyCode.Alpha3;
        keyExplode = KeyCode.Alpha4;

	}

    protected override void getAxis()
    {
        hAxis = Input.GetAxis("Horizontal2");
        vAxis = Input.GetAxis("Vertical2");
    }
}
