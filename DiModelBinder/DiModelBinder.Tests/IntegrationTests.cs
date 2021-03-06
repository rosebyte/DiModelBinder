﻿using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DiModelBinder.IntegrationTests;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json.Linq;
using Xunit;

namespace DiModelBinder.Tests
{
	public class IntegrationTests
	{
		// ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
		private readonly TestServer _server;
		private readonly HttpClient _client;

		public IntegrationTests()
		{
			_server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
			_client = _server.CreateClient();
		}

		[Fact]
		public async Task ShouldPostApiValue()
		{
			const int id = 123;
			const bool readOnly = true;
			var created = DateTime.Now.ToString("MM/dd/yyyy");

			var json = new JObject {{"readOnly", readOnly }};
			var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
			var response = await _client.PostAsync($"/api/values/{id}?Created={created}", content);
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();
			var expected = $"\"ID: {id} | Created: {created} | ReadOnly {readOnly}\"";
			Assert.Equal(expected, responseString);
		}

		[Fact]
		public async Task ShouldGetApiValue()
		{
			const int id = 123;
			var created = DateTime.Now.ToString("MM/dd/yyyy");
			var userName = "TestName";

			var request = new HttpRequestMessage(
				HttpMethod.Get, 
				$"/api/values/{id}?Created={created}");
			request.Headers.Add("UserName", userName);

			var response = await _client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();
			var expected = $"\"ID: {id} | Created: {created} | UserName: {userName}\"";
			Assert.Equal(expected, responseString);
		}

		[Fact]
		public async Task ShouldGetApiValueWithParamAttribute()
		{
			const int id = 123;
			var userName = "TestName";

			var request = new HttpRequestMessage(HttpMethod.Get, $"/api/values/{id}");
			request.Headers.Add("UserName", userName);
			
			var response = await _client.SendAsync(request);
			response.EnsureSuccessStatusCode();
			var responseString = await response.Content.ReadAsStringAsync();
			var expected = $"\"ID: {id} | Created: 01.01.0001 | UserName: {userName}\"";
			Assert.Equal(expected, responseString);
		}
	}
}
