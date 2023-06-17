using Movies.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Movies.Controllers
{
    public class authenController : Controller
    {

        examEntities2 db = new examEntities2();
        // GET: authen
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult checkUser(user u)
        {
            var usr = db.user.FirstOrDefault((o => o.login == u.login));
            if (usr != null)
            {
                
                return RedirectToAction("list");
            }
            else
            {

            return View("index");
            }
        }

        public ActionResult list(user u)
        {
            ViewBag.film = db.film.ToList();
            ViewBag.acteur = db.acteur.ToList();

            return View("list");

        }
        public ActionResult detail(int id)
        {
            ViewBag.film = db.film.Find(id);
            ViewBag.acteur = db.acteur.ToList();
            return View("detail");

        }


        public ActionResult addFilm()
        {
            ViewBag.film = db.film.ToList();
            ViewBag.acteur = db.acteur.ToList();

            return View("addFilmView");

        }

        [HttpPost]
        public ActionResult addFilm(film f)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0]; //le nom du fichier
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName); // recupere le nom du fichier
                        var path = Path.Combine(Server.MapPath("~/Images"), fileName); //recuperer le chemin d'access ou sera mis notre fichier
                        file.SaveAs(path); //enregister sur le serveur

                        f.photo = fileName;
                    }
                }
                db.film.Add(f);
                db.SaveChanges();

                return View("addFilmView");

            }

        }


        
    }
}