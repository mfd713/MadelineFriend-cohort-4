using SustainableForaging.Core.Models;
using SustainableForaging.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SustainableForaging.BLL
{
    public class ForagerService
    {
        private readonly IForagerRepository repository;

        public ForagerService(IForagerRepository repository)
        {
            this.repository = repository;
        }

        public List<Forager> FindByState(string stateAbbr)
        {
            return repository.FindByState(stateAbbr);
        }

        public List<Forager> FindByLastName(string prefix)
        {
            return repository.FindAll()
                    .Where(i => i.LastName.StartsWith(prefix))
                    .ToList();
        }

        public Result<Forager> Add(Forager forager)
        {
            Result<Forager> result = Validate(forager);
            if (!result.Success)
            {
                return result;
            }

            result.Value = repository.Add(forager);

            return result;
        }

        public Result<Forager> Validate(Forager forager)
        {
            //validate nulls/empties
            var result = new Result<Forager>();
            if(forager == null)
            {
                result.AddMessage("Nothing to save.");
                return result;
            }

            if (string.IsNullOrEmpty(forager.FirstName))
            {
                result.AddMessage("First name required.");
            }

            if (string.IsNullOrEmpty(forager.LastName))
            {
                result.AddMessage("Last name required.");
            }

            if (string.IsNullOrEmpty(forager.State))
            {
                result.AddMessage("State required.");
            }

            //Validate First+Last+State is unique
            var query =repository.FindAll()
                .Where(f => (f.FirstName == forager.FirstName) && (f.LastName == forager.LastName) && (f.State == forager.State));

            if(query.Count() != 0)
            {
                result.AddMessage("Combination of first name, last name, and state must be unique");
      
            }

            return result;
        }
    }
}
