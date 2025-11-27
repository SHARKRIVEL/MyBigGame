using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[SerializeField] AudioClip footOnSand;
		[SerializeField] AudioClip footOnMetal;
		[SerializeField] AudioClip footOnStone;
		[SerializeField] AudioClip footOnWood;
		AudioSource footAudios;
		[SerializeField] LayerMask footLayers;
		[SerializeField] LayerMask groundLayer;

		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;

		[Header("Movement Settings")]
		public bool analogMovement;

		public static StarterAssetsInputs starterAssetsInputs;
		public float additionalDist = 0f;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED

		void Awake()
		{
			starterAssetsInputs = this;
		}

		void Start()
		{
			footAudios = GetComponent<AudioSource>();
		}

		void Update()
		{
			RaycastHit groundHit;
			Physics.Raycast(transform.position,Vector3.down,out groundHit,Mathf.Infinity,groundLayer);
			if(groundHit.collider != null)
			{
				float dist = Vector3.Distance(transform.position,groundHit.point);
				if(dist >= 1f && dist <= 2f)
				{ 
					additionalDist = dist; 
				}
				else
				{
					additionalDist = 0f;
				}
			}

			if(move != Vector2.zero && !jump)
			{
				if(sprint) { footAudios.pitch = 1.5f; }
				else { footAudios.pitch = 1f; }

				RaycastHit hit;
				Physics.Raycast(transform.position,Vector3.down,out hit,1f,footLayers);

				if(hit.collider != null)
				{
					if(hit.collider.CompareTag("SandGround"))
					{
						AudiosManager(footOnSand);
					}

					if(hit.collider.CompareTag("Metal"))
					{
						AudiosManager(footOnMetal);
					}

					if(hit.collider.CompareTag("TileGround"))
					{
						AudiosManager(footOnStone);
					}

					if(hit.collider.CompareTag("Wood"))
					{
						AudiosManager(footOnWood);
					}
				}
				else
				{
					footAudios.Stop();
				}
			}
			else
			{
				footAudios.Stop();
			}
		}

		void AudiosManager(AudioClip ac)
		{
			footAudios.clip = ac;
			if(!footAudios.isPlaying)
			{
				footAudios.Play();
			}
		}

		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}