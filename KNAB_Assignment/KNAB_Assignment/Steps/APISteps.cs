using Dynamitey.DynamicObjects; 
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using KNAB_Assignment.API_Responses;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace KNAB_Assignment.Steps
{
    [Binding]
    public sealed class APISteps
    {
        private IRestResponse<List<boards>> restResponse = null;

        public void SimpleRequest(string url, Method method)
        {
            // Setup the Client.
            // The added parameters are credentials that are needed to reach the specific boards on the account.
            // The token and key can be acquired at https://trello.com/app-key/ 

            RestClient client = new RestClient("https://api.trello.com/1/members/me/");
            var request = new RestRequest(url);
            request.Method = method;
            request.AddParameter("key", "30a68f8535bce715ac7384462cbd60c7", ParameterType.QueryString);
            request.AddParameter("token", "b34aa729168e10b148b1b38f6e55c58104efef5227866ef8170948c8e43cad8b", ParameterType.QueryString);

            // Execute the created request
            restResponse = client.Execute<List<boards>>(request);
            //var responseJSON = JsonConvert.DeserializeObject<boards>(restResponse.Content);
        }


        [Given(@"I send an API request to Trello for all boards")]
        public void GivenISendAnAPIRequestToTrelloForAllBoards()
        {
            // For now this is a set url. this will be a variable in a later step.
            SimpleRequest("boards?fields=name,url", Method.GET);
        }


        [Then(@"The API response will contain the following board: ""(.*)""")]
        public void TheAPIResponseWillContainTheFollowingBoard(string boardname)
        {
            Assert.AreEqual(boardname, restResponse.Data[0].name, "Expected results is " + boardname + ", but result was " + restResponse.Data[0].name);
        }


    }
}
