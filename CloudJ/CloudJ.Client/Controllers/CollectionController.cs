
using CloudJ.Client.Clients;
using CloudJ.Client.Models;
using CloudJ.Contracts.DTOs.CollectionDtos;
using CloudJ.Contracts.DTOs.SolutionDtos.Photo;
using CloudJ.Contracts.DTOs.SolutionDtos.Solution;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CloudJ.Client.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class CollectionController : Controller
    {
        private readonly ISolutionApiClient _solutionApiClient;
        private readonly ICollectionApiClient _collectionApiClient;
        public CollectionController(ISolutionApiClient solutionApiClient, ICollectionApiClient collectionApiClient)
        {
            _solutionApiClient = solutionApiClient;
            _collectionApiClient = collectionApiClient;
        }

        [HttpGet]
        public async Task<IActionResult> AddCollection()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCollection(NewCollectionModel dto)
        {
            var new_col = new NewCollectionDto
            {
                Name = dto.Name,
                Description = dto.Description,
                Preview = dto.Preview
            };

            var ph = dto.PhotoPreview;
            byte[] p1 = null;
            using (var fs1 = ph.OpenReadStream())
            using (var ms1 = new MemoryStream())
            {
                fs1.CopyTo(ms1);
                p1 = ms1.ToArray();
            }

            new_col.PhotoPreview = new NewPhotoDto { Data = p1, Type = "Logo" };
            var response = await _collectionApiClient.AddCollectionAsync(new_col);
            return Redirect($"~/Collection/AddCollectionSol?id={response.Data.Id}");
        }

        [HttpGet]
        [Route("addCollectionSol")]
        public async Task<IActionResult> AddCollectionSol(long id)
        {
            var response = await _collectionApiClient.GetByFilterAsync(new CollectionFilter { Id = id });
            ViewBag.Solutions = response.Data.FirstOrDefault().Solutions;
            return View(new CollectionSolModel { CollectionId = id});

        }

        [HttpPost]
        [Route("addCollectionSol")]
        public async Task<IActionResult> AddCollectionSol(CollectionSolModel model)
        {
            var currentCollection = await _collectionApiClient.GetByFilterAsync(new CollectionFilter { Id = model.CollectionId });

            var truncSols = currentCollection.Data.FirstOrDefault().Solutions.ToList();
            truncSols.Add(new TruncSolutionDto { SolutionId = model.SolutionId });

            var otvet= await _collectionApiClient.UpdateCollectionAsync(new UpdateCollectionDto {Id= model.CollectionId, Solutions = truncSols });
            //ViewBag.Solutions = response.Data.FirstOrDefault().Solutions;
            return Ok(otvet);



        }

        [HttpGet]
        [Route("collectionEdit")]
        public async Task<IActionResult> EditCollection(long id)
        {
            return View();
        }

        /// <summary>
        /// Получение информации о выборке
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetCollectionInfo(int id)
        {
            var col = await _collectionApiClient.GetByFilterAsync(new CollectionFilter { Id = id});
            var model = col.Data;



            List<SolutionDto> slist = new List<SolutionDto>();

            foreach(TruncSolutionDto t in model.First().Solutions)
            {
                var tempResponse = await _solutionApiClient.GetByFilterAsync(new SolutionFilter { SolutionId = t.SolutionId });
                slist.Add(tempResponse.Data.FirstOrDefault());
                    
                    
                    
            }

            ViewBag.Solutions = slist;

            return View(model.First());
        }
    }
}
