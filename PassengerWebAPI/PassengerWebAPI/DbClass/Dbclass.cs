using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using PassengerWebAPI.Models;

namespace PassengerWebAPI.DbClass
{
    public class Dbclass
    {
        public string GetDate(DateTime date)
        {
            if (date == null)
            {
                return string.Empty;
            }
            return String.Format("{0:yyyy-MM-dd}", date);
        }

        public int RegisterEvent(RegEvent registerEvent)
        {
            int eventId = 0;

            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
                {
                    var cmd = "INSERT INTO RegEvent (Client,ProcessType,Location,DepartDate,ArrivalDate,City,LocationID,Source)VALUES (@Client,@ProcessType,@Location,@DepartDate,@ArrivalDate,@City,@LocationID,@Source);SELECT CAST(scope_identity() AS int)";
                    using (var insertCommand = new SqlCommand(cmd, con))
                    {
                        insertCommand.Parameters.AddWithValue("@Client", registerEvent.Client);
                        insertCommand.Parameters.AddWithValue("@ProcessType", registerEvent.ProcessType);
                        insertCommand.Parameters.AddWithValue("@Location", registerEvent.Location);
                        insertCommand.Parameters.AddWithValue("@DepartDate", GetDate(registerEvent.DepartDate));
                        insertCommand.Parameters.AddWithValue("@ArrivalDate", GetDate(registerEvent.ArrivalDate));
                        insertCommand.Parameters.AddWithValue("@City", registerEvent.City);
                        insertCommand.Parameters.AddWithValue("@LocationID", registerEvent.LocationID);
                        insertCommand.Parameters.AddWithValue("@Source", registerEvent.Source);
                        con.Open();
                        eventId = (int)insertCommand.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return eventId;
        }

        public bool RegisterGuest(RegGuest registerGuest)
        {
            int eventGuesId = 0;

            try
            {
                if (registerGuest.Segments.Count > 0)
                {
                    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
                    {
                        var cmd = "INSERT INTO RegGuest" +
                            "(Client,ProcessType,EventId,FirstName,LastName,MiddleName,EmailAddress,Phone,NumberOfBags,Status,Gender,BirthDate,PassportCitizenship,PassportNumber,PassportExpiration,PassportCountry,Opt1,FirstNameAlias,LastNameAlias,Eligible,PartyGroupID,ArrivalDate,DepartDate,DepartGroup,GuestType,Segments)"
                            + "VALUES (@Client,@ProcessType,@EventId,@FirstName,@LastName,@MiddleName,@EmailAddress,@Phone,@NumberOfBags,@Status,@Gender,@BirthDate,@PassportCitizenship,@PassportNumber,@PassportExpiration,@PassportCountry,@Opt1,@FirstNameAlias,@LastNameAlias,@Eligible,@PartyGroupID,@ArrivalDate,@DepartDate,@DepartGroup,@GuestType,@Segments);SELECT CAST(scope_identity() AS int)";
                        using (var insertCommand = new SqlCommand(cmd, con))
                        {
                            insertCommand.Parameters.AddWithValue("@Client", registerGuest.Client);
                            insertCommand.Parameters.AddWithValue("@ProcessType", registerGuest.ProcessType);
                            insertCommand.Parameters.AddWithValue("@EventId", registerGuest.EventId);
                            insertCommand.Parameters.AddWithValue("@DepartDate", GetDate(registerGuest.DepartDate));
                            insertCommand.Parameters.AddWithValue("@ArrivalDate", GetDate(registerGuest.ArrivalDate));
                            insertCommand.Parameters.AddWithValue("@FirstName", registerGuest.FirstName);
                            insertCommand.Parameters.AddWithValue("@LastName", registerGuest.LastName);
                            insertCommand.Parameters.AddWithValue("@MiddleName", registerGuest.MiddleName);
                            insertCommand.Parameters.AddWithValue("@EmailAddress", registerGuest.EmailAddress);
                            insertCommand.Parameters.AddWithValue("@Phone", registerGuest.Phone);
                            insertCommand.Parameters.AddWithValue("@NumberOfBags", registerGuest.NumberOfBags);
                            insertCommand.Parameters.AddWithValue("@Status", registerGuest.Status);
                            insertCommand.Parameters.AddWithValue("@Gender", registerGuest.Gender);
                            insertCommand.Parameters.AddWithValue("@BirthDate", GetDate(registerGuest.BirthDate));
                            insertCommand.Parameters.AddWithValue("@PassportCitizenship", registerGuest.PassportCitizenship);
                            insertCommand.Parameters.AddWithValue("@PassportNumber", registerGuest.PassportNumber);
                            insertCommand.Parameters.AddWithValue("@PassportExpiration", GetDate(registerGuest.PassportExpiration));
                            insertCommand.Parameters.AddWithValue("@PassportCountry", registerGuest.PassportCountry);
                            insertCommand.Parameters.AddWithValue("@Opt1", registerGuest.Opt1);
                            insertCommand.Parameters.AddWithValue("@FirstNameAlias", registerGuest.FirstNameAlias);
                            insertCommand.Parameters.AddWithValue("@LastNameAlias", registerGuest.LastNameAlias);
                            insertCommand.Parameters.AddWithValue("@Eligible", registerGuest.Eligible);
                            insertCommand.Parameters.AddWithValue("@PartyGroupID", registerGuest.PartyGroupID);
                            insertCommand.Parameters.AddWithValue("@DepartGroup", registerGuest.DepartGroup);
                            insertCommand.Parameters.AddWithValue("@GuestType", registerGuest.GuestType);
                            insertCommand.Parameters.AddWithValue("@Segments", registerGuest.Segments.Count);


                            con.Open();
                            eventGuesId = (int)insertCommand.ExecuteScalar();
                        }
                        SaveSegments(eventGuesId, registerGuest.Segments);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return eventGuesId > 0;
        }

        public void SaveSegments(int eventGuestId, List<FlightSegment> segments)
        {
            try
            {
                foreach (var segment in segments)
                {
                    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString))
                    {
                        var cmd = "INSERT INTO FlightSegment (EventGuestID,Carrier,FlightNumber,DepartureCity,ArrivalCity,DepartureDate,DepartureTime,PNR,Operation)VALUES (@EventGuestID,@Carrier,@FlightNumber,@DepartureCity,@ArrivalCity,@DepartureDate,@DepartureTime,@PNR,@Operation)";
                        using (var insertCommand = new SqlCommand(cmd, con))
                        {
                            insertCommand.Parameters.AddWithValue("@EventGuestID", eventGuestId);
                            insertCommand.Parameters.AddWithValue("@Carrier", segment.Carrier);
                            insertCommand.Parameters.AddWithValue("@FlightNumber", segment.FlightNumber);
                            insertCommand.Parameters.AddWithValue("@DepartureCity", segment.DepartureCity);
                            insertCommand.Parameters.AddWithValue("@ArrivalCity", segment.ArrivalCity);
                            insertCommand.Parameters.AddWithValue("@DepartureDate", GetDate(segment.DepartureDate));
                            insertCommand.Parameters.AddWithValue("@DepartureTime", segment.DepartureTime);
                            insertCommand.Parameters.AddWithValue("@PNR", segment.PNR);
                            insertCommand.Parameters.AddWithValue("@Operation", segment.Operation);
                            con.Open();
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
