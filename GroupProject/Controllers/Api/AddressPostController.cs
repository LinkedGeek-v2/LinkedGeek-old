using AutoMapper;
using GroupProject.ApiModels.Incoming.ProfilePage;
using GroupProject.DAL;
using GroupProject.Models.SharedModels;
using GroupProject.Persistence;
using GroupProject.Repositories;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GroupProject.Controllers.Api
{
    [Authorize]
    [RoutePrefix("address")]
    public class AddressPostController : ApiController
    {
        private readonly ApplicationDbContext db;
        private readonly AddressRepository _addressRepository;
        private readonly UnitOfWork _unitOfWork;
        private readonly CityRepository _cityRepository;
        private readonly CountryRepository _countryRepository;

        private string userID => User.Identity.GetUserId();


        public AddressPostController()
        {
            db = new ApplicationDbContext();
            _addressRepository = new AddressRepository(db);
            _unitOfWork = new UnitOfWork(db);
            _cityRepository = new CityRepository(db);
            _countryRepository = new CountryRepository(db);
        }


        [Route("cities/{Id}")]
        public IHttpActionResult GetCitiesOnClick(string Id)
        {
            var cities = _cityRepository.GetCitiesOfCountry(Id);

            if(cities == null) return BadRequest("Wrong Id");

            return Ok(cities);
        }

        [Route("countries")]
        public IHttpActionResult GetCountriesOnClick()
        {

            var countries = _countryRepository.GetCountries();

            return Ok(countries);
        }

        //Edit or Add Address
        [Route("add-edit")]
        [HttpPost]
        public IHttpActionResult AddorEditAddress(AddressPostDto addressPostDto)
        {

            if (!ModelState.IsValid)
            {
                var a = ModelState.Values.SelectMany(msE => msE.Errors).Select(err => err.ErrorMessage);
                var bf = "";
                foreach (var item in a)
                {
                    bf += item + ",";
                }
                return BadRequest(bf);
            }

            addressPostDto.MatchWithUser(userID);

            var address = Mapper.Map<AddressPostDto, Address>(addressPostDto);

            _addressRepository.AddOrEdit(address);

            _unitOfWork.Save();

            return Ok();
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }
    }
}
