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
            // arrange
            RestClient client = new RestClient(url);
            RestRequest getRequest = new RestRequest(Method.GET);
            getRequest.AddParameter("key", "30a68f8535bce715ac7384462cbd60c7", ParameterType.QueryString);
            getRequest.AddParameter("token", "b34aa729168e10b148b1b38f6e55c58104efef5227866ef8170948c8e43cad8b", ParameterType.QueryString);

            // act
            restResponse = client.Execute(getRequest);


        }

        [Given(@"I send an API request to Trello for all boards")]
        public void GivenISendAnAPIRequestToTrelloForAllBoards()
        {
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
