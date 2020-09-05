using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;

namespace KNAB_Assignment.Steps
{
    [Binding]
    public sealed class APISteps
    {
        IRestResponse restResponse = null;
        public void SimpleRequest(string url)
        {
            // Setup the Client.
            // The added parameters are credentials that are needed to reach the specific boards on the account.
            // The token and key can be acquired at https://trello.com/app-key/ 

            RestClient client = new RestClient(url);
            RestRequest getRequest = new RestRequest(Method.GET);
            getRequest.AddParameter("key", "30a68f8535bce715ac7384462cbd60c7", ParameterType.QueryString);
            getRequest.AddParameter("token", "b34aa729168e10b148b1b38f6e55c58104efef5227866ef8170948c8e43cad8b", ParameterType.QueryString);

            // Execute the created request
            restResponse = client.Execute(getRequest);


        }

        [Given(@"I send an API request to Trello for all boards")]
        public void GivenISendAnAPIRequestToTrelloForAllBoards()
        {
            // For now this is a set url. this will be a variable in a later step.
            SimpleRequest("https://api.trello.com/1/members/me/boards");
        }

        [Then(@"It will find the board ""(.*)""")]
        public void ThenItWillFindTheBoard(string boardName)
        {
            // assert if the boardname is in the response.
            string response = restResponse.Content;
            Assert.That(response.Contains(boardName));
        }

    }
}
