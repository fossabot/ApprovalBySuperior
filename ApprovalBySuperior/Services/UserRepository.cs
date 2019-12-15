using System;
using System.Linq;
using System.Collections.Generic;
using ApprovalBySuperior.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;

namespace ApprovalBySuperior.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly MyContext _context;


        public UserRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<UsersViewModel> GetAll()
        {
            var model = _context.Users
                .Select(u => new UsersViewModel
                {
                    Userid = u.Userid,
                    Username = u.Username,
                    Email = u.Email,
                    Positionname = u.Users_position,
                    Depname = u.Users_depname,
                    Groupname = u.Users_depgroupname
                });

            return model;
        }

        public Users GetUserId(string id)
        {
            return _context.Users.Find(id);
        }

        public int GetPositionNameToid(string positionname)
        {
            var positidlist = _context.Positions
                .Where(n => n.Positionname == positionname)
                .Select(n => new { Positionid = n.Positionid })
                .FirstOrDefault();


            return positidlist.Positionid;
        }

        public int GetDepartmentNameToId(string departmentname,string groupname)
        {
      
            var depidlist = _context.Departments
                .Where(d => d.Depname == departmentname && d.Groupname == groupname)
                .Select(d => new { Depid = d.Depid })
                .FirstOrDefault();

            return depidlist.Depid;

        }

        public void Add(Users item)

        {
            _context.Users.Add(item);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Users item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(string id)
        {
            Users users = _context.Users.Find(id);
            _context.Users.Remove(users);
        }

        public SelectList Positionslist()
        {
            var posinamelist = _context.Positions
                .Select(p => new { Positionname = p.Positionname })
                .Distinct();
            var opts1 = new SelectList(posinamelist, "Positionname", "Positionname");

            return opts1;

        }

        public SelectList DepNamelist()
        {
            var depnamelist = _context.Departments
               .Select(d => new { Depname = d.Depname })
               .Distinct();
            var opts2 = new SelectList(depnamelist, "Depname", "Depname");

            return opts2;
        }

        public List<SelectListItem> DepGrplist(string depname)
        {

            var depgrplist = _context.Departments
                .Where(d => d.Depname == depname)
                .Select(d => new SelectListItem()
                   { Value = d.Depname, Text = d.Groupname })
                .ToList();

            return depgrplist;

        }

    }
}
