﻿using ObjectFactory;
using SatelliteService.Contracts;
using SatelliteService.LocalBackups;
using SatelliteService.LocalEnvironment;
using SatelliteService.Packages;

namespace SatelliteService.Bootstrapper
{
    public class PrimaryRegistry : Registry
    {
        public PrimaryRegistry()
        {
            this.Map<IPathRepository, PathRepository>(LifecycleType.Application);
            this.Map<IPackageRepositoryFactory, PackageRepositoryFactory>(LifecycleType.Application);
            this.Map<IBackupRepository, LocalBackupRepository>();
        }
    }
}