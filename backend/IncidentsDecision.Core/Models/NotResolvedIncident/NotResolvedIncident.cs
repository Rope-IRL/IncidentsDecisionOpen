using IncidentsDecision.Core.Helpers;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace IncidentsDecision.Core.Models.NotResolvedIncident
{
    public class NotResolvedIncident
    {
        public int? Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private NotResolvedIncident(int? id, string name, string description, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }

        public static Result<NotResolvedIncident> Create(int? id, string name, string description)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return Result<NotResolvedIncident>.Failure("Name and Description has to be not empty");
            }

            if (name.Length > 50 || description.Length > 1000)
            {
                return Result<NotResolvedIncident>.Failure("Name length has to be less than 50 symbols and Description length had to be less than 1000 symbols");
            }

            var time = DateTime.UtcNow;

            var notResolvedIncident = new NotResolvedIncident(id, name, description, time);

            return Result<NotResolvedIncident>.Success(notResolvedIncident);
        }

        public static Result<NotResolvedIncident> Create(int? id, string name, string description,
            int day, int month, int year, int hour, int minutes, int seconds = 0)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return Result<NotResolvedIncident>.Failure("Name and Description has to be not empty");
            }

            if (year < 2000)
            {
                return Result<NotResolvedIncident>.Failure("Year has to be higher than 2000");
            }

            if (month < 1 || month > 12)
            {
                return Result<NotResolvedIncident>.Failure("Month has to be greater than 0 and less than 12");
            }

            int daysInMonth = DateTime.DaysInMonth(year, month);

            if (day < 1 || day > daysInMonth)
            {
                return Result<NotResolvedIncident>.Failure($"Day in current month has to be greater than 0 and less than {daysInMonth}");
            }

            if (hour < 0 || hour > 23)
            {
                return Result<NotResolvedIncident>.Failure("Number of hours has to be between 0 and 23");
            }

            if (minutes < 0 || minutes > 59)
            {
                return Result<NotResolvedIncident>.Failure("Number of minutes has to be between 0 and 59");
            }

            DateTime dateTime = new DateTime(year, month, day, hour, minutes, seconds);

            var notResolvedIncident = new NotResolvedIncident(id, name, description, dateTime.ToUniversalTime());

            return Result<NotResolvedIncident>.Success(notResolvedIncident);
        }

        public void UpdateName(string name)
        {
            this.Name = name;
        }

        public void UpdateDescription(string description)
        {
            this.Description = description;
        }

        public void UpdateDateAndTime(DateTime dateTime)
        {
            this.CreatedAt = dateTime;
        }

        public override string ToString()
        {
            return $"Name of the cur incident is {Name} Description is {Description} Date is {CreatedAt}";
        }

    }
}
