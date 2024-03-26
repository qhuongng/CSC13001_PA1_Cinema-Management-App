using CineManagement.Models;
using Microsoft.EntityFrameworkCore;

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

        public bool addProjector(Projector projector)
        {
            using (var _context = new CinemaManagementContext())
            {
                var existingProjector = _context.Projectors.FirstOrDefault(x => x.ProjectorId == projector.ProjectorId);
                if (existingProjector == null)
                {
                    _context.Projectors.Add(projector);
                    try
                    {
                        _context.SaveChanges();
                        return true;
                    }
                    catch (DbUpdateException ex)
                    {
                        // Handle database update exception
                        throw new Exception("An error occurred while saving the projector.", ex);
                    }

                }
                else
                {
                    throw new Exception("Projector already exists!");
                }
            }
        }

        public Projector getProjectorById(int id)
        {
            using (var context = new CinemaManagementContext())
            {
                Projector prjt = context.Projectors.FirstOrDefault(x => x.ProjectorId == id);

                if (prjt == null)
                {
                    throw new Exception("Projector not found.");
                }
                else
                {
                    return prjt;
                }
            }
        }
        public bool deleteProjectorById(int id)
        {
            using (var context = new CinemaManagementContext())
            {
                Projector prjt = context.Projectors.FirstOrDefault(m => m.ProjectorId == id);
                if (prjt == null)
                {
                    throw new Exception("Projector not found.");
                }
                else
                {
                    context.Projectors.Remove(prjt);
                    context.SaveChanges();
                    return true;
                }
            }
        }

        public bool updateProjector(Projector projector)
        {
            using (var context = new CinemaManagementContext())
            {
                Projector oldPrjt = context.Projectors.FirstOrDefault(m => m.ProjectorId.Equals(projector.ProjectorId));

                if (oldPrjt == null)
                {
                    throw new Exception("Projector not found.");
                }
                else
                {
                    oldPrjt.ProjectorInfo = projector.ProjectorInfo;
                    context.SaveChanges();

                    return true;
                }
            }
        }
    }
}
