using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utilities.ReadOnlyAttribute;

namespace ArcadeShuffle
{
	[CreateAssetMenu(fileName = "Input Reader", menuName = "Scotty Dog/Input/Input Reader", order = 0)]
	public class InputReader : ScriptableObject, ScottyDogInputActions.IGameplayActions
	{
		private ScottyDogInputActions _inputActions;

		public Action OnActionPerformed;
		public Action OnActionReleased;
		public Action<Vector2> MoveChanged;
		public Action UpPressed;
		public Action DownPressed;
		public Action LeftPressed;
		public Action RightPressed;

		public Vector2 Move => _move;
		[SerializeField,ReadOnly]
		private Vector2 _move;

		public bool ActionIsPressed => _actionIsPressed;
		[SerializeField,ReadOnly]
		private bool _actionIsPressed;

		[SerializeField, Range(0, 1)] private float _analogAsButtonDeadzone = 0.9f;
		
		public Vector2Int MoveAsButton => _moveAsButton;
		private Vector2Int _moveAsButton;

		public bool GetUp => _moveAsButton.y > _analogAsButtonDeadzone;
		public bool GetDown => _moveAsButton.y < -_analogAsButtonDeadzone;
		public bool GetRight => _moveAsButton.x > _analogAsButtonDeadzone;
		public bool GetLeft => _moveAsButton.x < -_analogAsButtonDeadzone;

		private Vector2Int prevMoveAsButton;
		
		private void OnEnable()
		{
			ForceInit();
		}
		
		[ContextMenu("Do Action")]
		public void DoAction()
		{
			OnActionPerformed?.Invoke();
		}

		public void OnAction(InputAction.CallbackContext context)
		{
			if (context.performed)
			{
				DoAction();
			}
			_actionIsPressed = context.ReadValueAsButton();

			if (context.canceled)
			{
				OnActionReleased?.Invoke();
			}
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			var move = context.ReadValue<Vector2>();
			
			//Handle as-Button 
			int x = move.x > _analogAsButtonDeadzone ? 1 : (move.x < -_analogAsButtonDeadzone ? -1 : 0);
			int y  = move.y > _analogAsButtonDeadzone ? 1 : (move.y < -_analogAsButtonDeadzone ? -1 : 0);
			_moveAsButton = new Vector2Int(x, y);

			if (_moveAsButton.y == 1 && prevMoveAsButton.y != 1)
			{
				UpPressed?.Invoke();
			}

			if (_moveAsButton.y == -1 && prevMoveAsButton.y != -1)
			{
				DownPressed?.Invoke();
			}

			if (_moveAsButton.x == 1 && prevMoveAsButton.x != 1)
			{
				RightPressed?.Invoke();
			}

			if (_moveAsButton.x == -1 && prevMoveAsButton.x != -1)
			{
				LeftPressed?.Invoke();
			}

			prevMoveAsButton = _moveAsButton;

			
			//Handle as-Vector
			
			if (move != _move)
			{
				_move = move;
				MoveChanged?.Invoke(_move);
			}

		}

		public void ForceInit()
		{
			_inputActions = new ScottyDogInputActions();
			_inputActions.Gameplay.Enable();
			_inputActions.Gameplay.SetCallbacks(this);
			_move = Vector2.zero;
		}
	}
}