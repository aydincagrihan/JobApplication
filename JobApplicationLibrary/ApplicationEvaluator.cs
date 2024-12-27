using JobApplicationLibrary.Models;
using JobApplicationLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobApplicationLibrary
{
    public class ApplicationEvaluator
    {
        private const int _ageLimit = 18;
        private List<string> techStackList = new() { "C#", "RabbitMQ", "MicroService", "ReactJS", "TypeScript" };
        private const int autoAcceptedYearsOfExperience = 15;

        private IdentityValidator identityValidator;
        public ApplicationEvaluator()
        {
            identityValidator = new IdentityValidator();
        }

        public ApplicatonResult Evaluate(JobApplication form)
        {
            if (form.Applicant.Age < _ageLimit)
            {
                return ApplicatonResult.AutoReject;
            }

            var validIdentity = identityValidator.IsValid(form.Applicant.IdentityNumber);

            if (!validIdentity)
                return ApplicatonResult.TransferredToHR;

            var sr = GetTechStackSimilartyRate(form.TechStackList);
            if (sr < 25)
            {
                return ApplicatonResult.AutoReject;
            }
            if (sr > 75 && form.YearsOfExperience > autoAcceptedYearsOfExperience)

            {
                return ApplicatonResult.AutoAccept;
            }

            return ApplicatonResult.AutoAccept;
        }

        private int GetTechStackSimilartyRate(List<string> techStacks)
        {
            if (techStackList == null || techStackList.Count == 0)
                return 0;

            var matchedCounts = techStacks
                .Where(x => techStackList.Contains(x, StringComparer.OrdinalIgnoreCase))
                .Count();

            // Oranı hesaplayıp yüzdeye dönüştür
            return (int)(((double)matchedCounts / techStackList.Count) * 100);
        }

    }


    public enum ApplicatonResult
    {
        AutoReject,
        TransferredToHR,
        TransferredToLead,
        TransferredToCTO,
        AutoAccept
    }
}
