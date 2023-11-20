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

		private bool _canFlap;
		private void Awake()
		{
			_canFlap = true;
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
			_gravityScale = _rigidbody2D.gravityScale;
			_rigidbody2D.gravityScale = 0;
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
			_flappyManager.GetPoint();
		}
	}
}