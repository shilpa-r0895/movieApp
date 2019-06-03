using MovieApp.Helpers.Interfaces;
using MovieApp.Model.RequestModel;
using MovieApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace MovieApp.Helpers
{
    public class PersonHelper : IPersonHelper
    {
        private readonly IPersonRepository _personRepository;

        public PersonHelper(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        private string ValidatePersonForAdd(string name, string sex, string bio, string dob)
        {
            if (!Utils.IsStringValid(name))
            {
                return ErrorMessages.NAME_EMPTY;
            }

            if (!Utils.IsStringValid(sex))
            {
                return ErrorMessages.SEX_EMPTY;
            }
            else
            {
                if (!(sex.ToLower().Equals("male") || sex.ToLower().Equals("female")))
                {
                    return ErrorMessages.SEX_INVALID;
                }
            }

            if (!Utils.IsStringValid(bio))
            {
                return ErrorMessages.BIO_EMPTY;
            }
            
            if(dob == null)
            {
                return ErrorMessages.DOB_EMPTY;
            }

            return string.Empty;
        }

        public string AddActor(AddPerson actor)
        {
            var checkExists = _personRepository.GetPerson(actor.Name, PersonType.Actor);
            if (checkExists != null)
            {
                return ErrorMessages.ACTOR_ALREADY_EXISTS;
            }

            var result = ValidatePersonForAdd(actor.Name, actor.Sex, actor.Bio, actor.DOB);
            if (string.IsNullOrEmpty(result))
            {
                var newActor = _personRepository.AddActor(actor);
                if (newActor != null)
                {
                    return newActor.Id.ToString();
                }
                else
                {
                    return ErrorMessages.SERVER_ERROR;
                }
            }
            else
            {
                return result;
            }
        }

        public string AddProducer(AddPerson producer)
        {
            var checkExists = _personRepository.GetPerson(producer.Name, PersonType.Producer);
            if (checkExists != null)
            {
                return ErrorMessages.PRODUCER_ALREADY_EXISTS;
            }

            var result = ValidatePersonForAdd(producer.Name, producer.Sex, producer.Bio, producer.DOB);
            if (string.IsNullOrEmpty(result))
            {
                var newProducer = _personRepository.AddProducer(producer);
                if (newProducer != null)
                {
                    return newProducer.Id.ToString();
                }
                else
                {
                    return ErrorMessages.SERVER_ERROR;
                }
            }
            else
            {
                return result;
            }
        }

        public string DeletePerson(Guid personId, PersonType personType)
        {
            var checkExists = _personRepository.GetPerson(personId);
            if (checkExists == null)
            {
                return personType == PersonType.Actor ? ErrorMessages.ACTOR_NOT_FOUND : ErrorMessages.PRODUCER_NOT_FOUND;
            }
            else
            {
                if (_personRepository.DeletePerson(personId))
                {
                    return string.Empty;
                }
                else
                {
                    return personType == PersonType.Actor ? ErrorMessages.ACTOR_MAPPED : ErrorMessages.PRODUCER_MAPPED;
                }
            }
        }

        public string EditPerson(Model.RequestModel.Person person, PersonType personType)
        {
            var checkExists = _personRepository.GetPerson(person.Id);
            if (checkExists == null)
            {
                return personType == PersonType.Actor? ErrorMessages.ACTOR_NOT_FOUND : ErrorMessages.PRODUCER_NOT_FOUND;
            }

            var result = ValidatePersonForEdit(person.Name, person.Sex, person.Bio, person.DOB);
            if (string.IsNullOrEmpty(result))
            {
                var edited = _personRepository.EditPerson(person.Id, person);
                if (edited != null)
                {
                    return edited.Id.ToString();
                }
                else
                {
                    return ErrorMessages.SERVER_ERROR;
                }
            }
            else
            {
                return result;
            }
        }

        Model.ClientModel.Person IPersonHelper.GetPerson(Guid personId)
        {
            return AutoMapper.Mapper.Map<Model.ClientModel.Person>(_personRepository.GetPerson(personId));
        }

        List<Model.ClientModel.Person> IPersonHelper.GetAllActors()
        {
            var allActors = _personRepository.GetAllActors();
            List<Model.ClientModel.Person> result = new List<Model.ClientModel.Person>();
            foreach (Entity.Person actor in allActors)
            {
                var actorObj = AutoMapper.Mapper.Map<Model.ClientModel.Person>(actor);
                result.Add(actorObj);
            }
            return result;
        }

        List<Model.ClientModel.Person> IPersonHelper.GetAllProducers()
        {
            var allProducers = _personRepository.GetAllProducers();
            List<Model.ClientModel.Person> result = new List<Model.ClientModel.Person>();
            foreach (Entity.Person producer in allProducers)
            {
                var producerObj = AutoMapper.Mapper.Map<Model.ClientModel.Person>(producer);
                result.Add(producerObj);
            }
            return result;
        }


        private string ValidatePersonForEdit(string name, string sex, string bio, string dob)
        {
            if (name != null)
            {
                if(Utils.IsStringEmpty(name))
                    return ErrorMessages.NAME_EMPTY;
            }

            if (sex != null)
            {
                if (!(sex.ToLower().Equals("male") || sex.ToLower().Equals("female")))
                {
                    return ErrorMessages.SEX_INVALID;
                }
            }

            if (bio != null)
            {
                if(Utils.IsStringEmpty(bio))
                    return ErrorMessages.BIO_EMPTY;
            }

            return string.Empty;
        }
    }
}
