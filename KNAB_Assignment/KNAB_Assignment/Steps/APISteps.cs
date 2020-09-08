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
        private IRestResponse<List<boards>> boardsResponse = null;

        public RestClient SetupClient()
        {
            // Setup the Client. 
            RestClient client = new RestClient("https://api.trello.com/1");
            return client;
        }

        public RestRequest SetupRequest(string url, Method method)
        {
            // Setup the Request.
            // The added parameters are credentials that are needed to reach the specific boards on the account.
            // The token and key can be acquired at https://trello.com/app-key/ 

            RestRequest request = new RestRequest(url);
            request.Method = method;
            request.AddParameter("key", "30a68f8535bce715ac7384462cbd60c7", ParameterType.QueryString);
            request.AddParameter("token", "b34aa729168e10b148b1b38f6e55c58104efef5227866ef8170948c8e43cad8b", ParameterType.QueryString);

            return request;
        }


        [When(@"I send an API request to Trello for all boards")]
        public void GivenISendAnAPIRequestToTrelloForAllBoards()
        {
            RestClient client = SetupClient();
            RestRequest request = SetupRequest("/members/me/boards?fields=name,url", Method.GET);
            // For now this is a set url. this will be a variable in a later step.
            boardsResponse = client.Execute<List<boards>>(request);
            Assert.AreEqual("OK", boardsResponse.StatusDescription, "Status code is not OK");
        }

        [When(@"I create a new board with name ""(.*)"" using the API")]
        public void WhenICreateANewBoardWithNameUsingTheAPI(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I delete a board with name ""(.*)"" using the API")]
        public void WhenIDeleteABoardWithNameUsingTheAPI(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the board with name ""(.*)"" is in the API list")]
        public void ThenTheBoardWithNameIsInTheAPIList(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the board with name ""(.*)"" is not in the API list")]
        public void ThenTheBoardWithNameIsNotInTheAPIList(string p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The API response will contain the following board: ""(.*)""")]
        public void TheAPIResponseWillContainTheFollowingBoard(string boardname)
        {
            Assert.AreEqual(boardname, boardsResponse.Data[0].name, "Expected results is " + boardname + ", but result was " + boardsResponse.Data[0].name);
        }


    }
}
