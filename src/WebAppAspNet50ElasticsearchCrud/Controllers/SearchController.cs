﻿using Microsoft.AspNetCore.Mvc;
using System;
using WebAppAspNet50ElasticsearchCrud.Providers;

namespace WebAppAspNet50ElasticsearchCrud.Controllers
{
    public class SearchController : Controller
    {
        readonly ISearchProvider _searchProvider;

        public SearchController(ISearchProvider searchProvider)
        {
            _searchProvider = searchProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Skill model)
        {
            if (ModelState.IsValid)
            {
                model.Created = DateTime.UtcNow;
                model.Updated = DateTime.UtcNow;
                _searchProvider.AddUpdateEntity(model);

                return Redirect("Search/Index");
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult Update(long updateId, string updateName, string updateDescription)
        {
            _searchProvider.UpdateSkill(updateId, updateName, updateDescription);
            return Redirect("Index");
        }

        [HttpPost]
        public ActionResult Delete(long deleteId)
        {
            _searchProvider.DeleteSkill(deleteId);
            return Redirect("Index");
        }

        public JsonResult Search(string term)
        {
            return Json(_searchProvider.QueryString(term));
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}