using System;
using System.Linq;
using System.Collections.Generic;
using ApprovalBySuperior.Models;
using Microsoft.EntityFrameworkCore;

namespace ApprovalBySuperior.Services
{
    public class PositionRepository : IPositionRepository
    {
        private readonly MyContext _context;

        public PositionRepository(MyContext context)
        {
            _context = context;
        }

        public IEnumerable<Positions> GetAll()
        {
            return _context.Positions.ToList();
        }

        public Positions GetPositionId(int? id)
        {
            return _context.Positions.Find(id);
        }

        public void Add(Positions item)
        {
            _context.Positions.Add(item);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Positions item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Positions positions = _context.Positions.Find(id);
            _context.Positions.Remove(positions);
        }

    }
}
