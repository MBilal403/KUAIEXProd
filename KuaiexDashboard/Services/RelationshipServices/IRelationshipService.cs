using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KuaiexDashboard.Services.RelationshipServices
{
    public interface IRelationshipService
    {
        string AddRelationship(Relationship_Lookup objRelationship);
        List<Relationship_Lookup> GetActiveRelationships();
        string UpdateRelationship(Relationship_Lookup objRelationship);
        Relationship_Lookup GetRelatioshipById(int Id);
    }
}