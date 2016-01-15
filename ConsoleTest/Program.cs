﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using AspNetDeploy.BuildServices.MSBuild;
using AspNetDeploy.Contracts;
using AspNetDeploy.DeploymentServices.WCFSatellite;
using AspNetDeploy.Model;
using AspNetDeploy.Variables;
using AspNetDeploy.WebUI.Bootstrapper;
using BuildServices.NuGet;
using LocalEnvironment;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using ObjectFactory;
using ThreadHostedTaskRunner;
using Microsoft.Build.Construction;

namespace ConsoleTest
{
    public class ConsoleLogger : ILogger
    {
        public void Initialize(IEventSource eventSource)
        {
            //eventSource.ProjectStarted += (sender, args) => Console.WriteLine("Started " + args.ProjectFile);
            eventSource.ProjectFinished += (sender, args) => Console.WriteLine("finished " + args.ProjectFile);
            eventSource.ErrorRaised += (sender, args) =>
            {
                Console.WriteLine("ERROR");
                Console.WriteLine("* " + args.ProjectFile);
                Console.WriteLine("* " + args.File);
                Console.WriteLine("* " + args.LineNumber + " / " + args.ColumnNumber);
                Console.WriteLine("* " + args.Message);
            };
        }

        public void Shutdown()
        {
            
        }

        public LoggerVerbosity Verbosity { get; set; }
        public string Parameters { get; set; }
    }

    class Program
    {
        private static bool WorkerComplete = false;



        static void Main(string[] args)
        {
            /*MSBuildBuildService buildBuildService = new MSBuildBuildService(new NugetPackageManager(new PathServices()));

            DateTime? startDate = null;

            BuildSolutionResult buildSolutionResult = buildBuildService.Build(@"H:\Documentoved\Latest\Management.WebUI\Management.WebUI.csproj", s =>
            {
                if (startDate == null)
                {
                    startDate = DateTime.Now;
                }
            }, null, null);
            DateTime endDate = DateTime.Now;

            Console.WriteLine(buildSolutionResult.IsSuccess);
            Console.WriteLine((endDate - startDate.Value).TotalMilliseconds);


           

            
*/

            //string path = @"H:\Documentoved\Latest\Services.ImsPrimary\Databases.ImsPrimary.sqlproj";
            string path = @"H:\Documentoved\Latest\Services.ExceptionHandlingService\Services.Logging.csproj";

            //  SolutionFile solutionFile = SolutionFile.Parse(path);

            // ProjectRootElement element = ProjectRootElement.Open(@"H:\Documentoved\Latest\Services.ExceptionHandlingService\Services.Logging.csproj");


            Dictionary<string, string> globalProperty = new Dictionary<string, string>
            {
                {"Configuration", "Release"}
            };

            ProjectCollection projectCollection = new ProjectCollection();

            BuildRequestData buildRequestData = new BuildRequestData(path, globalProperty, "14.0", new[] { "Rebuild" }, null);

            BuildParameters buildParameters = new BuildParameters(projectCollection);
            buildParameters.MaxNodeCount = 1;
            buildParameters.Loggers = new List<ILogger>
            {
                new ConsoleLogger()
            };


            

            BuildResult buildResult = Microsoft.Build.Execution.BuildManager.DefaultBuildManager.Build(buildParameters, buildRequestData);
      
           

            //Console.WriteLine("building " + buildResult.OverallResult);

/*

            ObjectFactoryConfigurator.Configure();

            Console.WriteLine("Testing satellites");

            AspNetDeployEntities entities = new AspNetDeployEntities();

            MSBuildBuildService service = new MSBuildBuildService(new NugetPackageManager(new PathServices()));
            service.Build(
                @"H:\AspNetDeployWorkingFolder\Sources\5\45\CodeBase.Documents.WebUI\CodeBase.Documents.WebUI.csproj",
                s => Console.WriteLine("started: " + s), (s, b, arg3) => Console.WriteLine("done: " + arg3), (s, s1, arg3, arg4, arg5, arg6) => Console.WriteLine("Err: " + s));*/

            /*foreach (Machine machine in entities.Machine)
            {
                Console.Write(machine.Name + "...");
                Console.WriteLine(Factory.GetInstance<ISatelliteMonitor>().IsAlive(machine));
            }

            Console.ReadKey();

            RunScheduler();*/

            WriteLine("Main thread complete");
            Console.ReadKey();
        }

        private static void RunScheduler()
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (sender, eventArgs) => ThreadTaskRunner.ProcessTasks();
            worker.RunWorkerCompleted += (sender, eventArgs) =>
            {
                WriteLine("---------------");
                WriteLine("Worker Complete");
                WorkerComplete = true;
            };
            worker.RunWorkerAsync();

            AspNetDeployEntities entities = new AspNetDeployEntities();

            while (true)
            {
                WriteLine(DateTime.Now.ToString());
                List<SourceControlVersion> sourceControls = entities.SourceControlVersion.Include("SourceControl").ToList();
                List<BundleVersion> bundleVersions = entities.BundleVersion.Include("Bundle").ToList();
                List<ProjectVersion> projectVersions = entities.ProjectVersion.Include("Project").ToList();
                List<Machine> machines = entities.Machine.ToList();

                foreach (SourceControlVersion sourceControlVersion in sourceControls)
                {
                    WriteLine(sourceControlVersion.Id + " - " + sourceControlVersion.SourceControl.Name + " / " + sourceControlVersion.Name + " - " + TaskRunnerContext.GetSourceControlVersionState(sourceControlVersion.Id));
                }

                WriteLine("");

                foreach (BundleVersion bundleVersion in bundleVersions)
                {
                    WriteLine(bundleVersion.Id + " - " + bundleVersion.Bundle.Name + " / " + bundleVersion.Name + " - " + TaskRunnerContext.GetBundleVersionState(bundleVersion.Id));
                }

                WriteLine("");

                foreach (ProjectVersion projectVersion in projectVersions.Where(p => TaskRunnerContext.GetProjectVersionState(p.Id) != ProjectState.Idle))
                {
                    WriteLine(
                        projectVersion.SourceControlVersion.SourceControl.Name + "/ " +  
                        projectVersion.SourceControlVersion.Name + "/ " +  
                        projectVersion.Id + " - " + projectVersion.Project.Name + " - " + TaskRunnerContext.GetProjectVersionState(projectVersion.Id));
                }

                WriteLine("");

                foreach (Machine machine in machines)
                {
                    WriteLine(
                        machine.Name + " - " + TaskRunnerContext.GetMachineState(machine.Id));
                }

                WriteLine("");

                /*if (sourceControls.All(scv => TaskRunnerContext.GetSourceControlVersionState(scv.Id) == SourceControlState.Idle) &&
                    bundleVersions.All(bv => TaskRunnerContext.GetBundleVersionState(bv.Id) == BundleState.Idle))
                {
                    break;
                }*/

                if (WorkerComplete)
                {
                    break;
                }

                Thread.Sleep(200);

                Console.SetCursorPosition(0,0);
            }
        }

        public static void WriteLine(string value)
        {
            Console.WriteLine(new string(' ', Console.WindowWidth - 1) + "\r" + value);
        }
    }
}
