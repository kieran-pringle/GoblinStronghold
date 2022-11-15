using System;
namespace GoblinStronghold.ECS
{
	/**
	 *	Call back for <c>System</c> that might need to register an additional
	 *	subsystem or similar.
	 */
	public interface IOnSystemRegister
	{
		public void OnSystemRegister(IContext context);
	}
}

