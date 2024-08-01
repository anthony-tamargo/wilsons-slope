using System.Runtime.InteropServices;
using Sandbox;

public sealed class DeathCam : Component
{
	CameraComponent cam;
	public Angles rotAngles { get; private set;}
	Vector3 currentOffset = Vector3.Zero;


	protected override void OnEnabled()
	{
		cam = Components.GetInChildren<CameraComponent>();

		HandleCamMovementInit(); // initializes cam movement vars and rotations
	}

	protected override void OnUpdate()
	{
		HandleCamMovement();
	}

	private void HandleCamMovementInit()
	{
		if(cam is not null)
		{
		var eyeAngles = cam.Transform.Rotation.Angles();
		eyeAngles.pitch += Input.MouseDelta.y * .1f;
		eyeAngles.yaw -= Input.MouseDelta.x * .1f;
		eyeAngles.roll = 0f;
		eyeAngles.pitch = eyeAngles.pitch.Clamp(-89.9f, 89.9f);
		cam.Transform.Rotation = eyeAngles.ToRotation();
		}

	}

	private void HandleCamMovement()
	{
		var eyeAngles = cam.Transform.Rotation.Angles();
		eyeAngles.pitch += Input.MouseDelta.y * .1f;
		eyeAngles.yaw -= Input.MouseDelta.x * .1f;
		eyeAngles.roll = 0f;
		eyeAngles.pitch = eyeAngles.pitch.Clamp(-89.9f, 89.9f);
		cam.Transform.Rotation = eyeAngles.ToRotation();





		if(cam is not null)
		{
			var camPos = GameObject.Transform.Position ;
		
				var camFoward = eyeAngles.ToRotation().Forward;
				var camTrace = Scene.Trace.Ray(camPos , camPos - (camFoward * 300))
					.WithoutTags("player" , "trigger")
					.Run();

				if(camTrace.Hit	)
				{
					camPos = camTrace.HitPosition + camTrace.Normal;
				}
				else
				{
					camPos = camTrace.EndPosition;
				}
			cam.Transform.Position = camPos;
			cam.Transform.Rotation = eyeAngles.ToRotation();
		}


	}
}
