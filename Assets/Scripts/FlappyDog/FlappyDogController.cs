using System;
using ArcadeShuffle;
using ArcadeShuffle.FlappyDog;
using UnityEngine;

namespace ScottyDog.FlappyDog
{
	public class ArcadeShuffle : PlayerController
	{
		private Rigidbody2D _rigidbody2D;
		private Animator _animator;
		[SerializeField] private SceneShuffler _shuffler;

		[Header("Flappy Settings")] [SerializeField]
		private FlappyManager _flappyManager;
		[SerializeField] private float jumpSpeed;

		private float _gravityScale;
		private static readonly int VerticalSpeed = Animator.StringToHash("VerticalSpeed");

		private bool _canFlap;//We could call this "game over" instead of "canFlap", but the player controller doesn't control game state, it controls flapping. 'IsDead" might be a middle-ground naming that also works.
		private void Awake()
		{
			_animator = GetComponent<Animator>();
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		protected override void OnEnable()
		{
			FlappyManager.OnStartGame += OnStartGame;
			base.OnEnable();
		}

		protected override void OnDisable()
		{
			FlappyManager.OnStartGame -= OnStartGame;
			base.OnDisable();
		}

		private void OnStartGame()
		{
			_rigidbody2D.gravityScale = _gravityScale;
		}

		private void Start()
		{
			//Note that we work with component references in start, and do getcomponents in Awake. This is a good habit. Makes race condition bugs more rare.
			
			_gravityScale = _rigidbody2D.gravityScale;//Cache a reference to the gravity scale in the rb settings. This let's us use the rigidbody to edit rigidbody things. I don't want to add my own gravityScale override.
			//It makes the most sense to edit gravityscale in the rb settings. So lets use the value so it means what it means, and we don't have two ways to edit one value.
			
			_rigidbody2D.gravityScale = 0;//We don't start falling until the first flap. We could check that in Action in this script, but the game could get started via a countdown or other system. The game manager is in charge, so we just listen to them.
			//This approach may seem unnecesary, and at this scope it is, but it's also the most flexible. We can change to a countdown, and more easily implement  a bunch of other things to happen on "first flap" that don't make sense to have in the controller, like music.
			_canFlap = true;
		}

		public override void Action()
		{
			if (_canFlap)
			{
				_rigidbody2D.velocity = new Vector2(0, jumpSpeed);
			}
		}

		private void Update()
		{
			_animator.SetFloat(VerticalSpeed,_rigidbody2D.velocity.y);
			//todo: rotate with y speed. Do that in animator?
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			//canFlap is here, acting as "game is over" indicator.
			if (_canFlap)
			{
				_shuffler.LoadGameOverScene();
				_canFlap = false;
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			//We have to determine, somehow, what trigger space we entered. This is usually done with tags, layers, or checking components (GetComponent, see frogger).
			//In this game, the only thing in the scene that is a trigger are the pipe score zones. So "for now", we can assume if we hit any trigger at all, it was going through a pipe.
			_flappyManager.GetPoint();
			
			//I think it's possible to enter one multiple times (eg: if we just lost, and are spinning), so we shouldn't get more points for that. Turning the trigger are off is the most robust way to solve this.
			other.enabled = false;
		}
	}
}