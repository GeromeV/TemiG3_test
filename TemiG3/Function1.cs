using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using TemiG3.Models;
using Microsoft.Azure.Cosmos;

namespace TemiG3
{
    public static class Function1
    {
        [FunctionName("GetReservation")]
        public static async Task<IActionResult> GetReservation(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;

            //connect to database
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            //get container
            Container container = client.GetContainer("TemiG3", "reservations");

            //Creating query
            QueryDefinition query = new QueryDefinition("select * from reservations");

            //Creating list to put items in that will be returned
            List<Reservation> items = new List<Reservation>();
            using (FeedIterator<Reservation> resultSet = container.GetItemQueryIterator<Reservation>(
                queryDefinition: query))
            {
                while (resultSet.HasMoreResults)
                {
                    //Get the items and put them in the list
                    FeedResponse<Reservation> response = await resultSet.ReadNextAsync();
                    items.AddRange(response);
                }
            }
            //return the list
            return new OkObjectResult(items);
            
        }

        [FunctionName("AddReservation")]
        public static async Task<IActionResult> AddReservation(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            string json = await new StreamReader(req.Body).ReadToEndAsync();

            //Cast json to the required object
            Reservation request = JsonConvert.DeserializeObject<Reservation>(json);
            //MANDATORY property has to be created called id
            request.Id = Guid.NewGuid().ToString();
            request.ReservationId = Guid.NewGuid().ToString();

            //Create Cosmos client
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;
            //connect to database
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            //get container in a database
            Container container = client.GetContainer("TemiG3", "reservations");
            //Get the response
            ItemResponse<Reservation> response = await container.CreateItemAsync(request, new PartitionKey(request.ReservationId));

            return new OkObjectResult(response);

        }

        [FunctionName("GetReservationByName")]
        public static async Task<IActionResult> GetReservationByName(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetReservationByName/{name}")] HttpRequest req,
           ILogger log, string name)
        {
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;

            //connect to database
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            //get container
            Container container = client.GetContainer("TemiG3", "reservations");

            //Creating query
            QueryDefinition query = new QueryDefinition("SELECT * FROM reservations r where r.lastName = @name").WithParameter("@name", name);

            //Creating list to put items in that will be returned
            List<Reservation> items = new List<Reservation>();
            using (FeedIterator<Reservation> resultSet = container.GetItemQueryIterator<Reservation>(
                queryDefinition: query))
            {
                while (resultSet.HasMoreResults)
                {
                    //Get the items and put them in the list
                    FeedResponse<Reservation> response = await resultSet.ReadNextAsync();
                    items.AddRange(response);
                }
            }
            //return the list
            return new OkObjectResult(items);

        }

        [FunctionName("GetReservationByDate")]
        public static async Task<IActionResult> GetReservationByDate(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetReservationByDate/{date}")] HttpRequest req,
           ILogger log, string date)
        {
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;

            //connect to database
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            //get container
            Container container = client.GetContainer("TemiG3", "reservations");

            //Creating query
            QueryDefinition query = new QueryDefinition("SELECT * FROM reservations r where r.arrivalDate = @date").WithParameter("@date", date);

            //Creating list to put items in that will be returned
            List<Reservation> items = new List<Reservation>();
            using (FeedIterator<Reservation> resultSet = container.GetItemQueryIterator<Reservation>(
                queryDefinition: query))
            {
                while (resultSet.HasMoreResults)
                {
                    //Get the items and put them in the list
                    FeedResponse<Reservation> response = await resultSet.ReadNextAsync();
                    items.AddRange(response);
                }
            }
            //return the list
            return new OkObjectResult(items);

        }

        [FunctionName("GetReservationById")]
        public static async Task<IActionResult> GetReservationById(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetReservationById/{id}")] HttpRequest req,
           ILogger log, string id)
        {
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;

            //connect to database
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            //get container
            Container container = client.GetContainer("TemiG3", "reservations");

            //Creating query
            QueryDefinition query = new QueryDefinition("SELECT * FROM reservations r where r.id = @id").WithParameter("@id", id);

            //Creating list to put items in that will be returned
            List<Reservation> items = new List<Reservation>();
            using (FeedIterator<Reservation> resultSet = container.GetItemQueryIterator<Reservation>(
                queryDefinition: query))
            {
                while (resultSet.HasMoreResults)
                {
                    //Get the items and put them in the list
                    FeedResponse<Reservation> response = await resultSet.ReadNextAsync();
                    items.AddRange(response);
                }
            }
            //return the list
            return new OkObjectResult(items);

        }


        [FunctionName("DeleteReservationByName")]
        public static async Task<IActionResult> DeleteReservationByName(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteReservationByName/{name}")] HttpRequest req,
            ILogger log,string name)
        {
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;

            //connect to database
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            //get container
            Container container = client.GetContainer("TemiG3", "reservations");

            //Creating query
            QueryDefinition query = new QueryDefinition("SELECT * FROM reservations r where r.lastName = @name").WithParameter("@name", name);

            //Creating list to put items in that will be returned
            List<Reservation> items = new List<Reservation>();
            using (FeedIterator<Reservation> resultSet = container.GetItemQueryIterator<Reservation>(
                queryDefinition: query))
            {
                while (resultSet.HasMoreResults)
                {
                    //Get the items and put them in the list
                    FeedResponse<Reservation> response = await resultSet.ReadNextAsync();
                    items.AddRange(response);
                    
                }
            }
            foreach (var item in items)
            {
                await container.DeleteItemAsync<Reservation>(item.Id, new PartitionKey(item.ReservationId));
            }
            //return the list
            return new OkObjectResult(items);

        }


        [FunctionName("DeleteReservationById")]
        public static async Task<IActionResult> DeleteReservationById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "DeleteReservationById/{id}")] HttpRequest req,
            ILogger log, string id)
        {
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;

            //connect to database
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            //get container
            Container container = client.GetContainer("TemiG3", "reservations");

            //Creating query
            QueryDefinition query = new QueryDefinition("SELECT * FROM reservations r where r.id = @id").WithParameter("@id", id);

            //Creating list to put items in that will be returned
            List<Reservation> items = new List<Reservation>();
            using (FeedIterator<Reservation> resultSet = container.GetItemQueryIterator<Reservation>(
                queryDefinition: query))
            {
                while (resultSet.HasMoreResults)
                {
                    //Get the items and put them in the list
                    FeedResponse<Reservation> response = await resultSet.ReadNextAsync();
                    items.AddRange(response);

                }
            }
            foreach (var item in items)
            {
                await container.DeleteItemAsync<Reservation>(item.Id, new PartitionKey(item.ReservationId));
            }
            //return the list
            return new OkObjectResult(items);

        }


        [FunctionName("UpdateReservationByName")]
        public static async Task<IActionResult> UpdateReservationByName(
           [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateReservationByName/{name}")] HttpRequest req, string name,
           ILogger log)
        {
            string json = await new StreamReader(req.Body).ReadToEndAsync();

            //Cast json to the required object
            Reservation request = JsonConvert.DeserializeObject<Reservation>(json);
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;

            //connect to database
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            //get container
            Container container = client.GetContainer("TemiG3", "reservations");

            //Creating query
            QueryDefinition query = new QueryDefinition("SELECT * FROM reservations r where r.lastName = @name").WithParameter("@name", name);

            //Creating list to put items in that will be returned
            Reservation items = null;
            using (FeedIterator<Reservation> resultSet = container.GetItemQueryIterator<Reservation>(queryDefinition: query))
            {
                while (resultSet.HasMoreResults)
                {
                    //Get the items and put them in the list
                    FeedResponse<Reservation> response = await resultSet.ReadNextAsync();
                    foreach (var ev in response)
                    {
                        items = ev;
                        break;
                    }

                }
            }
            items.ArrivalTime = request.ArrivalTime;
            await container.ReplaceItemAsync<Reservation>(items, items.Id.ToString());

            //return the list
            return new OkObjectResult(items);
        }


        [FunctionName("UpdateReservationById")]
        public static async Task<IActionResult> UpdateReservationById(
          [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "UpdateReservationById/{id}")] HttpRequest req, string id,
          ILogger log)
        {
            string json = await new StreamReader(req.Body).ReadToEndAsync();

            //Cast json to the required object
            Reservation request = JsonConvert.DeserializeObject<Reservation>(json);
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;

            //connect to database
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            //get container
            Container container = client.GetContainer("TemiG3", "reservations");

            //Creating query
            QueryDefinition query = new QueryDefinition("SELECT * FROM reservations r where r.id = @id").WithParameter("@id", id);

            //Creating list to put items in that will be returned
            Reservation items = null;
            using (FeedIterator<Reservation> resultSet = container.GetItemQueryIterator<Reservation>(queryDefinition: query))
            {
                while (resultSet.HasMoreResults)
                {
                    //Get the items and put them in the list
                    FeedResponse<Reservation> response = await resultSet.ReadNextAsync();
                    foreach (var ev in response)
                    {
                        items = ev;
                        break;
                    }

                }
            }
            items.ArrivalTime = request.ArrivalTime;
            await container.ReplaceItemAsync<Reservation>(items, items.Id.ToString());

            //return the list
            return new OkObjectResult(items);
        }


        //log in
        [FunctionName("GetUsersByEmailAndPassword")]
        public static async Task<IActionResult> GetUsers(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "GetUsersByEmailAndPassword/{email}/{password}")] HttpRequest req,string email, string password,
           ILogger log)
        {
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;

            //connect to database
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            //get container
            Container container = client.GetContainer("TemiG3", "Users");

            //Creating query
            QueryDefinition query = new QueryDefinition("SELECT * FROM Users u where u.email = @email and u.password = @password ").WithParameter("@email", email).WithParameter("@password",password);

            //Creating list to put items in that will be returned
            List<Users> items = new List<Users>();
            using (FeedIterator<Users> resultSet = container.GetItemQueryIterator<Users>(
                queryDefinition: query))
            {
                while (resultSet.HasMoreResults)
                {
                    //Get the items and put them in the list
                    FeedResponse<Users> response = await resultSet.ReadNextAsync();
                    items.AddRange(response);
                }
            }
            //return the list
            return new OkObjectResult(items);

        }

        [FunctionName("AddUsers")]
        public static async Task<IActionResult> AddUsers(
           [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
           ILogger log)
        {
            string json = await new StreamReader(req.Body).ReadToEndAsync();

            //Cast json to the required object
            Users request = JsonConvert.DeserializeObject<Users>(json);
            //MANDATORY property has to be created called id
            request.Id = Guid.NewGuid().ToString();
            request.UserId = Guid.NewGuid().ToString();
            //Create Cosmos client
            CosmosClientOptions options = new CosmosClientOptions();
            options.ConnectionMode = ConnectionMode.Gateway;
            //connect to database
            CosmosClient client = new CosmosClient(Environment.GetEnvironmentVariable("cosmos"), options);
            //get container in a database
            Container container = client.GetContainer("TemiG3", "Users");
            //Get the response
            ItemResponse<Users> response = await container.CreateItemAsync(request, new PartitionKey(request.UserId));

            return new OkObjectResult(response);

        }
    }

}
