using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Drawing;

namespace CineManagement
{
    public class Actor
    {
        [Key]
        [Column(name: "actorId")]
        public int actorId { get; set; }
        [Column(name: "actorName")]
        public string actorName { get; set; }
        [Column(name: "avatar")]
        public byte[] avatar { get; set; }
        [Column(name: "shotDes")]
        public string shotDes { get; set; }
    }

    public class ActorDbContext: BaseDbContext
    {
        public DbSet<Actor> Actor { get; set; }
    }
    public class ActorManage
    {
        public Actor getActorById(int id)
        {
            using (var _context = new ActorDbContext()) 
            {
                var existingActor = _context.Actor.FirstOrDefault(x => x.actorId == id);
                if(existingActor != null)
                {
                    return existingActor;
                } else
                {
                    throw new Exception("Actor is not exists!");
                }
            }
        }
    }

}
