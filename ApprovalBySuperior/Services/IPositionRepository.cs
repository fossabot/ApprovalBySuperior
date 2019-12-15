using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ApprovalBySuperior.Models;

namespace ApprovalBySuperior.Services
{
    public interface IPositionRepository
    {
        //Postionsすべて取得
        IEnumerable<Positions> GetAll();

        Positions GetPositionId(int? id);

        void Add(Positions item);

        void Save();

        void Update(Positions item);

        void Delete(int id);
    }
}
