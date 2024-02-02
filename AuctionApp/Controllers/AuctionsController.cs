using AuctionApp.Data;
using AuctionApp.Data.Tables;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;

namespace AuctionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : Controller
    {
        private AuctionsDatabase _database = new();

        #region Примеры

        [HttpGet("[action]")]
        public string GetFirstAuctionNumber()
        {
            return _database.Auctions.First().Number;
        }

        [HttpGet("[action]")]
        public IList<Company> GetCompanies()
        {
            return (from company in _database.Companies
                    where company.Phone.StartsWith("+79")
                    select company).ToList();
        }

        [HttpDelete("Action")]
        public void DeleteCompany()
        {
            using (var t = _database.BeginTransaction())
            {
                var companiesToDelete = _database.Companies.Take(2).ToList();

                _database.Delete(companiesToDelete[0]);
                _database.Delete(companiesToDelete[1]);

                t.Commit();
            }
        }

        #endregion

        #region Для задания

        [HttpPost("[action]")]
        public async Task<ActionResult> UploadJson()
        {
            try
            {
                string jsonText;
                var file = Request.Form.Files[0];

                using (var stream = file.OpenReadStream())
                using (var sr = new StreamReader(stream))
                    jsonText = sr.ReadToEnd();

                if (!string.IsNullOrEmpty(jsonText))
                {
                    var db = new AuctionsDatabase();

                    var jObject = JsonConvert.DeserializeObject<JObject>(jsonText);
                    var auctionData = jObject.ToObject<Auction>();

                    auctionData.Id = Guid.NewGuid();
                    db.Insert(auctionData);

                    // Id добавленного аукциона
                    var insertedAuctionId = auctionData.Id;

                    // Десериализация лотов из JSON
                    var lotsData = jObject["Lots"].ToObject<List<Lot>>();

                    foreach (var lot in lotsData)
                    {

                        lot.Id = Guid.NewGuid();
                        lot.AuctionId = insertedAuctionId;


                        db.Insert(lot);

                        // Десериализация компаний из JSON
                        var companiesData = lot.Companies;


                        foreach (var company in companiesData)
                        {

                            company.Id = Guid.NewGuid();
                            if (company.Ownership != null)
                            {
                                // Поиск соответствующего OwnershipId в таблице CompanyOwnership
                                var ownershipId = db.GetTable<CompanyOwnership>()
                                                    .Where(co => co.Name == company.Ownership)
                                                    .Select(co => co.Id)
                                                    .FirstOrDefault();

                                // Установка найденного значения в поле OwnershipId компании
                                company.OwnershipId = ownershipId;
                            }

                            db.Insert(company);

                            // связь между лотом и компанией
                            db.Insert(new LotCompany { LotId = lot.Id, CompanyId = company.Id });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //return BadRequest(ex.Message);
            }

            return Ok();
        }




        [HttpGet("[action]/{search}")]
        public object LoadData(string search)
        {
            var result = from auction in _database.Auctions
                         join lot in _database.Lots on auction.Id equals lot.AuctionId
                         join lotCompany in _database.LotCompanies on lot.Id equals lotCompany.LotId
                         join company in _database.Companies on lotCompany.CompanyId equals company.Id
                         join ownership in _database.CompanyOwnerships on company.OwnershipId equals ownership.Id
                         where company.CompanyName.Contains(search)
                         select new
                         {
                             AuctionNumber = auction.Number,
                             CompanyName = company.CompanyName,
                             OwnershipName = ownership.Name,
                             LotNumber = lot.LotNum
                         };

            return result.ToList();
        }
        #endregion
    }
}
