﻿using System;
using System.ServiceModel;
using System.ServiceModel.Security;
using AspNetDeploy.Contracts;
using AspNetDeploy.Contracts.MachineSummary;
using AspNetDeploy.DeploymentServices.WCFSatellite;
using AspNetDeploy.Model;

namespace AspNetDeploy.DeploymentServices.SatelliteMonitoring
{
    public class SatelliteMonitor : ISatelliteMonitor
    {
        public SatelliteState IsAlive(Machine machine)
        {
            if (string.IsNullOrWhiteSpace(machine.URL) || string.IsNullOrWhiteSpace(machine.Login) || string.IsNullOrWhiteSpace(machine.Password))
            {
                return SatelliteState.NotConfigured;
            }

            WCFSatelliteDeploymentAgent agent = new WCFSatelliteDeploymentAgent(null, machine.URL, machine.Login, machine.Password, new TimeSpan(0, 0, 0, 3), new TimeSpan(0, 0, 0, 3));

            try
            {
                agent.IsReady();
                return SatelliteState.Alive;
            }
            catch (Exception e)
            {
                if (e is MessageSecurityException)
                {
                    return SatelliteState.UnableToEstablishSecureConnection;
                }

                if (e is TimeoutException)
                {
                    return SatelliteState.Timeout;
                }

                if (e is EndpointNotFoundException)
                {
                    return SatelliteState.Inactive;
                }

                return SatelliteState.Inactive;
            }
        }

        public IServerSummary GetServerSummary(Machine machine)
        {
            if (string.IsNullOrWhiteSpace(machine.URL) || string.IsNullOrWhiteSpace(machine.Login) || string.IsNullOrWhiteSpace(machine.Password))
            {
                return null;
            }

            WCFSatelliteDeploymentAgent agent = new WCFSatelliteDeploymentAgent(null, machine.URL, machine.Login, machine.Password, new TimeSpan(0, 0, 0, 2), new TimeSpan(0, 0, 0, 2));

            try
            {
                return agent.GetServerSummary();
            }
            catch (Exception e)
            {
                if (e is MessageSecurityException)
                {
                    return null;
                }

                if (e is TimeoutException)
                {
                    return null;
                }

                if (e is EndpointNotFoundException)
                {
                    return null;
                }

                return null;
            }
        }
    }
}