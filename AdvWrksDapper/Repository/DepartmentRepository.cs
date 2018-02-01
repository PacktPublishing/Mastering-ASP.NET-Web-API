using AdvWrksDapper.Models;
using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AdvWrksDapper.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AdvWorksConfig _advConfig;
        public DepartmentRepository(IOptions<AdvWorksConfig> advconfig)
        {
            _advConfig = advconfig.Value;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_advConfig.DbConnectionString);
            }
        }

        public bool AddDepartment(Department deptdetails)
        {
            bool isSuccess = false;
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var rows = dbConnection.Execute("INSERT INTO HumanResources.Department (name,groupname) VALUES(@Name,@GroupName)", deptdetails);
                if (rows == 1)
                {
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        public bool DeleteDepartment(int deptid)
        {
            bool isSuccess = false;
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var rows = dbConnection.Execute("DELETE FROM HumanResources.Department WHERE DepartmentID=@Id", new { Id = deptid });
                if (rows == 1)
                {
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        public bool DepartmentExists(int id)
        {
            var queryresult = false;
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = dbConnection.Query<Department>("SELECT * FROM HumanResources.Department WHERE DepartmentID = @Id", new { Id = id }).FirstOrDefault();
                if (result != null)
                {
                    queryresult = true;
                }
            }
            return queryresult;
        }

        public Department GetDepartment(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Department>("SELECT * FROM HumanResources.Department WHERE DepartmentID = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public IEnumerable<Department> GetDepartments()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Department>("SELECT * FROM HumanResources.Department");
            }
        }

        public bool UpdateDepartment(Department deptdetails)
        {
            bool queryresult = false;
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                var result = dbConnection.Execute("UPDATE HumanResources.Department SET name = @Name,  groupname  = @GroupName WHERE DepartmentID = @DepartmentID", deptdetails);
                if (result == 1)
                {
                    queryresult = true;
                }
            }
            return queryresult;
        }
    }
}