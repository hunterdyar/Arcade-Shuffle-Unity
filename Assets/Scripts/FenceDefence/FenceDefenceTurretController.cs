using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.NoSceneObjects;

namespace ArcadeShuffle
{
    public class FenceDefenceTurretController : PlayerController
    {
	    [NoSceneObjects]
	    [SerializeField] private Missile _missilePrefab;
	    
	    [SerializeField] private Transform _spawnPoint;//child objects only
	    [Tooltip("Opposite Rotation limit is assumed flipped around y-up axis.")]
	    public float maxRotation;

	    [Tooltip("I didn't bother with the math to make this unit meaningful. Bigger is faster.")]
	    [Min(0)]
	    [SerializeField] private float rotationSpeed;
	    private float _rotationInput = 0;

	    private Missile _activeMissile;
	    public override void Action()
	    {
		    if (_activeMissile == null)
		    {
			    _activeMissile = CreateMissile();
		    }
	    }

	    private Missile CreateMissile()
	    {
		    return Instantiate(_missilePrefab, _spawnPoint.position, transform.rotation);
	    }

	    public override void ActionUp()
	    {
		    if (_activeMissile != null)
		    {
			    _activeMissile.Explode();
			    _activeMissile = null;
		    }
	    }

	    void Update()
	    {
		    RotateInputTick();
	    }

	    private void RotateInputTick()
	    {
			//Store rotationInput as a value between -1 and 1.
			_rotationInput = _rotationInput + (rotationSpeed/Mathf.PI) * -Horizontal * Time.deltaTime;
			_rotationInput = Mathf.Clamp(_rotationInput, -1, 1);
			//Remap from -1-1 to 0-1, but otherwise we just set the angle to the rotationInput.
			//LerpAngle wouldn't be needed but it does the 0/360 wrap math for us.
		    float rot = Mathf.LerpAngle(-maxRotation, maxRotation, (_rotationInput+1) / 2);
		    transform.rotation = Quaternion.Euler(0,0,rot);
	    }
    }
}
