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
        private IRestResponse<List<boards>> boardsCreateResponse = null;
        private string boardID = null;

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
        public void WhenISendAnAPIRequestToTrelloForAllBoards()
        {
            RestClient client = SetupClient();
            RestRequest request = SetupRequest("/members/me/boards?fields=name,url", Method.GET);
            boardsResponse = client.Execute<List<boards>>(request);
            Assert.AreEqual("OK", boardsResponse.StatusDescription, "Status code is not OK");
        }

        [When(@"I create a new board with name ""(.*)"" using the API")]
        public void WhenICreateANewBoardWithNameUsingTheAPI(string boardname)
        {
            RestClient client = SetupClient();
            RestRequest request = SetupRequest("/boards", Method.POST);
            request.AddParameter("name", boardname);
            boardsCreateResponse = client.Execute<List<boards>>(request);
            Assert.AreEqual("OK", boardsCreateResponse.StatusDescription, "Status code is not OK");
            Assert.AreEqual(boardname, boardsCreateResponse.Data[0].name, "Boardname is not created or correct");
            boardID = boardsCreateResponse.Data[0].id;
        }

        [When(@"I delete a board with name ""(.*)"" using the API")]
        public void WhenIDeleteABoardWithNameUsingTheAPI(string boardname)
        {
            RestClient client = SetupClient();
            RestRequest request = SetupRequest("/boards/" + boardID, Method.DELETE);
            request.AddParameter("id", boardID);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual("OK", response.StatusDescription, "Status code is not OK");
        }

        [Then(@"the board with name ""(.*)"" is in the API list")]
        public void ThenTheBoardWithNameIsInTheAPIList(string boardname)
        {
            RestClient client = SetupClient();
            RestRequest request = SetupRequest("/members/me/boards?fields=name,url", Method.GET);
            boardsResponse = client.Execute<List<boards>>(request);
            Assert.AreEqual("OK", boardsResponse.StatusDescription, "Status code is not OK");
            int i;
            for (i = 0; i < boardsResponse.Data.Count; i++)
            {
                if (boardID == boardsResponse.Data[i].id) { break; }
            }
            Assert.AreEqual(boardID, boardsResponse.Data[i].id, "Expected results is " + boardname + ", but result was " + boardsResponse.Data[i].name);           
        }

        [Then(@"the board with name ""(.*)"" is not in the API list")]
        public void ThenTheBoardWithNameIsNotInTheAPIList(string boardname)
        {
            //to be sure you dont have the response from last time
            boardsResponse = null;
            RestClient client = SetupClient();
            RestRequest request = SetupRequest("/members/me/boards?fields=name,url", Method.GET);
            boardsResponse = client.Execute<List<boards>>(request);
            Assert.AreEqual("OK", boardsResponse.StatusDescription, "Status code is not OK");
            int i=0;
            while  (boardname!= boardsResponse.Data[i].name && i < boardsResponse.Data.Count)
            {
                if (boardname == boardsResponse.Data[i].name) { break; }
                else if ((i+1)==boardsResponse.Data.Count) { break; }
                else { i++; }
            }
            Assert.AreNotEqual(boardname, boardsResponse.Data[i].name, "Expected results is you dont find " + boardname + ", but result was " + boardsResponse.Data[i].name);

        }

        [Then(@"The API response will contain the following board: ""(.*)""")]
        public void TheAPIResponseWillContainTheFollowingBoard(string boardname)
        {
            Assert.AreEqual(boardname, boardsResponse.Data[0].name, "Expected results is " + boardname + ", but result was " + boardsResponse.Data[0].name);
        }


    }
}
