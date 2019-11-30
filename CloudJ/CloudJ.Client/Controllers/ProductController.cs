using CloudJ.Client.Clients;
using CloudJ.Client.Models;
using CloudJ.Contracts.DTOs.SolutionDtos.Category;
using CloudJ.Contracts.DTOs.SolutionDtos.Cloud;
using CloudJ.Contracts.DTOs.SolutionDtos.Photo;
using CloudJ.Contracts.DTOs.SolutionDtos.Plan;
using CloudJ.Contracts.DTOs.SolutionDtos.Review;
using CloudJ.Contracts.DTOs.SolutionDtos.Solution;
using Microsoft.AspNetCore.Authorization;
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
    public class ProductController : Controller
    {
        private readonly ISolutionApiClient _solutionApiClient;
        private readonly IBillingApiClient _billingApiClient;
        private readonly IIdentityClient _identityClient;
        public ProductController(ISolutionApiClient solutionApiClient, IBillingApiClient billingApiClient, IIdentityClient identityClient)
        {
            _solutionApiClient = solutionApiClient;
            _billingApiClient = billingApiClient;
            _identityClient = identityClient;
            
        }

        /// <summary>
        /// Страница конкретного решения
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            var response = await _solutionApiClient.GetByFilterAsync(new SolutionFilter
            {
                SolutionId = id
            });
            var dto = response.Data.FirstOrDefault();

            if (dto.Reviews.Any())
            {
                IDictionary<string, string> id_name = new Dictionary<string, string>();
                foreach(ReviewDto rd in dto.Reviews)
                {
                    if (id_name.ContainsKey(rd.AuthorId))
                        continue;

                    var user = await _identityClient.GetUserInfoAsync(rd.AuthorId);
                    var userData = user.Data;

                    if (userData != null)
                        id_name.Add(rd.AuthorId, userData.Email);
                    else
                        id_name.Add(rd.AuthorId, "User deleted");

                }
                ViewBag.ReviewAuthors = id_name;

            }

            return View(dto);
        }

        /// <summary>
        /// Добавление отзыва к решению
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("id:long")]
        public async Task<IActionResult> AddReview(NewReviewDto dto)
        {
            dto.AuthorId = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var result = await _solutionApiClient.AddReviewAsync(dto);
            return Redirect($"~/Product/{dto.SolutionId}");

        }

        /// <summary>
        /// Добавить ссылку к решению
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("links")]
        public async Task<IActionResult> AddLink([FromQuery]long id)
        {
            var solution = await _solutionApiClient.GetByFilterAsync(new SolutionFilter { SolutionId = id });
            ViewBag.Links = solution.Data.FirstOrDefault().SolutionLinks;
            return View(new NewSolutionLinkDto { SolutionId = id});
        }

        /// <summary>
        /// Запрос к апи на добавление ссылки решения
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("links")]
        public async Task<IActionResult> AddLink(NewSolutionLinkDto dto)
        {
            var response = await _solutionApiClient.AddSolutionLinkAsync(dto);
            var solution = await _solutionApiClient.GetByFilterAsync(new SolutionFilter { SolutionId = dto.SolutionId});
            ViewBag.Links = solution.Data.FirstOrDefault().SolutionLinks;
            return View(dto);
        }
        /// <summary>
        /// Запрос к апи на удаление ссылки
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deleteLink")]
        public async Task<IActionResult> DeleteLink(RemoveSolutionLinkDto dto)
        {
            var response = await _solutionApiClient.RemoveSolutionLinkAsync(dto);
            return Redirect($"~/Product/links?id={dto.SolutionId}");
        }

        /// <summary>
        /// Страница добавления плана
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("plan")]
        public async Task<IActionResult> AddPlan([FromQuery]long id)
        {
            var response = await _solutionApiClient.GetByFilterAsync(new SolutionFilter { SolutionId = id});
            ViewBag.Plans = response.Data.FirstOrDefault().Plans;
            return View(new NewPlanDto { SolutionId = id });
        }

        /// <summary>
        /// Запрос к апи на добавление плана к решению
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("plan")]
        public async Task<IActionResult> AddPlan(NewPlanDto dto)
        {
            var response = await _solutionApiClient.AddSolutionPlanAsync(dto);
            var sols = await _solutionApiClient.GetByFilterAsync(new SolutionFilter { SolutionId = dto.SolutionId });
            ViewBag.Plans = sols.Data.FirstOrDefault().Plans;
            return View(dto);
        }

        /// <summary>
        /// Добавить решение(первая форма с выбором докера или нашей спецификации)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("add")]
        public async Task<IActionResult> AddSolution()
        {
            return View();
        }

        /// <summary>
        /// Страница добавления по нашей спецификации
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("addPersonalPanel")]
        public async Task<IActionResult> AddPersonalSolution()
        {
            var categories = await _solutionApiClient.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Data;
            return View();
        }

        /// <summary>
        /// Запрос к апи на добавление решения по нашей спецификации
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("addPersonalPanel")]
        public async Task<IActionResult> AddPersonalSolution(PersonalSolutionModel model)
        {
            model.UserId = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            NewSolutionDto dto = new NewSolutionDto 
            { 
                UserId = model.UserId,
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cloud = new NewCloudDto 
                {
                    Name = model.CloudName,
                    Url = model.CloudUrl
                }
            };
            List<NewPhotoDto> photoList = new List<NewPhotoDto>();

            var ph = model.Logo;
            byte[] p1 = null;
            using (var fs1 = ph.OpenReadStream())
            using (var ms1 = new MemoryStream())
            {
                fs1.CopyTo(ms1);
                p1 = ms1.ToArray();
            }

            photoList.Add(new NewPhotoDto { Data = p1, Type = "Logo" });

            foreach (var photo in model.Photos)
            {
                p1 = null;
                using (var fs1 = photo.OpenReadStream())
                using (var ms1 = new MemoryStream())
                {
                    fs1.CopyTo(ms1);
                    p1 = ms1.ToArray();
                }

                photoList.Add(new NewPhotoDto { Data = p1, Type="Description" });
            }
            dto.Photos = photoList.ToArray();

            var response = await _solutionApiClient.AddSolutionAsync(dto);

            long id = response.Data.Id;

            return Redirect($"~/Product/{id}");
        }

        /// <summary>
        /// Страница добавления с помощью докер контейнера
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("addDocker")]
        public async Task<IActionResult> AddDocker()
        {
            var categories = await _solutionApiClient.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Data;
            return View();
        }

        /// <summary>
        /// Запрос к апи на добавление решения с помощью докера
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addDocker")]
        public async Task<IActionResult> AddDocker(DockerSolutionModel model)
        {
            model.UserId = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            NewSolutionDto dto = new NewSolutionDto
            {
                UserId = model.UserId,
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cloud = new NewCloudDto
                {
                    Name = model.CloudName,
                    Container = new NewDockerImageDto { Image = model.DockerUrl}
                }
            };
            List<NewPhotoDto> photoList = new List<NewPhotoDto>();

            var ph = model.Logo;
            byte[] p1 = null;
            using (var fs1 = ph.OpenReadStream())
            using (var ms1 = new MemoryStream())
            {
                fs1.CopyTo(ms1);
                p1 = ms1.ToArray();
            }

            photoList.Add(new NewPhotoDto { Data = p1, Type = "Logo" });

            foreach (var photo in model.Photos)
            {
                p1 = null;
                using (var fs1 = photo.OpenReadStream())
                using (var ms1 = new MemoryStream())
                {
                    fs1.CopyTo(ms1);
                    p1 = ms1.ToArray();
                }

                photoList.Add(new NewPhotoDto { Data = p1, Type = "Description" });
            }
            dto.Photos = photoList.ToArray();

            var response = await _solutionApiClient.AddSolutionAsync(dto);

            long id = response.Data.Id;

            return Redirect($"~/Product/{id}");
        }

        /// <summary>
        /// [Администратор] Страница добавления категории
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("addCategory")]
        public async Task<IActionResult> AddCategory()
        {
            var categories = await _solutionApiClient.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Data;
            return View();
        }

        /// <summary>
        /// [Администратор] Запрос к апи на добавление категории
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addCategory")]
        public async Task<IActionResult> AddCategory(NewCategoryModel model)
        {
            var categories = await _solutionApiClient.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Data;
            var dto = new NewCategoryDto 
            {
                Name = model.Name,
                Description = model.Description,
                ParentCategoryId = model.ParentCategoryId
            };

            

            var ph = model.Logo;
            byte[] p1 = null;
            using (var fs1 = ph.OpenReadStream())
            using (var ms1 = new MemoryStream())
            {
                fs1.CopyTo(ms1);
                p1 = ms1.ToArray();
            }
            dto.Logo = new NewPhotoDto { Data = p1, Type = "Logo" };

            await _solutionApiClient.AddCategoryAsync(dto);

            return Redirect("addCategory");
        }

        /// <summary>
        /// [Администратор] запрос к апи на удаление категории
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deleteCategory")]
        public async Task<IActionResult> DeleteCategory(RemoveCategoryDto dto)
        {
            await _solutionApiClient.RemoveCategoryAsync(dto);
            return Redirect("addCategory");
        }

        /// <summary>
        /// Страница изменения категории
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("editCategory")]
        public async Task<IActionResult> EditCategory(long id)
        {
            var categories = await _solutionApiClient.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Data;
            var cat = categories.Data.Where(c => c.Id == id).FirstOrDefault();
            return View(new UpdateCategoryModel 
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description,
                ParentCategoryId = cat.ParentCategory?.Id
            });
        }

        

        /// <summary>
        /// [Администратор] запрос к апи на изменение категории
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("editCategory")]
        public async Task<IActionResult> EditCategory(UpdateCategoryModel model)
        {
            var categories = await _solutionApiClient.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Data;

            var dto = new UpdateCategoryDto 
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                ParentCategoryId = model.ParentCategoryId
            };

            var ph = model.Logo;
            byte[] p1 = null;
            using (var fs1 = ph.OpenReadStream())
            using (var ms1 = new MemoryStream())
            {
                fs1.CopyTo(ms1);
                p1 = ms1.ToArray();
            }
            dto.Logo = new NewPhotoDto { Data = p1, Type = "Logo" };

            var updated_cat = await _solutionApiClient.UpdateCategoryAsync(dto);
            return Redirect("addCategory");
        }

        
    }
}
