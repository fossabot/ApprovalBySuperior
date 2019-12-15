using System.Collections.Generic;
using ApprovalBySuperior.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ApprovalBySuperior.Services
{
    public interface IUserRepository
    {
        //Users,Postions,Departmetnsすべて取得
        //IEnumerable<UsersViewModel> GetAll();
        IEnumerable<UsersViewModel> GetAll();

        Users GetUserId(string id);

        int GetPositionNameToid(string positionname);

        int GetDepartmentNameToId(string departmentname, string groupname);

        void Add(Users item);

        void Update(Users item);

        void Save();

        void Delete(string id);

        //PostionsモデルのPositionnameをドロップダウンリスト化
        SelectList Positionslist();

        //DepartmentsモデルのDepnameをドロップダウンリスト化
        SelectList DepNamelist();

        //DepartmentsモデルのGroupnameをドロップダウンリスト化
        //引数にDepnameに紐づくDepidを指定する
        List<SelectListItem> DepGrplist(string depname);
    }
}
