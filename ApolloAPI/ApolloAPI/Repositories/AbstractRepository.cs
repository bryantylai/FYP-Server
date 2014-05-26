﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Models;

namespace ApolloAPI.Repositories
{
    public abstract class AbstractRepository
    {
        protected ApolloDatabaseEntities dbEntities;

        public AbstractRepository()
        {
            dbEntities = new ApolloDatabaseEntities();
        }

        internal bool SaveUpdate()
        {
            return dbEntities.SaveChanges() != 0;
        }

        internal bool UpdateLogin(Guid userId, DateTime now)
        {
            Credential credential = dbEntities.Credentials.Single((c) => c.PersonId == userId);
            credential.LastLogin = now;
            return SaveUpdate();
        }

        internal Address GetAddressByAddressId(Guid addressId)
        {
            return dbEntities.Addresses.Single((a) => a.Id == addressId);
        }
    }
}