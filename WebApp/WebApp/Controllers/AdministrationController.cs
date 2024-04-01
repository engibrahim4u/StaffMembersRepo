using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Models.Repository;
using WebApp.Models.ViewModels;
using WebApp.Services;
using WebApp.Helper;

namespace WebApp.Controllers
{
    [Authorize(Roles = "superAdmin,admin")]
    public class AdministrationController : Controller
    {
        private readonly IPlaceUserRepository placeUsers;
        private readonly IPlaceRepository places;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdministrationController(
            IPlaceUserRepository _PlaceUsers,
                                      IPlaceRepository _places,
                                        RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager
                                        



            )
        {
            placeUsers = _PlaceUsers;
            places = _places;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(RoleVM model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole
                {
                    Name = model.Name
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("ListRoles");
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "superAdmin")]

        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        [Authorize(Roles = "superAdmin")]
        public async Task<IActionResult> EditRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "Account");
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"role with id: {id} not found";
                return View("NotFound");
            }
            RoleVM model = new RoleVM
            {
                ID = role.Id,
                Name = role.Name,
            };
            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    //model.Users.Add(user.UserName+"/"+user.Name);
                    model.Users.Add(user.Name);

                }
            }
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "superAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(RoleVM model)
        {
            var role = await _roleManager.FindByIdAsync(model.ID);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"role with id: {model.ID} not found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.Name;
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);

                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            if (string.IsNullOrEmpty(roleId)) return RedirectToAction("Index", "Account");

            ViewBag.roleId = roleId;
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"role with id: {roleId} not found";
                return View("NotFound");
            }
            var model = new List<UserRoleVM>();
            foreach (var user in _userManager.Users)
            {
                var UserRoleVM = new UserRoleVM
                {
                    UserId = user.Id,
                    UserName = user.Name

                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    UserRoleVM.IsSelected = true;
                else
                    UserRoleVM.IsSelected = false;
                model.Add(UserRoleVM);

            }
            return View(model);
        }

        public async Task<IActionResult> EditUsersInRole(List<UserRoleVM> model, string roleId)
        {
            if (string.IsNullOrEmpty(roleId)) return RedirectToAction("Index", "Account");

            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"role with id: {roleId} not found";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < model.Count - 1)
                        continue;
                    else
                        return RedirectToAction("EditRole", new { id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { id = roleId });
        }
        [Authorize(Roles = "admin,superAdmin")]
        public async Task<IActionResult> ListUsers(IFormCollection fc, string id = "All")
        {
            var selectedItem = 0;
            if (fc.Count > 0)
                selectedItem = Convert.ToInt32(fc["ddlCourseId"].ToString());
            //ViewBag.CourseList = DBTools.NotRepeatedNames(TeacherCourses());
            IList<ApplicationUser> users = new List<ApplicationUser>();
            switch (id)
            {
                case "Authors":
                    users = await _userManager.GetUsersInRoleAsync("author");
                    break;
                case "Reviewers":
                    users = await _userManager.GetUsersInRoleAsync("auditor");
                    break;
                case "Admin":
                    users = await _userManager.GetUsersInRoleAsync("admin");
                    break;
                case "Editorials":
                    users = await _userManager.GetUsersInRoleAsync("editorial");
                    break;
                case "Secretaries":
                    users = await _userManager.GetUsersInRoleAsync("secretary");
                    break;
                default:
                    users = _userManager.Users.ToList();
                    break;
            }

            return View(users);
        }


        private List<ApplicationUser> ReturnNotRpeated(List<ApplicationUser> list)
        {
            List<ApplicationUser> avialbleList = new List<ApplicationUser>();
            string name = "";
            foreach (var item in list)
            {
                if (item.Name == name) continue;
                else
                {
                    name = item.Name;
                    avialbleList.Add(item);
                }
            }
            return avialbleList.OrderBy(x => x.Name).ToList();
        }
        


        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "Account");

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"role with id: {id} not found";
                return View("NotFound");
            }
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("ListRoles");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View("ListRoles");
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"user with id: {id} not found";
                return View("NotFound");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            EditUserVM model = new EditUserVM
            {
                Id = user.Id,
                EmailConfirmed = user.EmailConfirmed,
                UserName = user.UserName,
                Mobile = user.Mobile,
                Roles = RolesAr(userRoles),
                Name = user.Name,
                Claims = userClaims.Select(x => x.Value).ToList()
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserVM vM)
        {
            var user = await _userManager.FindByIdAsync(vM.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"user with id: {vM.Id} not found";
                return View("NotFound");
            }
            user.Name = vM.Name;
            //user.Mobile = vM.Mobile;
            //user.UserName = vM.UserName;
            //user.Email = vM.UserName;
            user.EmailConfirmed = vM.EmailConfirmed;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);
            }
            else
            {
                ModelState.AddModelError("", "تم التحديث بنجاح");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            vM.Roles = userRoles;
            vM.Claims = userClaims.Select(x => x.Value).ToList();
            return View(vM);
        }
        private IList<string> RolesAr(IList<string> roles)
        {
            IList<string> rolesAr = new List<string>();
            foreach (var role in roles)
            {
                rolesAr.Add(ServiceTools.RoleNameAr(role));
            }
            return rolesAr;
        }
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string userId)
        {
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Index", "Account");

            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (userId == null)
            {
                ViewBag.ErrorMessage = $"role with id: {userId} not found";
                return View("NotFound");
            }
            var model = new List<UserRolesVM>();
            var rolList = _roleManager.Roles.ToList();
            foreach (var role in rolList)
                {
                    if (role.Name == "superAdmin" && !User.IsInRole("superAdmin")) continue;
                var UserRolesVM = new UserRolesVM
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    RoleNameAr=ServiceTools.RoleNameAr(role.Name)
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                    UserRolesVM.IsSelected = true;
                else
                    UserRolesVM.IsSelected = false;
                model.Add(UserRolesVM);

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesVM> model, string userId)
        {

            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (userId == null)
            {
                ViewBag.ErrorMessage = $"role with id: {userId} not found";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var role = await _roleManager.FindByIdAsync(model[i].RoleId);
                IdentityResult result = null;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < model.Count - 1)
                        continue;
                    else
                        return RedirectToAction("EditUser", new { id = userId });
                }
            }
            return RedirectToAction("EditUser", new { id = userId });
        }

        public IActionResult PlacesUser(IFormCollection fc, int id = 0)
        {
            ViewBag.Places = places.List().OrderBy(x => x.Name);
            var selectedItem = fc["ddlPlace"].ToString();
            if (!string.IsNullOrEmpty(selectedItem)) id = Convert.ToInt32(selectedItem);
            ViewBag.PlaceName = "";
            ViewBag.placeId = id;
            var row = placeUsers.List().Where(x => x.PlaceId == id)
                .Select(x => new { Name = x.Place.Name }).SingleOrDefault();
            if (row != null)
                ViewBag.placeName = row.Name;
            var model = new List<UserPlaceListVM>();

            foreach (var place in places.List())
            {
                UserPlaceListVM userPlace = new UserPlaceListVM
                { PlaceId = place.Id, PlaceName = place.Name };
                userPlace.Users = placeUsers.List()
                                    .Where(x => x.PlaceId == place.Id)
                                    .Select(x => x.UserName).ToList();
                model.Add(userPlace);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> EditPlaceUsers(int placeId)
        {
            ViewBag.placeId = placeId;

            var PlaceIsExsist = places.Find(placeId);
            if (PlaceIsExsist == null)
            {
                ViewBag.ErrorMessage = $" id: {placeId} not found";
                return View("NotFound");
            }
            var userList = await _userManager.GetUsersInRoleAsync("instructor");
            var model = new List<UserPlaceVM>();
            foreach (var user in userList)
            {

                var UserPlaceVM = new UserPlaceVM
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Name = user.Name
                };
                if (placeUsers.Find(placeId, user.UserName) != null)
                    UserPlaceVM.IsSelected = true;
                else
                    UserPlaceVM.IsSelected = false;
                model.Add(UserPlaceVM);

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPlaceUsers(List<UserPlaceVM> model, int placeId)
        {

            var PlaceIsExsist = places.Find(placeId);
            if (PlaceIsExsist == null)
            {
                ViewBag.ErrorMessage = $" id: {placeId} not found";
                return View("NotFound");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                if (model[i].IsSelected && placeUsers.Find(placeId, user.UserName) == null)
                {
                    PlaceUser PlaceUser = new PlaceUser { PlaceId = placeId, UserName = user.UserName };
                    placeUsers.Add(PlaceUser);
                }
                else if (!model[i].IsSelected && placeUsers.Find(placeId, user.UserName) != null)
                {
                    var PlaceUserDel = placeUsers.Find(placeId, user.UserName);
                    if (PlaceUserDel != null) placeUsers.Delete(PlaceUserDel);
                }
                else
                {
                    continue;
                }

            }
            return RedirectToAction("PlacesUser", new { id = placeId });
        }
            public async Task<IActionResult> UserManage1(string searchText="",string selectedRole="All",string active="All")
        {
            var users =_userManager.Users;
            UserManageDataVM userManageData = new UserManageDataVM();
            List<UserManageVM> userList = new List<UserManageVM>();
            foreach(var user in users)
            {
                UserManageVM userData = new UserManageVM();
                userData.Roles =await UserRoles(user);
                userData.Mobile = user.Mobile;
                userData.Email = user.Email;
                userData.Active = user.EmailConfirmed;
                userData.UserId = user.Id;
                userData.Name = user.Name;
                userList.Add(userData);

            }
            userManageData.Roles = AvaiableRoles();
            userManageData.Users = userList;
            userManageData.Active = active;
            userManageData.RoleName = selectedRole;
            userManageData.Name = searchText;
            return View(userManageData);
        }

        private async Task<List<UserRolesVM>> UserRoles(ApplicationUser user)
        {
            var list = await _userManager.GetRolesAsync(user);
            List<UserRolesVM> roles = new List<UserRolesVM>();
            foreach(var item in list)
            {
                if (item == "superAdmin") continue;
                UserRolesVM role = new UserRolesVM
                {
                    RoleName = item,
                    RoleNameAr = ServiceTools.RoleNameAr(item)

                };
            }
            return roles;
        }

        private List<UserRolesVM> AvaiableRoles()
        {
            List<UserRolesVM> list = new List<UserRolesVM>();
            var rolList = _roleManager.Roles.ToList();
            foreach (var role in rolList)
            {
                if (role.Name == "superAdmin" && !User.IsInRole("superAdmin")) continue;
                var UserRolesVM = new UserRolesVM
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    RoleNameAr = ServiceTools.RoleNameAr(role.Name)
                };
                list.Add(UserRolesVM);

            }
            return list;
        }

        //private List<UserManageVM> UserList()
        //{
        //    var users = members.Users();
        //    var roles = members.Roles();
        //    var userRoles = members.UserRoles();
        //    var result = users
        //   .Join(userRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
        //   .Join(roles, ur => ur.ur.RoleId, r => r.ID, (ur, r) => new { ur, r })
        //   .Select(c => new UserManageVM()
        //   {
        //       Name = c.ur.u.Name,
        //       Email = c.ur.u.Email,
        //       Role = c.r.Name,
        //       RoleAr = ServiceTools.RoleNameAr(c.r.Name),

        //       Active = c.ur.u.EmailConfirmed,
        //       Mobile = c.ur.u.Mobile,
        //       UserId = c.ur.u.Id

        //   }).Where(x=>x.Role!="superAdmin").ToList();
        //   //.GroupBy(uv => new { uv.Name, uv.Email, uv.Mobile, uv.Active,uv.UserId })
        //   //.Select(r => new UserManageVM()
        //   //{
        //   //    Name = r.Key.Name,
        //   //    Email = r.Key.Email,
        //   //    Mobile = r.Key.Mobile,
        //   //    Active = r.Key.Active,

        //   //    Role = string.Join(",", r.Select(c => c.Role).ToArray())
        //   //}).ToList();

        //    return result;
        //}

        [HttpGet]
        public async Task<IActionResult> UserManage(string Name, string RoleName, string Active, int pag = 1)
        {
            var users =await UserManageData(Name, RoleName, Active);
            const int pageSize = 15;
            if (pag < 1)
                pag = 1;
            int recordCount = users.Users.Count();
            var pager = new UserPager(recordCount, pag, Name, RoleName, Active, pageSize, "UserManage", "Administration");
            int recordSkip = (pag - 1) * pageSize;
            var data = users.Users.Skip(recordSkip).Take(pageSize).ToList();
            users.Users = data;

            ViewBag.Pager = pager;
            users.Roles = AvaiableRoles();
            return View(users);
        }


        private async Task<UserManageDataVM> UserManageData(string searchText, string selectedRole, string active)
        {
            var UserManageData = new UserManageDataVM();
            //var roles = _roleManager.Roles
            //    .Select(x=>new UserRolesVM { RoleName=x.Name,RoleNameAr=ServiceTools.RoleNameAr(x.Name)})
            //    .OrderBy(x=>x.RoleNameAr).ToList();
            var allUsers =await UserList(selectedRole);
            if (!string.IsNullOrEmpty(active))
            {
                switch (active)
                {
                    case "Yes":
                        allUsers = allUsers.Where(x => x.Active == true).ToList();
                        break;
                    case "No":
                        allUsers = allUsers.Where(x => x.Active == false).ToList();
                        break;

                }
            }
            if (!string.IsNullOrEmpty(searchText))
            {
                allUsers = allUsers.Where(x => x.Name.Contains(searchText)).ToList();
            }
            UserManageData.Active = active;
            UserManageData.Name = searchText;
            UserManageData.RoleName = selectedRole;
            UserManageData.Users = allUsers.OrderBy(x => x.Name).ToList();
            //UserManageData.Roles=roles;
            return UserManageData;
        }

        private async Task<List<UserManageVM>> UserList(string selectedRole)
        {
            var users = string.IsNullOrEmpty(selectedRole)? _userManager.Users.ToList():  await _userManager.GetUsersInRoleAsync("selectedRole");

            var result = users
           .Select(c => new UserManageVM()
           {
               Name =c.Name,
               Email = c.Email,
               Active = c.EmailConfirmed,
               Mobile = c.Mobile,
               UserId = c.Id

           }).ToList();


            return result;
        }
    }
}
