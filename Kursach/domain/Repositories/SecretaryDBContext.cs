using Kursach.domain;
using System.Data.Entity;

namespace Kursach
{
    public class SecretaryDBContext: DbContext
    {
        public SecretaryDBContext() : base("DbConnectionString")
        {

        }

        public DbSet<Call> Calls { get; set; }
        public DbSet<Meet> Meets { get; set; }
        public DbSet<ToDo> ToDoList { get; set; }
        public DbSet<OtherThing> OtherThings { get; set; }
    }
}
