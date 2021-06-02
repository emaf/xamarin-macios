﻿using System;
using Microsoft.Build.Framework;
using Xamarin.Messaging.Build.Client;

namespace Xamarin.iOS.Tasks
{
	public class CompileSceneKitAssets : CompileSceneKitAssetsTaskBase, ICancelableTask
	{
		public override bool Execute ()
		{
			if (!ShouldExecuteRemotely ())
				return base.Execute ();

			var taskRunner = new TaskRunner (SessionId, BuildEngine4);

			taskRunner.FixReferencedItems (SceneKitAssets);

			FixUpRootedPaths (SceneKitAssets);

			return taskRunner.RunAsync (this).Result;
		}

		public void Cancel ()
		{
			if (!string.IsNullOrEmpty (SessionId))
				BuildConnection.CancelAsync (SessionId, BuildEngine4).Wait ();
		}

		void FixUpRootedPaths (ITaskItem [] sceneKitAssets)
		{
			foreach (var item in sceneKitAssets) {
				item.ItemSpec = item.ItemSpec.Replace (":", "");
			}
		}
	}
}
