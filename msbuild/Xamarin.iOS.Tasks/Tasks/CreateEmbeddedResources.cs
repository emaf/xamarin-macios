﻿using System;
using Xamarin.MacDev.Tasks;

namespace Xamarin.iOS.Tasks
{
	public class CreateEmbeddedResources : CreateEmbeddedResourcesTaskBase
	{
		public override bool Execute ()
		{
			if (Environment.OSVersion.Platform == PlatformID.Win32NT && !string.IsNullOrEmpty (SessionId)) {
				foreach (var bundleResource in this.BundleResources) {
					var logicalName = bundleResource.GetMetadata ("LogicalName");

					if (!string.IsNullOrEmpty (logicalName)) {
						logicalName = logicalName.Replace ("\\", "/");
						bundleResource.SetMetadata ("LogicalName", logicalName);
					}
				}
			}

			return base.Execute ();
		}
	}
}
