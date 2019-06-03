using MovieApp.Helpers;
using System;
using System.Collections.Generic;

namespace MovieApp.Repository.Interfaces
{
    public interface IPersonRepository
    {
        Entity.Person AddActor(Model.RequestModel.AddPerson actor);

        Entity.Person AddProducer(Model.RequestModel.AddPerson producer);

        bool DeletePerson(Guid personId);

        Entity.Person EditPerson(Guid personId, Model.RequestModel.Person person);

        Entity.Person GetPerson(Guid personId);

        Entity.Person GetPerson(string name, PersonType personType);

        IList<Entity.Person> GetAllActors();

        IList<Entity.Person> GetAllProducers();
    }
}
