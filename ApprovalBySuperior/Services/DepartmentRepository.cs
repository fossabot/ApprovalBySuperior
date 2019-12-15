using System;
using System.Linq;
using System.Collections.Generic;
using ApprovalBySuperior.Models;
using Microsoft.EntityFrameworkCore;

namespace ApprovalBySuperior.Services
{
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly MyContext _context;

        public DepartmentRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<Departments> GetAll()
        {
            return _context.Departments.ToList();
        }

        public Departments GetDepartmentId(int? id)
        {
            return _context.Departments.Find(id);
        }

        public void Add(Departments item)
        {
            _context.Departments.Add(item);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Departments item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Departments departments = _context.Departments.Find(id);
            _context.Departments.Remove(departments);
        }

    }
}
