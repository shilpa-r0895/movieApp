using System;
using System.Collections.Generic;
using System.Linq;
using MovieApp.Entity;
using MovieApp.Helpers;
using MovieApp.Repository.Interfaces;

namespace MovieApp.Repository
{
    public class PersonRepository : IPersonRepository
    {

        private MovieAppDbContext _context;

        public PersonRepository(MovieAppDbContext context)
        {
            _context = context;
        }

        public Entity.Person AddActor(Model.RequestModel.AddPerson actor)
        {
            var newActor = AutoMapper.Mapper.Map<Entity.Person>(actor);
            newActor.PersonType = Helpers.PersonType.Actor;
            _context.People.Add(newActor);
            if (_context.SaveChanges() > 0)
            {
                return newActor;
            }
            return null;
        }

        public Entity.Person AddProducer(Model.RequestModel.AddPerson producer)
        {
            var newProducer = AutoMapper.Mapper.Map<Entity.Person>(producer);
            newProducer.PersonType = Helpers.PersonType.Producer;
            _context.People.Add(newProducer);
            if (_context.SaveChanges() > 0)
            {
                return newProducer;
            }
            return null;
        }

        public bool DeletePerson(Guid personId)
        {
            var person = _context.People.FirstOrDefault(p => p.Id == personId);
            if(person.PersonType == Helpers.PersonType.Actor)
            {
                var actorMovieMap = _context.ActorMovieMap.FirstOrDefault(am => am.ActorId == personId);
                if (actorMovieMap == null)
                {
                    _context.People.Remove(GetPerson(personId));
                    return _context.SaveChanges() > 0;
                }
            }
            else
            {
                var movie = _context.Movies.FirstOrDefault(m => m.ProducerId == personId);
                if(movie == null)
                {
                    _context.People.Remove(GetPerson(personId));
                    return _context.SaveChanges() > 0;
                }
            }

            return false;
        }

        public Entity.Person EditPerson(Guid personId, Model.RequestModel.Person person)
        {
            var personObj = GetPerson(personId);

            AutoMapper.Mapper.Map(person, personObj);

            if (_context.SaveChanges() > 0)
                return personObj;

            return null;
        }

        public IList<Entity.Person> GetAllActors()
        {
            return _context.People.Where(p => p.PersonType == Helpers.PersonType.Actor).ToList();
        }

        public IList<Entity.Person> GetAllProducers()
        {
            return _context.People.Where(p => p.PersonType == Helpers.PersonType.Producer).ToList();
        }

        public Entity.Person GetPerson(Guid personId)
        {
            return _context.People.FirstOrDefault(p => p.Id == personId);
        }

        public Person GetPerson(string name, PersonType personType)
        {
            return _context.People.FirstOrDefault(p => p.Name == name && p.PersonType == personType);
        }
    }
}
