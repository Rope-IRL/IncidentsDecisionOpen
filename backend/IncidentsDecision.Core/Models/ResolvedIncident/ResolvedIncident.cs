using IncidentsDecision.Core.Helpers;
using Microsoft.Identity.Client;

namespace IncidentsDecision.Core.Models.ResolvedIncident
{
    public class ResolvedIncident
    {
        public int? Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private ResolvedIncident(int? id, string name, string description, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
        }

        public static Result<ResolvedIncident> Create(int? id, string name, string description, int day, int month,
            int year, int hour, int minutes, int seconds)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return Result<ResolvedIncident>.Failure("Name and Description has to be not empty");
            }

            if (year < 2000)
            {
                return Result<ResolvedIncident>.Failure("Year has to be higher than 2000");
            }

            if (month < 1 || month > 12)
            {
                return Result<ResolvedIncident>.Failure("Month has to be greater than 0 and less than 12");
            }

            int daysInMonth = DateTime.DaysInMonth(year, month);

            if (day < 1 || day > daysInMonth)
            {
                return Result<ResolvedIncident>.Failure($"Day in current month has to be greater than 0 and less than {daysInMonth}");
            }

            if (hour < 0 || hour > 23)
            {
                return Result<ResolvedIncident>.Failure("Number of hours has to be between 0 and 23");
            }

            if (minutes < 0 || minutes > 59)
            {
                return Result<ResolvedIncident>.Failure("Number of minutes has to be between 0 and 59");
            }

            DateTime dateTime = new DateTime(year, month, day, hour, minutes, seconds);

            var resolvedIncident = new ResolvedIncident(id, name, description, dateTime.ToUniversalTime());

            return Result<ResolvedIncident>.Success(resolvedIncident);
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
            return $"Name os the incident is {Name} Description of the incident is {Description} Date and Time of the incident is {CreatedAt}";
        }
    }
}
