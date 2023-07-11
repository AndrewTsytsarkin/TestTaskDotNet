namespace WebApplication1.Services
{
    public abstract class AbstractPersonService
    {
        public abstract IEnumerable<PersonEntity> GetPersons();
        public abstract PersonEntity CreatePerson(PersonCreateDto person);
        public abstract PersonEntity EditPerson(PersonEditDto person);
        public abstract bool DeletePerson(PersonDto person);
    }

    public class PersonService : AbstractPersonService
    {
        private PersonDbContext _ctx;
        public PersonService(PersonDbContext ctx)
        {
            _ctx = ctx;
        }
        public override IEnumerable<PersonEntity> GetPersons()
        {
            return _ctx.Persons.ToList();
        }

        public override PersonEntity CreatePerson(PersonCreateDto person)
        {
            _ctx.Persons.Add(new()
            {
                FirstName = person.FirstName,
                SecondName = person.SecondName,
                Position = person.Position

            });
            _ctx.SaveChanges();
            var result = _ctx.Persons.First(e => e.FirstName == person.FirstName && e.SecondName == person.SecondName);
            return result;

        }

        public override PersonEntity EditPerson(PersonEditDto person)
        {
            if (_ctx.Persons.Any(e => e.FirstName == person.FirstName && e.SecondName == person.SecondName))
            {
                // throw new($"Person with FirstName={person.FirstName} and SecondName={person.SecondName} is already exist");
                var editPerson = _ctx.Persons.First(e => e.Id == person.Id);


                _ctx.Remove(editPerson);
                _ctx.SaveChanges();


                PersonEntity newEditPerson = new()
                {
                    Position = person.Position,
                    FirstName = person.FirstName,
                    SecondName = person.SecondName
                };


                _ctx.Add(newEditPerson);
                _ctx.SaveChanges();

                var result = _ctx.Persons.First(e => e.Id == newEditPerson.Id);
                return result;

            }
            else throw new("no person found");
        }

        public override bool DeletePerson(PersonDto person)
        {
            var delPerson = _ctx.Persons.First(e => e.Id == person.Id);
            _ctx.Persons.Remove(delPerson);
            _ctx.SaveChanges();

            return true;
        }
    }
}