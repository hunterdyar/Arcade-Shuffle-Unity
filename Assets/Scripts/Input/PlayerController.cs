using UnityEngine;

namespace ArcadeShuffle
{
	public class PlayerController : MonoBehaviour
	{
		[SerializeField] private InputReader _inputReader;
		protected Vector2 MoveInput => _inputReader.Move;
		public float Horizontal => MoveInput.x;
		public float Vertical => MoveInput.y;
		protected virtual void OnEnable()
		{
			_inputReader.OnActionPerformed += Action;
			_inputReader.OnActionReleased += ActionUp;
			_inputReader.UpPressed += UpPressed;
			_inputReader.DownPressed += DownPressed;
			_inputReader.LeftPressed += LeftPressed;
			_inputReader.RightPressed += RightPressed;
		}

		protected virtual void OnDisable()
		{
			_inputReader.OnActionPerformed -= Action;
			_inputReader.OnActionReleased -= ActionUp;
			_inputReader.UpPressed -= UpPressed;
			_inputReader.DownPressed -= DownPressed;
			_inputReader.LeftPressed -= LeftPressed;
			_inputReader.RightPressed -= RightPressed;
		}

		public virtual void UpPressed()
		{
			
		}

		public virtual void DownPressed()
		{

		}

		public virtual void LeftPressed()
		{

		}

		public virtual void RightPressed()
		{

		}
		public virtual void Action()
		{
			
		}

		public virtual void ActionUp()
		{
			
		}
	}
}