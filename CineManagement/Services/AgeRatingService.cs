using CineManagement.Models;
using CineManagement.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineManagement.Services
{
    public class AgeRatingService
    {
        public List<AgeRating> getListAgeRating()
        {
            using(var context = new CinemaManagementContext())
            {
                List<AgeRating> ageRatings = context.AgeRatings.ToList();
                if (ageRatings == null)
                {
                    throw new Exception("No AgeRating found.");
                } else
                {
                    return ageRatings;
                }
            }
        }
    }
}
