using CineManagement.Models;

namespace CineManagement.Services
{
    class ProjectorService
    {
        public List<Projector> getProjectors()
        {
            using (var context = new CinemaManagementContext())
            {
                List<Projector> projectors = context.Projectors.ToList();

                if (projectors == null)
                {
                    throw new Exception("No projector found.");
                }
                else
                {
                    return projectors;
                }
            }
        }

    }
}
