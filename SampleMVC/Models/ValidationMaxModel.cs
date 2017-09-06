using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SampleMVC.Models
{

    public class ValidationMaxModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name required (1)!")]
        public string FullName { get; set; }

        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        //Hard coded date range. Invalid If you don't fix it
        [Range(typeof(DateTime), "2011.01.01", "2011.12.31")]
        public DateTime LastPurchaseDate { get; set; }

        [Required]
        public int RequiredInt { get; set; }

        [Required]
        public bool RequiredBool { get; set; }

        public static Task<ValidationMaxModel> GetModell(int id)
        {
            if (_datalist == null) _datalist = new Dictionary<int, ValidationMaxModel>();

            if (!_datalist.ContainsKey(id))
            {
                _datalist.Add(id, new ValidationMaxModel()
                {
                    Id = id,
                    FullName = "Student " + id,
                    Address = string.Format("Budapest {0}.", id + 1),
                    Email = "proba@proba.hu",
                    LastPurchaseDate = DateTime.Now.AddDays(-2 * id)
                });
            }
            return Task.FromResult(_datalist[id]);
        }

        private static Dictionary<int, ValidationMaxModel> _datalist;
    }

    #region IValidatableObject model
    //First version
    public class ValidationMaxIvoModel1 : ValidationMaxModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (this.LastPurchaseDate > DateTime.Now || this.LastPurchaseDate < DateTime.Today.AddYears(-2))
            {
                results.Add(new ValidationResult("Last purchase date must be after we opend the store!",
                    new[] { "LastPurchaseDate", "FullName" }));
            }
            return results;
        }
    }

    //Second version
    public class ValidationMaxIvoModel2 : ValidationMaxModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateValue(this.LastPurchaseDate, validationContext, results, new[]
            {
            new RangeAttribute(typeof(DateTime),
                DateTime.Today.AddYears(-1).ToString("d"),
                DateTime.Today.AddYears(1).ToString("d"))
        }))
            {
                var badresults = new List<ValidationResult>();
                foreach (var validationResult in results)
                    badresults.Add(new ValidationResult(validationResult.ErrorMessage, new[] { "LastPurchaseDate" }));

                return badresults;
            }

            return results;
        }

    }
    #endregion

    #region Realative Date Validator models

    public class ValidationMaxRelativeModel : ValidationMaxModel
    {
        [Display(Name = "Travel day")]
        [RelativeDateValidator(RelativeDateValidatorAttribute.RelativeDate.PrevMonth)]
        public DateTime TravelDate { get; set; }

        public new static Task<ValidationMaxRelativeModel> GetModell(int id)
        {
            return Task.FromResult(new ValidationMaxRelativeModel
            {
                Id = id,
                FullName = "Student " + id,
                Address = string.Format("Budapest {0}.", id + 1),
                Email = "proba@proba.hu",
                LastPurchaseDate = DateTime.Now.AddDays(-2 * id),
                TravelDate = DateTime.Today
            });
        }
    }


    #endregion
}