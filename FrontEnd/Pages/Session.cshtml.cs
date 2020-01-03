using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontEnd.Services;
using ConferenceDTO;
using FrontEnd.Services;
using System.Runtime;

namespace FrontEnd.Pages
{
    public class SessionModel : PageModel
    {
            private readonly IApiClient _apiClient;

            public SessionModel(IApiClient apiClient)
            {
                _apiClient = apiClient;
            }

            public SessionResponse Session { get; set; }

            public int? DayOffset { get; set; }
            public async Task<IActionResult> OnGetAsync(int id)
            {
                Session = await _apiClient.GetSessionAsync(id);

                if (Session == null)
                {
                    return RedirectToPage("/Index");
                }

                var allSessions = await _apiClient.GetSessionsAsync();

                var startDate = allSessions.Min(s => s.StartTime?.Date);

                DayOffset = Session.StartTime?.Subtract(startDate ?? DateTimeOffset.MinValue).Days;

                return Page();
            }
    }
}
