﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AspNetDeploy.Model;

namespace AspNetDeploy.WebUI.Controllers
{
    public class EnvironmentsController : GenericController
    {
        public ActionResult Index()
        {
            List<Environment> environments = this.Entities.Environment
                .Include("Machines.MachineRoles")
                .ToList();

            this.ViewBag.Environments = environments;

            return this.View();

        }
    }
}