 using BackEnd.Data;
using ConferenceDTO;
using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Threading.Tasks;

 namespace BackEnd.Data
 {
     public static class EntityExtensions
     {
         public static SessionResponse MapSessionResponse(this Session session)=>
            new SessionResponse
            {
                 ID = session.ID,
                 Title = session.Title,
                 StartTime = session.StartTime, 
                 EndTime = session.EndTime,
                 Tags = session.SessionTags?
                    .Select(st=>new ConferenceDTO.Tag{
                        ID = st.TagID,
                        Name = st.Tag.Name
                    }).ToList(),
                Speakers = session.SessionSpeakers?
                .Select(sp=>new ConferenceDTO.Speaker{
                    ID = sp.SpeakerId,
                    Name = sp.Speaker.Name
                }).ToList(),
                TrackId = session.TrackId,
                Track = new ConferenceDTO.Track{
                    TrackID = session?.TrackId??0,
                    Name = session.Track?.Name
                },
                ConferenceID = session.ConferenceID,
                Abstract = session.Abstract
                };


        
            public static SpeakerResponse MapSpeakerResponse(this Speaker speaker) =>
                new SpeakerResponse
                {
                    ID = speaker.ID,
                    Name = speaker.Name,
                    Bio = speaker.Bio,
                    WebSite = speaker.WebSite,
                    Sessions = speaker.SessionSpeakers?
                        .Select(ss =>
                            new ConferenceDTO.Session
                            {
                                ID = ss.SessionId,
                                Title = ss.Session.Title
                            })
                        .ToList()
                };
                public static AttendeeResponse MapAttendeeResponse(this Attendee attendee)=>
                new AttendeeResponse{
                     ID = attendee.ID,
                     FirstName = attendee.FirstName,
                     LastName = attendee.LastName,
                     UserName = attendee.UserName,
                     Sessions = attendee.SessionsAttendees?
                     .Select(att=> new ConferenceDTO.Session{
                         ID = att.AttendeeID,
                         Title = att.Session.Title,
                         StartTime = att.Session.StartTime,
                         EndTime = att.Session.EndTime
                     }).ToList(),
                     Conferences = attendee.ConferenceAttendees?
                     .Select(att=>new ConferenceDTO.Conference{
                          ID = att.ConferenceID,
                          Name = att.Conference.Name
                     }).ToList()
                };
     }
 }