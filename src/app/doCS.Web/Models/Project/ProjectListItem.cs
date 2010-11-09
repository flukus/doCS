using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace doCS.Web.Models
{
    public class ProjectListItem : Controller
    {
			public Guid ProjectId { get; set; }
			public string ProjectName { get; set; }
    }
}
