using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	public float Move { get; private set; }
	public float Rotate { get; private set; }
	public bool Fire { get; private set; }
	public bool Reload { get; private set; }

	// Update is called once per frame
	void Update()
	{
		Move = Input.GetAxis(Defines.AxisVertical);
		Rotate = Input.GetAxis(Defines.AxisHorizontal);

		Fire = Input.GetButton(Defines.AxisFire1);
		Reload = Input.GetButtonDown(Defines.AxisReload);
	}
}
