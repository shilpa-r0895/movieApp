using System;
using System.Collections.Generic;

namespace MovieApp.Helpers.Interfaces
{
    public interface IPersonHelper
    {
        string AddActor(Model.RequestModel.AddPerson actor);

        string AddProducer(Model.RequestModel.AddPerson producer);

        string DeletePerson(Guid personId, PersonType personType);

        string EditPerson(Model.RequestModel.Person person, PersonType personType);

        Model.ClientModel.Person GetPerson(Guid personId);

        List<Model.ClientModel.Person> GetAllActors();

        List<Model.ClientModel.Person> GetAllProducers();
    }
}