using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArcadeShuffle.Frogger
{
	public class FroggerDogController : PlayerController
	{
		[SerializeField] private SceneShuffler _shuffler;
		private FroggerMap _map;
		private BoxCollider2D _boxCollider2D;
		private FroggerPlatform _currentPlatform;
		private bool _dead = false;
		private readonly Collider2D[] _results = new Collider2D[3];

		
		private void Awake()
		{
			_dead = false;
			_boxCollider2D = GetComponent<BoxCollider2D>();
			_map = GetComponent<FroggerMap>();
		}

		private void Update()
		{
			CheckPositionTick();
		}

		private void CheckPositionTick()
		{
			var overlaps = Physics2D.OverlapBoxNonAlloc(transform.position, _boxCollider2D.bounds.size, 0, _results);
			bool overlappingPlatform = false;//this instead of bothering to check if the collider is our collider, i guess.
			bool _shouldDie = false;
			for (int i = 0; i < overlaps; i++)
			{
				var obstacle = _results[i].GetComponent<Obstacle>();
				
				//Right now i think we find ourselves every frame. a layermask will fix that.
				//regardless, should still check against null. Here we ignore anything that isn't an obstacle.
				if (obstacle == null)
				{
					continue;
				}
				
				if (obstacle.IsDeadly)
				{
					//water can just be a big non-moving car. It's fine. don't @ me; it saves a GetComponent call.
					//in which case you can't get hit by an obstacle if you are on a platform. That ... feels... true?
					//we have to cache until after all the checks in case we step onto water, then platfrom, on same frame.
					if (_currentPlatform == null)
					{
						_shouldDie = true;
					}
				}

				var platform = obstacle.FroggerPlatform;

					//are we overlapping a platform? enter it.
				
				if (platform != null)
				{
					overlappingPlatform = true;
					
					if(platform != _currentPlatform)
					{
						_currentPlatform = platform;
						_currentPlatform.EnterPlatform(this);
						_shouldDie = false;//if stepping onto platform, "step off of water".
					}
				}
			}
			//are we NOT overlapping a platform? exit it.
			if (!overlappingPlatform)
			{
				if (_currentPlatform != null)
				{
					_currentPlatform.ExitPlatform(this);
					_currentPlatform = null;
				}
			}

			if (_shouldDie)
			{
				KillTheFrog();
			}
		}

		private void KillTheFrog()
		{
			if (!_dead)
			{
				_shuffler.LoadGameOverScene();
				_dead = true;
			}//else debug log stop stop he's already dead
		}

		private void TryMove(int dx, int dy)
		{
			//we don't technically know if the move was successful or not. we could have hit an outer wall.
			if (dx != 0 || dy != 0)
			{
				transform.position = _map.GetPosition(transform.position, dx, dy);
			}
			//check position
		}
		public override void UpPressed()
		{
			TryMove(0,1);
		}

		public override void DownPressed()
		{
			TryMove(0,-1);
		}

		public override void LeftPressed()
		{
			TryMove(-1,0);
		}

		public override void RightPressed()
		{
			TryMove(1,0);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			Debug.Log("Womp");
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			Debug.Log("Bwomp");
		}
	}
}