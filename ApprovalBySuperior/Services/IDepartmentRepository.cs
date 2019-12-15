using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApprovalBySuperior.Models;

namespace ApprovalBySuperior.Services
{
    public interface IDepartmentRepository
    {
        //Departmentすべて取得
        IEnumerable<Departments> GetAll();

        Departments GetDepartmentId(int? id);

        void Add(Departments item);

        void Save();

        void Update(Departments item);

        void Delete(int id);
    }
}
