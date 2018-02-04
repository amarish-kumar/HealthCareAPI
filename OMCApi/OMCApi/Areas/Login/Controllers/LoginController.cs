using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using OMCApi.Areas.Login.Models;
using OMC.Models;
using Ninject;
using OMC.BL.Interface;

namespace OMCApi.Areas.Login.Controllers
{
    public class LoginController : Controller
    {

        #region Declarations

        private readonly IKernel _Kernel;

        #endregion

        #region Constructor

        public LoginController()
        {
            //_Kernel = new StandardKernel(new OMC.Modules.SignInModule());
            _Kernel = new StandardKernel();
            _Kernel.Load(new OMC.Modules.SignInModule());
        }

        #endregion

        #region Methods

        // GET: Login/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signin(UserLogin user)
        {
            var SignInObj = _Kernel.Get<ISignIn>();

            string username = user.Username;
            string password = user.Password;

            var SignInResult = SignInObj.InitiateSignInProcess(user);

            return View();
        }
        // GET: Login/Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion
    }
}
