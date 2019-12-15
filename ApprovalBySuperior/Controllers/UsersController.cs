using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApprovalBySuperior.Models;
using ApprovalBySuperior.Services;
using System.Data;

namespace DotNetCode.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;


        }

        // GET: Users
        public IActionResult Index()
        {
            // UserRepository参照
            var _usrs = _userRepository.GetAll();


            return View(_usrs);
        }


        // GET: Users/Details/nc100000
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usrs = _userRepository.GetUserId(id);

            if ( usrs == null)
            {
                return NotFound();
            }

            return View(usrs);
        }

        // GET: Users/Create
        // 役職、所属部署、課班はプルダウンリスト化
        public IActionResult Create()
        {
            //Positionsから役職情報を取得しリスト化
            ViewBag.Opts1 = _userRepository.Positionslist();
            //Departmentsから課班情報を取得しリスト化
            ViewBag.Opts2 = _userRepository.DepNamelist();

            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public IActionResult Create([Bind("Userid,Username,Email,Users_position,Users_depname,Users_depgroupname")] Users users)
        public IActionResult Create(Users users)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var positionid = _userRepository.GetPositionNameToid(users.Users_position);
                    //var departmentid = _userRepository.GetDepartmentNameToId(users.Users_depname,
                    //                                                         users.Users_depgroupname);

                    //users.Positionid = positionid;
                    //users.Depid = departmentid;

                    _userRepository.Add(users);
                    _userRepository.Save();
                    return RedirectToAction("Index");
                }

            }
            catch (DataException)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty,
                                         "保存に失敗しました。再度実行してください, " +
                                         "もし、それでも問題が発生するようであれば管理者へ連絡してくだいさい。");
            }

            return View(users);
        }

        // GET: Users/Edit/nc100000
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = _userRepository.GetUserId(id);
            //Positionsから役職情報を取得しリスト化
            ViewBag.Opts1 = _userRepository.Positionslist();
            //Departmentsから課班情報を取得しリスト化
            ViewBag.Opts2 = _userRepository.DepNamelist();
            return View(users);
        }

        // POST: Users/Edit/nc100000
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public IActionResult Edit(string id, [Bind("Userid,Username,Email,Users_position,Users_depname,Users_depgroupname")] Users users)
        public IActionResult Edit(string id, Users users)
        {
            if (id != users.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userRepository.Update(users);
                    _userRepository.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    ModelState.AddModelError(string.Empty,
                                         "保存に失敗しました。再度実行してください, " +
                                         "もし、それでも問題が発生するようであれば管理者へ連絡してくだいさい。");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Users/Delete/5
        public IActionResult Delete(bool? saveChangesError = false, string id = null)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "削除に失敗しました。再度実行してください、 " +
                                       "もし、それでも問題が発生するようであれば管理者へ連絡してくだいさい。";
            }

            var users = _userRepository.GetUserId(id);

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            try
            {
                Users users = _userRepository.GetUserId(id);
                _userRepository.Delete(id);
                _userRepository.Save();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }

            return RedirectToAction("index");
        }


        // <summary>
        // 所属部Idより、その関係課班リストをJsonで返す
        // 本メソッドはUsersViewの所属部ViewModel(knockoutjs)から呼び出される
        // SelectList化されたGroupnameをJson形式で返却
        // </summary>
        // <param name="depname">所属部名</param>
        // <returns>(JsonResult)_depgrpSelectList</returns>
        [HttpGet]
        public JsonResult GetDepGroupname(string depname)
        {
            // UserRepositoryからDepartmentsのGroupname（SelectList)を取得する
            var _depgrpSelectList = _userRepository.DepGrplist(depname);

            return Json(_depgrpSelectList);
        }

    }
}
