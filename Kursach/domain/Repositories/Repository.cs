using Kursach.domain;
using System.Linq;

namespace Kursach.infrastructure
{
    public class Repository : IRepository
    {
        public void Add(string tableName, params string[] parameters)
        {
            using (var context = new SecretaryDBContext())
            {
                switch (tableName)
                {
                    case "Calls":
                        var call = new Call()
                        {
                            FirstName = parameters[0],
                            LastName = parameters[1],
                            MidleName = parameters[2],
                            PhoneNumber = parameters[3],
                            Date = parameters[4],
                            Description = parameters[5]
                        };
                        context.Calls.Add(call);
                        break;
                    case "Meets":
                        var meet = new Meet()
                        {
                            FirstName = parameters[0],
                            LastName = parameters[1],
                            MidleName = parameters[2],
                            Date = parameters[3],
                            Description = parameters[4]
                        };
                        context.Meets.Add(meet);
                        break;
                    case "ToDoList":
                        var toDo = new ToDo()
                        {
                            Description = parameters[0],
                            Date=parameters[1],
                        };
                        context.ToDoList.Add(toDo);
                        break;
                    case "OtherThings":
                        var other = new OtherThing()
                        {
                            Description = parameters[0],
                            Date = parameters[1],
                        };
                        context.OtherThings.Add(other);
                        break;
                }
                context.SaveChanges();
            }
        }

        public void Delete(string tableName, int id)
        {
            using (var context = new SecretaryDBContext())
            {
                switch (tableName)
                {
                    case "Calls":
                        var call = context.Calls.Where(x => x.Id == id).FirstOrDefault();
                        context.Calls.Remove(call);
                        break;
                    case "Meets":
                        var meet = context.Meets.Where(x => x.Id == id).FirstOrDefault();
                        context.Meets.Remove(meet);
                        break;
                    case "ToDoList":
                        var toDo = context.ToDoList.Where(x => x.Id == id).FirstOrDefault();
                        context.ToDoList.Remove(toDo);
                        break;
                    case "OtherThings":
                        var other = context.OtherThings.Where(x => x.Id == id).FirstOrDefault();
                        context.OtherThings.Remove(other);
                        break;
                }
                context.SaveChanges();
            }
        }

        public object[] GetItemsByDate(string tableName, string date)
        {
            using (var context = new SecretaryDBContext())
            {
                switch (tableName)
                {
                    case "Calls":
                        return context.Calls.Where(call => call.Date == date).ToArray();
                    case "Meets":
                        return context.Meets.Where(meet => meet.Date == date).ToArray();
                    case "ToDoList":
                        return context.ToDoList.Where(toDo => toDo.Date == date).ToArray();
                    case "OtherThings":
                        return context.OtherThings.Where(other => other.Date == date).ToArray();
                }
            }
            return new object[0];
        }

        public void Update(string tableName,object entity)
        {
            using (var context = new SecretaryDBContext())
            {
                switch (tableName)
                {
                    case "Calls":
                        var call = entity as Call;
                        var callToUpdate = context.Calls.Where(x => x.Id == call.Id).FirstOrDefault();
                        callToUpdate.FirstName = call.FirstName;
                        callToUpdate.LastName = call.LastName;
                        callToUpdate.MidleName = call.MidleName;
                        callToUpdate.PhoneNumber = call.PhoneNumber;
                        callToUpdate.Status = call.Status;
                        callToUpdate.Description = call.Description;
                        callToUpdate.Date = call.Date;
                        break;
                    case "Meets":
                        var meet=entity as Meet;
                        var meetToUpdate = context.Meets.Where(x => x.Id == meet.Id).FirstOrDefault();
                        meetToUpdate.FirstName = meet.FirstName;
                        meetToUpdate.LastName = meet.LastName;
                        meetToUpdate.MidleName = meet.MidleName;
                        meetToUpdate.Status = meet.Status;
                        meetToUpdate.Description = meet.Description;
                        meetToUpdate.Date = meet.Date;
                        break;
                    case "ToDoList":
                        var toDo = entity as ToDo;
                        var toDoToUpdate = context.ToDoList.Where(x => x.Id == toDo.Id).FirstOrDefault();
                        toDoToUpdate.Description = toDo.Description;
                        toDoToUpdate.Date = toDo.Date;
                        toDoToUpdate.Status = toDo.Status;
                        break;
                    case "OtherThings":
                        var other = entity as OtherThing;
                        var otherToUpdate = context.OtherThings.Where(x => x.Id == other.Id).FirstOrDefault();
                        otherToUpdate.Description = other.Description;
                        otherToUpdate.Date = other.Date;
                        otherToUpdate.Status = other.Status;
                        break;
                }
                context.SaveChanges();
            }
        }
    }
}
