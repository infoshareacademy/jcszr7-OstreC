﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OstreCWEB.Services.StoryService;
using OstreCWEB.ViewModel.StoryBuilder;

namespace OstreCWEB.Controllers
{
    public class StoryBuilderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly StoryService _storyService;

        public StoryBuilderController(IMapper mapper)
        {
            _mapper = mapper;
            _storyService = new StoryService();
        }

        // GET: StoryBuilderController
        public ActionResult Index()
        {
            var stories = _storyService.GetAll();
            var model = new List<StoryView>();
            foreach (var item in stories)
            {
                model.Add(_mapper.Map<StoryView>(item));
            }

            return View(model);
        }

        // GET: StoryBuilderController/Details/5/1
        public ActionResult Details(int id, int paragraphId)
        {
            var story = _storyService.GetStoryById(id);
            var model = _mapper.Map<StoryDetailsView>(story);
            foreach (var item in _storyService.GetPreviousParagraphsById(paragraphId, id))
            {
                model.PreviousParagraphs.Add(_mapper.Map<ParagraphView>(item));
            }
            model.ActuallyParagraphView = _mapper.Map<ParagraphView>(_storyService.GetParagraphById(paragraphId));

            return View(model);
        }

        // GET: StoryBuilderController/ParagraphDetails/5
        public ActionResult ParagraphDetails(int id)
        {
            var paragraph = _storyService.GetParagraphById(id);
            var model = _mapper.Map<ParagraphDetailsView>(paragraph);

            return View(model);
        }

        // GET: StoryBuilderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoryBuilderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoryBuilderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StoryBuilderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StoryBuilderController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StoryBuilderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}