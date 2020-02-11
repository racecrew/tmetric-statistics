using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using tmetricstatistics.Data;
using tmetricstatistics.Model;

namespace tmetricstatistics.Services
{
    public class TMetricDatabaseServices : ITMetricDatabaseServices
    {

        public List<Project> GetAllProjects()
        {
            List<Project> projects = null;
            
            
            //using (var dataContext = new TMetricDatabaseContext(SqliteDbContextOptionsBuilderExtensions.UseSqlite(Configuration.GetConnectionString("DefaultConnection")))
            //{
            //    projects = dataContext.Projects.ToList<Project>();
            //}          
        
            return projects;
        }

    }
}
