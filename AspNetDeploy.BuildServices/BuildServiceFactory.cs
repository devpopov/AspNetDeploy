﻿using AspNetDeploy.BuildServices.MSBuild;
using AspNetDeploy.Contracts;
using AspNetDeploy.Model;

namespace AspNetDeploy.BuildServices
{
    public class BuildServiceFactory : IBuildServiceFactory
    {
        private readonly INugetPackageManager nugetPackageManager;

        public BuildServiceFactory(INugetPackageManager nugetPackageManager)
        {
            this.nugetPackageManager = nugetPackageManager;
        }

        public IBuildService Create(ProjectType projectType)
        {
            return new MSBuildBuildService(this.nugetPackageManager);
        }
    }
}
