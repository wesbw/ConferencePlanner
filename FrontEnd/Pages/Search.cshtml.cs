using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ConferenceDTO;
using FrontEnd.Services;

namespace FrontEnd.Pages
{
    public class SearchModel : PageModel
    {
         private readonly IApiClient _apiClient;

        public SearchModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public string Term { get; set; }

        public List<SearchResult> SearchResults { get; set; }
        public async Task OnGetAsync(string term)
        {
            Term = term;
            SearchResults = await _apiClient.SearchAsync(term);
        }
    }
}
