﻿using Firebase.Database;
using Firebase.Database.Query;
using Microsoft.Extensions.Options;
using Standard.Framework.Data.Abstractions;
using Standard.Framework.Data.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Standard.Framework.Data.Concretes
{
    public class FirebaseRepository<T> : IRepository<T> where T : class
    {
        //https://github.com/step-up-labs/firebase-authentication-dotnet

        protected FirebaseClient Firebase { get; }
        protected IOptions<FirebaseClientOptions> ClientOptions { get; }

        public FirebaseRepository(IOptions<FirebaseClientOptions> clientOptions)
        {
            ClientOptions = clientOptions;
            Firebase = new FirebaseClient(clientOptions.Value.Uri, clientOptions.Value);
        }

        public async Task DeleteAsync(T model)
        {
            await Firebase.Child(ClientOptions.Value.Child).DeleteAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Func<T, bool> predicate)
        {
            Func<FirebaseObject<T>, bool> firebasePredicate = (firebaseObject) => predicate(firebaseObject.Object);
            IReadOnlyCollection<FirebaseObject<T>> collection = await Firebase.Child(ClientOptions.Value.Child).OnceAsync<T>();

            return collection.Where(firebasePredicate).Select(it => it.Object);
        }

        public async Task InsertAsync(T model)
        {
            await Firebase.Child(ClientOptions.Value.Child).PostAsync(model);
        }

        public async Task UpdateAsync(T model)
        {
            await Firebase.Child(ClientOptions.Value.Child).PutAsync(model);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            IReadOnlyCollection<FirebaseObject<T>> collection = await Firebase.Child(ClientOptions.Value.Child).OnceAsync<T>();
            return collection.Select(it => it.Object);
        }
    }
}
