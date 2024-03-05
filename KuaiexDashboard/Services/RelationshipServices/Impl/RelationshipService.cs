using DataAccessLayer.Entities;
using DataAccessLayer.Helpers;
using DataAccessLayer.Recources;
using DataAccessLayer.Repository.Impl;
using KuaiexDashboard.Repository;
using KuaiexDashboard.Repository.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Services.RelationshipServices.Impl
{
    public class RelationshipService : IRelationshipService
    {
        private readonly IRepository<Relationship_Lookup> _relationshipRepository;
        public RelationshipService()
        {
            _relationshipRepository = new GenericRepository<Relationship_Lookup>();
        }
        public string AddRelationship(Relationship_Lookup objRelationship)
        {
            try
            {
                Relationship_Lookup existingRelationship = GetRelatioshipByName(objRelationship.Name);
                if (existingRelationship != null)
                {
                    return MsgKeys.DuplicateValueExist;
                }
                else
                {
                    objRelationship.Status = objRelationship.Status ?? 0;
                    if (_relationshipRepository.Insert(objRelationship) > 0)
                    {
                        return MsgKeys.CreatedSuccessfully;
                    }
                }
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
            return MsgKeys.Error;
        }

        public List<Relationship_Lookup> GetActiveRelationships()
        {
            try
            {
                List<Relationship_Lookup> list = _relationshipRepository.GetAll(null ,null);
                return list;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }

        }

        public string UpdateRelationship(Relationship_Lookup objRelationship)
        {
            try
            {
                Relationship_Lookup existingRelationship = GetRelatioshipById(objRelationship.Relationship_Id);
                if (existingRelationship != null)
                {
                    existingRelationship.Status = objRelationship.Status ?? 0 ;
                    _relationshipRepository.Update(existingRelationship, $" Relationship_Id = '{objRelationship.Relationship_Id}' ");
                    return MsgKeys.UpdatedSuccessfully;
                }

                return MsgKeys.Error;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }

        }

        public Relationship_Lookup GetRelatioshipById(int Id)
        {
            try
            {
                Relationship_Lookup relationship_Lookup = _relationshipRepository.FindBy(x => x.Relationship_Id == Id);
                return relationship_Lookup;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }
        
        public Relationship_Lookup GetRelatioshipByName(string Name)
        {
            try
            {
                Relationship_Lookup relationship_Lookup = _relationshipRepository.FindBy(x => x.Name == Name);
                return relationship_Lookup;
            }
            catch (Exception ex)
            {
                // throw the exception to propagate it up the call stack
                throw new Exception(MsgKeys.SomethingWentWrong, ex);
            }
        }



    }
}