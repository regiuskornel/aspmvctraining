using System;
using System.ComponentModel.DataAnnotations;

namespace SampleMVC.Models
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RelativeDateValidatorAttribute : ValidationAttribute
    {
        public enum RelativeDate
        {
            PrevMonth, Today, NextMonth
        }

        private readonly RequiredAttribute innerRequired = new RequiredAttribute();
        protected readonly RelativeDate rdate;

        public RelativeDateValidatorAttribute(RelativeDate relDate)
        {
            this.rdate = relDate;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !innerRequired.IsValid(value))
                return new ValidationResult("Date required!");

            DateTime datum = (DateTime)value;
            switch (this.rdate)
            {
                case RelativeDate.PrevMonth:
                    if (StartOfMonth(datum) != MonthStart(DateTime.Today, -1))
                        return new ValidationResult("Date only in prevoius month!");
                    break;
                case RelativeDate.Today:
                    if (datum.Date != DateTime.Today)
                        return new ValidationResult("Date only today!");
                    break;
                case RelativeDate.NextMonth:
                    if (StartOfMonth(datum) != MonthStart(DateTime.Today, +1))
                        return new ValidationResult("Date only in next month!");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return ValidationResult.Success;
        }

        private static DateTime StartOfMonth(DateTime d)
        {
            return d.AddDays(-d.Day + 1);
        }

        private static DateTime MonthStart(DateTime d, int monthRel)
        {
            return StartOfMonth(d.AddMonths(monthRel));
        }

    }

}