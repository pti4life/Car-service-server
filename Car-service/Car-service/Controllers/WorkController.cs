using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car_service.Modells;
using Car_service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Car_service.Controllers
{
    [Route("api/work")]
    [ApiController]
    public class WorkController : ControllerBase
    {

        [HttpGet]
        public ActionResult<IEnumerable<Work>> GetAll()
        {
            var works = WorkRepository.GetWorks();
            return Ok(works);
        }

        [HttpGet("{id}")]
        public ActionResult<Work> Get(long id)
        {
            var work = WorkRepository.GetWorks().FirstOrDefault(x => x.Id == id);

            if(work != null)
            {
                return Ok(work);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Post(Work work)
        {
            var works = WorkRepository.GetWorks();

            work.Id = GetNewId(works);
            works.Add(work);
            WorkRepository.StoreWorks(works);
            return Ok();
        }

        public ActionResult Put(Work work) 
        {
            var works = WorkRepository.GetWorks();
            var foundedWork = works.FirstOrDefault(innerWork => innerWork.Id == work.Id);

            if (foundedWork == null)
            {
                return NotFound();
            }
            else
            {
                foundedWork.CarLicensePlate = work.CarLicensePlate;
                foundedWork.NameOfCustomer = work.NameOfCustomer;
                foundedWork.StateOfWork = work.StateOfWork;
                foundedWork.DetailOfIssues = work.DetailOfIssues;
                foundedWork.RecordingDate = work.RecordingDate;
                foundedWork.TypeOfCar = work.TypeOfCar;
            }

            WorkRepository.StoreWorks(works);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            var works = WorkRepository.GetWorks();
            var workToRemove = works.FirstOrDefault(work => work.Id == id);

            if( workToRemove != null)
            {
                works.Remove(workToRemove);
                WorkRepository.StoreWorks(works);
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        private long GetNewId(IList<Work> works)
        {
            long id = 0;
            foreach (var work in works)
            {
                if(id < work.Id)
                {
                    id = work.Id;
                }
            }

            return id + 1;
        }

    }
}